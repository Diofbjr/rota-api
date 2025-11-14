using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Rota.Api.Dtos;

namespace Rota.Api.Validators
{
    public class CreateRouteRequestValidator : AbstractValidator<CreateRouteRequestDto>
    {
        public CreateRouteRequestValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("O nome é obrigatório.");

            RuleFor(r => r.TotalLoadWeightKg).GreaterThanOrEqualTo(0);
            RuleFor(r => r.TotalLoadVolumeM3).GreaterThanOrEqualTo(0);

            RuleForEach(r => r.Waypoints)
                .SetValidator(new WaypointValidator());
        }
    }
}
