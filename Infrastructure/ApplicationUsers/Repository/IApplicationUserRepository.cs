using Infrastructure.ApplicationUsers.Entity;
using Infrastructure.Form.Entity;

namespace Infrastructure.ApplicationUsers.Repository
{
    public interface IApplicationUserRepository
    {
        Task<int> Create(Users model);
        Task<Users> AuthenticateAsync(string username);
        Task<IEnumerable<Users>> GetByQuery(string where);
        Task<Users> GetById(int id);
        Task<int> Delete(int id, int? userId = null);
        Task<int> Update(Users forms);
    }
}
