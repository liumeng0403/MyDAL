using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EasyDAL.Exchange.X
{
    internal sealed class ParamInfo
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public ParameterDirection ParameterDirection { get; set; }
        public DbType? DbType { get; set; }
        public int? Size { get; set; }
        public IDbDataParameter AttachedParam { get; set; }
        
        public byte? Precision { get; set; }
        public byte? Scale { get; set; }
    }
}
