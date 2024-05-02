using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.CampusLife.Application.Universities.Commands.DeleteUniversity
{
    public class DeleteUniversityValidation : AbstractValidator<DeleteUniversityByIdCommand>
    {
        public DeleteUniversityValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be null");
        }
    }
}
