using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.CampusLife.Application.Posts.Queries.GetPost.ByMessage
{
    public class GetPostByMessageValidation : AbstractValidator<GetPostByMessageQuery>
    {
        public GetPostByMessageValidation()
        {
            RuleFor(post => post.Message).NotEmpty().WithMessage("Message does not contain any data. Please provide a valid message.");
        }
    }
}
