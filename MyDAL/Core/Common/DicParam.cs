using MyDAL.AdoNet;
using MyDAL.Core.Bases;
using System;
using System.Collections.Generic;

namespace MyDAL.Core.Common
{
    internal class DicParam
        : DicBase
    {
        //
        internal string TbMName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(TbMFullName))
                {
                    return string.Empty;
                }

                var arr = TbMFullName.Split('.');
                return arr[arr.Length - 1];
            }
        }
        internal string TbMFullName { get; set; }
        internal string TbMProp { get; set; }
        internal string TbName { get; set; }
        internal string TbAlias { get; set; }
        internal string TbCol { get; set; }
        internal string TbColAlias { get; set; }
        internal string TableTwo { get; set; }
        internal string TableAliasTwo { get; set; }
        internal string ColumnTwo { get; set; }

        //
        internal string Param { get; set; }
        internal string ParamRaw { get; set; }
        internal object CsValue { get; set; }
        internal string CsValueStr { get; set; }
        internal Type CsType { get; set; }
        internal string Format { get; set; }

        //
        internal string Key { get; set; }
        internal string ColumnType { get; set; }
        internal ParamInfo ParamInfo { get; set; }

        //
        internal List<DicParam> Group { get; set; }
        internal List<DicParam> InItems { get; set; }
        internal List<DicParam> Inserts { get; set; }
        internal List<DicParam> Columns { get; set; }
    }
}
