using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TrackingRemoteHostService.Models;
using TrackingRemoteHostService.Services.DbService;

namespace TrackingRemoteHostService.Services.UserService
{
    class UserService : IUserService
    {
        #region Fields
        private readonly ILogger<UserService> _logger;
        private readonly EfCoreService _dBContext;
        #endregion

        #region Constructor
        public UserService(ILogger<UserService> logger,
                           EfCoreService dBContext)
        {
            _logger = logger;
            _dBContext = dBContext;
        }
        #endregion

        #region IUserService
        public async Task<User> AddUser(CreateUser createUser)
        {
            try
            {
                _logger.LogInformation("AddUser");

                var user = new User
                {
                    FirstName = createUser.FirstName,
                    SecondName = createUser.SecondName
                };

                await _dBContext.Users.AddAsync(user);
                await _dBContext.SaveChangesAsync();

                var auth = new AuthUser
                {
                    UserId = user.Id,
                    Login = createUser.Login,
                    Password = createUser.Password
                };

                await _dBContext.AuthUsers.AddAsync(auth);
                await _dBContext.SaveChangesAsync();
                _logger.LogInformation($"Added new user {user.Id}");
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AddUser error.\nMessage: {ex.Message}\nStack trace: {ex.StackTrace}");
                return null;
            }
        }
        #endregion
    }
}
