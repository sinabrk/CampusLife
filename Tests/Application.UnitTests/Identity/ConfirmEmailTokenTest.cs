using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Identity.Commands.ConfirmEmailToken;
using BG.CampusLife.Application.Interfaces;
using BG.CampusLife.Application.Interfaces.Services;
using Moq;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Identity
{
    public class ConfirmEmailTokenTest
    {
        private readonly Mock<IUserManager> _signInManager;

        public ConfirmEmailTokenTest()
        {
            _signInManager = new Mock<IUserManager>();
            _signInManager.Setup(m => m.ConfirmEmail(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(IdentityResultHandler.Success);
        }


        [Fact]
        public async Task Handle_GivenValidParameter_ShouldBeWithOutExceptionAsync()
        {
            var command = new ConfirmEmailTokenCommandHandler(_signInManager.Object);

            var valid = new ConfirmEmailTokenCommand() { Email = "adasdas", Token = "sadasd" };

            var result = await command.Handle(valid, CancellationToken.None);

            result.ShouldBeOfType<MediatR.Unit>();
        }


        [Fact]
        public async Task Handle_GivenInvalidParameter_ThrowAuthenticationExceptionAsync()
        {
            _signInManager.Setup(m => m.ConfirmEmail(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new AuthenticationException());

            var command = new ConfirmEmailTokenCommandHandler(_signInManager.Object);

            var invalid = new ConfirmEmailTokenCommand() { Email = "adasdas", Token = "sadasd" };

            await Assert.ThrowsAsync<AuthenticationException>(async () => await command.Handle(invalid, CancellationToken.None));
        }
    }
}