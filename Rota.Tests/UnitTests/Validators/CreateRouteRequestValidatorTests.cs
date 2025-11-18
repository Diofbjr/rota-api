using FluentValidation.TestHelper;
using Rota.Api.Dtos;
using Rota.Api.Validators;

namespace Rota.Tests.UnitTests.Validators;

public class CreateRouteRequestValidatorTests
{
    private readonly CreateRouteRequestValidator _validator = new();

    [Fact]
    public void Deve_falhar_quando_nome_estiver_vazio()
    {
        var dto = new CreateRouteRequestDto
        {
            Name = "",
            TotalLoadWeightKg = 200,
            TotalLoadVolumeM3 = 2,
            Waypoints = new List<WaypointDto>
            {
                new WaypointDto { Order = 0, Latitude = -15, Longitude = -47 },
                new WaypointDto { Order = 1, Latitude = -16, Longitude = -48 }
            }
        };

        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(r => r.Name);
    }

    [Fact]
    public void Deve_falhar_quando_peso_negativo()
    {
        var dto = new CreateRouteRequestDto
        {
            Name = "Rota",
            TotalLoadWeightKg = -10,
            TotalLoadVolumeM3 = 2,
            Waypoints = new List<WaypointDto>
            {
                new WaypointDto { Order = 0, Latitude = -15, Longitude = -47 },
                new WaypointDto { Order = 1, Latitude = -16, Longitude = -48 }
            }
        };

        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(r => r.TotalLoadWeightKg);
    }

    [Fact]
    public void Deve_falhar_quando_volume_negativo()
    {
        var dto = new CreateRouteRequestDto
        {
            Name = "Rota",
            TotalLoadWeightKg = 100,
            TotalLoadVolumeM3 = -5,
            Waypoints = new List<WaypointDto>
            {
                new WaypointDto { Order = 0, Latitude = -15, Longitude = -47 },
                new WaypointDto { Order = 1, Latitude = -16, Longitude = -48 }
            }
        };

        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(r => r.TotalLoadVolumeM3);
    }

    [Fact]
    public void Deve_falhar_quando_waypoints_tiverem_erro()
    {
        var dto = new CreateRouteRequestDto
        {
            Name = "Rota",
            TotalLoadWeightKg = 100,
            TotalLoadVolumeM3 = 5,
            Waypoints = new List<WaypointDto>
            {
                new WaypointDto { Order = -1, Latitude = 999, Longitude = 999 } // inválido
            }
        };

        var result = _validator.TestValidate(dto);

        Assert.True(result.Errors.Count > 0);
    }

    [Fact]
    public void Deve_passar_para_request_valida()
    {
        var dto = new CreateRouteRequestDto
        {
            Name = "Rota válida",
            TotalLoadWeightKg = 100,
            TotalLoadVolumeM3 = 5,
            Waypoints = new List<WaypointDto>
            {
                new WaypointDto { Order = 0, Latitude = -15, Longitude = -47 },
                new WaypointDto { Order = 1, Latitude = -16, Longitude = -48 }
            }
        };

        var result = _validator.TestValidate(dto);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
