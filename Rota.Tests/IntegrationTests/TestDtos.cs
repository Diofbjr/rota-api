namespace Rota.Tests.IntegrationTests;

public class VehicleResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int MaxWeightKg { get; set; }
    public int MaxVolumeM3 { get; set; }
    public int MaxLoadWeightKg { get; set; }
    public double CostPerKm { get; set; }
    public double CostPerHour { get; set; }
    public double DriverCostPerHour { get; set; }
    public double MaxDistanceWithoutRefuelKm { get; set; }
    public double MaxHeightM { get; set; }
    public string VehicleType { get; set; } = "";
}

public class RouteRequestResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}
