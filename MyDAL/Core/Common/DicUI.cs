using System;
using System.Collections.Generic;
using Yunyong.DataExchange.Core.Bases;

namespace Yunyong.DataExchange.Core.Common
{
    internal class DicUI : DicBase
    {
        internal string ClassName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ClassFullName))
                {
                    return string.Empty;
                }

                var arr = ClassFullName.Split('.');
                return arr[arr.Length - 1];
            }
        }
        internal string ClassFullName { get; set; }
        internal string TableAliasOne { get; set; }
        internal string ColumnOne { get; set; }
        internal string ColumnOneAlias { get; set; }
        internal string TableTwo { get; set; }
        internal string TableAliasTwo { get; set; }
        internal string ColumnTwo { get; set; }

        internal string Param { get; set; }
        internal string ParamRaw { get; set; }
        internal object CsValue { get; set; }
        internal string CsValueStr { get; set; }
        internal Type CsType { get; set; }

        internal int TvpIndex { get; set; }

        internal List<DicUI> Group { get; set; }
        internal List<DicUI> InItems { get; set; }
    }
}
