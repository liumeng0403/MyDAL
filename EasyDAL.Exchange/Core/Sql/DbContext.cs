
using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Enums;
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

        private static ConcurrentDictionary<Type, List<PropertyInfo>> ModelPropertiesCache { get; } = new ConcurrentDictionary<Type, List<PropertyInfo>>();
        private static ConcurrentDictionary<string, List<ColumnInfo>> TableColumnsCache { get; } = new ConcurrentDictionary<string, List<ColumnInfo>>();

        private string GetTCKey<M>()
        {
            var key = string.Empty;
            key += Conn.Database;
            SqlProvider.TryGetTableName<M>(out var tableName);
            key += tableName;
            return key;
        }

        internal AttributeHelper AH { get; private set; }

        internal GenericHelper GH { get; private set; }
        internal Hints Hint { get; set; }
        internal ExpressionHelper EH { get; private set; }

        internal List<DicModel> Conditions { get; private set; }

        internal IDbConnection Conn { get; private set; }

        internal MySqlProvider SqlProvider { get; set; }

        internal void AddConditions(DicModel dic)
        {
            if (!string.IsNullOrWhiteSpace(dic.Param)
                && Conditions.Any(it => it.Param.Equals(dic.Param, StringComparison.OrdinalIgnoreCase)))
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

        private async Task SetInsertValue<M>(M m,OptionEnum option,int index)
        {
            var props = default(List<PropertyInfo>);
            if (!ModelPropertiesCache.TryGetValue(m.GetType(), out props))
            {
                props = GH.GetPropertyInfos(m);
                ModelPropertiesCache[m.GetType()] = props;
            }
            var tcKey = GetTCKey<M>();
            var columns = default(List<ColumnInfo>);
            if (!TableColumnsCache.TryGetValue(tcKey,out columns))
            {
                columns = await SqlProvider.GetColumnsInfos<M>();
                TableColumnsCache[tcKey] = columns;
            }

            foreach (var prop in props)
            {
                var val = GH.GetTypeValue(prop.PropertyType, prop, m);
                var valType = ValueTypeEnum.None;
                if(prop.PropertyType==typeof(bool))
                {
                    valType = ValueTypeEnum.Bool;
                }
                else
                {
                    valType = ValueTypeEnum.None;
                }
                AddConditions(new DicModel
                {
                    KeyOne = prop.Name,
                    Param = prop.Name,
                    ParamRaw = prop.Name,
                    Value = val,
                    ValueType = valType,
                    ColumnType = columns.Where(it => it.ColumnName.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)).First().DataType,
                    Action = ActionEnum.Insert,
                    Option = option,
                    TvpIndex = index
                });
            }
        }
        internal async Task GetProperties<M>(M m)
        {
            await SetInsertValue(m, OptionEnum.Insert,0);
        }
        internal async Task GetProperties<M>(IEnumerable<M> mList)
        {
            var i = 0;
            foreach (var m in mList)
            {
                await SetInsertValue(m, OptionEnum.InsertTVP,i);
                i++;
            }
        }

        internal DbContext(IDbConnection conn)
        {
            Conn = conn;
            Conditions = new List<DicModel>();
            AH = AttributeHelper.Instance;
            GH = GenericHelper.Instance;
            EH = ExpressionHelper.Instance;
            SqlProvider = new MySqlProvider(this);
        }

    }
}
