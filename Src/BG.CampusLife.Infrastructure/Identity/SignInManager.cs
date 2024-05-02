namespace BG.CampusLife.Infrastructure;

public partial class DbRepositories : ISignInManager
{
   

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
                    await _identityContext.SaveChangesAsync();
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
            new Claim("Id", user.Id),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim("UserName", user.UserName),
            new Claim("ConnectionId", !string.IsNullOrEmpty(user.ConnectionId) ? user.ConnectionId : ""),
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