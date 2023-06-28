using Infrastructure.Base.Repository;
using Infrastructure.Form.Entity;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Form.Repository
{
    public class FormRepository : RepositoryBase<TemplateForms>, IFormRepository
    {
        private readonly IConfiguration _configuration;

        public FormRepository(IConfiguration configuration) : base(configuration)
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

        public async Task<int> Create(TemplateForms model)
        {
            return await AddAsync(model);
        }

        public async Task<int> Update(TemplateForms model)
        {
            return await UpdateAsync(model);
        }
        public async Task<IEnumerable<TemplateForms>> GetByQuery(string where)
        {
            var res = await GetByQueryAsync(where);
            return res;
        }
        public async Task<int> Delete(int id, int? userId = null)
        {
            return await DeleteAsync(id, userId);
        }

        public async Task<TemplateForms> GetById(int id)
        {
            return await GetByIdAsync(id);
        }
        public async Task<int> UpdateByQuery(string where)
        {
            return await UpdateAsyncByQuery(where);
        } 
    }
}
