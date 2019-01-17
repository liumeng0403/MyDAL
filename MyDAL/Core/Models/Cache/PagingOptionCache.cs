using System;
using System.Reflection;

namespace MyDAL.Core.Models.Cache
{
    internal class PagingOptionCache
    {
        internal XQueryAttribute Attr { get; set; }
        internal Type TbMType { get; set; }
        internal PropertyInfo TbMProp { get; set; }
        internal string TbName { get; set; }
        internal string TbCol { get; set; }
        internal Type PgType { get; set; }
        internal PropertyInfo PgProp { get; set; }
        internal CompareEnum Compare { get; set; }
    }
}
