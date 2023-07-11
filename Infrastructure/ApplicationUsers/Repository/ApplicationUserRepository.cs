using Infrastructure.ApplicationUsers.Entity;
using Infrastructure.Base.Repository;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.ApplicationUsers.Repository
{
    public class ApplicationUserRepository : RepositoryBase<Users>, IApplicationUserRepository
    {
        private readonly IConfiguration _configuration;

        public ApplicationUserRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_configuration.GetConnectionString("DbConstr"));
            }
        }
        public async Task<int> Create(Users model)
        {
            return await AddAsync(model);
        }
        public async Task<Users> AuthenticateAsync(string username)
        {
            var sql = String.Format("where (email = '{0}' or username = '{0}') and status = 1", username);
            var res = await GetByQueryAsync(sql);
            var user = res.FirstOrDefault();
            if (user == null)
                return null;
            return user;
        }
        public async Task<int> Update(Users model)
        {
            return await UpdateAsync(model);
        }
        public async Task<IEnumerable<Users>> GetByQuery(string where)
        {
            var res = await GetByQueryAsync(where);
            return res;
        }
        public async Task<int> Delete(int id, int? userId = null)
        {
            return await DeleteAsync(id, userId);
        }

        public async Task<Users> GetById(int id)
        {
            return await GetByIdAsync(id);
        }
    }
}
