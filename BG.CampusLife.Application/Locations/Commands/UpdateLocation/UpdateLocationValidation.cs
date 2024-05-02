using FluentValidation;

namespace BG.CampusLife.Application.Locations.Commands.UpdateLocation
{
    public class UpdateLocationValidation : AbstractValidator<UpdateLocationCommand>
    {
        public UpdateLocationValidation()
        {
            RuleFor(location => location.Id).NotEmpty().WithMessage("Id can not be empty");
            RuleFor(location => location).Must(CheckProperties).WithMessage("Street, Door Number, Postal Code, State, City, Country can not be empty");
        }

        private bool CheckProperties(UpdateLocationCommand location)
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
