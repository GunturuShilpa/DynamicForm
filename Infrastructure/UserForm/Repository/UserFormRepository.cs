using Infrastructure.Base.Repository;
using Infrastructure.UserForm.Entity;
using Infrastructure.UserFormValue.Entity;
using Infrastructure.UserFormValue.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UserForm.Repository
{
    public class UserFormRepository : RepositoryBase<UserForms>, IUserFormRepository
    {
        private readonly IConfiguration _configuration;

        public UserFormRepository(IConfiguration configuration) : base(configuration)
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

        public async Task<int> Create(UserForms userForms)
        {
            return await AddAsync(userForms);
        }

        public async Task<int> Delete(int id, int? userId = null)
        {
            return await DeleteAsync(id, userId);
        }

        public async Task<UserForms> GetById(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<IEnumerable<UserForms>> GetByQuery(string where)
        {
            var res = await GetByQueryAsync(where);
            return res;
        }

        public async Task<int> Update(UserForms userForms)
        {
            return await UpdateAsync(userForms);
        }
    }
}
