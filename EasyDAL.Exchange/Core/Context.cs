using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yunyong.DataExchange.AdoNet;
using Yunyong.DataExchange.Cache;
using Yunyong.DataExchange.Common;
using Yunyong.DataExchange.Enums;
using Yunyong.DataExchange.ExpressionX;
using Yunyong.DataExchange.Extensions;
using Yunyong.DataExchange.Helper;

namespace Yunyong.DataExchange.Core
{
    internal abstract class Context
    {

        internal void Init(IDbConnection conn)
        {
            Conn = conn;
            Conditions = new List<DicModel>();
            AH = AttributeHelper.Instance;
            VH = new ValHandle(this);
            GH = GenericHelper.Instance;
            EH = new ExpressionHandleX(this);
            SC = StaticCache.Instance;
            PPH = ParameterPartHandle.Instance;
            BDH = BatchDataHelper.Instance;
            SqlProvider = new MySqlProvider(this);
        }

        internal bool IsParameter(DicModel item)
        {
            switch (item.Action)
            {
                case ActionEnum.Insert:
                //case ActionEnum.Set:
                //case ActionEnum.Change:
                case ActionEnum.Update:
                case ActionEnum.Where:
                case ActionEnum.And:
                case ActionEnum.Or:
                    return true;
            }
            return false;
        }
        
        internal AttributeHelper AH { get; private set; }

        internal GenericHelper GH { get; private set; }
        internal XDebug Hint { get; set; }
        internal ExpressionHandleX EH { get; private set; }

        internal StaticCache SC { get; private set; }

        internal ParameterPartHandle PPH { get; private set; }

        internal ValHandle VH { get; private set; }

        internal BatchDataHelper BDH { get; private set; }

        internal List<DicModel> Conditions { get; private set; }

        internal IDbConnection Conn { get; private set; }
        internal IDbTransaction Tran { get; set; }
        
        internal MySqlProvider SqlProvider { get; set; }

        internal Operator OP { get; set; }

        internal void AddConditions(DicModel dic)
        {
            if (!string.IsNullOrWhiteSpace(dic.CsValue)
                && dic.CsValue.Contains(",")
                && dic.Option == OptionEnum.In)
            {
                var vals = dic.CsValue.Split(',').Select(it => it);
                var i = 0;
                foreach (var val in vals)
                {
                    //
                    i++;
                    var op = OptionEnum.None;
                    if (i == 1)
                    {
                        op = OptionEnum.In;
                    }
                    else
                    {
                        op = OptionEnum.InHelper;
                    }

                    //
                    var dicx = new DicModel
                    {
                        TableOne = dic.TableOne,
                        ClassFullName = dic.ClassFullName,
                        ColumnOne = dic.ColumnOne,
                        TableAliasOne = dic.TableAliasOne,
                        TableTwo = dic.TableTwo,
                        KeyTwo = dic.KeyTwo,
                        AliasTwo = dic.AliasTwo,
                        Param = dic.Param,
                        ParamRaw = dic.ParamRaw,
                        CsValue = val,
                        ValueType = dic.ValueType,
                        ColumnType = dic.ColumnType,
                        Option = op,
                        Action = dic.Action,
                        Crud = dic.Crud,
                        Compare = dic.Compare,
                        TvpIndex = dic.TvpIndex
                    };
                    AddConditions(dicx);
                }
                Conditions.Remove(dic);
            }
            else if (!string.IsNullOrWhiteSpace(dic.Param)
                && Conditions.Any(it => dic.Param.Equals(it.Param, StringComparison.OrdinalIgnoreCase)))
            {
                if (dic.Param.Contains("__"))
                {
                    var arr = dic.Param.Split(new string[] { "__" }, StringSplitOptions.RemoveEmptyEntries);
                    var val = Convert.ToInt32(arr[arr.Length - 1]);
                    val++;
                    dic.Param = dic.ParamRaw + "__" + val.ToString();
                }
                else
                {
                    dic.Param += "__1";
                }
                AddConditions(dic);
            }
            else
            {
                Conditions.Add(dic);
            }
        }

        internal void ResetConditions()
        {
            Conditions = new List<DicModel>();
        }

        //internal string TableAttributeName(Type mType)
        //{
        //    var tableName = string.Empty;
        //    tableName = AH.GetAttributePropVal<TableAttribute>(mType, a => a.Name);
        //    if (string.IsNullOrWhiteSpace(tableName))
        //    {
        //        throw new Exception("DB Entity 缺少 TableAttribute 指定的表名!");
        //    }
        //    return tableName;
        //}

        internal void SetMTCache<M>()
        {
            //
            var type = typeof(M);
            var key = SC.GetKey(type.FullName, Conn.Database);

            //
            var table = SqlProvider.GetTableName(type);
            SC.SetModelTableName(key, table);
            SC.SetModelType(key, type);
            SC.SetModelProperys(type, this);
            (SC.SetModelColumnInfos(key, this)).GetAwaiter().GetResult();
        }

        private void SetInsertValue<M>(M m, OptionEnum option, int index)
        {
            var key = SC.GetKey(m.GetType().FullName, Conn.Database);
            var props = SC.GetModelProperys(key);
            var columns = SC.GetColumnInfos(key);

            foreach (var prop in props)
            {
                var val = GH.GetTypeValue(prop.PropertyType, prop, m);
                AddConditions(new DicModel
                {
                    ColumnOne = prop.Name,
                    Param = prop.Name,
                    ParamRaw = prop.Name,
                    CsValue = val,
                    ValueType = prop.PropertyType,
                    ColumnType = columns.First(it => it.ColumnName.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)).DataType,
                    Action = ActionEnum.Insert,
                    Option = option,
                    TvpIndex = index
                });
            }
        }
        internal void GetProperties<M>(M m)
        {
            SetInsertValue(m, OptionEnum.Insert, 0);
        }
        internal void GetProperties<M>(IEnumerable<M> mList)
        {
            var i = 0;
            foreach (var m in mList)
            {
                SetInsertValue(m, OptionEnum.InsertTVP, i);
                i++;
            }
        }

        internal DynamicParameters GetParameters()
        {
            var paras = new DynamicParameters();

            //
            foreach (var item in Conditions)
            {
                if (IsParameter(item))
                {
                    if (item.ValueType == typeof(bool)
                        || item.ValueType == typeof(bool?))
                    {
                        paras.Add(PPH.BoolParamHandle(item));
                    }
                    else if (item.ValueType == typeof(short)
                             || item.ValueType == typeof(short?))
                    {
                        item.DbValue = item.CsValue.ToShort().ToString();
                        paras.Add(item.Param, item.CsValue.ToShort(), DbType.Int16);
                    }
                    else if (item.ValueType == typeof(int)
                             || item.ValueType == typeof(int?))
                    {
                        item.DbValue = item.CsValue.ToInt().ToString();
                        paras.Add(item.Param, item.CsValue.ToInt(), DbType.Int32);
                    }
                    else if (item.ValueType == typeof(long)
                             || item.ValueType == typeof(long?))
                    {
                        item.DbValue = item.CsValue.ToLong().ToString();
                        paras.Add(item.Param, item.CsValue.ToLong(), DbType.Int64);
                    }
                    else
                    {
                        item.DbValue = item.CsValue;
                        paras.Add(item.Param, item.CsValue);
                    }
                }
            }

            //
            return paras;
        }

    }
}
