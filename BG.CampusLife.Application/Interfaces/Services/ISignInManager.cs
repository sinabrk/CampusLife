using System.Threading.Tasks;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Identity.DTOs;

namespace BG.CampusLife.Application.Interfaces.Services
{
    public interface ISignInManager
    {
        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<Result<LoginData>> Login(string email, string password);      
        
        /// <summary>
        /// Retrieve new Refresh Token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<Result<LoginData>> RefreshToken(string userId, string refreshToken);
    }
}