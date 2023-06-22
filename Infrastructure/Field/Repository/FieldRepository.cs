using Infrastructure.Base.Repository;
using Infrastructure.Field.Entity;
using Infrastructure.Form.Entity;
using Infrastructure.Form.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

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

        public async Task<IEnumerable<Entity.TemplateFormFields>> GetByQuery(string id = null)
        {
            var res = await GetByQueryAsync(id);
            return res;
        }
    }
}
