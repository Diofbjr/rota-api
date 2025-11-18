using FluentValidation.TestHelper;
using Rota.Api.Dtos;
using Rota.Api.Validators;

namespace Rota.Tests.UnitTests.Validators;

public class CreateVehicleValidatorTests
{
    private readonly CreateVehicleValidator _validator = new();

    [Fact]
    public void Deve_falhar_quando_nome_estiver_vazio()
    {
        var dto = new CreateVehicleDto
        {
            Name = "",
            MaxLoadWeightKg = 1000,
            MaxVolumeM3 = 10,
            CostPerKm = 2
        };

        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(v => v.Name);
    }

    [Fact]
    public void Deve_falhar_quando_peso_maximo_for_zero_ou_negativo()
    {
        var dto = new CreateVehicleDto
        {
            Name = "Caminhão",
            MaxLoadWeightKg = 0,
            MaxVolumeM3 = 10,
            CostPerKm = 2
        };

        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(v => v.MaxLoadWeightKg);
    }

    [Fact]
    public void Deve_falhar_quando_volume_maximo_for_zero()
    {
        var dto = new CreateVehicleDto
        {
            Name = "Van",
            MaxLoadWeightKg = 500,
            MaxVolumeM3 = 0,
            CostPerKm = 2
        };

        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(v => v.MaxVolumeM3);
    }

    [Fact]
    public void Deve_passar_para_dados_validos()
    {
        var dto = new CreateVehicleDto
        {
            Name = "Caminhão",
            MaxLoadWeightKg = 1500,
            MaxVolumeM3 = 12,
            CostPerKm = 3
        };

        var result = _validator.TestValidate(dto);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
