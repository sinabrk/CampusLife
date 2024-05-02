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

namespace BG.CampusLife.Application.Posts.Commands.UpdatePost
{
    public class UpdatePostHandler : BaseHandler<UpdatePostHandler>,
        IRequestHandler<UpdatePostCommand, CreateOrUpdatePostDto>
    {
        private readonly IPostRepository _repo;

        public UpdatePostHandler(ILogger<UpdatePostHandler> logger, IMapper mapper, IPostRepository repo) : base(logger,
            mapper)
        {
            _repo = repo;
        }

        public async Task<CreateOrUpdatePostDto> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.Update(Mapper.Map<Domain.Entities.Post>(request));
            if (result.Succeeded)
                return Mapper.Map<CreateOrUpdatePostDto>(result.Entity);
            Logger.LogWarning(result.Message);
            throw new NotFoundException(result.Message);
        }
    }
}