using Dapper.Base;
using Dapper.Base.Dto;
using Dapper.Base.Models;
using System;
using System.Data;
using System.Linq;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Npgsql;
using System.Collections.Generic;

namespace Dapper.NPGSql
{
    public class DapperNPGSqlHelper : TableDapperHelper, IDapperNPGSqlHelper
    {
        //public static string conn= "DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=172.19.20.147)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));PASSWORD=BMIS;PERSIST SECURITY INFO=TRUE;USER ID=BMIS; ENLIST=DYNAMIC";
        //public static string conn = "DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=172.19.20.147)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));PASSWORD=BMIS;PERSIST SECURITY INFO=True;USER ID=BMIS; enlist=dynamic;";
        //public static string conn = "DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.10.1.5)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orclhis)));PASSWORD=BMIS;PERSIST SECURITY INFO=True;USER ID=BMIS; enlist=dynamic;";
        public static string _connectionString = "Uid=auth;Pwd=auth;Server=172.19.20.66;Port=54321;Database=auth;";

        private static string Table_Schema = "auth";
        public override IDbConnection DbConnection
        {
            get
            {
                return new NpgsqlConnection(_connectionString);
            }
        }

        public override string ConnectionString { get; set; } = _connectionString;



        public DapperNPGSqlHelper(string conn) : base(conn)
        {

        }
        public DapperNPGSqlHelper() : base(_connectionString)
        { }


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
            var sql = $"SELECT * FROM (SELECT T.TABLE_NAME AS TABLENAME,T.TABLE_TYPE AS TABLETYPE,CAST(OBJ_DESCRIPTION(P.RELFILENODE,'PG_CLASS') AS VARCHAR) AS COMMENT  FROM (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='{Table_Schema}') T LEFT JOIN PG_CLASS P ON T.TABLE_NAME = P.RELNAME) T WHERE T.TABLE_TYPE='BASE TABLE' AND UPPER(T.TABLE_NAME) = '{tableName.ToUpper()}%'";
            return sql;
        }

        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public override string GetTableListSql(string like)
        {
            var sql = $"SELECT * FROM (SELECT T.TABLE_NAME AS TABLENAME,T.TABLE_TYPE AS TABLETYPE,CAST(OBJ_DESCRIPTION(P.RELFILENODE,'PG_CLASS') AS VARCHAR) AS COMMENT  FROM (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='{Table_Schema}') T LEFT JOIN PG_CLASS P ON T.TABLE_NAME = P.RELNAME) T WHERE T.TABLE_TYPE='BASE TABLE' AND UPPER(T.TABLE_NAME) LIKE  '{like.ToUpper()}%'";
            return sql;
        }

        /// <summary>
        /// 获取所有表
        /// </summary>
        /// <returns></returns>
        public override string GetAllTableSql()
        {
            var sql = $"SELECT * FROM (SELECT T.TABLE_NAME AS TABLENAME,T.TABLE_TYPE AS TABLETYPE,CAST(OBJ_DESCRIPTION(P.RELFILENODE,'PG_CLASS') AS VARCHAR) AS COMMENT  FROM (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='{Table_Schema}') T LEFT JOIN PG_CLASS P ON T.TABLE_NAME = P.RELNAME) T WHERE T.TABLE_TYPE='BASE TABLE'";
            return sql;
        }

        /// <summary>
        /// 获取所有视图
        /// </summary>
        /// <returns></returns>
        public override string GetAllViewSql()
        {
            var sql = $"SELECT * FROM (SELECT T.TABLE_NAME AS TABLENAME,T.TABLE_TYPE AS TABLETYPE,CAST(OBJ_DESCRIPTION(P.RELFILENODE,'PG_CLASS') AS VARCHAR) AS COMMENT  FROM (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='{Table_Schema}') T LEFT JOIN PG_CLASS P ON T.TABLE_NAME = P.RELNAME) T WHERE T.TABLE_TYPE='VIEW'";
            return sql;
        }

        /// <summary>
        /// 获取字段
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public override string GetColSql(string tableName)
        {
            var sql = $"SELECT TABLE_NAME,COLUMN_NAME,DATA_TYPE,NUMERIC_SCALE AS DATASCALE,IS_NULLABLE AS NULLABLE,'' AS COLUMN_COMMENT FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME ='{tableName}' AND  TABLE_SCHEMA = '{Table_Schema}'";
            return sql;
        }

        /// <summary>
        /// 获取字段对应的表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public override string GetTableByColSql(string colName)
        {
            var sql = $@"SELECT TABLE_NAME,COLUMN_NAME,DATA_TYPE,NUMERIC_SCALE AS DATASCALE,IS_NULLABLE AS NULLABLE,'' AS COLUMN_COMMENT FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '{Table_Schema}' AND A.COLUMN_NAME='{colName.ToUpper()}' ORDER BY T.ORDINAL_POSITION";
            return sql;
        }

    }
}
