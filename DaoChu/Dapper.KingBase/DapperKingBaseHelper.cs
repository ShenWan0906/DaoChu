using Dapper.Base;
using Dapper.Base.Dto;
using Dapper.Base.Models;
using Kdbndp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dapper.KingBase
{
    public class DapperKingBaseHelper : TableDapperHelper, IDapperKingBaseHelper
    {
        public static string _connectionString = "Uid=saas;Pwd=saas;Server=172.19.20.66;Port=54321;Database=saas;Search Path=LYRA_JICHUSS;";

        //private static string Table_Schema = "LYRA_JICHUSS";
        private string Table_Schema
        {
            get
            {
                var key = "Search Path".ToLower();
                if (ConnectionString.ToLower().Contains(key))
                {
                    var value = ConnectionString.Split(";").Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Split("=")).ToDictionary(x => x[0].ToLower(), x => x[1])[key];
                    return value;
                }
                return "PUBLIC";
            }
        }

        private string Table_Owner
        {
            get
            {
                var key = "Uid".ToLower();
                if (ConnectionString.ToLower().Contains(key))
                {
                    var value = ConnectionString.Split(";").Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Split("=")).ToDictionary(x => x[0].ToLower(), x => x[1])[key];
                    return value;
                }
                throw new Exception("未找到用户账户");
            }
        }

        public override IDbConnection DbConnection
        {
            get
            {
                return new KdbndpConnection(ConnectionString);
            }
        }

        public override string ConnectionString { get; set; }

        public DapperKingBaseHelper() : base(_connectionString)
        {
            ConnectionString = _connectionString;
        }

        public DapperKingBaseHelper(string _con) : base(_con)
        {
            ConnectionString = _con;
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

                case "TIMESTAMP WITHOUT TIME ZONE":
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

                case "INTEGER":
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
                    //throw new Exception("找不到类型");
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
            var sql = $"SELECT * FROM (SELECT T.TABLE_NAME AS TABLENAME,T.TABLE_TYPE AS TABLETYPE,P.COMMENTs AS COMMENTS FROM (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='{Table_Schema}') T LEFT JOIN  USER_TAB_COMMENTS P ON T.TABLE_NAME = P.TABLE_NAME) T WHERE T.TABLETYPE='BASE TABLE' AND UPPER(T.TABLENAME) = '{tableName.ToUpper()}'";
            return sql;
        }

        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public override string GetTableListSql(string like)
        {
            var sql = $"SELECT * FROM (SELECT T.TABLE_NAME AS TABLENAME,T.TABLE_TYPE AS TABLETYPE,P.COMMENTs AS COMMENTS FROM (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='{Table_Schema}') T LEFT JOIN  USER_TAB_COMMENTS P ON T.TABLE_NAME = P.TABLE_NAME) T WHERE T.TABLETYPE='BASE TABLE'  AND UPPER(T.TABLENAME) LIKE  '%{like.ToUpper()}%'";
            return sql;
        }

        /// <summary>
        /// 获取所有表
        /// </summary>
        /// <returns></returns>
        public override string GetAllTableSql()
        {
            var sql = $"SELECT * FROM (SELECT T.TABLE_NAME AS TABLENAME,T.TABLE_TYPE AS TABLETYPE,P.COMMENTs AS COMMENTS FROM (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='{Table_Schema}') T LEFT JOIN  USER_TAB_COMMENTS P ON T.TABLE_NAME = P.TABLE_NAME) T WHERE T.TABLETYPE='BASE TABLE'";
            return sql;
        }

        /// <summary>
        /// 获取所有视图
        /// </summary>
        /// <returns></returns>
        public override string GetAllViewSql()
        {
            var sql = $"SELECT T.TABLE_NAME AS TABLENAME,'VIEW' AS TABLETYPE,'' AS COMMENT FROM INFORMATION_SCHEMA.VIEWS T WHERE TABLE_SCHEMA='{Table_Schema}'";
            return sql;
        }

        /// <summary>
        /// 获取字段
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public override string GetColSql(string tableName)
        {
            var sql = @$"SELECT A.TABLE_NAME AS TABLENAME,A.COLUMN_NAME AS COLUMNNAME,
A.DATA_TYPE AS DATATYPE,A.NUMERIC_SCALE AS DATASCALE,A.IS_NULLABLE AS NULLABLE,B.COMMENTS AS COMMENTS
FROM INFORMATION_SCHEMA.COLUMNS A
LEFT JOIN USER_COL_COMMENTS B ON A.TABLE_NAME = B.TABLE_NAME AND A.COLUMN_NAME = B.COLUMN_NAME AND A.TABLE_CATALOG = B.OWNER
WHERE A.TABLE_NAME = '{tableName}' AND A.TABLE_SCHEMA = '{Table_Schema}' AND  B.OWNER='{Table_Owner}'";
            return sql;
        }

        /// <summary>
        /// 修改字段说明
        /// </summary>
        /// <param name="cols"></param>
        public override string UpdateColCommentsSql(string tableName, string ColumnName, string Comments)
        {
            return $"comment on column \"{Table_Schema}\".\"{tableName}\".\"{ColumnName}\" IS '{Comments}'";
        }

        /// <summary>
        /// 修改表表述
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="commit"></param>
        /// <returns></returns>
        public override string UpdateTableCommentsSql(string tableName, string commit)
        {
            return $"COMMENT ON TABLE \"{Table_Schema}\".\"{tableName}\" IS '{commit}'";
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public override IList<object> GetTableData(string tableName, List<string> fields, Type type)
        {
            var sql = $"SELECT {string.Join(",", fields)} FROM \"{Table_Schema}\".\"{tableName}\"";
            return TryGetList(sql, type);
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

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="tableName"></param>
        public override void DeleteTableData(string tableName)
        {
            TryExecute($"DELETE FROM \"{Table_Schema}\".\"{tableName}\"");
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldList"></param>
        /// <param name="memberList"></param>
        /// <param name="list"></param>
        public override void InsertTableData(string tableName, List<string> fieldList, List<string> memberList, IList<object> list)
        {
            TryExecute($"INSERT INTO \"{Table_Schema}\".\"{tableName}\"({string.Join(',', fieldList.Select(x => "\"" + x + "\""))}) VALUES({string.Join(',', memberList.Select(x => ":" + x))})", list);
        }

        public override string GetIndexesSql(string tableName)
        {
            return @$"SELECT A.INDEX_NAME AS INDEXNAME,B.COLUMN_NAME AS INDEXCOLUMN,A.TABLE_NAME AS INDEXTABLE,(CASE WHEN A.UNIQUENESS='UNIQUE' THEN 1 ELSE 0 END ) ISPRIMARY
                         FROM USER_INDEXES A LEFT JOIN USER_IND_COLUMNS B
                        ON A.INDEX_NAME=B.INDEX_NAME AND A.TABLE_OWNER=B.TABLE_OWNER AND A.TABLE_NAME=B.TABLE_NAME
                        WHERE A.TABLE_OWNER='{Table_Owner}' AND A.TABLE_NAME='{tableName}'";
        }

        /// <summary>
        /// 修改索引
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="OriginalValue">原始值</param>
        /// <param name="CurrentValue">当前值</param>
        /// <returns></returns>
        public override string UpdateIndexSql(string tableName, string OriginalValue, string CurrentValue)
        {
            return $"ALTER INDEX \"{Table_Schema}\".\"{OriginalValue}\" RENAME TO \"{CurrentValue}\"";
        }

        public override string GetConstraintSql(string tableName)
        {
            return $@"SELECT TC.CONSTRAINT_NAME AS CONSTRAINTNAME, TC.TABLE_NAME AS CONSTRAINTTABLE, KCU.COLUMN_NAME AS CONSTRAINTCOLUMN
                     FROM
                         INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC
                         JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU ON TC.CONSTRAINT_NAME = KCU.CONSTRAINT_NAME
                         JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE AS CCU ON CCU.CONSTRAINT_NAME = TC.CONSTRAINT_NAME
                     WHERE TC.TABLE_NAME = '{tableName}' AND TC.CONSTRAINT_CATALOG='{Table_Owner}' AND TC.CONSTRAINT_SCHEMA='{Table_Schema}'";
        }

        /// <summary>
        /// 修改约束
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="OriginalValue">原始值</param>
        /// <param name="CurrentValue">当前值</param>
        /// <returns></returns>
        public override string UpdateConstraintSql(string tableName, string OriginalValue, string CurrentValue)
        {
            return $"ALTER TABLE \"{Table_Schema}\".\"{tableName}\" RENAME CONSTRAINT {OriginalValue} TO {CurrentValue}";
        }

        /// <summary>
        /// 修改表所有者
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public override string UpdateOwnerSql(string tableName)
        {
            return $"ALTER TABLE \"{Table_Schema}\".{tableName} OWNER TO {Table_Owner}";
        }
    }
}