using Dapper.Base;
using Dapper.Base.Dto;
using Dapper.Base.Models;
using System.Data;

namespace Dapper.MySql
{
    public class DapperMySqlHelper : TableDapperHelper, IDapperMySqlHelper
    {
        public static string _connectionString = $"server=172.19.32.31;uid=mcis;pwd=mcis;port=4000;database={dataBase};sslmode=Preferred;";
        public static string dataBase = "mcis";

        public override IDbConnection DbConnection
        {
            get
            {
                return new OracleConnection(_connectionString);
            }
        }
        public override string ConnectionString { get; set; } = _connectionString;


        //public override IDbConnection DbConnection
        //{
        //    get
        //    {
        //        return new OracleConnection(conn);
        //    }
        //}

        public DapperMySqlHelper() : base(_connectionString)
        {
            
        }

        public override Type GetType(string tableName, out List<string> selectFileds)
        {
            var sql = $"SELECT COLUMN_NAME,DATA_TYPE,NULLABLE FROM USER_TAB_COLUMNS WHERE TABLE_NAME='{tableName}'";
            var fieldList = TryGetList<FieldDto>(sql).ToList();
            selectFileds = fieldList.Select(x => x.Column_Name).ToList();
            return ConvertType(fieldList);
        }

        public override Type GetFieldType(string fieldType)
        {
            if (fieldType.ToLower().Contains("TIMESTAMP".ToLower()))
            {
                return typeof(DateTime);
            }
            switch (fieldType.ToLower())
            {
                case "VARCHAR":
                    return typeof(String);
                case "CHAR":
                    return typeof(String);
                case "DATE":
                    return typeof(DateTime?);
                case "DATETIME":
                    return typeof(DateTime?);
                case "INT":
                    return typeof(Int32?);
                case "DECIMAL":
                    return typeof(Decimal?);
                case "CLOB":
                    return typeof(String);
                default:
                    return typeof(String);
            }
        }


        /// <summary>
        /// 获取字段对应类型
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public override string GetColType(ColDto dto)
        {
            var type = "";
            switch (dto.DataType.ToUpper())
            {
                case "DATE":
                    type = typeof(DateTime).Name + "?";
                    break;
                case "DATETIME":
                    type = typeof(DateTime).Name + "?";
                    break;
                case "INT":
                    type = "int";
                    if (dto.DataScale.HasValue && dto.DataScale.Value > 0)
                    {
                        type = typeof(decimal).Name.ToLower();
                    }
                    type += "?";
                    break;
                case "DECIMAL":
                    type = typeof(decimal).Name.ToLower() + "?";
                    break;
                case "CLOB":
                    type = typeof(string).Name.ToLower();
                    break;
                default:
                    type = typeof(string).Name.ToLower();
                    break;
            }
            return type;
        }

        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public override string GetTableSql(string tableName)
        {
            var sql = $"SELECT T.TABLE_NAME AS TABLENAME,T.TABLE_TYPE AS TABLETYPE,T.TABLE_COMMENT AS COMMENTS FROM information_schema.`TABLES` T  where T.TABLE_NAME = '{tableName.ToUpper()}'";
            return sql;
        }

        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public override string GetTableListSql(string like)
        {
            var sql = $"SELECT T.TABLE_NAME AS TABLENAME,T.TABLE_TYPE AS TABLETYPE,T.TABLE_COMMENT AS COMMENTS FROM information_schema.`TABLES` T WHERE T.TABLE_NAME LIKE  '{like.ToUpper()}%'";
            return sql;
        }

        /// <summary>
        /// 获取所有表
        /// </summary>
        /// <returns></returns>
        public override string GetAllTableSql()
        {
            var sql = $"SELECT T.TABLE_NAME AS TABLENAME,T.TABLE_TYPE AS TABLETYPE,T.TABLE_COMMENT AS COMMENTS FROM information_schema.`TABLES` T WHERE TABLE_SCHEMA ='{dataBase}'";
            return sql;
        }

        /// <summary>
        /// 获取所有视图
        /// </summary>
        /// <returns></returns>
        public override string GetAllViewSql()
        {
            var sql = @"select T.TABLE_NAME AS TABLENAME,T.TABLE_TYPE AS TABLETYPE,'' AS COMMENTS
FROM information_schema.`VIEWS` T WHERE TABLE_SCHEMA ='{dataBase}'";
            return sql;
        }

        /// <summary>
        /// 获取字段
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public override string GetColSql(string tableName)
        {
            var sql = $"SELECT TABLE_NAME AS TABLENAME,COLUMN_NAME AS COLUMNNAME,IS_NULLABLE AS NULLABLE,DATA_TYPE AS DATATYPE,NUMERIC_SCALE AS DATASCALE,COLUMN_COMMENT AS COMMENTS FROM information_schema.`COLUMNS` WHERE TABLE_NAME ='{tableName}' AND  TABLE_SCHEMA = '{dataBase}'";
            return sql;
        }

        /// <summary>
        /// 获取字段对应的表
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        public override string GetTableByColSql(string colName)
        {
            var sql = $@"SELECT A.TABLE_NAME FROM information_schema.`COLUMNS` A LEFT JOIN information_schema.`TABLES` B ON A.TABLE_NAME =b.TABLE_NAME WHERE A.TABLE_SCHEMA = '{dataBase}' AND B.TABLE_SCHEMA ='{dataBase}' AND A.COLUMN_NAME='{colName.ToUpper()}' ORDER BY T.ORDINAL_POSITION";
            return sql;
        }

    }
}
