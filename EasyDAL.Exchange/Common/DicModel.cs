using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDAL.Exchange.Common
{
    internal class DicModel
    {
        public string TableOne { get; set; }
        public string KeyOne { get; set; }
        public string AliasOne{get;set;}
        public string TableTwo { get; set; }
        public string KeyTwo { get; set; }
        public string AliasTwo { get; set; }
        public string Param { get; set; }
        public string Value { get; set; }
        public ValueTypeEnum ValueType { get; set; }
        public OptionEnum Option { get; set; }
        public ActionEnum Action { get; set; }
        public CrudTypeEnum Crud { get; set; }

        public OptionEnum FuncSupplement { get; set; }
    }
}
