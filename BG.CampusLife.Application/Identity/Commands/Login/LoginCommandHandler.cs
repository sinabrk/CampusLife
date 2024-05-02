using System;
using AutoMapper;
using BG.CampusLife.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Application.Interfaces.Services;

namespace BG.CampusLife.Application.Identity.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginDto>
    {
        private readonly ISignInManager _signInManager;
        private readonly IMapper _mapper;

        public LoginCommandHandler(ISignInManager signInManager, IMapper mapper)
        {
            _mapper = mapper;
            _signInManager = signInManager;
        }


        public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.Login(request.Email, request.Password);
            if (!result.Succeeded) throw new UnauthorizedAccessException(result.Message);

            return _mapper.Map<LoginDto>(result.Entity);
        }
    }
}