using MyDAL.AdoNet;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Helper;
using MyDAL.DataRainbow.MySQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MyDAL.Core.Bases
{
    internal abstract class Context
    {

        /************************************************************************************************************************/

        internal void Init(IDbConnection conn)
        {
            //
            if (XConfig.DB == DbEnum.None)
            {
                if (XConfig.MySQL.Equals(conn.GetType().FullName, StringComparison.OrdinalIgnoreCase))
                {
                    XConfig.DB = DbEnum.MySQL;
                }
                else
                {
                    throw new Exception("MyDAL 目前只支持 【MySQL】,后续将会支持【Oracle/SQLServer/PostgreSQL/DB2/Access/SQLite/Teradata/MariaDB】.");
                }
            }

            //
            Conn = conn;
            Parameters = new List<DicParam>();
            AH = new AttributeHelper(this);
            VH = new CsValueHelper(this);
            GH = new GenericHelper(this);
            XE = new XExpression(this);
            CFH = new CsFuncHelper(this);
            TSH = new ToStringHelper(this);
            XC = new XCache(this);
            PH = new ParameterHelper(this);
            DPH = new DicParamHelper(this);
            BDH = new BatchDataHelper();
            DS = new DataSource(this);
            AR = new AutoRetry();
            TbMs = new List<TableDic>();

            //
            if (XConfig.DB == DbEnum.MySQL)
            {
                SqlProvider = new MySqlProvider(this);
            }
        }

        /************************************************************************************************************************/

        internal AttributeHelper AH { get; private set; }
        internal GenericHelper GH { get; private set; }
        internal ParameterHelper PH { get; private set; }
        internal BatchDataHelper BDH { get; private set; }
        internal AutoRetry AR { get; private set; }

        /************************************************************************************************************************/

        internal XExpression XE { get; private set; }

        internal CsFuncHelper CFH { get; private set; }
        internal ToStringHelper TSH { get; private set; }

        internal CsValueHelper VH { get; private set; }
        internal DicParamHelper DPH { get; private set; }

        /************************************************************************************************************************/

        internal CrudEnum Crud { get; set; } = CrudEnum.None;
        internal ActionEnum Action { get; set; } = ActionEnum.None;
        internal OptionEnum Option { get; set; } = OptionEnum.None;
        internal CompareXEnum Compare { get; set; } = CompareXEnum.None;
        internal FuncEnum Func { get; set; } = FuncEnum.None;

        internal UiMethodEnum Method { get; set; } = UiMethodEnum.None;

        internal SetEnum Set { get; set; } = SetEnum.AllowedNull;

        /************************************************************************************************************************/

        internal bool NeedSetSingle { get; set; } = true;
        internal Type TbM1 { get; set; }
        internal List<TableDic> TbMs { get; set; }
        internal int DicID { get; set; } = 1;
        internal List<DicParam> Parameters { get; set; }
        internal List<string> SQL { get; private set; } = new List<string>();
        internal int? PageIndex { get; set; } = null;
        internal int? PageSize { get; set; } = null;
        internal bool IsMultiColCount { get; set; } = false;

        /************************************************************************************************************************/

        internal IDbConnection Conn { get; private set; }
        internal ISqlProvider SqlProvider { get; set; }
        internal XCache XC { get; private set; }
        internal DataSource DS { get; private set; }

        /************************************************************************************************************************/

        internal bool IsInParameter(DicParam dic)
        {
            if (dic.Group == null
                && dic.CsValue != null
                && dic.Option == OptionEnum.Compare
                && (dic.Compare == CompareXEnum.In || dic.Compare == CompareXEnum.NotIn))
            {
                return true;
            }
            return false;
        }
        internal bool IsParameter(ActionEnum action)
        {
            switch (action)
            {
                case ActionEnum.Insert:
                case ActionEnum.Update:
                case ActionEnum.Where:
                case ActionEnum.And:
                case ActionEnum.Or:
                case ActionEnum.SQL:
                    return true;
            }
            return false;
        }
        internal bool IsSingleTableOption()
        {
            switch (Crud)
            {
                case CrudEnum.Query:
                case CrudEnum.Update:
                case CrudEnum.Delete:
                case CrudEnum.Create:
                    return true;
            }
            return false;
        }

        /************************************************************************************************************************/

        internal void ParseSQL(params string[] paras)
        {
            SQL.Clear();
            SQL.AddRange(paras);
        }
        internal void ParseParam(List<XParam> paras)
        {
            DPH.ResetParameter();
            foreach (var p in paras)
            {
                p.Name.Replace("@", "");
                if (p.Type == ParamTypeEnum.None)
                {
                    p.Type = ParamTypeEnum.MySQL_VarChar;
                }
                if (p.Direction == ParamDirectionEnum.None)
                {
                    p.Direction = ParamDirectionEnum.Input;
                }
                DPH.AddParameter(new DicParam
                {
                    Crud = CrudEnum.SQL,
                    Action = ActionEnum.SQL,
                    Param = p.Name,
                    ParamRaw = p.Name,
                    CsValue = p.Value,
                    CsValueStr = p.Value == null ? string.Empty : VH.ValueProcess(p.Value, p.Value.GetType(), string.Empty),
                    CsType = p.Value == null ? default(Type) : p.Value.GetType(),
                    ParamUI = p
                });
            }
            DPH.SetParameter();
        }

        /*********************************************************************************************************************************************/

        internal OptionEnum GetChangeOption(ChangeEnum change)
        {
            switch (change)
            {
                case ChangeEnum.Add:
                    return OptionEnum.ChangeAdd;
                case ChangeEnum.Minus:
                    return OptionEnum.ChangeMinus;
                default:
                    return OptionEnum.ChangeAdd;
            }
        }

        internal void SetMTCache<M>()
        {
            //
            var type = typeof(M);
            if (NeedSetSingle)
            {
                TbM1 = type;
                NeedSetSingle = false;
            }
            XC.GetTableModel(type);
        }

        internal void SetTbMs<M>(string alias)
        {
            if (!TbMs.Any(it => it.Alias.Equals(alias, StringComparison.OrdinalIgnoreCase)))
            {
                TbMs.Add(new TableDic
                {
                    TbM = typeof(M),
                    Alias = alias
                });
            }
        }

        internal Exception Exception(string code, string msg)
        {
            return new Exception($"{code} -- [[{msg}]] 未能解析!!! 请 EMail: --> liumeng0403@163.com <--");
        }

    }
}
