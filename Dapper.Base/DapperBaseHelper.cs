using Dapper.Base.Dto;
using Dapper.Base.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Dapper.Base
{
    public abstract class DapperBaseHelper : TableSql
    {
        public abstract IDbConnection DbConnection { get; }
        public abstract string ConnectionString { get; set; }

        public DapperBaseHelper(string conn)
        {
            ConnectionString = conn;
        }

        private IDbConnection GetConnection()
        {
            IDbConnection connection = null;
            if (connection == null)
                connection = DbConnection;
            if (connection.State != ConnectionState.Open)
                connection.Open();
            return connection;
        }

        public IList<TResult> TryGetList<TResult>(string sql, object param = null)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.Query<TResult>(sql: sql, param: param).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<object> TryGetList(string sql, Type type, object param = null)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.Query(type: type, sql: sql, param: param).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IList<TResult>> TryGetListAsync<TResult>(string sql, object param = null)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    var query = await conn.QueryAsync<TResult>(sql: sql, param: param);
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int TryExecute(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.Execute(sql, param, commandTimeout: commandTimeout, commandType: commandType);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<int> TryExecuteAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.ExecuteAsync(sql, param, commandTimeout: commandTimeout, commandType: commandType);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int TryExecute(IDbConnection conn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return conn.Execute(sql, param, transaction, commandTimeout, commandType);
        }

        public Task<int> TryExecuteAsync(IDbConnection conn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return conn.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
        }

        public void TryExecute(Action<IDbConnection, IDbTransaction> action)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    using (var tran = conn.BeginTransaction())
                    {
                        action(conn, tran);
                        tran.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TResult FirstOrDefault<TResult>(string sql, object param = null)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.QueryFirstOrDefault<TResult>(sql: sql, param: param);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<TResult> FirstOrDefaultAsync<TResult>(string sql, object param = null)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.QueryFirstOrDefaultAsync<TResult>(sql: sql, param: param);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TResult SingleOrDefault<TResult>(string sql, object param = null)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.QuerySingle<TResult>(sql: sql, param: param);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<TResult> SingleOrDefaultAsync<TResult>(string sql, object param = null)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.QuerySingleAsync<TResult>(sql: sql, param: param);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TResult ExecuteScalar<TResult>(string sql, object param = null)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.ExecuteScalar<TResult>(sql: sql, param: param);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<TResult> ExecuteScalarAsync<TResult>(string sql, object param = null)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.ExecuteScalarAsync<TResult>(sql: sql, param: param);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ExecuteAsync(string sql, object param = null)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    await conn.ExecuteAsync(sql: sql, param: param);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GridReader QueryMultiple(string sql, List<string> tupleList, object param = null)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    var queryMulti = conn.QueryMultiple(sql: sql, param: param);
                    if (!queryMulti.IsConsumed)
                    {
                        foreach (var item in tupleList)
                        {
                            var result = queryMulti.ReadFirstOrDefault<ResultDto>();
                            Console.WriteLine($"SQL:{item},结果:{result.Count}");
                        }
                    }
                    return queryMulti;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DataTable> ExecuteDataTableAsync(string sql, object param = null)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    var query = await conn.ExecuteReaderAsync(sql: sql, param: param);
                    return ConvertDataReaderToDataTable(query);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将?DataReader转a换?为aDataTable
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public DataTable ConvertDataReaderToDataTable(IDataReader reader)
        {
            DataTable objDataTable = new DataTable();
            objDataTable.TableName = "NewTable";
            int intFieldCount = reader.FieldCount;
            for (int intCounter = 0; intCounter < intFieldCount; ++intCounter)
            {
                objDataTable.Columns.Add(reader.GetName(intCounter), reader.GetFieldType(intCounter));
            }

            objDataTable.BeginLoadData();

            object[] objValues = new object[intFieldCount];
            while (reader.Read())
            {
                reader.GetValues(objValues);
                objDataTable.LoadDataRow(objValues, true);
            }
            reader.Close();
            objDataTable.EndLoadData();

            return objDataTable;
        }

        public DataTable ExecuteDataTable(string sql, object param = null)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    var query = conn.ExecuteReader(sql: sql, param: param);
                    return query.GetSchemaTable();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GridReader> QueryMultipleAsync(string sql, List<string> tupleList, object param = null)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    var queryMulti = await conn.QueryMultipleAsync(sql: sql, param: param);
                    if (!queryMulti.IsConsumed)
                    {
                        foreach (var item in tupleList)
                        {
                            var result = queryMulti.ReadFirstOrDefault<ResultDto>();
                            //Console.WriteLine($"SQL:{item},结果:{result.Count}");
                        }
                    }
                    return queryMulti;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual Type GetType(string tableName, out List<string> selectFileds)
        {
            //var sql = $"SELECT COLUMN_NAME AS FieldName,DATA_TYPE AS FieldType,IS_NULLABLE AS FiledNull FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '' AND TABLE_NAME = '{tableName.ToLower()}'";
            //var fieldList = TryGetList<FieldDto>(sql).ToList();
            //return ConvertType(fieldList);
            selectFileds = new List<string>();
            return null;
        }

        /// <summary>
        /// 动态生成Type
        /// </summary>
        /// <param name="fieldDtos"></param>
        /// <returns></returns>
        public virtual Type ConvertType(List<FieldDto> fieldDtos)
        {
            var newTypeName = Guid.NewGuid().ToString();//新建类型名称
            var assemblyName = new AssemblyName(newTypeName);
            var dynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            var dynamicModule = dynamicAssembly.DefineDynamicModule("Main");
            var dynamicType = dynamicModule.DefineType(newTypeName,
                    TypeAttributes.Public |
                    TypeAttributes.Class |
                    TypeAttributes.AutoClass |
                    TypeAttributes.AnsiClass |
                    TypeAttributes.BeforeFieldInit |
                    TypeAttributes.AutoLayout
                    //,typeof(T)
                    );
            // This is the type of class to derive from. Use null if there isn't one
            dynamicType.DefineDefaultConstructor(MethodAttributes.Public |
                                                MethodAttributes.SpecialName |
                                                MethodAttributes.RTSpecialName);
            var memberList = new List<string>();
            foreach (var property in fieldDtos)
            {
                AddProperty(dynamicType, property.Column_Name, GetFieldType(property.Data_Type));
            }
            return dynamicType.CreateType();
        }

        private void AddProperty(TypeBuilder typeBuilder, string propertyName, Type propertyType)
        {
            var fieldBuilder = typeBuilder.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);
            var propertyBuilder = typeBuilder.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);

            var getMethod = typeBuilder.DefineMethod("get_" + propertyName,
                MethodAttributes.Public |
                MethodAttributes.SpecialName |
                MethodAttributes.HideBySig, propertyType, Type.EmptyTypes);
            var getMethodIL = getMethod.GetILGenerator();
            getMethodIL.Emit(OpCodes.Ldarg_0);
            getMethodIL.Emit(OpCodes.Ldfld, fieldBuilder);
            getMethodIL.Emit(OpCodes.Ret);

            var setMethod = typeBuilder.DefineMethod("set_" + propertyName,
                  MethodAttributes.Public |
                  MethodAttributes.SpecialName |
                  MethodAttributes.HideBySig,
                  null, new[] { propertyType });
            var setMethodIL = setMethod.GetILGenerator();
            Label modifyProperty = setMethodIL.DefineLabel();
            Label exitSet = setMethodIL.DefineLabel();

            setMethodIL.MarkLabel(modifyProperty);
            setMethodIL.Emit(OpCodes.Ldarg_0);
            setMethodIL.Emit(OpCodes.Ldarg_1);
            setMethodIL.Emit(OpCodes.Stfld, fieldBuilder);
            setMethodIL.Emit(OpCodes.Nop);
            setMethodIL.MarkLabel(exitSet);
            setMethodIL.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getMethod);
            propertyBuilder.SetSetMethod(setMethod);
        }

        public virtual Type GetFieldType(string fieldType)
        {
            return typeof(String);
        }
    }
}