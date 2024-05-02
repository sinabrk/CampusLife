using FluentValidation;

namespace BG.CampusLife.Application.Posts.Commands.UpdatePost
{
    public class UpdatePostValidation : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostValidation()
        {
            RuleFor(post => post.Id).NotEmpty().WithMessage("Id can not be null");
            RuleFor(post => post.Title).NotEmpty().WithMessage("Please provide the title");
            RuleFor(post => post.Created).NotEmpty().WithMessage("Date of the creation can not be empty");
            RuleFor(post => post.UserId).NotEmpty().WithMessage("Each post must belong to a user");
        }
    }
}
