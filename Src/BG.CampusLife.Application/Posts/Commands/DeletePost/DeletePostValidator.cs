namespace BG.CampusLife.Application.Posts.Commands.DeletePost;

public class DeletePostValidator : AbstractValidator<DeletePostCommand>
{
    public DeletePostValidator()
    {
        RuleFor(post => post.Id).NotEmpty().NotNull();
    }
}
