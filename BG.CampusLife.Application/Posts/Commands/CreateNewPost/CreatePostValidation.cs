using FluentValidation;

namespace BG.CampusLife.Application.Posts.Commands.CreateNewPost
{
    public class CreatePostValidation : AbstractValidator<CreatePostCommand>
    {
        public CreatePostValidation()
        {
            RuleFor(post => post.Id).NotEmpty().WithMessage("Id can not be null");
            RuleFor(post => post.Title).NotEmpty().WithMessage("Please provide the title");
            RuleFor(post => post.Created).NotEmpty().WithMessage("Date of the creation can not be empty");
        }
    }
}
