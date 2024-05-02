using FluentValidation;

namespace BG.CampusLife.Application.Universities.Queries.GetUniversityById
{
    class GetUniversityByIdValidation : AbstractValidator<GetUniversityByIdQuery>
    {
        public GetUniversityByIdValidation()
        {
            RuleFor(uni => uni.Id).NotEmpty().WithMessage("Id can not be null");
        }
    }
}
