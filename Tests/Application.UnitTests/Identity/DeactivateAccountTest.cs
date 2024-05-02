// using System;
// using System.Security.Authentication;
// using System.Threading;
// using System.Threading.Tasks;
// using BG.CampusLife.Application.Common;
// using BG.CampusLife.Application.Identity.Commands.Deactivate;
// using BG.CampusLife.Application.Interfaces;
// using Moq;
// using Shouldly;
// using Xunit;
//
// namespace Application.UnitTests.Identity
// {
//     public class DeactivateAccountTest
//     {
//         private readonly Mock<IIdentityManager> _signInManager;
//         private readonly Mock<ICurrentUserService> _currentUser;
//         private readonly string _userId;
//         public DeactivateAccountTest(Mock<ICurrentUserService> currentUser)
//         {
//             _currentUser = new Mock<ICurrentUserService>();
//             _currentUser.Setup(m => m.UserId).Returns(_userId);
//             _signInManager = new Mock<IIdentityManager>();
//             _signInManager.Setup(m => m.AccountDeactivate(_userId)).Returns(() => null);
//         }
//
//
//         [Fact]
//         public async Task Handle_GivenValidParameter_ShouldBeWithOutExceptionAsync()
//         {
//             var command = new DeactivateAccountHandler(_currentUser.Object, _signInManager.Object);
//
//             var valid = new DeactivateAccountCommand();
//
//             var result = await command.Handle(valid, CancellationToken.None);
//
//             result.ShouldBeOfType<MediatR.Unit>();
//         }
//
//
//         [Fact]
//         public async Task Handle_GivenInvalidParameter_ThrowAuthenticationExceptionAsync()
//         {
//             _signInManager.Setup(m => m.AccountDeactivate(It.IsAny<string>()))
//                 .ThrowsAsync(new AuthenticationException());
//
//             var command = new DeactivateAccountHandler(_currentUser.Object, _signInManager.Object);
//
//             var invalid = new DeactivateAccountCommand() { };
//
//             await Assert.ThrowsAsync<AuthenticationException>(async () =>
//                 await command.Handle(invalid, CancellationToken.None));
//         }
//     }
// }