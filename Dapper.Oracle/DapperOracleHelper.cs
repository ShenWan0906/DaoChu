using Dapper.Base;
using Dapper.Base.Dto;
using Dapper.Base.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dapper.Oracle
{
    public class DapperOracleHelper : TableDapperHelper, IDapperOracleHelper
    {
        public static string _connectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 172.19.20.155)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = orcl)));User ID=BMIS_YONGHUYU;Password=BMIS_YONGHUYU;Min Pool Size=0;Max Pool Size=10;Connection Timeout=600;Incr Pool Size=5;Decr Pool Size=2;";

        public override IDbConnection DbConnection
        {
            get
            {
                return new OracleConnection(ConnectionString);
            }
        }

        public override string ConnectionString { get; set; }

        public DapperOracleHelper() : base(_connectionString)
        {
        }

        private string Owner
        {
            get
            {
                var key = "User ID".ToLower();
                if (ConnectionString.ToLower().Contains(key))
                {
                    var value = ConnectionString.Split(";").Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Split("=")).ToDictionary(x => x[0].ToLower(), x => x[1])[key];
                    return value;
                }
                throw new Exception("未找到用户账户");
            }
        }

        public DapperOracleHelper(string _con) : base(_con)
        {
            ConnectionString = _con;
        }

        public override Type GetType(string tableName, out List<string> selectFileds)
        {
            var sql = $"SELECT COLUMN_NAME,DATA_TYPE,NULLABLE FROM USER_TAB_COLUMNS WHERE TABLE_NAME='{tableName}'";
            var fieldList = TryGetList<FieldDto>(sql).ToList();
            var fieldNameList = new List<string>();
            fieldList.ForEach(t =>
            {
                var filedName = t.Column_Name;
                if (t.Column_Name.IndexOf("~") > 0)
                {
                    t.Column_Name = t.Column_Name.Replace("~", "_").ToLower();
                }
                fieldNameList.Add($"\"{filedName}\" as { t.Column_Name }");
            });
            selectFileds = fieldNameList;
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
                case "nvarchar2":
                    return typeof(String);

                case "varchar2":
                    return typeof(String);

                case "clob":
                    return typeof(String);

                case "nclob":
                    return typeof(String);

                case "char":
                    return typeof(Char);

                case "date":
                    return typeof(DateTime);

                case "number":
                    return typeof(Int32);

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
                case "VARCHAR2":
                    type = typeof(string).Name.ToLower();
                    break;

                case "NVARCHAR2":
                    type = typeof(string).Name.ToLower();
                    break;

                case "CHAR":
                    type = typeof(string).Name.ToLower();
                    break;

                case "DATE":
                    type = typeof(DateTime).Name + "?";
                    break;

                case "NUMBER":
                    type = "int";
                    if (dto.DataScale.HasValue && dto.DataScale.Value > 0)
                    {
                        type = typeof(decimal).Name.ToLower();
                    }
                    type += "?";
                    break;

                case "CLOB":
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
            var sql = $"select T.TABLE_NAME AS TABLENAME,T.TABLE_TYPE AS TABLETYPE,T.COMMENTS AS COMMENTS from user_tab_comments T where T.TABLE_NAME = UPPER('{tableName}')";
            return sql;
        }

        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public override string GetTableListSql(string like)
        {
            var sql = $"select T.TABLE_NAME AS TABLENAME,T.TABLE_TYPE AS TABLETYPE,T.COMMENTS AS COMMENTS from user_tab_comments T where T.TABLE_NAME LIKE  '{like.ToUpper()}%'";
            return sql;
        }

        /// <summary>
        /// 获取所有表
        /// </summary>
        /// <returns></returns>
        public override string GetAllTableSql()
        {
            var sql = @"select T.TABLE_NAME AS TABLENAME,T.TABLE_TYPE AS TABLETYPE,T.COMMENTS AS COMMENTS
from user_tab_comments T
where T.TABLE_TYPE = 'TABLE'";
            return sql;
        }

        /// <summary>
        /// 获取所有视图
        /// </summary>
        /// <returns></returns>
        public override string GetAllViewSql()
        {
            var sql = @"select T.TABLE_NAME AS TABLENAME,T.TABLE_TYPE AS TABLETYPE,T.COMMENTS AS COMMENTS
from user_tab_comments T
where T.TABLE_TYPE = 'VIEW'";
            return sql;
        }

        /// <summary>
        /// 获取字段
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public override string GetColSql(string tableName)
        {
            var sql = $@"SELECT T.TABLE_NAME AS TABLENAME,T.COLUMN_NAME AS COLUMNNAME,T.DATA_TYPE AS DATATYPE,T.NULLABLE AS NULLABLE,C.COMMENTS AS COMMENTS, DATA_SCALE AS DATASCALE FROM COLS T
                LEFT JOIN USER_COL_COMMENTS C ON C.TABLE_NAME = T.TABLE_NAME AND C.COLUMN_NAME = T.COLUMN_NAME
                WHERE T.TABLE_NAME = UPPER('{tableName}') AND C.TABLE_NAME = UPPER('{tableName}') ORDER BY T.COLUMN_ID ";
            return sql;
        }

        /// <summary>
        /// 获取字段对应的表
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        public override string GetTableByColSql(string colName)
        {
            var sql = $@"SELECT T.TABLE_NAME AS TABLENAME FROM COLS T
                LEFT JOIN USER_COL_COMMENTS C ON C.TABLE_NAME = T.TABLE_NAME AND C.COLUMN_NAME = T.COLUMN_NAME
                WHERE T.COLUMN_NAME = UPPER('{colName.ToUpper()}') AND C.COLUMN_NAME = UPPER('{colName}') ORDER BY T.COLUMN_ID ";
            return sql;
        }

        public override string GetConstraintSql(string tableName)
        {
            return $@"SELECT (CASE WHEN A.UNIQUENESS='UNIQUE' THEN 1 ELSE 0 END) AS IsPrimary,B.TABLE_NAME AS CONSTRAINTTABLE,B.INDEX_NAME CONSTRAINTNAME,B.COLUMN_NAME CONSTRAINTCOLUMN FROM USER_INDEXES A ,USER_IND_COLUMNS B
WHERE A.TABLE_NAME=B.TABLE_NAME AND A.INDEX_NAME = B.INDEX_NAME
AND A.TABLE_OWNER=UPPER('{Owner}') AND B.INDEX_NAME='{tableName}' ORDER BY A.UNIQUENESS DESC;";
        }

        //        public List<ConstraintDto> GetConstraints(string tableName)
        //        {
        //            var sql =  $@"SELECT (CASE WHEN A.UNIQUENESS='UNIQUE' THEN 1 ELSE 0 END) AS IsPrimary,B.TABLE_NAME AS CONSTRAINTTABLE,B.INDEX_NAME CONSTRAINTNAME,B.COLUMN_NAME CONSTRAINTCOLUMN FROM USER_INDEXES A ,USER_IND_COLUMNS B
        //WHERE A.TABLE_NAME=B.TABLE_NAME AND A.INDEX_NAME = B.INDEX_NAME
        //AND A.TABLE_OWNER=UPPER('{Owner}') AND B.INDEX_NAME='{tableName}' ORDER BY A.UNIQUENESS DESC;";
        //            return TryGetList<ConstraintDto>(sql).ToList();
        //        }
    }
}