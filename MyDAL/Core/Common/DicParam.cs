using MyDAL.AdoNet;
using MyDAL.Core.Bases;
using System;
using System.Collections.Generic;

namespace MyDAL.Core.Common
{
    /// <summary>
    /// 
    /// </summary>
    internal class DicParam
        : DicBase
    {
        /// <summary>
        /// 表模型类型
        /// </summary>
        internal Type TbMType { get; set; }
        /// <summary>
        /// 表模型属性名
        /// </summary>
        internal string TbMProp { get; set; }        
        
        /// <summary>
        /// 表别名
        /// </summary>
        internal string TbAlias { get; set; }
        
        /// <summary>
        /// 表列名
        /// </summary>
        internal string TbCol { get; set; }
        internal string TbColAlias { get; set; }
        internal string TableTwo { get; set; }
        internal string TableAliasTwo { get; set; }
        internal string ColumnTwo { get; set; }

        /// <summary>
        /// model property name + dic_id
        /// </summary>
        internal string Param { get; set; }
        /// <summary>
        /// model property name
        /// </summary>
        internal string ParamRaw { get; set; }

        internal object CsValue { get; set; }
        internal string CsValueStr { get; set; }
        internal Type CsType { get; set; }
        internal string Format { get; set; }

        //
        internal ParamTypeEnum ColumnType { get; set; }
        internal ParamInfo ParamInfo { get; set; }
        internal XParam ParamUI { get; set; }

        //
        internal List<DicParam> Group { get; set; }
        internal List<DicParam> InItems { get; set; }
        /// <summary>
        /// insert row params
        /// </summary>
        internal List<DicParam> Inserts { get; set; }
        internal List<DicParam> Columns { get; set; }
    }
}
