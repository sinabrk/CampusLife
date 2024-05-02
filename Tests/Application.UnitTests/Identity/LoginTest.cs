using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Common.Mappings;
using BG.CampusLife.Application.Identity.DTOs;
using BG.CampusLife.Application.Interfaces.Services;
using Moq;

namespace Application.UnitTests.Identity
{
    public class LoginTest
    {
        private readonly Mock<ISignInManager> _signInManager;
        private readonly IMapper _mapper;

        public LoginTest()
        {
            _signInManager = new Mock<ISignInManager>();
            _signInManager.Setup(m => m.Login(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new Result<LoginData>());
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

        }


        //[Fact]
        //public async Task Handle_GivenValidParameter_ShouldBeWithOutExceptionAsync()
        //{
        //    var command = new LoginCommandHandler(_mapper, _signInManager.Object);

        //    var valid = new LoginCommand() { Email = "adasdas", Password = "sadasd" };

        //    var result = await command.Handle(valid, CancellationToken.None);

        //    result.ShouldBeOfType<LoginDto>();
        //}


        //[Fact]
        //public async Task Handle_GivenInvalidParameter_ThrowAuthenticationExceptionAsync()
        //{
        //    _signInManager.Setup(m => m.Login( It.IsAny<string>(),It.IsAny<string>())).ThrowsAsync(new AuthenticationException());

        //    var command = new LoginCommandHandler(_mapper, _signInManager.Object);

        //    var invalid = new LoginCommand() { Email = "adasdas", Password = "sadasd" };

        //    await Assert.ThrowsAsync<AuthenticationException>(async () => await command.Handle(invalid, CancellationToken.None));
        //}
    }
}