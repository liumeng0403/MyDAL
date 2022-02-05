using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Core.Helper;
using MyDAL.DataRainbow.MySQL;
using MyDAL.DataRainbow.XCommon.Bases;
using MyDAL.DataRainbow.XCommon.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace MyDAL.Core.Bases
{
    /// <summary>
    /// sql 操作 上下文
    /// </summary>
    internal abstract class Context
    {

        /************************************************************************************************************************/

        internal void Init(XConnection xConn)
        {
            //
            if (XConfig.ConnTypes.TryGetValue(xConn.Conn.GetType().FullName, out var db))
            {
                DB = db;
            }
            else
            {
                throw XConfig.EC.Exception(XConfig.EC._043, "MyDAL 只支持【MySQL】.");
            }

            //
            XConn = xConn;
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
            TbMs = new List<TableDic>();

            //
            SqlProvider = new MySqlProvider(this);
        }

        /************************************************************************************************************************/

        internal AttributeHelper AH { get; private set; }
        internal GenericHelper GH { get; private set; }
        internal ParameterHelper PH { get; private set; }
        internal BatchDataHelper BDH { get; private set; }

        /************************************************************************************************************************/

        internal XExpression XE { get; private set; }

        internal CsFuncHelper CFH { get; private set; }
        internal ToStringHelper TSH { get; private set; }

        /// <summary>
        /// cs 对象 值 处理 
        /// </summary>
        internal CsValueHelper VH { get; private set; }
        
        /// <summary>
        /// sql 参数 对象 处理
        /// </summary>
        internal DicParamHelper DPH { get; private set; }

        /************************************************************************************************************************/

        internal DbEnum DB { get; set; } = DbEnum.None;


        internal CrudEnum Crud { get; set; } = CrudEnum.None;
        internal ActionEnum Action { get; set; } = ActionEnum.None;
        /// <summary>
        /// sql 操作动作 与 sql 关键字
        /// </summary>
        internal OptionEnum Option { get; set; } = OptionEnum.None;
        internal CompareXEnum Compare { get; set; } = CompareXEnum.None;
        internal FuncEnum Func { get; set; } = FuncEnum.None;

        internal UiMethodEnum Method { get; set; } = UiMethodEnum.None;

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

        internal XConnection XConn { get; private set; }
        internal ISqlProvider SqlProvider { get; set; }
        /// <summary>
        /// 元数据 缓存
        /// </summary>
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

        internal void ParseSQL(string paras)
        {
            SQL.Clear();
            SQL.Add(paras);
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
                //
                p.ParamName = p.ParamName.Replace(XSQL.QuestionMark.ToString(), "").Replace(XSQL.At.ToString(), "");

                //
                if (p.ParamType == ParamTypeEnum.None
                    && p.ParamValue != null)
                {
                    var type = p.ParamValue.GetType();
                    if (type.IsEnum)
                    {
                        type = Enum.GetUnderlyingType(type);
                    }
                    else if (type.IsNullable())
                    {
                        type = Nullable.GetUnderlyingType(type);
                    }

                    if (XConfig.DefaultParamTypes.TryGetValue(type, out var func))
                    {
                        p.ParamType = func(DB);
                    }
                    else
                    {
                        throw XConfig.EC.Exception(XConfig.EC._012, $"请为参数【{p.ParamName}】指定【XParam.ParamType】类型 ！");
                    }
                }

                //
                if (p.ParamDirection == ParamDirectionEnum.None)
                {
                    p.ParamDirection = ParamDirectionEnum.Input;
                }

                //
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

        internal bool FlatOutput { get; set; } = true;
        internal bool IsSetParam { get; set; } = false;
        internal bool AlreadyOutput { get; set; } = false;
        private List<string> SetDebugSQL()
        {
            //
            var result = new List<string>();
            var paramSymbol = default(char);
            if (Crud == CrudEnum.SQL)
            {
                var isQuestionMark = SQL.Any(it => it.Contains(XSQL.QuestionMark));
                var isAt = isQuestionMark ? false : SQL.Any(it => it.Contains(XSQL.At));

                if (isQuestionMark)
                {
                    paramSymbol = XSQL.QuestionMark;
                }
                else if (isAt)
                {
                    paramSymbol = XSQL.At;
                }
                else
                {
                    // none
                }
            }
            else
            {
                if (DB == DbEnum.MySQL)
                {
                    paramSymbol = XSQL.QuestionMark;
                }
                else if (DB == DbEnum.SQLServer)
                {
                    paramSymbol = XSQL.At;
                }
                else
                {
                    throw XConfig.EC.Exception(XConfig.EC._036, "暂时不支持的DB！！！");
                }
            }
            var flatDics = DPH.FlatDics(Parameters);

            //
            foreach (var sql in SQL)
            {
                var sqlStr = sql;
                foreach (var par in flatDics)
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
                        sqlStr = sqlStr.Replace($"{paramSymbol}{par.ParamInfo.Name}", par.ParamInfo.Value == DBNull.Value ? "null" : par.ParamInfo.Value.ToString());
                    }
                    else
                    {
                        sqlStr = sqlStr.Replace($"{paramSymbol}{par.ParamInfo.Name}", par.ParamInfo.Value == DBNull.Value ? "null" : $"'{par.ParamInfo.Value.ToString()}'");
                    }
                }
                result.Add(sqlStr);
            }
            return result;
        }
        internal void OutPutSQL()
        {
            //
            if (!FlatOutput)
            {
                return;
            }

            //
            var sqlList = new List<string>();
            if (AlreadyOutput)
            {
                return;
            }
            else
            {
                if (XConn.IsDebug
                    && Parameters != null)
                {
                    sqlList = SetDebugSQL();
                }
                else
                {
                    sqlList = SQL;
                }
                AlreadyOutput = true;
            }

            //
            foreach (var sql in sqlList)
            {
                var info = $@"
=================================================================  <--  参数化 SQL 开始{(XConn.IsDebug ? "，Debug 模式已开启！" : "")}
{sql}
=================================================================  <--  参数化 SQL 结束
                                        ";
                switch (XConn.DebugType)
                {
                    case DebugEnum.Output:
                        Trace.WriteLine(info);
                        break;
                    case DebugEnum.Console:
                        Console.WriteLine(info);
                        break;
                }
            }
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

    }
}
