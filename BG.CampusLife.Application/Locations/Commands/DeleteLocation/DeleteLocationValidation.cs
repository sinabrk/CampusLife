using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.CampusLife.Application.Locations.Commands.DeleteLocation
{
    public class DeleteLocationValidation : AbstractValidator<DeleteLocationCommand>
    {
        public DeleteLocationValidation()
        {
            RuleFor(location => location.Id).NotEmpty().WithMessage("Id can not be empty!");
        }
    }
}
