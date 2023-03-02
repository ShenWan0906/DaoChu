using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;
using System.Reflection;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using Dapper.Base.Dto;
using Dapper.Base.Models;

namespace Dapper.Base
{
    public interface IDapperHelper
    {
        IList<TResult> TryGetList<TResult>(string sql, object param = null);
        IList<object> TryGetList(string sql, Type type, object param = null);
        Task<IList<TResult>> TryGetListAsync<TResult>(string sql, object param = null);
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
        Task ExecuteAsync(string sql, object param = null);

        GridReader QueryMultiple(string sql, List<string> tupleList, object param = null);

        Task<DataTable> ExecuteDataTableAsync(string sql, object param = null);

        /// <summary>
        /// 将?DataReader转a换?为aDataTable
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        DataTable ConvertDataReaderToDataTable(IDataReader reader);
        DataTable ExecuteDataTable(string sql, object param = null);
        Task<GridReader> QueryMultipleAsync(string sql, List<string> tupleList, object param = null);
        Type GetType(string tableName, out List<string> selectFileds);

        /// <summary>
        /// 动态生成Type
        /// </summary>
        /// <param name="fieldDtos"></param>
        /// <returns></returns>
        Type ConvertType(List<FieldDto> fieldDtos);

        Type GetFieldType(string fieldType);

        /// <summary>
        /// 字段类型
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        string GetColType(ColDto dto);
    }
}
