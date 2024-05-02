using FluentValidation;

namespace BG.CampusLife.Application.Friends.Commands.BlockUser
{
    public class BlockFriendValidation : AbstractValidator<BlockFriendCommand>
    {
        public BlockFriendValidation()
        {
            RuleFor(x => x.FriendsId).NotEmpty().WithMessage("Friend id can not be null");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User id can not be null");
        }
    }
}
