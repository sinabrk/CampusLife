using FluentValidation;

namespace BG.CampusLife.Application.Friends.Commands.DeleteFriend
{
    public class DeleteFriendValidation : AbstractValidator<DeleteFriendCommand>
    {
        public DeleteFriendValidation()
        {
            RuleFor(x => x.FriendId).NotEmpty().WithMessage("Id can not be null");
        }
    }
}
