using System;
using System.Data;
using Yunyong.DataExchange.Common;
using Yunyong.DataExchange.Extensions;
using Yunyong.DataExchange.Others;

namespace Yunyong.DataExchange.Helper
{
    internal class ParameterPartHandle : ClassInstance<ParameterPartHandle>
    {
        private static ParamInfo GetDefault(string name, object value = null, DbType? dbType = null)
        {
            return new ParamInfo
            {
                Name = name,
                Value = value,
                ParameterDirection = ParameterDirection.Input,
                DbType = dbType,
                Size = null,
                Precision = null,
                Scale = null
            };
        }

        public ParamInfo BoolParamHandle(DicModel item)
        {
            if (!string.IsNullOrWhiteSpace(item.ColumnType)
                && item.ColumnType.Equals("bit", StringComparison.OrdinalIgnoreCase))
            {
                if (item.CsValue.ToBool())
                {
                    item.DbValue = 1.ToString();
                    return GetDefault(item.Param, 1, DbType.UInt16);
                }
                else
                {
                    item.DbValue = 0.ToString();
                    return GetDefault(item.Param, 0, DbType.UInt16);
                }
            }
            else
            {
                item.DbValue = item.CsValue.ToBool().ToString();
                return GetDefault(item.Param, item.CsValue.ToBool(), DbType.Boolean);
            }
        }

        public ParamInfo EnumParamHandle(DicModel item)
        {
            if (!string.IsNullOrWhiteSpace(item.ColumnType)
                && item.ColumnType.Equals("int", StringComparison.OrdinalIgnoreCase))
            {
                var val = (int)(Enum.Parse(item.ValueType, item.CsValue, true));
                item.DbValue=val.ToString();
                return GetDefault(item.Param, val, DbType.Int32);
            }
            else
            {
                item.DbValue = item.CsValue;
                return GetDefault(item.Param, item.CsValue.ToBool(), DbType.Boolean);
            }
        }

    }
}
