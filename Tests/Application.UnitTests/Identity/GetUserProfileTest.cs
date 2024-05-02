using System.Threading;
using System.Threading.Tasks;
using Application.UnitTests.Common;
using AutoMapper;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Application.Interfaces.Services;
using BG.CampusLife.Application.Users.Queries.GetUserProfile;
using BG.CampusLife.Persistence;
using Moq;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Identity
{
    [Collection("QueryCollection")]
    public class GetUserProfileTest
    {
        private readonly Mock<ICurrentUserService> _currentUserMock;
        private readonly Mock<IUserRepository> _identityMock;
        private readonly CampusContext _context;
        private readonly Mock<IUserManager> _userManagerMock;
        private readonly IMapper _mapper;

        public GetUserProfileTest(QueryTestFixture fixture, Mock<IUserManager> userManagerMock)
        {
            _userManagerMock = userManagerMock;
            _identityMock = new Mock<IUserRepository>();
            _mapper = fixture.Mapper;
            _context = fixture.Context;
            _currentUserMock = new Mock<ICurrentUserService>();
            const string userId = "123456";

            _currentUserMock.Setup(m => m.UserId).Returns(userId);
            // _identityMock.Setup(m => m.GetUserProfile(_userId)).ReturnsAsync();
        }

        //[Fact]
        //public async Task GetUserProfile()
        //{
        //    var query = new GetUserProfileHandler(_mapper, _currentUserMock.Object,
        //        _identityMock.Object, _userManagerMock.Object);
        //    var result = await query.Handle(new GetUserProfileQuery { }, CancellationToken.None);

        //    result.ShouldBeOfType<UserDto>();
        //}
    }
}