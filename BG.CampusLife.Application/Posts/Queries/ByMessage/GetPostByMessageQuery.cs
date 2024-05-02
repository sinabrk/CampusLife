using MediatR;
using System.Collections.Generic;

namespace BG.CampusLife.Application.Posts.Queries.GetPost.ByMessage
{
    public class GetPostByMessageQuery : IRequest<List<Domain.Entities.Post>>
    {
        public string Message { get; set; }
    }
}
