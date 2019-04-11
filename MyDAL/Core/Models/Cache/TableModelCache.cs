using HPC.DAL.Core.Common;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace HPC.DAL.Core.Models.Cache
{
    internal class TableModelCache
    {
        internal string TbMName
        {
            get
            {
                return TbMType.Name;
            }
        }
        internal string TbMFullName
        {
            get
            {
                return TbMType.FullName;
            }
        }
        internal Type TbMType { get; set; }

        internal string TbName
        {
            get
            {
                return TbMAttr.Name;
            }
        }
        internal XTableAttribute TbMAttr { get; set; }

        internal List<ColumnInfo> TbCols { get; set; }
        internal List<PropertyInfo> TbMProps { get; set; }

        internal List<TmPropColAttrInfo> TMPCA { get; set; }
    }
}
