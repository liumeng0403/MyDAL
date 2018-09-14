using Yunyong.DataExchange.Enums;
using System;

namespace Yunyong.DataExchange.Common
{
    internal class DicModel
    {
        public string ClassFullName { get; set; }
        public string TableOne { get; set; }
        public string TableAliasOne { get; set; }
        public string ColumnOne { get; set; }
        public string TableTwo { get; set; }
        public string KeyTwo { get; set; }
        public string AliasTwo { get; set; }
        public string ColumnAlias { get; set; }
        public string Param { get; set; }
        public string ParamRaw { get; set; }
        public string Value { get; set; }
        public string DbValue { get; set; }
        public Type ValueType { get; set; }
        public string ColumnType { get; set; }
        public OptionEnum Option { get; set; }
        public ActionEnum Action { get; set; }
        public CrudTypeEnum Crud { get; set; }

        public OptionEnum FuncSupplement { get; set; }
        public int TvpIndex { get; set; }
    }
}
