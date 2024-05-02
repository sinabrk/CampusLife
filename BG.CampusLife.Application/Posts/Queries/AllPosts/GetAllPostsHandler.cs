using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Domain.Entities;

namespace BG.CampusLife.Application.Posts.Queries.AllPosts
{
    public class GetAllPostsHandler : BaseHandler<GetAllPostsHandler>, IRequestHandler<GetAllPostsQuery, List<Post>>
    {
        private readonly IPostRepository _repo;

        public GetAllPostsHandler(ILogger<GetAllPostsHandler> logger, IMapper mapper, IPostRepository repo) : base(logger, mapper)
        {
            _repo = repo;
        }

        public async Task<List<Post>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetAllPosts();
            return result.Entities;
        }
    }
}
