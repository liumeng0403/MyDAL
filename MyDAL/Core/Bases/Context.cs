using MyDAL.AdoNet;
using MyDAL.AdoNet.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Core.Helper;
using MyDAL.DataRainbow.XCommon.Bases;
using MyDAL.DataRainbow.XCommon.Interfaces;
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
            if (XConfig.ConnTypes.TryGetValue(conn.GetType().FullName, out var db))
            {
                DB = db;
            }
            else
            {
                throw new Exception("MyDAL 目前只支持【MySQL/SQLServer】,后续将会支持【Oracle/PostgreSQL/DB2/Access/SQLite/Teradata/MariaDB】.");
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
            AR = new AutoRetry();
            TbMs = new List<TableDic>();

            //
            if (XConfig.DbProviders.TryGetValue(DB, out var func))
            {
                SqlProvider = func(this);
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

        internal DbEnum DB { get; set; } = DbEnum.None;


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
            //
            if (paras == null)
            {
                paras = new List<XParam>();
            }

            //
            DPH.ResetParameter();
            foreach (var p in paras)
            {
                p.ParamName = p.ParamName.Replace(XSQL.QuestionMark.ToString(), "").Replace(XSQL.At.ToString(), "");
                if (p.ParamType == ParamTypeEnum.None)
                {
                    p.ParamType = ParamTypeEnum.MySQL_VarChar;
                }
                if (p.ParamDirection == ParamDirectionEnum.None)
                {
                    p.ParamDirection = ParamDirectionEnum.Input;
                }
                DPH.AddParameter(new DicParam
                {
                    Crud = CrudEnum.SQL,
                    Action = ActionEnum.SQL,
                    Param = p.ParamName,
                    ParamRaw = p.ParamName,
                    CsValue = p.ParamValue,
                    CsValueStr = p.ParamValue == null ? string.Empty : VH.ValueProcess(p.ParamValue, p.ParamValue.GetType(), string.Empty),
                    CsType = p.ParamValue == null ? default(Type) : p.ParamValue.GetType(),
                    ParamUI = p
                });
            }
            DPH.SetParameter();
        }

        /*********************************************************************************************************************************************/

        private List<DicParam> FlatDics { get; set; }
        private List<string> FlatParameters { get; set; }
        internal List<string> FlatSQL { get; set; }
        private List<string> FlatSqlWithParams { get; set; }
        internal bool FlatOutput { get; set; } = true;
        private char GetParamSymbol()
        {
            switch (DB)
            {
                case DbEnum.MySQL:
                    return XSQL.QuestionMark;
                case DbEnum.SQLServer:
                    return XSQL.At;
                default:
                    throw Exception(XConfig.EC._036, "暂时不支持的DB！！！");
            }
        }
        internal void SetValue()
        {
            //
            FlatDics = DPH.FlatDics(Parameters);

            //
            FlatParameters = FlatDics
                .Select(dbM =>
                {
                    var field = string.Empty;
                    if (dbM.Crud == CrudEnum.Query
                        || dbM.Crud == CrudEnum.Update
                        || dbM.Crud == CrudEnum.Create
                        || dbM.Crud == CrudEnum.Delete)
                    {
                        field = dbM.TbCol;
                    }
                    else if (dbM.Crud == CrudEnum.Join)
                    {
                        field = $"{dbM.TbAlias}.{dbM.TbCol}";
                    }

                    //
                    var csVal = string.Empty;
                    if (dbM.CsValue == null)
                    {
                        csVal = "Null";
                    }
                    else if (dbM.CsType == XConfig.CSTC.DateTime)
                    {
                        try
                        {
                            csVal = dbM.CsValue.ToDateTimeStr();
                        }
                        catch
                        {
                            csVal = dbM.CsValue.ToString();
                        }
                    }
                    else
                    {
                        csVal = dbM.CsValue.ToString();
                    }

                    //
                    var dbVal = string.Empty;
                    dbVal = dbM.ParamInfo.Value == DBNull.Value ? "DbNull" : dbM.ParamInfo.Value.ToString();

                    //
                    if (dbM.Action == ActionEnum.SQL)
                    {
                        return $"参数:【{dbM.ParamInfo.Name}】-->【{dbVal}】.";
                    }
                    else
                    {
                        return $"字段:【{field}】-->【{csVal}】;参数:【{dbM.Param}】-->【{dbVal}】.";
                    }
                })
                .ToList();

            //
            FlatSQL = SQL;

            //
            FlatSqlWithParams = new List<string>();
            var paramSymbol = GetParamSymbol();
            foreach (var sql in FlatSQL)
            {
                var sqlStr = sql;
                foreach (var par in FlatDics)
                {
                    if (par.ParamInfo.Type == DbType.Boolean
                        || par.ParamInfo.Type == DbType.Decimal
                        || par.ParamInfo.Type == DbType.Double
                        || par.ParamInfo.Type == DbType.Int16
                        || par.ParamInfo.Type == DbType.Int32
                        || par.ParamInfo.Type == DbType.Int64
                        || par.ParamInfo.Type == DbType.Single
                        || par.ParamInfo.Type == DbType.UInt16
                        || par.ParamInfo.Type == DbType.UInt32
                        || par.ParamInfo.Type == DbType.UInt64)
                    {
                        if (par.Action == ActionEnum.SQL)
                        {
                            sqlStr = sqlStr.Replace($"{paramSymbol}{par.ParamInfo.Name}", par.ParamInfo.Value == DBNull.Value ? "null" : par.ParamInfo.Value.ToString());
                        }
                        else
                        {
                            sqlStr = sqlStr.Replace($"{paramSymbol}{par.Param}", par.ParamInfo.Value == DBNull.Value ? "null" : par.ParamInfo.Value.ToString());
                        }
                    }
                    else
                    {
                        if (par.Action == ActionEnum.SQL)
                        {
                            sqlStr = sqlStr.Replace($"{paramSymbol}{par.ParamInfo.Name}", par.ParamInfo.Value == DBNull.Value ? "null" : $"'{par.ParamInfo.Value.ToString()}'");
                        }
                        else
                        {
                            sqlStr = sqlStr.Replace($"{paramSymbol}{par.Param}", par.ParamInfo.Value == DBNull.Value ? "null" : $"'{par.ParamInfo.Value.ToString()}'");
                        }
                    }
                }
                FlatSqlWithParams.Add(sqlStr);
            }

            //
            XDebug.SQL = FlatSQL;
            XDebug.Parameters = FlatParameters;
            XDebug.SqlWithParams = FlatSqlWithParams;
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
            return new Exception($"【ERR-{code}】 -- [[{msg}]] 未能解析!!!，请 EMail: --> liumeng0403@163.com <--");
        }

    }
}
