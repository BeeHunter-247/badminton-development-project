using Badminton.Web.Models;

namespace Badminton.Web.Interfaces
{
    public interface IUserRepository
    {
        Task<User> RegisterUserAsync(User user);
        Task<User> GetUserByUsernameAsync(string username);
    }
}
