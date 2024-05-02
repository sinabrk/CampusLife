using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Identity.DTOs;
using BG.CampusLife.Application.Interfaces.Services;
using BG.CampusLife.Domain.Entities;
using BG.CampusLife.Domain.Enums;
using BG.CampusLife.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BG.CampusLife.Infrastructure.Identity
{
    public class SignInManager : ISignInManager
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IdentityDbContext _context;

        public SignInManager(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
            IConfiguration configuration
            , IdentityDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
        }

        public async Task<Result<LoginData>> Login(string email, string password)
        {
            var result = new Result<LoginData>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful
            };
            
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                // Maybe some other logics here
                if (await _userManager.IsEmailConfirmedAsync(user) && !user.IsSuspend)
                {
                    if (!user.IsActive)
                    {
                        user.IsActive = true;
                        await _context.SaveChangesAsync();
                    }

                    var identityResult = await _signInManager.CheckPasswordSignInAsync(user, password, true);
                    // Todo Send Warning email after lockout
                    if (identityResult.Succeeded)
                    {
                        var refreshToken = Guid.NewGuid().ToString();
                        await _userManager.SetAuthenticationTokenAsync(user, "Admin", "RefreshToken", refreshToken);
                        result.Entity = new LoginData()
                        {
                            Token = await GenerateJwtToken(user),
                            RefreshToken = refreshToken,
                        };
                        return result;
                    }
                    else
                    {
                        result.StatusCode = ResultStatusCodes.UnAuthorized;
                        result.Message = "Wrong Email or Password.";
                        result.Succeeded = false;
                    }
                }
                else
                {
                    result.StatusCode = ResultStatusCodes.UnAuthorized;
                    result.Message = "Email Not Confirmed.";
                    result.Succeeded = false;
                }
            }
            else
            {
                result.StatusCode = ResultStatusCodes.UnAuthorized;
                result.Message = "Wrong Email or Password.";
                result.Succeeded = false;
            }

            return result;
        }

        public async Task<Result<LoginData>> RefreshToken(string userId, string refreshToken)
        {
            var result = new Result<LoginData>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
            };
            
            var user = await _userManager.FindByIdAsync(userId);

            var token = await _userManager.GetAuthenticationTokenAsync(user, "Admin", "RefreshToken");

            if (token != refreshToken)
            {
                result.StatusCode = ResultStatusCodes.UnAuthorized;
                result.Message = "Token is not valid.";
                result.Succeeded = false;
                return result;
            }

            await _userManager.RemoveAuthenticationTokenAsync(user, "Admin", "RefreshToken");

            var newRefreshToken = Guid.NewGuid().ToString();
            await _userManager.SetAuthenticationTokenAsync(user, "Admin", "RefreshToken", refreshToken);
            result.Entity = new LoginData()
            {
                Token = await GenerateJwtToken(user),
                RefreshToken = newRefreshToken
            };
            return result;
        }
        
        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("UserName", user.UserName),
                new Claim("ConnectionId", !string.IsNullOrEmpty(user.ConnectionId) ? user.ConnectionId : "")
                // Todo Add Roles
            };
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(item => new Claim(ClaimTypes.Role, item)));

            var tokenManagement = _configuration.GetSection("tokenManagement").Get<TokenManagement>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}