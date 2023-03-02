using Dapper.Base.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dapper.Base
{
    public abstract class TableSql
    {


        public abstract string GetColType(ColDto dto);

        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public abstract string GetTableSql(string tableName);

        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public abstract string GetTableListSql(string like);

        /// <summary>
        /// 获取所有表
        /// </summary>
        /// <returns></returns>
        public abstract string GetAllTableSql();

        /// <summary>
        /// 获取所有视图
        /// </summary>
        /// <returns></returns>
        public abstract string GetAllViewSql();

        /// <summary>
        /// 获取字段
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public abstract string GetColSql(string tableName);

        /// <summary>
        /// 获取字段对应的表
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        public abstract string GetTableByColSql(string colName);

        /// <summary>
        /// 修改字段描述
        /// </summary>
        /// <param name="cols"></param>
        public abstract string UpdateColCommentsSql(string tableName, string ColumnName, string Comments);

        /// <summary>
        /// 修改表表述
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="commit"></param>
        public abstract string UpdateTableCommentsSql(string tableName, string commit);

        /// <summary>
        /// 获取表中数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public abstract string GetTableDataSql(string tableName, List<string> fields);

        /// <summary>
        /// 根据表名获取当前表索引
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public abstract string GetIndexesSql(string tableName);

        /// <summary>
        /// 修改索引
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="OriginalValue">原始值</param>
        /// <param name="CurrentValue">当前值</param>
        /// <returns></returns>
        public abstract string UpdateIndexSql(string tableName, string OriginalValue, string CurrentValue);

        /// <summary>
        /// 根据表名获取当前表约束
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public abstract string GetConstraintSql(string tableName);

        /// <summary>
        /// 修改约束
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="OriginalValue">原始值</param>
        /// <param name="CurrentValue">当前值</param>
        /// <returns></returns>
        public abstract string UpdateConstraintSql(string tableName, string OriginalValue, string CurrentValue);


        /// <summary>
        /// 修改表所有者
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public abstract string UpdateOwnerSql(string tableName);
    }
}
