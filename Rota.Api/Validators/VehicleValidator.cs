using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Rota.Api.Dtos;

namespace Rota.Api.Validators
{
    public class CreateVehicleValidator : AbstractValidator<CreateVehicleDto>
    {
        public CreateVehicleValidator()
        {
            RuleFor(v => v.Name).NotEmpty();

            RuleFor(v => v.MaxLoadWeightKg).GreaterThan(0);
            RuleFor(v => v.MaxVolumeM3).GreaterThan(0);

            RuleFor(v => v.CostPerKm).GreaterThanOrEqualTo(0);
        }
    }
}
