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
        internal string PropName { get; set; }
        internal string ColName { get; set; }
        internal CompareEnum Compare { get; set; }
    }
}
