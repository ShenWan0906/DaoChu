using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Base.Models
{
    public class ColDto
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 字段名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// 是否为空
        /// </summary>
        public string NullAble { get; set; }
        /// <summary>
        /// 字段解释
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// 是否有小数点
        /// </summary>
        public int? DataScale { get; set; }
    }
}
