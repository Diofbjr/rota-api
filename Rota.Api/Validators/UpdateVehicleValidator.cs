using FluentValidation;
using Rota.Api.Dtos;

namespace Rota.Api.Validators;

public class UpdateVehicleValidator : AbstractValidator<UpdateVehicleDto>
{
    public UpdateVehicleValidator()
    {
        RuleFor(v => v.Name).NotEmpty();

        RuleFor(v => v.MaxLoadWeightKg).GreaterThan(0);
        RuleFor(v => v.MaxVolumeM3).GreaterThan(0);

        RuleFor(v => v.CostPerKm).GreaterThanOrEqualTo(0);
    }
}
