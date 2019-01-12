using System;

namespace MyDAL.Core.Models.ExpPara
{
    internal class ColumnParam
    {
        internal string Prop { get; set; }
        internal string Key { get; set; }
        internal string Alias { get; set; }
        internal Type ValType { get; set; }
        internal string TbMFullName { get; set; }
        internal string Format { get; set; }
    }
}
