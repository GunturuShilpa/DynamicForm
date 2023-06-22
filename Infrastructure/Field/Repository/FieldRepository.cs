using Infrastructure.Base.Repository;
using Infrastructure.Field.Entity;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Field.Repository
{
    public class FieldRepository : RepositoryBase<Entity.TemplateFormFields>, IFieldRepository
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

        //public async Task<int> Create(Entity.TemplateFields model)
        //{
        //    return await AddAsync(model);
        //}

        public async Task<int> Create(TemplateFormFields fields)
        {
            return await AddAsync(fields);
        }

        public async Task<IEnumerable<Entity.TemplateFormFields>> GetByQuery(string clause)
        {
            var res = await GetByQueryAsync(clause);
            return res;
        }
    }
}
