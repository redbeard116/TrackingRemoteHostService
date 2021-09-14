using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;
using TrackingRemoteHostService.Models;
using TrackingRemoteHostService.Services.DbService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using TrackingRemoteHostService.Models.Config;
using Microsoft.IdentityModel.Tokens;

namespace TrackingRemoteHostService.Services.IAuthService
{
    class AuthService : IAuthService
    {
        #region Fields
        private readonly ILogger<AuthService> _logger;
        private readonly EfCoreService _efCoreService;
        private readonly AuthOptions _authOptions;
        #endregion

        #region Constructor
        public AuthService(ILogger<AuthService> logger,
                           EfCoreService efCoreService,
                           AppSettings appSettings)
        {
            _logger = logger;
            _efCoreService = efCoreService;
            _authOptions = appSettings.AuthOptions;
        }
        #endregion

        #region IAuthService
        public async Task<Responce> AuthUser(UserAuth userAuth)
        {
            try
            {
                _logger.LogDebug($"Auth user {userAuth.Login}");
                if (userAuth == null)
                    return new Responce();

                var user = await GetUserAuth(userAuth);

                if (user != null)
                {
                    var claimIdenity = GetClaimIdentity(user);
                    var accessToken = GetAccessToken(claimIdenity);

                    return new Responce
                    {
                        AccessToken = accessToken,
                        User = user
                    };
                }

                return new Responce();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(AuthService)}");
                throw;
            }
        }
        #endregion

        #region Private methods
        private ClaimsIdentity GetClaimIdentity(User user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.FirstName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, "");

            return claimsIdentity;
        }

        private async Task<User> GetUserAuth(UserAuth user)
        {
            try
            {
                _logger.LogInformation($"Auth in server");
                var auth = await _efCoreService.AuthUsers.FirstOrDefaultAsync(w => w.Login == user.Login && w.Password == user.Password);

                if (auth == null)
                {
                    return null;
                }

                return await _efCoreService.Users.FirstOrDefaultAsync(w => w.Id == auth.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetUser error.\nMessage: {ex.Message}\nStack trace: {ex.StackTrace}");
                throw;
            }
        }

        public string GetAccessToken(ClaimsIdentity claimsIdentity)
        {
            var date = DateTime.Now;
            var jwt = new JwtSecurityToken(
                    issuer: _authOptions.Issuer,
                    audience: _authOptions.Audience,
                    notBefore: date,
                    claims: claimsIdentity.Claims,
                    expires: date.Add(TimeSpan.FromMinutes(_authOptions.Lifetime)),
                    signingCredentials: new SigningCredentials(_authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        #endregion
    }
}
