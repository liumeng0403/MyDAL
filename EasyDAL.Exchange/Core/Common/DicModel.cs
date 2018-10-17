using MyDAL.Core.Enums;
using System;
using System.Data;

namespace MyDAL.Core.Common
{
    internal class DicModelBase
    {
        public int ID { get; set; }
        public CrudTypeEnum Crud { get; set; }
        public ActionEnum Action { get; set; }
        public OptionEnum Option { get; set; }
        public CompareEnum Compare { get; set; }

    }

    internal class DicModelUI:DicModelBase
    {
        public string ClassName
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
        public string ClassFullName { get; set; }
        //public string TableOne { get; set; }
        public string TableAliasOne { get; set; }
        public string ColumnOne { get; set; }
        public string ColumnOneAlias { get; set; }
        public string TableTwo { get; set; }
        public string TableAliasTwo { get; set; }
        public string ColumnTwo { get; set; }

        public string Param { get; set; }
        public string ParamRaw { get; set; }
        public object CsValue { get; set; }
        public string CsValueStr { get; set; }
        public Type CsType { get; set; }
        
        public int TvpIndex { get; set; }
    }

    internal class DicModelDB : DicModelBase
    {
        //public string ClassFullName { get; set; }
        public string Key { get; set; }
        public string TableOne { get; set; }
        public string TableAliasOne { get; set; }
        public string ColumnOne { get; set; }
        public string KeyTwo { get; set; }
        public string AliasTwo { get; set; }
        public string ColumnAlias { get; set; }
        public string Param { get; set; }
        public string ParamRaw { get; set; }
        public object DbValue { get; set; }
        public DbType? DbType { get; set; }

        public string ColumnType { get; set; }

        public int TvpIndex { get; set; }
    }

    internal class DicQueryModel
    {
        public string MField { get; set; }
        public string VmField { get; set; }
        public CompareEnum Compare { get; set; }
    }
}
