using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Base.Models
{
    /// <summary>
    /// 索引
    /// </summary>
    public class IndexDto
    {
        public bool IsPrimary { get; set; }
        /// <summary>
        /// 索引名字
        /// </summary>
        public string IndexName { get; set; }
        /// <summary>
        /// 索引字段
        /// </summary>
        public string IndexColumn { get; set; }
        /// <summary>
        /// 索引所在的表
        /// </summary>
        public string IndexTable { get; set; }
    }
}
