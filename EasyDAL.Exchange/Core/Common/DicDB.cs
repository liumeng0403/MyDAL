using MyDAL.Core.Bases;
using System.Collections.Generic;
using System.Data;

namespace MyDAL.Core.Common
{

    internal class DicDB : DicBase
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

        internal List<DicDB> Group { get; set; }

        internal List<DicDB> InItems { get; set; }
    }
}
