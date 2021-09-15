using System.Threading.Tasks;
using TrackingRemoteHostService.Models;

namespace TrackingRemoteHostService.Services.UserService
{
    /// <summary>
    /// Интерфейс для управления пользователями
    /// </summary>
    public interface IUserService
    {
        Task<User> AddUser(CreateUser createUser);
    }
}
