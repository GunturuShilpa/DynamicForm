namespace Infrastructure.Base.Repository
{
    public interface IAsyncGenericRepository<T>
    {
        Task<T> GetByIdAsync(int id);

        Task<int> AddAsync(T entity);

        Task<int> UpdateAsync(T entity);

        Task<int> UpdateAsyncByQuery(string query);

        Task<int> DeleteAsync(int id, int? UserId);

        Task<T> QueryFirstOrDefaultAsync(string sql, object param);

        Task<T> QuerySingleAsync(string sql, object param);

        Task<IEnumerable<T>> GetByQueryAsync(string where);

        Task<int> DeleteByQueryAsync(int userId, string where);

        public Task<int> QueryCountAsync(string where);

        Task<IEnumerable<T>> GetListByQueryAsync(string where);

        Task<int> DeletePermanentByQuery(string where);
    }
}
