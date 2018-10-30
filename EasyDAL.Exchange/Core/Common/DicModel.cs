using MyDAL.Core.Enums;
using System;
using System.Collections.Generic;
using System.Data;

namespace MyDAL.Core.Common
{
    internal class DicModelBase
    {
        internal int ID { get; set; }
        internal CrudTypeEnum Crud { get; set; }
        internal ActionEnum Action { get; set; }
        internal OptionEnum Option { get; set; }
        internal CompareEnum Compare { get; set; }
        internal FuncEnum Func { get; set; }
        
        internal ActionEnum GroupAction { get; set; }
        internal DicModelBase GroupRef { get; set; }
    }

    internal class DicModelUI : DicModelBase
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

        internal List<DicModelUI> Group { get; set; }
    }

    internal class DicModelDB : DicModelBase
    {
        internal string Key { get; set; }
        internal string TableOne { get; set; }
        internal string TableAliasOne { get; set; }
        internal string ColumnOne { get; set; }
        internal string KeyTwo { get; set; }
        internal string AliasTwo { get; set; }
        internal string ColumnAlias { get; set; }
        internal string Param { get; set; }
        internal string ParamRaw { get; set; }
        internal object DbValue { get; set; }
        internal DbType DbType { get; set; }

        internal string ColumnType { get; set; }

        internal int TvpIndex { get; set; }

        internal List<DicModelDB> Group { get; set; }
    }

    internal class DicQueryModel
    {
        internal string MField { get; set; }
        internal string VmField { get; set; }
        internal CompareEnum Compare { get; set; }
    }
}
