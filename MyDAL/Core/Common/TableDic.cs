using System;

namespace MyDAL.Core.Common
{
    /// <summary>
    /// model-type , table-alias dic
    /// </summary>
    internal class TableDic
    {
        /// <summary>
        /// model-type
        /// </summary>
        internal Type TbM { get; set; }
        
        /// <summary>
        /// table-alias
        /// </summary>
        internal string Alias { get; set; }
    }
}
