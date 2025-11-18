using Rota.Api.Domain;

namespace Rota.Api.Services
{
    public class RouteCalculationException : Exception
    {
        public RouteCalculationException(string message) : base(message) { }
    }

    public class RouteCalculator : IRouteCalculator
    {
        private readonly IDistanceService _distanceService;
        private readonly double _defaultAverageKmh = 50.0;

        public RouteCalculator(IDistanceService distanceService)
        {
            _distanceService = distanceService;
        }

        public RouteResult Calculate(RouteRequest request)
        {
            if (request.Waypoints == null || request.Waypoints.Count < 2)
                throw new RouteCalculationException("A rota precisa de pelo menos 2 waypoints.");

            // distância total
            var totalKm = _distanceService.TotalDistanceKm(request.Waypoints);

            // tempo estimado
            var averageKmh = _defaultAverageKmh;
            var totalMinutes = (totalKm / averageKmh) * 60.0;

            // validação de capacidade
            if (request.Vehicle != null)
            {
                if (request.TotalLoadWeightKg > request.Vehicle.MaxLoadWeightKg)
                    throw new RouteCalculationException($"A carga pesa {request.TotalLoadWeightKg}kg, excedendo {request.Vehicle.MaxLoadWeightKg}kg permitidos.");

                if (request.TotalLoadVolumeM3 > request.Vehicle.MaxVolumeM3)
                    throw new RouteCalculationException($"O volume é {request.TotalLoadVolumeM3}m³, excedendo {request.Vehicle.MaxVolumeM3}m³ permitidos.");

                if (totalKm > request.Vehicle.MaxDistanceWithoutRefuelKm)
                    throw new RouteCalculationException("A rota excede a autonomia do veículo.");
            }

            // cálculo de custo (se veículo informado)
            double? cost = null;
            if (request.Vehicle != null)
            {
                cost = (totalKm * request.Vehicle.CostPerKm)
                       + ((totalMinutes / 60.0) * request.Vehicle.CostPerHour)
                       + ((totalMinutes / 60.0) * request.Vehicle.DriverCostPerHour);
            }

            var result = new RouteResult
            {
                RouteRequestId = request.Id,
                TotalDistanceKm = Math.Round(totalKm, 3),
                TotalTimeMinutes = Math.Round(totalMinutes, 1),
                TotalCost = cost != null ? Math.Round(cost.Value, 2) : null,
                SerializedPath = System.Text.Json.JsonSerializer.Serialize(
                    request.Waypoints.OrderBy(w => w.Order).Select(w => new { w.Latitude, w.Longitude, w.Order })
                ),
                CalculatedAt = DateTime.UtcNow
            };

            return result;
        }
    }
}
