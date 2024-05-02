using FluentValidation;

namespace BG.CampusLife.Application.Posts.Commands.DeletePost
{
    public class DeletePostValidation : AbstractValidator<DeletePostCommand>
    {
        public DeletePostValidation()
        {
            RuleFor(post => post.Id).NotEmpty().WithMessage("Id can not be null");
        }
    }
}
