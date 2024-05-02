using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.CampusLife.Application.Locations.Queries.GetLocationById
{
    public class GetLocationByIdValidation : AbstractValidator<GetLocationByIdQuery>
    {
        public GetLocationByIdValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty");
        }
    }
}
