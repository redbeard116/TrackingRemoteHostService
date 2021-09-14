using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TrackingRemoteHostService.Models;
using TrackingRemoteHostService.Services.DbService;
using Microsoft.EntityFrameworkCore;

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
        public async Task<int?> AddUser(User user)
        {
            try
            {
                _logger.LogInformation("AddUser");
                await _dBContext.Users.AddAsync(user);
                await _dBContext.SaveChangesAsync();
                return user.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AddUser error.\nMessage: {ex.Message}\nStack trace: {ex.StackTrace}");
                return null;
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                _logger.LogInformation($"DeleteUser {id}");
                var user = await _dBContext.Users.FirstOrDefaultAsync(w => w.Id == id);

                _dBContext.Users.Remove(user);
                await _dBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteUser error.\nMessage: {ex.Message}\nStack trace: {ex.StackTrace}");
                return false;
            }
        }

        public async Task<User> GetUser(int id)
        {
            try
            {
                _logger.LogInformation($"GetUser {id}");
                var rent = await _dBContext.Users.FindAsync(id);
                if (rent != null)
                {
                    return rent;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetUser error.\nMessage: {ex.Message}\nStack trace: {ex.StackTrace}");
                return null;
            }
        }
        #endregion
    }
}
