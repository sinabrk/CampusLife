using FluentValidation;

namespace BG.CampusLife.Application.Posts.Queries.GetPost.ById
{
    public class GetPostByIdValidtion : AbstractValidator<GetPostByIdQuery>
    {
        public GetPostByIdValidtion()
        {
            RuleFor(post => post.Id).NotEmpty().WithMessage("Id can not be null or empty");
        }
    }
}
