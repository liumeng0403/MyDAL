
using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.Cache;
using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Extensions;
using EasyDAL.Exchange.Helper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Core.Sql
{
    internal class DbContext
    {


        internal static ConcurrentDictionary<string, List<ColumnInfo>> TableColumnsCache { get; } = new ConcurrentDictionary<string, List<ColumnInfo>>();

        internal string GetTCKey<M>()
        {
            var key = string.Empty;
            key += Conn.Database;
            SqlProvider.TryGetTableName<M>(out var tableName);
            key += tableName;
            return key;
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
        internal ExpressionHelper EH { get; private set; }

        internal StaticCache SC { get; private set; }

        internal ParameterPartHandle PPH { get; private set; }

        internal List<DicModel> Conditions { get; private set; }

        internal IDbConnection Conn { get; private set; }

        internal MySqlProvider SqlProvider { get; set; }

        internal void AddConditions(DicModel dic)
        {
            if (dic.Value.Contains(",")
                && dic.Option== OptionEnum.In)
            {
                var vals = dic.Value.Split(',').Select(it => it);
                var i = 0;
                foreach(var val in vals)
                {
                    //
                    i++;
                    var op = OptionEnum.None;
                    if(i==1)
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
                        KeyOne = dic.KeyOne,
                        AliasOne = dic.AliasOne,
                        TableTwo = dic.TableTwo,
                        KeyTwo = dic.KeyTwo,
                        AliasTwo = dic.AliasTwo,
                        Param = dic.Param,
                        ParamRaw = dic.ParamRaw,
                        Value = val,
                        ValueType = dic.ValueType,
                        ColumnType = dic.ColumnType,
                        Option = op,
                        Action = dic.Action,
                        Crud = dic.Crud,
                        FuncSupplement = dic.FuncSupplement,
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

        private async Task SetInsertValue<M>(M m, OptionEnum option, int index)
        {
            var props = SC.GetModelProperys(m.GetType());
            var tcKey = GetTCKey<M>();
            var columns = default(List<ColumnInfo>);
            if (!TableColumnsCache.TryGetValue(tcKey, out columns))
            {
                columns = await SqlProvider.GetColumnsInfos<M>();
                TableColumnsCache[tcKey] = columns;
            }

            foreach (var prop in props)
            {
                var val = GH.GetTypeValue(prop.PropertyType, prop, m);
                //var valType = ValueTypeEnum.None;
                //if(prop.PropertyType==typeof(bool))
                //{
                //    valType = ValueTypeEnum.Bool;
                //}
                //else
                //{
                //    valType = ValueTypeEnum.None;
                //}
                AddConditions(new DicModel
                {
                    KeyOne = prop.Name,
                    Param = prop.Name,
                    ParamRaw = prop.Name,
                    Value = val,
                    ValueType = prop.PropertyType,
                    ColumnType = columns.Where(it => it.ColumnName.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)).First().DataType,
                    Action = ActionEnum.Insert,
                    Option = option,
                    TvpIndex = index
                });
            }
        }
        internal async Task GetProperties<M>(M m)
        {
            await SetInsertValue(m, OptionEnum.Insert, 0);
        }
        internal async Task GetProperties<M>(IEnumerable<M> mList)
        {
            var i = 0;
            foreach (var m in mList)
            {
                await SetInsertValue(m, OptionEnum.InsertTVP, i);
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
                    if (item.ValueType == typeof(bool))
                    {
                        paras.Add(PPH.BoolParamHandle(item));
                    }
                    else if(item.ValueType==typeof(short))
                    {
                        paras.Add(item.Param, item.Value.ToShort(), DbType.Int16);
                    }
                    else if (item.ValueType==typeof(int))
                    {
                        paras.Add(item.Param, item.Value.ToInt(), DbType.Int32);
                    }
                    else if(item.ValueType==typeof(long))
                    {
                        paras.Add(item.Param, item.Value.ToLong(), DbType.Int64);
                    }
                    else
                    {
                        paras.Add(item.Param, item.Value);
                    }
                }
            }

            //
            return paras;
        }


        internal DbContext(IDbConnection conn)
        {
            Conn = conn;
            Conditions = new List<DicModel>();
            AH = AttributeHelper.Instance;
            GH = GenericHelper.Instance;
            EH = ExpressionHelper.Instance;
            SC = StaticCache.Instance;
            PPH = ParameterPartHandle.Instance;
            SqlProvider = new MySqlProvider(this);
        }

    }
}
