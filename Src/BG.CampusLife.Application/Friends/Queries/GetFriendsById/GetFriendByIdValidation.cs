using FluentValidation;

namespace BG.CampusLife.Application.Friends.Queries.GetFriendsById
{
    public class GetFriendByIdValidation : AbstractValidator<GetFriendByIdQuery>
    {
        public GetFriendByIdValidation()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Id can not be null");
        }
    }
}
