using System;

namespace MyDAL.Core.Common
{
    internal class SetParam
    {
        internal string Key { get; set; }
        internal string Param { get; set; }
        internal ValueInfo Val { get; set; }
        internal Type ValType { get; set; }
    }
}
