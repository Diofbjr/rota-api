using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Rota.Api.Dtos;

namespace Rota.Api.Validators
{
    public class WaypointValidator : AbstractValidator<WaypointDto>
    {
        public WaypointValidator()
        {
            RuleFor(w => w.Latitude)
                .InclusiveBetween(-90, 90)
                .WithMessage("Latitude deve estar entre -90 e 90.");

            RuleFor(w => w.Longitude)
                .InclusiveBetween(-180, 180)
                .WithMessage("Longitude deve estar entre -180 e 180.");

            RuleFor(w => w.Order)
                .GreaterThanOrEqualTo(0);
        }
    }
}
