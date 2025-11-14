using Rota.Api.Data;


namespace Rota.Tests.UnitTests;

public class HaversineTests
{
    [Fact]
    public void Deve_Calcular_Distancia_Correta()
    {
        double resultado = HaversineDistanceKm(-15.0, -47.0, -15.5, -47.5);

        resultado.Should().BeGreaterThan(70);
        resultado.Should().BeLessThan(100);
    }

    private double HaversineDistanceKm(double lat1, double lon1, double lat2, double lon2)
    {
        const double R = 6371;

        double toRad(double val) => val * Math.PI / 180.0;

        var dLat = toRad(lat2 - lat1);
        var dLon = toRad(lon2 - lon1);

        var a =
            Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(toRad(lat1)) * Math.Cos(toRad(lat2)) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return R * c;
    }
}
