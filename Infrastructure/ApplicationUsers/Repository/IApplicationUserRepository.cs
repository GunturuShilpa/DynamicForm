using Infrastructure.ApplicationUsers.Entity;

namespace Infrastructure.ApplicationUsers.Repository
{
    public interface IApplicationUserRepository
    {
        Task<int> Create(Users model);
        Task<Users> AuthenticateAsync(string username);
    }
}
