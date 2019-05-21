using MyDAL.Core.Common;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MyDAL.Core.Models.Cache
{
    internal class TableModelCache
    {
        internal Type MType { get; set; }
        internal List<PropertyInfo> MProps { get; set; }

        internal string TbName
        {
            get
            {
                return TbAttr.Name;
            }
        }
        internal XTableAttribute TbAttr { get; set; }
        internal List<ColumnInfo> TbCols { get; set; }

        /// <summary>
        /// Prop-Col-Attr
        /// </summary>
        internal List<TmPropColAttrInfo> PCA { get; set; }
    }
}
