using Rota.Api.Domain;

namespace Rota.Api.Services
{
    public class DistanceService : IDistanceService
    {
        private const double EarthRadiusKm = 6371.0;

        public double HaversineDistanceKm(double lat1, double lon1, double lat2, double lon2)
        {
            double ToRad(double deg) => deg * Math.PI / 180.0;

            var dLat = ToRad(lat2 - lat1);
            var dLon = ToRad(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRad(lat1)) * Math.Cos(ToRad(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return EarthRadiusKm * c;
        }

        public double TotalDistanceKm(IEnumerable<Waypoint> waypoints)
        {
            var ordered = waypoints.OrderBy(w => w.Order).ToList();
            double total = 0.0;
            for (int i = 0; i < ordered.Count - 1; i++)
            {
                total += HaversineDistanceKm(
                    ordered[i].Latitude, ordered[i].Longitude,
                    ordered[i + 1].Latitude, ordered[i + 1].Longitude);
            }
            return total;
        }
    }
}
