using MediatR;
using System.Collections.Generic;
using BG.CampusLife.Domain.Entities;

namespace BG.CampusLife.Application.Posts.Queries.AllPosts
{
    public class GetAllPostsQuery : IRequest<List<Post>>
    {
    }
}
