using AutoMapper;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Domain.Entities;
using BG.CampusLife.Domain.Exceptions;
using BG.CampusLife.Infrastructure.Persistence;
using BG.CampusLife.SharedKernel.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Domain.Enums;

namespace BG.CampusLife.SharedKernel.Repository
{
    public class PostRepository : BaseRepository<CampusContext, PostRepository>, IPostRepository
    {
        private readonly IdentityDbContext _identity;

        public PostRepository(CampusContext context, ILogger<PostRepository> logger, IMapper mapper,
            IdentityDbContext identity) : base(context, logger, mapper)
        {
            _identity = identity;
        }

        #region Commands

        public async Task<Result<Post>> Create(Post post)
        {
            var result = new Result<Post>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Created,
            };

            await Context.Posts.AddAsync(post);
            await Context.SaveChangesAsync();

            Logger.LogInformation($"User {post.UserId} has a new Post with the id: {post.Id} created.");

            result.Entity = post;

            return result;
        }

        public async Task<Result<int>> Delete(Guid id)
        {
            var result = new Result<int>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.NoContent,
            };

            var post = await Context.Posts.FirstOrDefaultAsync(c => c.Id == id);

            if (post == null)
            {
                result.Message = $"Post not found with {id}";
                result.Succeeded = false;
                result.StatusCode = ResultStatusCodes.NotFound;
            }
            else
            {
                Context.Posts.Remove(post);
                await Context.SaveChangesAsync();

                Logger.LogInformation($"Post with the Id {id} has been removed!");
            }

            return result;
        }

        public async Task<Result<Post>> Update(Post post)
        {
            var result = new Result<Post>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Created,
            };

            var updatePost = await Context.Posts.FirstOrDefaultAsync(p => p.Id == post.Id);

            if (updatePost is null)
            {
                result.Message = $"Post not found with {post.Id}";
                result.StatusCode = ResultStatusCodes.NotFound;
                result.Succeeded = false;
            }

            else
            {
                updatePost = Mapper.Map<Post>(post);

                Context.Entry(updatePost).State = EntityState.Modified;
                Context.Posts.Update(updatePost);
                await Context.SaveChangesAsync();


                result.Entity = updatePost;
                Logger.LogInformation($"Post with the id {post.Id} has been updated!");
            }

            return result;
        }

        #endregion

        #region Queries

        public async Task<Result<Post>> GetAllPosts()
        {
            var entities = await Context.Posts
                .Include(x => x.User)
                .AsSplitQuery()
                .Select(x => new Post()
                {
                    Id = x.Id, Title = x.Title, Body = x.Body, Attachments = x.Attachments,
                    User = new Domain.Entities.User() { Email = x.User.Email }
                })
                .ToListAsync();

            return new Result<Post>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entities = entities,
                Total = entities.Count,
            };
        }

        public async Task<Result<Post>> GetAllUserPosts(User user)
        {
            var result = new Result<Post>
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entities = await Context.Posts
                    .Include(x => x.User)
                    .AsSplitQuery()
                    .Where(x => x.UserId == user.Id)
                    .Select(x => new Post()
                    {
                        Id = x.Id, Title = x.Title, Body = x.Body, Created = x.Created, Attachments = x.Attachments,
                        User = new Domain.Entities.User() { FirstName = x.User.FirstName, LastName = x.User.LastName }
                    })
                    .ToListAsync()
            };

            result.Total = result.Entities.Count;

            return result;
        }

        public async Task<Result<Post>> GetPostById(Guid id)
        {
            var result = new Result<Post>
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entity = await Context.Posts.FirstOrDefaultAsync(p => p.Id == id)
            };

            if (result.Entity is not null) return result;

            result.Message = $"Post not found with {id}";
            result.StatusCode = ResultStatusCodes.NotFound;
            result.Succeeded = false;

            return result;
        }

        public async Task<Result<Post>> GetPostByMessage(string message)
        {
            var entities = await Context.Posts
                .Include(x => x.User)
                .AsSplitQuery()
                .Where(x => x.Body.Contains(message) || x.Title.Contains(message))
                .Select(x => new Post()
                {
                    Id = x.Id, Body = x.Body, Attachments = x.Attachments, Title = x.Title,
                    User = new Domain.Entities.User() { Email = x.User.Email }
                })
                .ToListAsync();

            return new Result<Post>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entities = entities,
                Total = entities.Count,
            };
        }

        #endregion
    }
}