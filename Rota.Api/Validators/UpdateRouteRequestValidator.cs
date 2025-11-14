using FluentValidation;
using Rota.Api.Dtos;

namespace Rota.Api.Validators;

public class UpdateRouteRequestValidator : AbstractValidator<UpdateRouteRequestDto>
{
    public UpdateRouteRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty();

        RuleFor(r => r.TotalLoadWeightKg)
            .GreaterThanOrEqualTo(0);

        RuleFor(r => r.TotalLoadVolumeM3)
            .GreaterThanOrEqualTo(0);

        RuleForEach(r => r.Waypoints)
            .SetValidator(new WaypointValidator());
    }
}
