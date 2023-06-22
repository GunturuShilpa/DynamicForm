namespace Infrastructure.Base.Repository
{
    public interface IAsyncGenericRepository<T>
    {
        Task<T> GetByIdAsync(int id);

        Task<int> AddAsync(T entity);

        Task<int> UpdateAsync(T entity);

        Task<int> UpdateAsyncByQuery(string query);

        Task<int> DeleteAsync(int id, int? UserId);

        Task<T> QueryFirstOrDefaultAsync(string sql, object param = null);

        Task<T> QuerySingleAsync(string sql, object param = null);

        Task<IEnumerable<T>> GetByQueryAsync(string where = null);

        Task<int> DeleteByQueryAsync(int userId, string where = null);

        public Task<int> QueryCountAsync(string where);

        Task<IEnumerable<T>> GetListByQueryAsync(string where = null);

        Task<int> DeletePermanentByQuery(string where);
    }
}
