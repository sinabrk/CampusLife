using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Domain.Entities;

namespace BG.CampusLife.Application.Interfaces.Repositories
{
    public interface IPostRepository
    {
        Task<Result<Post>> Create(Post post);

        Task<Result<int>> Delete(Guid id);

        Task<Result<Post>> Update(Post post);

        Task<Result<Post>> GetPostById(Guid id);

        Task<Result<Post>> GetAllUserPosts(User user);

        Task<Result<Post>> GetAllPosts();

        Task<Result<Post>> GetPostByMessage(string message);
    }
}
