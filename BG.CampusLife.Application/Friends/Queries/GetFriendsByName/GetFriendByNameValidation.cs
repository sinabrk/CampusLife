using FluentValidation;

namespace BG.CampusLife.Application.Friends.Queries.GetFriendsByName
{
    public class GetFriendByNameValidation : AbstractValidator<GetFriendByNameQuery>
    {
        public GetFriendByNameValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty().NotNull().WithMessage("First name can not be empty/null");
            RuleFor(x => x.LastName).NotEmpty().NotNull().WithMessage("Last name can not be empty/null");
        }
    }
}
