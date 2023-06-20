using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Services.Base.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository
{
    public abstract class RepositoryBase<T>: IAsyncGenericRepository<T>
    {
        private readonly string _tableName;
        protected string _connectionString;

        public RepositoryBase(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConstr");
            _tableName = "PLC_" + typeof(T).Name;
        }

        public async Task<int> AddAsync(T entity)
        {
            var columns = GetColumns();
            var stringOfColumns = string.Join(", ", columns);
            var stringOfParameters = string.Join(", ", columns.Select(e => "@" + e));
            var query = $"insert into {_tableName} ({stringOfColumns}) output inserted.Id values ({stringOfParameters})";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var result = await conn.ExecuteScalarAsync(query, entity);
                return Convert.ToInt32(result);
            }
        }

        public async Task<int> DeleteAsync(int id, int? UserId = 0)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var result = await conn.ExecuteAsync($"update {_tableName} set ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate,Status = @Status  WHERE [Id] = @Id", new { Id = id, ModifiedBy = UserId, ModifiedDate = DateTime.Now, Status = true });
                return Convert.ToInt32(result);
            }
        }

        public async Task DeletePermanentAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                await conn.ExecuteAsync($"DELETE FROM {_tableName} WHERE [Id] = @Id", new { Id = id });
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var data = await conn.QueryAsync<T>($"SELECT * FROM {_tableName} WHERE Id = @Id", new { Id = id });
                return data.FirstOrDefault();
            }

        }

        public async Task<int> UpdateAsync(T entity)
        {
            var columns = GetColumns();
            var stringOfColumns = string.Join(", ", columns.Select(e => $"{e} = @{e}"));
            var query = $"update {_tableName} set {stringOfColumns} where Id = @Id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var result = await conn.ExecuteAsync(query, entity);
                return Convert.ToInt32(result);

            }
        }
        public async Task<int> UpdateAsyncByQuery(T entity, string query = null)
        {
            var sqlQuery = $"update {_tableName} set ";
            if (!string.IsNullOrWhiteSpace(query))
                sqlQuery += query;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var result = await conn.ExecuteAsync(sqlQuery, entity);
                return Convert.ToInt32(result);
            }
        }
        public async Task<IEnumerable<T>> GetByQueryAsync(string where = null)
        {
            var query = $"select * from {_tableName} ";

            if (!string.IsNullOrWhiteSpace(where))
                query += where;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var data = await conn.QueryAsync<T>(query, null);
                return data;
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync(string sql, object param = null)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return await conn.QueryFirstOrDefaultAsync<T>(sql, param);
            }
        }

        public async Task<T> QuerySingleAsync(string sql, object param = null)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return await conn.QuerySingleAsync<T>(sql, param);
            }
        }

        public async Task<int> DeleteByQueryAsync(int userId, string where = null)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var result = await conn.ExecuteAsync($"update {_tableName} set ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate,Status = @Status " + where + "", new { ModifiedBy = userId, ModifiedDate = DateTime.Now, Status = true });
                return Convert.ToInt32(result);
            }
        }

        private IEnumerable<string> GetColumns()
        {
            return typeof(T)
                    .GetProperties()
                                        //.Where(e => e.Name != "Id" && !e.PropertyType.GetTypeInfo().IsGenericType)
                                        .Where(e => e.Name != "Id")

                    .Select(e => e.Name);
        }
    }
}
