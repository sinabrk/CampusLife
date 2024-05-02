using BG.CampusLife.Domain.Entities;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;
using BG.CampusLife.Domain.Enums;

namespace BG.CampusLife.Application.Validations
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(user => user.Username).NotEmpty().WithMessage("Username can not be empty!");

            RuleFor(user => user.FirstName).NotEmpty().WithMessage("First name can not be empty!")
                .MaximumLength(200).WithMessage("First name can not be more than 200 charecters");

            RuleFor(user => user.LastName).NotEmpty().WithMessage("Last name can not be empty")
                .MaximumLength(200).WithMessage("Last name can not be more than 200 charecters");

            RuleFor(x => x.Birthday).LessThan(DateTime.Now);
        }

        public bool isAcademic(User user)
        {
            return (user.Role == Roles.Student || user.Role == Roles.Professor) && user.University != null;
        }

        public bool IsValidEmail(User user)
        {
            string email = user.Email;

            if (email != null && user.Role == Roles.Student || user.Role == Roles.Professor)
                return false;

            return new EmailAddressAttribute().IsValid(email);
        }
    }
}
