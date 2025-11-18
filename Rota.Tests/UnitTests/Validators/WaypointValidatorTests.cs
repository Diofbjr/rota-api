using FluentValidation.TestHelper;
using Rota.Api.Dtos;
using Rota.Api.Validators;

namespace Rota.Tests.UnitTests.Validators;

public class WaypointValidatorTests
{
    private readonly WaypointValidator _validator = new();

    [Fact]
    public void Deve_falhar_quando_ordem_for_negativa()
    {
        var dto = new WaypointDto
        {
            Order = -1,
            Latitude = -15,
            Longitude = -47
        };

        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(w => w.Order);
    }

    [Fact]
    public void Deve_falhar_quando_latitude_estiver_fora_do_limite()
    {
        var dto = new WaypointDto
        {
            Order = 0,
            Latitude = 999,
            Longitude = -47
        };

        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(w => w.Latitude);
    }

    [Fact]
    public void Deve_falhar_quando_longitude_estiver_fora_do_limite()
    {
        var dto = new WaypointDto
        {
            Order = 0,
            Latitude = -15,
            Longitude = 999
        };

        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(w => w.Longitude);
    }

    [Fact]
    public void Deve_passar_para_waypoint_valido()
    {
        var dto = new WaypointDto
        {
            Order = 0,
            Latitude = -15.0,
            Longitude = -47.0
        };

        var result = _validator.TestValidate(dto);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
