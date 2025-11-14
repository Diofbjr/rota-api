using System.Collections.Generic;

namespace Rota.Api.Domain
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        #region CAPACIDADES
        public double MaxWeightKg { get; set; }
        public double MaxVolumeM3 { get; set; }
        public double MaxLoadWeightKg { get; set; }
        #endregion

        #region CUSTOS
        public double CostPerKm { get; set; }
        public double CostPerHour { get; set; }
        public double DriverCostPerHour { get; set; }
        #endregion

        #region AUTONOMIA E RESTRIÇÕES
        public double MaxDistanceWithoutRefuelKm { get; set; }
        public double MaxHeightM { get; set; }
        #endregion

        public string VehicleType { get; set; } = "Car";

        #region RELACIONAMENTO
        public ICollection<RouteRequest>? RouteRequests { get; set; }
        #endregion
    }
}
