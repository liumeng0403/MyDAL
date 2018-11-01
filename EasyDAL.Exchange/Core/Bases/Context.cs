using System;
using System.Collections.Generic;
using System.Data;
using Yunyong.DataExchange.AdoNet;
using Yunyong.DataExchange.Cache;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Core.Helper;
using Yunyong.DataExchange.DBRainbow.MySQL;

namespace Yunyong.DataExchange.Core.Bases
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
            UiConditions = new List<DicUI>();
            DbConditions = new List<DicDB>();
            AH = new AttributeHelper(this);
            VH = new CsValueHelper(this);
            GH = new GenericHelper(this);
            EH = new XExpression(this);
            SC = new StaticCache(this);
            PH = new ParameterHelper(this);
            DH = new DicModelHelper(this);
            BDH = new BatchDataHelper();
            DS = new DataSource();

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

        /************************************************************************************************************************/

        internal XExpression EH { get; private set; }
        internal CsValueHelper VH { get; private set; }
        internal DicModelHelper DH { get; private set; }

        /************************************************************************************************************************/

        internal CrudTypeEnum Crud { get; set; } = CrudTypeEnum.None;
        internal ActionEnum Action { get; set; } = ActionEnum.None;
        internal OptionEnum Option { get; set; } = OptionEnum.None;
        internal CompareEnum Compare { get; set; } = CompareEnum.None;
        internal FuncEnum Func { get; set; } = FuncEnum.None;

        /************************************************************************************************************************/

        internal int DicID { get; set; } = 1;
        internal List<DicUI> UiConditions { get; private set; }
        internal List<DicDB> DbConditions { get; private set; }

        /************************************************************************************************************************/

        internal IDbConnection Conn { get; private set; }
        internal IDbTransaction Tran { get; set; }

        /************************************************************************************************************************/

        internal ISqlProvider SqlProvider { get; set; }
        internal Operator OP { get; set; }
        internal Impler IP { get; set; }

        /************************************************************************************************************************/

        internal StaticCache SC { get; private set; }
        internal DataSource DS { get; private set; }

        /************************************************************************************************************************/

        internal bool IsInParameter(object value, OptionEnum option)
        {
            if (value != null
                && (option == OptionEnum.In || option == OptionEnum.NotIn))
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
                    return true;
            }
            return false;
        }
        internal bool IsFilterCondition(ActionEnum action)
        {
            switch (action)
            {
                case ActionEnum.Where:
                case ActionEnum.And:
                case ActionEnum.Or:
                    return true;
            }
            return false;
        }
        internal bool IsSingleTableOption(CrudTypeEnum crud)
        {
            switch (crud)
            {
                case CrudTypeEnum.Query:
                case CrudTypeEnum.Update:
                case CrudTypeEnum.Delete:
                    return true;
            }
            return false;
        }

        /************************************************************************************************************************/

        private List<DicUI> FlatDics(List<DicUI> dics)
        {
            var ds = new List<DicUI>();

            //
            foreach (var d in dics)
            {
                if (IsParameter(d.Action))
                {
                    if (d.Group != null)
                    {
                        ds.AddRange(FlatDics(d.Group));
                    }
                    else if (d.InItems != null)
                    {
                        ds.AddRange(FlatDics(d.InItems));
                    }
                    else
                    {
                        ds.Add(d);
                    }
                }
            }

            //
            return ds;
        }
        private List<DicDB> FlatDics(List<DicDB> dics)
        {
            var ds = new List<DicDB>();

            //
            foreach (var d in dics)
            {
                if (IsParameter(d.Action))
                {
                    if (d.Group != null)
                    {
                        ds.AddRange(FlatDics(d.Group));
                    }
                    else if (d.InItems != null)
                    {
                        ds.AddRange(FlatDics(d.InItems));
                    }
                    else
                    {
                        ds.Add(d);
                    }
                }
            }

            //
            return ds;
        }
        internal DbParameters GetParameters(List<DicDB> dbs)
        {
            var paras = new DbParameters();

            //
            foreach (var db in dbs)
            {
                if (IsParameter(db.Action))
                {
                    if (db.Group != null)
                    {
                        paras.Add(GetParameters(db.Group));
                    }
                    else if (IsInParameter(db.DbValue, db.Option))
                    {
                        paras.Add(GetParameters(db.InItems));
                    }
                    else
                    {
                        paras.Add(db.Param, db.DbValue, db.DbType);
                    }
                }
            }

            //
            if (XConfig.IsDebug)
            {
                lock (XDebug.Lock)
                {
                    XDebug.UIs = FlatDics(UiConditions);
                    XDebug.DBs = FlatDics(DbConditions);
                    XDebug.SetValue();
                }
            }

            //
            return paras;
        }

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

        internal void AddConditions(DicUI dic)
        {
            if (IsInParameter(dic.CsValue, dic.Option))
            {
                dic.InItems = new List<DicUI>();
                DH.UniqueDicContext(dic, dic.InItems);
            }
            else
            {
                DH.UniqueDicContext(dic, UiConditions);
            }
            UiConditions.Add(dic);

            //
            Compare = CompareEnum.None;
            Func = FuncEnum.None;
        }

        internal void ResetConditions()
        {
            UiConditions = new List<DicUI>();
            DbConditions = new List<DicDB>();
        }

        internal void SetMTCache<M>()
        {
            //
            var type = typeof(M);
            var key = SC.GetModelKey(type.FullName);

            //
            var table = SqlProvider.GetTableName<M>();
            SC.SetModelTableName(key, table);
            SC.SetModelType(key, type);
            SC.SetModelProperys(type, this);
            (SC.SetModelColumnInfos(key, this)).GetAwaiter().GetResult();
        }

    }
}
