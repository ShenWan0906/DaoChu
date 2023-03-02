using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Base.Models
{
    public class TableDto
    {
        public string FileName { get; set; }
        public string ModelName { get; set; }
        public string TableName { get; set; }
        public string TableType { get; set; }
        public string Comments { get; set; }
    }
}
