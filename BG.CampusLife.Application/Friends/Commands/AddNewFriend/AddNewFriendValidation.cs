using FluentValidation;

namespace BG.CampusLife.Application.Friends.Commands.AddNewFriend
{
    public class AddNewFriendValidation : AbstractValidator<AddNewFriendCommand>
    {
        public AddNewFriendValidation()
        {
            RuleFor(x => x.FriendId).NotEmpty().WithMessage("Friend id can not be empty");
        }
    }
}
