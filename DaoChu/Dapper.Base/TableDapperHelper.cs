using Dapper.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dapper.Base
{
    public abstract class TableDapperHelper : DapperBaseHelper
    {
        public TableDapperHelper(string conn) : base(conn)
        {
        }

        /// <summary>
        /// 获取所有字段
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<ColDto> GetColList(string tableName)
        {
            return TryGetList<ColDto>(GetColSql(tableName)).ToList();
        }

        /// <summary>
        /// 根据字段查找表
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        public List<string> GetTableByCol(string colName)
        {
            return TryGetList<string>(GetTableByColSql(colName)).ToList();
        }

        /// <summary>
        /// 获取所有表数据
        /// </summary>
        /// <returns></returns>
        public List<TableDto> GetAllTableList()
        {
            return this.TryGetList<TableDto>(GetAllTableSql()).ToList();
        }

        /// <summary>
        /// 获取所有视图数据
        /// </summary>
        /// <returns></returns>
        public List<TableDto> GetAllViewList()
        {
            return this.TryGetList<TableDto>(GetAllViewSql()).ToList();
        }

        /// <summary>
        /// 匹配表名查询表数据
        /// </summary>
        /// <param name="like"></param>
        /// <returns></returns>
        public List<TableDto> GetQueryTableName(string like)
        {
            return this.TryGetList<TableDto>(GetTableListSql(like)).ToList();
        }

        /// <summary>
        /// 获取单表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public TableDto GetTable(string tableName)
        {
            return FirstOrDefault<TableDto>(GetTableSql(tableName));
        }

        /// <summary>
        /// 修改字段描述
        /// </summary>
        /// <param name="cols"></param>
        public void UpdateColComments(ColDto col)
        {
            TryExecute(UpdateColCommentsSql(col.TableName, col.ColumnName, col.Comments));
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual IList<object> GetTableData(string tableName, List<string> fields, Type type)
        {
            var sql = $"SELECT {string.Join(",", fields)} FROM \"{tableName}\"";
            return TryGetList(sql, type);
        }

        public void UpdateTableComments(string tableName, string commit)
        {
            TryExecute(UpdateTableCommentsSql(tableName, commit));
        }

        /// <summary>
        /// 获取索引列表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<IndexDto> GetIndexes(string tableName)
        {
            return TryGetList<IndexDto>(GetIndexesSql(tableName)).ToList();
        }

        /// <summary>
        /// 修改索引
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="OriginalValue">原始值</param>
        /// <param name="CurrentValue">当前值</param>
        /// <returns></returns>
        public void UpdateIndex(string tableName, string OriginalValue, string CurrentValue)
        {
            TryExecute(UpdateIndexSql(tableName, OriginalValue, CurrentValue));
        }

        /// <summary>
        /// 获取索引列表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<ConstraintDto> GetConstraints(string tableName)
        {
            return TryGetList<ConstraintDto>(GetConstraintSql(tableName)).ToList();
        }


        /// <summary>
        /// 修改约束
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="OriginalValue">原始值</param>
        /// <param name="CurrentValue">当前值</param>
        /// <returns></returns>
        public void UpdateConstraint(string tableName, string OriginalValue, string CurrentValue)
        {
            TryExecute(UpdateConstraintSql(tableName, OriginalValue, CurrentValue));
        }


        /// <summary>
        /// 修改表所有者
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public void UpdateOwner(string tableName)
        {
            TryExecute(UpdateOwnerSql(tableName));
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="tableName"></param>
        public virtual void DeleteTableData(string tableName)
        {
            TryExecute($"DELETE FROM {tableName}");
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldList"></param>
        /// <param name="memberList"></param>
        /// <param name="list"></param>
        public virtual void InsertTableData(string tableName, List<string> fieldList, List<string> memberList, IList<object> list)
        {
            TryExecute($"INSERT INTO \"{tableName}\"({string.Join(',', fieldList.Select(x => "\"" + x + "\""))}) VALUES({string.Join(',', memberList.Select(x => ":" + x))})", list);
        }

        //        /// <summary>
        //        /// 获取索引列表
        //        /// </summary>
        //        /// <param name="tableName"></param>
        //        /// <returns></returns>
        //        public virtual List<ConstraintDto> GetConstraints(string tableName)
        //        {
        //            var sql = @$"SELECT
        //     tc.constraint_name as ConstraintName, tc.table_name as ConstraintTable, kcu.column_name as ConstraintColumn, 
        //     ccu.table_name AS foreign_table_name,
        //     ccu.column_name AS foreign_column_name,
        //     tc.is_deferrable,tc.initially_deferred
        // FROM 
        //     information_schema.table_constraints AS tc 
        //     JOIN information_schema.key_column_usage AS kcu ON tc.constraint_name = kcu.constraint_name
        //     JOIN information_schema.constraint_column_usage AS ccu ON ccu.constraint_name = tc.constraint_name
        // WHERE tc.table_name = '{tableName}';";
        //            sql = @"select b.TABLE_NAME as ConstraintTable,b.index_name ConstraintName,b.column_name ConstraintColumn from user_indexes a ,user_ind_columns b
        //where a.table_name=b.table_name and a.index_name = b.index_name 
        //and a.table_owner=upper('BMIS') and b.index_name like '%,%'  order by a.uniqueness desc;";
        //            return TryGetList<ConstraintDto>(sql).ToList();
        //        }

        #region abstract

        public override string GetColType(ColDto dto)
        {
            throw new Exception("方法未实现");
        }

        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public override string GetTableSql(string tableName)
        {
            throw new Exception("方法未实现");
        }

        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public override string GetTableListSql(string like)
        {
            throw new Exception("方法未实现");
        }

        /// <summary>
        /// 获取所有表
        /// </summary>
        /// <returns></returns>
        public override string GetAllTableSql()
        {
            throw new Exception("方法未实现");
        }

        /// <summary>
        /// 获取所有视图
        /// </summary>
        /// <returns></returns>
        public override string GetAllViewSql()
        {
            throw new Exception("方法未实现");
        }

        /// <summary>
        /// 获取字段
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public override string GetColSql(string tableName)
        {
            throw new Exception("方法未实现");
        }

        /// <summary>
        /// 获取字段对应的表
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        public override string GetTableByColSql(string colName)
        {
            throw new Exception("方法未实现");
        }


        /// <summary>
        /// 修改字段描述
        /// </summary>
        /// <param name="cols"></param>
        public override string UpdateColCommentsSql(string tableName, string ColumnName, string Comments)
        {
            throw new Exception("方法未实现");
        }


        /// <summary>
        /// 修改表表述
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="commit"></param>
        public override string UpdateTableCommentsSql(string tableName, string commit)
        {
            throw new Exception("方法未实现");
        }


        /// <summary>
        /// 获取表中数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public override string GetTableDataSql(string tableName, List<string> fields)
        {
            throw new Exception("方法未实现");
        }

        /// <summary>
        /// 根据表名获取索引
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public override string GetIndexesSql(string tableName)
        {
            throw new Exception("方法未实现");
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
            throw new Exception("方法未实现");
        }

        /// <summary>
        /// 根据表名获取当前表约束
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public override string GetConstraintSql(string tableName)
        {
            throw new Exception("方法未实现");
        }

        /// <summary>
        /// 修改索引
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="OriginalValue">原始值</param>
        /// <param name="CurrentValue">当前值</param>
        /// <returns></returns>
        public override string UpdateConstraintSql(string tableName, string OriginalValue, string CurrentValue)
        {
            throw new Exception("方法未实现");
        }


        /// <summary>
        /// 修改表所有者
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public override string UpdateOwnerSql(string tableName)
        {
            throw new Exception("方法未实现");
        }

        #endregion
    }
}
