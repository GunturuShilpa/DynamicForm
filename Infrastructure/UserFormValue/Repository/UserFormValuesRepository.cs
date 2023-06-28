using Infrastructure.Base.Repository;
using Infrastructure.Field.Entity;
using Infrastructure.Field.Repository;
using Infrastructure.UserFormValue.Entity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UserFormValue.Repository
{
    public class UserFormValuesRepository : RepositoryBase<UserFormValues>, IUserFormValuesRepository
    {
        private readonly IConfiguration _configuration;

        public UserFormValuesRepository(IConfiguration configuration) : base(configuration)
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
        public async Task<int> Create(UserFormValues userFormValues)
        {
            return await AddAsync(userFormValues);
        }

        public async Task<int> Delete(int id, int? userId = null)
        {
            return await DeleteAsync(id, userId);
        }

        public async Task<UserFormValues> GetById(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<IEnumerable<UserFormValues>> GetByQuery(string where)
        {
            var res = await GetByQueryAsync(where);
            return res;
        }

        public async Task<int> Update(UserFormValues userFormValues)
        {
            return await UpdateAsync(userFormValues);
        }
    }
}
