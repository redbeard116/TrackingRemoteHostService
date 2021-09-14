using System.Threading.Tasks;
using TrackingRemoteHostService.Models;

namespace TrackingRemoteHostService.Services.UserService
{
    /// <summary>
    /// Интерфейс для управления пользователями
    /// </summary>
    public interface IUserService
    {
        Task<int?> AddUser(User user);
        Task<bool> DeleteUser(int id);
        Task<User> GetUser(int id);
    }
}
