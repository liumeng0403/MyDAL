using HPC.DAL.Core.Common;
using HPC.DAL.Core.Models.ExpPara;
using System;
using System.Reflection;

namespace HPC.DAL.Core.Models.Page
{
    internal class XQueryParam
    {
        internal Type PgType { get; set; }
        internal PropertyInfo PgProp { get; set; }
        internal XQueryAttribute PgAttr { get; set; }
        internal ColumnParam Cp { get; set; }
        internal ValueInfo Val { get; set; }
        internal CompareEnum Compare { get; set; }
    }
}
