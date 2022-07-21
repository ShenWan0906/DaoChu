using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Oracle
{
    public interface IDapperHelper
    {
        IList<TResult> TryGetList<TResult>(string sql, object param = null);
        IList<object> TryGetList(string sql, Type type, object param = null);
        int TryExecute(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null);
        Task<int> TryExecuteAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null);
        int TryExecute(IDbConnection conn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        Task<int> TryExecuteAsync(IDbConnection conn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        void TryExecute(Action<IDbConnection, IDbTransaction> action);
        TResult FirstOrDefault<TResult>(string sql, object param = null);
        Task<TResult> FirstOrDefaultAsync<TResult>(string sql, object param = null);
        TResult SingleOrDefault<TResult>(string sql, object param = null);
        Task<TResult> SingleOrDefaultAsync<TResult>(string sql, object param = null);
        TResult ExecuteScalar<TResult>(string sql, object param = null);
        Task<TResult> ExecuteScalarAsync<TResult>(string sql, object param = null);
        void ExecuteAsync(string sql, object param = null);
    }
}
