using System;

namespace MyDAL.Core.Common
{
    internal class ColumnParam
    {
        internal string Prop { get; set; }
        internal string Key { get; set; }
        internal string Alias { get; set; }
        internal Type ValType { get; set; }
        internal string ClassFullName { get; set; }
        internal string Format { get; set; }
    }
}
