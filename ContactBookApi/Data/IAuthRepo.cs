using ContactBookApi.Models;

namespace ContactBookApi.Data
{
    public interface IAuthRepo
    {
        Task<int> Register(User user, string password);
        Task<string> Login(string email, string password);
        Task<bool> UserExists(string email);
    }
}
