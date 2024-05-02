using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.CampusLife.Application.Locations.Commands.CreateLocation
{
    public class CreateLocationValidation : AbstractValidator<CreateLocationCommand>
    {
        public CreateLocationValidation()
        {
            RuleFor(location => location.Id).NotEmpty().WithMessage("Id can not be empty");
            RuleFor(location => location).Must(CheckProperties).WithMessage("Street, Door Number, Postal Code, State, City, Country can not be empty");
        }

        private bool CheckProperties(CreateLocationCommand location)
        {
            foreach (var item in location.GetType().GetProperties())
            {
                if (item.Name != "Id" && item.Name != "Longitude" && item.Name != "Latitude" && item.GetValue(location) == null)
                    return false;
            }

            return true;
        }
    }
}
