using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Base.Interfaces
{
    public interface IAsyncGenericRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> UpdateAsyncByQuery(T entity, string query = null);
        Task<int> DeleteAsync(int id, int? UserId);
        Task<T> QueryFirstOrDefaultAsync(string sql, object param = null);
        Task<T> QuerySingleAsync(string sql, object param = null);
        Task<IEnumerable<T>> GetByQueryAsync(string where = null);
        Task<int> DeleteByQueryAsync(int userId, string where = null);
    }
}
