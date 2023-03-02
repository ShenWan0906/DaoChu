using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Base.Models
{
    /// <summary>
    /// 约束
    /// </summary>
    public class ConstraintDto
    {
        /// <summary>
        /// 约束名字
        /// </summary>
        public string ConstraintName { get; set; }
        /// <summary>
        /// 约束字段
        /// </summary>
        public string ConstraintColumn { get; set; }
        /// <summary>
        /// 约束所在的表
        /// </summary>
        public string ConstraintTable { get; set; }
    }
}
