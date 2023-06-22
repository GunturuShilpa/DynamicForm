using Infrastructure.Base.Repository;
using Infrastructure.ControlType.Entity;
using Infrastructure.Form.Entity;
using Infrastructure.Form.Repository;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.ControlType.Repository
{
    public class ControlTypesRepository : RepositoryBase<ControlTypes>, IControlTypesRepository
    {
        private readonly IConfiguration _configuration;

        public ControlTypesRepository(IConfiguration configuration) : base(configuration)
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
        public async Task<IEnumerable<ControlTypes>> GetByQuery(string where = null)
        {
            var res = await GetByQueryAsync(where);
            return res;
        }
    }
}
