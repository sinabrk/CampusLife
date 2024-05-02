using FluentValidation;

namespace BG.CampusLife.Application.Friends.Queries.GetAllBlockedFriends
{
    public class GetAllBlockedFriendsValidation : AbstractValidator<GetAllBlockedFriendsQuery>
    {
        public GetAllBlockedFriendsValidation()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User id can not be null");
        }
    }
}
