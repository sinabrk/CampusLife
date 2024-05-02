using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Application.Posts.DTOs;
using BG.CampusLife.Domain.Enums;

namespace BG.CampusLife.Application.Posts.Queries.GetPost.ById
{
    public class GetPostByIdHandler : BaseHandler<GetPostByIdHandler>, IRequestHandler<GetPostByIdQuery, GetPostDto>
    {
        private readonly IPostRepository _repo;

        public GetPostByIdHandler(ILogger<GetPostByIdHandler> logger, IMapper mapper, IPostRepository repo) : base(logger, mapper)
        {
            _repo = repo;
        }

        public async Task<GetPostDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetPostById(request.Id);
            if (!result.Succeeded)
            {
                Logger.LogWarning(result.Message);
                throw new NotFoundException(result.Message);
            }
            var postWithTheUser = Mapper.Map<GetPostDto>(result.Entity);
            // Todo Fix
            // postWithTheUser.FullName = string.Join(" ", result.Entity.User.FirstName, result.Entity.User.LastName);
            return postWithTheUser;
        }
    }
}
