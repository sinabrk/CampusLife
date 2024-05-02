using System.Threading.Tasks;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Domain.Entities;

namespace BG.CampusLife.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Result<User>> GetById(string userId);
        
        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<Result<User>> GetByEmail(string email);

        /// <summary>
        /// Update User Profile
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<Result<User>> UpdateProfile(User user);

        /// <summary>
        /// Gets user profile base on role
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<Result<User>> GetUserProfile(User user);
    }
}