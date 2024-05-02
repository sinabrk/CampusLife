using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Application.Interfaces.Services;
using BG.CampusLife.Domain.Enums;

namespace BG.CampusLife.Application.Posts.Queries.AllUserPosts
{
    public class GetAllUserPostsHandler : BaseHandler<GetAllUserPostsHandler>, IRequestHandler<GetAllUserPostsQuery, List<Domain.Entities.Post>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUser;

        public GetAllUserPostsHandler(ILogger<GetAllUserPostsHandler> logger, IMapper mapper, IPostRepository repo, IUserRepository userRepository, ICurrentUserService currentUser) : base(logger, mapper)
        {
            _postRepository = repo;
            _userRepository = userRepository;
            _currentUser = currentUser;
        }
        public async Task<List<Domain.Entities.Post>> Handle(GetAllUserPostsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(_currentUser.UserId);
            if (!user.Succeeded) throw new NotFoundException(user.Message);
            
            var result = await _postRepository.GetAllUserPosts(user.Entity);
            if (result.Succeeded) return result.Entities;
            Logger.LogWarning(result.Message);
            throw new NotFoundException(result.Message);
        }
    }
}
