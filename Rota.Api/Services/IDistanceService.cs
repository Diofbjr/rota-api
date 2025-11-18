namespace Rota.Api.Services
{
    public interface IDistanceService
    {
        double HaversineDistanceKm(double lat1, double lon1, double lat2, double lon2);
        double TotalDistanceKm(IEnumerable<Rota.Api.Domain.Waypoint> waypoints);
    }
}
