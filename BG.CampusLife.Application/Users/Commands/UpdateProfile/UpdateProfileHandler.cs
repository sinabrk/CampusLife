using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Identity.DTOs;
using BG.CampusLife.Application.Interfaces;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Application.Interfaces.Services;
using BG.CampusLife.Domain.Entities;
using BG.CampusLife.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BG.CampusLife.Application.Identity.Commands.UpdateProfile
{
    public class UpdateProfileHandler : BaseHandler<UpdateProfileHandler>, IRequestHandler<UpdateProfileCommand, UserDto>
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IUserRepository _userRepository;
        public UpdateProfileHandler(ILogger<UpdateProfileHandler> logger, IMapper mapper, ICurrentUserService currentUser, IUserRepository userRepository) : base(logger, mapper)
        {
            _currentUser = currentUser;
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetById(_currentUser.UserId);
            if (!result.Succeeded) throw new NotFoundException(result.Message);
            
            result.Entity.NormalizedPersonalEmail = result.Entity.PersonalEmail.ToUpper();
            result.Entity.NormalizedAdditionalEmail = result.Entity.AdditionalEmail.ToUpper();
            await _userRepository.UpdateProfile(result.Entity);
            
            return Mapper.Map<UserDto>(result.Entity);
        }
    }
}