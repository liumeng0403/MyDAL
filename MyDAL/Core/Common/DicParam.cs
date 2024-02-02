using MyDAL.AdoNet;
using MyDAL.Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using MyDAL.Core.Enums;

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
        /// 处理后的SQL参数名：原始参数名_编号
        /// </summary>
        internal string Param { get; set; }
        
        /// <summary>
        /// 原始参数名
        /// </summary>
        internal string ParamRaw { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
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
        
        
        /// <summary>
        /// 初始 dic 实例
        /// </summary>
        internal void SetDicBase(Context dc)
        {
                //
                this.ID = 0;
                this.IsDbSet = false;
                
                //
                this.Crud = dc.Crud;
                this.Action = dc.Action;
                this.Option = dc.Option;
                this.Compare = dc.Compare;
                
                //
                this.Func = ColFuncEnum.None;

                //
                this.GroupAction = ActionEnum.None;
                this.GroupRef = null;
        }
        
        /// <summary>
        /// 获取列，, 由 属性 得到 列
        /// </summary>
        internal string GetCol(Context dc,Type mType, string prop)
        {
            //
            if (mType == null)
            {
                return prop;
            }

            //
            var tb = dc.XC.GetTableModel(mType);

            //
            var pc = tb?.PCA?.FirstOrDefault(it => prop.Equals(it.PropName, StringComparison.OrdinalIgnoreCase));
            if (pc != null)
            {
                return pc.ColName;
            }
            else
            {
                return prop;
            }
        }
    }
}
