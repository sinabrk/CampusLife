using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Identity.DTOs;
using BG.CampusLife.Application.Interfaces;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Application.Interfaces.Services;
using BG.CampusLife.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BG.CampusLife.Application.Identity.Queries.GetUserProfile
{
    public class GetUserProfileHandler : BaseHandler<GetUserProfileHandler>,IRequestHandler<GetUserProfileQuery, UserDto>
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IUserRepository _userRepository;
        public GetUserProfileHandler(ILogger<GetUserProfileHandler> logger, IMapper mapper, ICurrentUserService currentUser, IUserRepository userRepository) : base(logger, mapper)
        {
            _currentUser = currentUser;
            _userRepository = userRepository;
        }
        
        public async Task<UserDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(_currentUser.UserId);
            if (!user.Succeeded) throw new NotFoundException(user.Message);
            
            var profile = await _userRepository.GetUserProfile(user.Entity);
            return Mapper.Map<UserDto>(profile);
        }
    }
}