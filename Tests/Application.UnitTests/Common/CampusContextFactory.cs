using System;
using System.Collections.Generic;
using BG.CampusLife.Application.Interfaces.Services;
using BG.CampusLife.Domain.Entities;
using BG.CampusLife.Domain.Enums;
using BG.CampusLife.Persistence;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.UnitTests.Common
{
    public class CampusContextFactory
    {
        public static CampusContext Create()
        {
            const string userId = "00000000-0000-0000-0000-000000000000";
            var _currentUserServiceMock = new Mock<ICurrentUserService>();
            _currentUserServiceMock.Setup(m => m.UserName).Returns(userId);

            var options = new DbContextOptionsBuilder<CampusContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new CampusContext(options);

            context.Database.EnsureCreated();

            var staticIds = new Dictionary<string, string>
            {
                { "user", Guid.NewGuid().ToString() },
                { "location1", Guid.NewGuid().ToString() },
                { "location2", Guid.NewGuid().ToString() },
                { "university1", Guid.NewGuid().ToString() },
                { "university2", Guid.NewGuid().ToString() },
                { "university3", Guid.NewGuid().ToString() },
                { "university4", Guid.NewGuid().ToString() },
                { "category1", Guid.NewGuid().ToString() },
                { "category2", Guid.NewGuid().ToString() },
                { "post1", Guid.NewGuid().ToString() },
                { "post2", Guid.NewGuid().ToString() },
                { "property1", Guid.NewGuid().ToString() },
                { "property2", Guid.NewGuid().ToString() },
                { "property3", Guid.NewGuid().ToString() },
                { "tag1", Guid.NewGuid().ToString() },
                { "tag2", Guid.NewGuid().ToString() },
                { "tag3", Guid.NewGuid().ToString() },
                { "market1", Guid.NewGuid().ToString() },
                { "market2", Guid.NewGuid().ToString() },
                { "market3", Guid.NewGuid().ToString() },
            };

            context.Users.AddRange(new[]
            {
                new User()
                {
                    Id = Guid.Parse(staticIds["user"]),
                    UserId = userId.ToString(),
                    UniversityId = null,
                    LocationId = null,
                    Gender = GenderType.Male,
                    Bio = "",
                    FirstName = "User",
                    LastName = "Test",
                    Email = "Test@Email.com",
                    NormalizedEmail = "Test@Email.com".ToUpper(),
                }
            });

            context.Locations.AddRange(new[]
            {
                new Location
                {
                    Id = Guid.Parse(staticIds["location1"]),
                    Title = "Sasdasjdklas",
                    Longitude = 0,
                    Latitude = 0,
                },
                new Location
                {
                    Id = Guid.Parse(staticIds["location2"]),
                    Title = "Sasdasjdklaasdxccxs",
                    Longitude = 0,
                    Latitude = 0,
                },
            });

            context.Universities.AddRange(new[]
            {
                new BG.CampusLife.Domain.Entities.University
                {
                    Id = Guid.Parse(staticIds["university1"]),
                    Name = "Azad",
                    LocationId = Guid.Parse(staticIds["location1"])
                },
                new BG.CampusLife.Domain.Entities.University
                {
                    Id = Guid.Parse(staticIds["university2"]),
                    Name = "Dolati",
                    LocationId = Guid.Parse(staticIds["location1"])
                },
                new BG.CampusLife.Domain.Entities.University
                {
                    Id = Guid.Parse(staticIds["university3"]),
                    Name = "Azad",
                    LocationId = Guid.Parse(staticIds["location2"])
                },
                new BG.CampusLife.Domain.Entities.University
                {
                    Id = Guid.Parse(staticIds["university4"]),
                    Name = "Azad",
                    LocationId = Guid.Parse(staticIds["location2"])
                }
            });

            context.Categories.AddRange(new[]
            {
                new Category
                {
                    Id = Guid.Parse(staticIds["category1"]),
                    Title = "Category1",
                    CategoryType = (CategoryTypes)0,
                    Level = 1,
                    Code = "123",
                    Slug = "category-1",
                    CreatedBy = userId,
                },
                new Category
                {
                    Id = Guid.Parse(staticIds["category2"]),
                    Title = "Category2",
                    CategoryType = (CategoryTypes)0,
                    Level = 1,
                    Code = "123",
                    Slug = "category-2",
                    CreatedBy = userId,
                }
            });

            context.Posts.AddRange(new[]
            {
                new Post
                {
                    Id = Guid.Parse(staticIds["post1"]),
                    CategoryId = Guid.Parse(staticIds["category1"]),
                    UserId = Guid.Parse(staticIds["user"]),
                    Title = "title",
                    Body = "body",
                    Created = DateTime.Now,
                    LocationId = Guid.Parse(staticIds["location1"]),
                },
                new Post
                {
                    Id = Guid.Parse(staticIds["post2"]),
                    CategoryId = Guid.Parse(staticIds["category2"]),
                    UserId = Guid.Parse(staticIds["user"]),
                    Title = "title-2",
                    Body = "body-2",
                    Created = DateTime.Now,
                    LocationId = Guid.Parse(staticIds["location2"]),
                },
            });

            context.Properties.AddRange(new[]
            {
                new Property()
                {
                    Id = Guid.Parse(staticIds["property1"]),
                    CategoryId = Guid.Parse(staticIds["category1"]),
                    Name = "asdasdasdas",
                    Options = "asdasdas|asdasdasd|xzcasdasd",
                    Required = true,
                    ControlType = PropertyControlTypes.ComboBox,
                },
                new Property()
                {
                    Id = Guid.Parse(staticIds["property2"]),
                    CategoryId = Guid.Parse(staticIds["category2"]),
                    Name = "asdasdasdas",
                    Options = "asdasdas|asdasdasd|xzcasdasd",
                    Required = true,
                    ControlType = PropertyControlTypes.CheckBox,
                },
                new Property()
                {
                    Id = Guid.Parse(staticIds["property3"]),
                    CategoryId = Guid.Parse(staticIds["category2"]),
                    Name = "asdasdasdas",
                    Options = "asdasdas|asdasdasd|xzcasdasd",
                    Required = true,
                    ControlType = PropertyControlTypes.TextBox,
                },
            });

            context.Tags.AddRange(new[]
            {
                new Tag
                {
                    Id = Guid.Parse(staticIds["tag1"]),
                    Title = "asdasdasx",
                    Created = DateTime.Now,
                    UserId = Guid.Parse(staticIds["user"]),
                },
                new Tag
                {
                    Id = Guid.Parse(staticIds["tag2"]),
                    Title = "asdasawqedasx",
                    Created = DateTime.Now,
                    UserId = Guid.Parse(staticIds["user"]),
                },
                new Tag
                {
                    Id = Guid.Parse(staticIds["tag3"]),
                    Title = "asdasdsdxasx",
                    Created = DateTime.Now,
                    UserId = Guid.Parse(staticIds["user"]),
                },
            });
            
            context.MarketItems.AddRange(
                new []
                {
                    new MarketItem
                    {
                        Id = Guid.Parse(staticIds["market1"]),
                        UserId = Guid.Parse(staticIds["user"]),
                        CategoryId = Guid.Parse(staticIds["category1"]),
                        LocationId = Guid.Parse(staticIds["location1"]),
                        Title = "asdasdxzcas",
                        Description = "asdasdqweqwyxzmcn,manfksjad;asl;dk;alsd",
                        Status = MarketItemStatuses.Approved,
                    },
                    new MarketItem
                    {
                        Id = Guid.Parse(staticIds["market2"]),
                        UserId = Guid.Parse(staticIds["user"]),
                        CategoryId = Guid.Parse(staticIds["category2"]),
                        LocationId = Guid.Parse(staticIds["location2"]),
                        Title = "asdasdxzcas",
                        Description = "asdasdqweqwyxzmcn,manfksjad;asl;dk;alsd",
                        Status = MarketItemStatuses.Approved,
                    },
                });

            context.SaveChanges();
            return context;
        }

        public static void Destroy(CampusContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}