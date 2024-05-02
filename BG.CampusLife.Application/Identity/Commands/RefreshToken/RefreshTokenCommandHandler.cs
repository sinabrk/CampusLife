using System;
using AutoMapper;
using BG.CampusLife.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Application.Interfaces.Services;

namespace BG.CampusLife.Application.Identity.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenDto>
    {
        private readonly ISignInManager _signInManager;
        private readonly IMapper _mapper;

        public RefreshTokenCommandHandler(ISignInManager signInManager, IMapper mapper)
        {
            _signInManager = signInManager;
            _mapper = mapper;
        }


        public async Task<RefreshTokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.RefreshToken(request.UserId, request.RefreshToken);
            if (!result.Succeeded) throw new UnauthorizedAccessException(result.Message);

            return _mapper.Map<RefreshTokenDto>(result.Entity);
        }
    }
}
