using Dapper.Base.Models;
using System;
using System.Collections.Generic;

namespace Dapper.Base
{
    public interface ITableDapperHelper : IDapperHelper
    {
        /// <summary>
        /// 获取所有字段
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        List<ColDto> GetColList(string tableName);

        /// <summary>
        /// 根据字段查找表
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        List<string> GetTableByCol(string colName);

        /// <summary>
        /// 获取所有表数据
        /// </summary>
        /// <returns></returns>
        List<TableDto> GetAllTableList();

        /// <summary>
        /// 获取所有视图数据
        /// </summary>
        /// <returns></returns>
        List<TableDto> GetAllViewList();

        /// <summary>
        /// 匹配表名查询表数据
        /// </summary>
        /// <param name="like"></param>
        /// <returns></returns>
        List<TableDto> GetQueryTableName(string like);

        /// <summary>
        /// 获取单表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        TableDto GetTable(string tableName);

        /// <summary>
        /// 修改字段描述
        /// </summary>
        /// <param name="cols"></param>
        void UpdateColComments(ColDto col);

        /// <summary>
        /// 修改表表述
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="commit">表描述</param>
        void UpdateTableComments(string tableName, string commit);

        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        string GetTableSql(string tableName);

        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        string GetTableListSql(string like);

        /// <summary>
        /// 获取所有表
        /// </summary>
        /// <returns></returns>
        string GetAllTableSql();

        /// <summary>
        /// 获取所有视图
        /// </summary>
        /// <returns></returns>
        string GetAllViewSql();

        /// <summary>
        /// 获取字段
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        string GetColSql(string tableName);

        /// <summary>
        /// 获取字段对应的表
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        string GetTableByColSql(string colName);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        IList<object> GetTableData(string tableName, List<string> fields, Type type);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="tableName"></param>
        void DeleteTableData(string tableName);

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldList"></param>
        /// <param name="memberList"></param>
        /// <param name="list"></param>
        void InsertTableData(string tableName, List<string> fieldList, List<string> memberList, IList<object> list);

        /// <summary>
        /// 获取索引列表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        List<IndexDto> GetIndexes(string tableName);

        /// <summary>
        /// 修改索引
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="OriginalValue">原始值</param>
        /// <param name="CurrentValue">当前值</param>
        /// <returns></returns>
        void UpdateIndex(string tableName, string OriginalValue, string CurrentValue);

        /// <summary>
        /// 获取索引列表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        List<ConstraintDto> GetConstraints(string tableName);

        /// <summary>
        /// 修改约束
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="OriginalValue">原始值</param>
        /// <param name="CurrentValue">当前值</param>
        /// <returns></returns>
        void UpdateConstraint(string tableName, string OriginalValue, string CurrentValue);

        /// <summary>
        /// 修改表所有者
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        void UpdateOwner(string tableName);

    }
}
