using FluentValidation;

namespace BG.CampusLife.Application.Universities.Commands.UpdateUniversity
{
    public class UpdateUniversityValidation : AbstractValidator<UpdateUniversityCommand>
    {
        public UpdateUniversityValidation()
        {
            RuleFor(uni => uni.Id).NotEmpty().WithMessage("Id can not be null. Please provide the id");
            RuleFor(uni => uni.Name).NotEmpty().WithMessage("Name can not be null or empty");
            RuleFor(uni => uni.Location).Must(CheckAddress).WithMessage("Please provide the the following fields: City, Country");
        }

        private bool CheckAddress(Domain.Entities.Location location)
        {
            if (location == null)
                return false;

            foreach (var item in location.GetType().GetProperties())
            {
                if ((item.Name == "Country" || item.Name == "City") && item.GetValue(location) == null)
                    return false;
            }

            return true;
        }
    }
}
