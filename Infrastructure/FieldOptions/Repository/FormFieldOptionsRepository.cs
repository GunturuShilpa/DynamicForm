using Infrastructure.Base.Repository;
using Infrastructure.Field.Entity;
using Infrastructure.FieldOptions.Entity;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.FieldOptions.Repository
{
    public class FormFieldOptionsRepository : RepositoryBase<FormFieldOptions>, IFormFieldOptionsRepository
    {
        private readonly IConfiguration _configuration;

        public FormFieldOptionsRepository(IConfiguration configuration) : base(configuration)
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
        public async Task<int> Create(FormFieldOptions fieldOptions)
        {
            return await AddAsync(fieldOptions);
        }

        public async Task<FormFieldOptions> GetById(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<IEnumerable<FormFieldOptions>> GetByQuery(string clause)
        {
            var res = await GetByQueryAsync(clause);
            return res;
        }

        public async Task<int> Delete(int id, int? userId = null)
        {
            return await DeleteAsync(id, userId);
        }
        public async Task<int> Update(FormFieldOptions fieldOptions)
        {
            return await UpdateAsync(fieldOptions);
        }
    }
}
