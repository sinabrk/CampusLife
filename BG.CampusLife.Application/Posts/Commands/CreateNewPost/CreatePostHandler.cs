using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Application.Posts.DTOs;

namespace BG.CampusLife.Application.Posts.Commands.CreateNewPost
{
    public class CreatePostHandler : BaseHandler<CreatePostHandler>,
        IRequestHandler<CreatePostCommand, CreateOrUpdatePostDto>
    {
        private readonly IPostRepository _repo;

        public CreatePostHandler(ILogger<CreatePostHandler> logger, IMapper mapper, IPostRepository repo) : base(logger,
            mapper)
        {
            _repo = repo;
        }

        public async Task<CreateOrUpdatePostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.Create(Mapper.Map<Domain.Entities.Post>(request));

            var postWithUser = Mapper.Map<CreateOrUpdatePostDto>(result.Entity);

            // Todo Fix
            // postWithUser.RelatedUserFullName =
            //     string.Join(" ", result.Entity.User.FirstName, result.Entity.User.LastName);

            return postWithUser;
        }
    }
}