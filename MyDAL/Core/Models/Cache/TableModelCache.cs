using MyDAL.Core.Common;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MyDAL.Core.Models.Cache
{
    /// <summary>
    /// db-table & cs-model pre cache 
    /// </summary>
    internal class TableModelCache
    {
        /// <summary>
        /// model type
        /// </summary>
        internal Type MType { get; set; }
        
        /// <summary>
        /// model properties
        /// </summary>
        internal List<PropertyInfo> MProps { get; set; }

        internal string TbName
        {
            get
            {
                return TbAttr.Name;
            }
        }
        /// <summary>
        /// XTable
        /// </summary>
        internal XTableAttribute TbAttr { get; set; }
        
        /// <summary>
        /// table columns 
        /// </summary>
        internal List<ColumnInfo> TbCols { get; set; }

        /// <summary>
        /// Prop-Col-Attr
        /// </summary>
        internal List<TmPropColAttrInfo> PCA { get; set; }

        internal bool HaveAutoIncrementPK { get; set; } = false;
        internal TmPropColAttrInfo AutoIncrementPK { get; set; }
    }
}
