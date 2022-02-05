using System;

namespace MyDAL.Core.Common
{
    /// <summary>
    /// sql param 键-值 对象
    /// </summary>
    internal class SetParam
    {
        internal string Key { get; set; }
        internal string Param { get; set; }
        internal ValueInfo Val { get; set; }
        internal Type ValType { get; set; }
    }
}
