using FluentValidation;

namespace BG.CampusLife.Application.Friends.Queries.GetFriendByEmail
{
    public class GetFriendByEmailValidation : AbstractValidator<GetFriendByEmailQuery>
    {
        public GetFriendByEmailValidation()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().NotNull().WithMessage("Invalid email address");
        }
    }
}
