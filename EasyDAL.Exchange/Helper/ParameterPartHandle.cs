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
                if (item.Value.ToBool())
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
                item.DbValue = item.Value.ToBool().ToString();
                return GetDefault(item.Param, item.Value.ToBool(), DbType.Boolean);
            }
        }

    }
}
