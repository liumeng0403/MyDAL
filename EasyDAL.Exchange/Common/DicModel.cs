using System;
using Yunyong.DataExchange.Enums;

namespace Yunyong.DataExchange.Common
{
    internal class DicModel
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
        public string TableOne { get; set; }
        public string TableAliasOne { get; set; }
        public string ColumnOne { get; set; }
        public string TableTwo { get; set; }
        public string KeyTwo { get; set; }
        public string AliasTwo { get; set; }
        public string VmColumn { get; set; }
        public string ColumnAlias { get; set; }
        public string Param { get; set; }
        public string ParamRaw { get; set; }
        public string CsValue { get; set; }
        public string DbValue { get; set; }
        public Type ValueType { get; set; }
        public string ColumnType { get; set; }
        public CompareEnum Compare { get; set; }
        public OptionEnum Option { get; set; }
        public ActionEnum Action { get; set; }
        public CrudTypeEnum Crud { get; set; }
        
        public int TvpIndex { get; set; }
    }

    internal class DicQueryModel
    {
        public string MField { get; set; }
        public string VmField { get; set; }
        public CompareEnum Compare { get; set; }
    }
}
