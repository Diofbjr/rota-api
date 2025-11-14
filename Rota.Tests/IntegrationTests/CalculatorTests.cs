namespace Rota.Tests.IntegrationTests;

public class CalculatorTests : IClassFixture<TestServerFactory>
{
    private readonly HttpClient _client;

    public CalculatorTests(TestServerFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Deve_Falhar_Se_Carga_Exceder()
    {
        // Criar veículo
        var vehicle = new
        {
            name = "Carro Teste",
            maxWeightKg = 1000,
            maxVolumeM3 = 3,
            maxLoadWeightKg = 800,
            costPerKm = 1,
            costPerHour = 10,
            driverCostPerHour = 5,
            maxDistanceWithoutRefuelKm = 200,
            maxHeightM = 1.5,
            vehicleType = "Car"
        };

        var vResponse = await _client.PostAsJsonAsync("/vehicles", vehicle);
        var vResult = await vResponse.Content.ReadFromJsonAsync<VehicleResponse>();
        int vehicleId = vResult!.Id;

        // Criar rota com carga excedida
        var route = new
        {
            name = "Rota Teste",
            optimization = "distance",
            totalLoadWeightKg = 2000, // excesso
            totalLoadVolumeM3 = 2,
            vehicleId = vehicleId,
            waypoints = new[]
            {
                new { order = 0, latitude = -15.0, longitude = -47.0 },
                new { order = 1, latitude = -15.5, longitude = -47.5 }
            }
        };

        var rResponse = await _client.PostAsJsonAsync("/route-requests", route);
        var request = await rResponse.Content.ReadFromJsonAsync<RouteRequestResponse>();

        var calcResp = await _client.PostAsync($"/route-requests/{request!.Id}/calculate", null);
        calcResp.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
}
