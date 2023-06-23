using Infrastructure.Base.Repository;
using Infrastructure.Field.Entity;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Field.Repository
{
    public class FieldRepository : RepositoryBase<TemplateFormFields>, IFieldRepository
    {
        private readonly IConfiguration _configuration;

        public FieldRepository(IConfiguration configuration) : base(configuration)
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

        public async Task<int> Create(TemplateFormFields fields)
        {
            return await AddAsync(fields);
        }

        public async Task<TemplateFormFields> GetById(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<IEnumerable<TemplateFormFields>> GetByQuery(string clause)
        {
            var res = await GetByQueryAsync(clause);
            return res;
        }

        public async Task<int> Delete(int id, int? userId = null)
        {
            return await DeleteAsync(id, userId);
        }        
        public async Task<int> Update(TemplateFormFields fields)
        {
            return await UpdateAsync(fields);
        }
    }
}
