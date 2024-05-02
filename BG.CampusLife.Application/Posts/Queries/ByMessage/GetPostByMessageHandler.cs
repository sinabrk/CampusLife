using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Application.Posts.DTOs;
using BG.CampusLife.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BG.CampusLife.Application.Posts.Queries.GetPost.ByMessage
{
    public class GetPostByMessageHandler : BaseHandler<GetPostByMessageHandler>,
        IRequestHandler<GetPostByMessageQuery, List<Domain.Entities.Post>>
    {
        private IPostRepository _repo;

        public GetPostByMessageHandler(ILogger<GetPostByMessageHandler> logger, IMapper mapper, IPostRepository repo) :
            base(logger, mapper)
        {
            _repo = repo;
        }

        public async Task<List<Domain.Entities.Post>> Handle(GetPostByMessageQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _repo.GetPostByMessage(request.Message);
            return result.Entities;
        }
    }
}