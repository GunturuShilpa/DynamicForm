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
    }
}
