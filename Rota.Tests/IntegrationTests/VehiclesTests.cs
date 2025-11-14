namespace Rota.Tests.IntegrationTests;

public class VehiclesTests : IClassFixture<TestServerFactory>
{
    private readonly HttpClient _client;

    public VehiclesTests(TestServerFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Deve_Criar_Veiculo_Com_Sucesso()
    {
        var dto = new
        {
            name = "Caminhão Teste",
            maxWeightKg = 5000,
            maxVolumeM3 = 20,
            maxLoadWeightKg = 4000,
            costPerKm = 2.5,
            costPerHour = 30,
            driverCostPerHour = 20,
            maxDistanceWithoutRefuelKm = 300,
            maxHeightM = 3.2,
            vehicleType = "Truck"
        };

        var response = await _client.PostAsJsonAsync("/vehicles", dto);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        var result = await response.Content.ReadFromJsonAsync<VehicleResponse>();
        result!.Name.Should().Be("Caminhão Teste");
    }
}
