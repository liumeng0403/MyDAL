using System;

namespace MyDAL.Core.Common
{
    internal class PagingOptionCache
    {
        internal Type TbType { get; set; }
        internal string TbName { get; set; }
        internal string PropName { get; set; }
        internal string ColName { get; set; }
        internal CompareEnum Compare { get; set; }
    }
}
