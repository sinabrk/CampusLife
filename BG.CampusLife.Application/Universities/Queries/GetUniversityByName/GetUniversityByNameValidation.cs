using FluentValidation;

namespace BG.CampusLife.Application.Universities.Queries.GetUniversityByName
{
    public class GetUniversityByNameValidation : AbstractValidator<GetUniversityByNameQuery>
    {
        public GetUniversityByNameValidation()
        {
            RuleFor(uni => uni.Name).NotEmpty().WithMessage("Name can not be empty");
        }
    }
}
