using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.CampusLife.Application.Posts.Queries.AllUserPosts
{
    public class GetAllUserPostValidation : AbstractValidator<GetAllUserPostsQuery>
    {
        public GetAllUserPostValidation()
        {
        }
    }
}
