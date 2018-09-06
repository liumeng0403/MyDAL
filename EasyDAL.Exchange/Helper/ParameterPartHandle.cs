using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Extensions;
using EasyDAL.Exchange.X;
using System;
using System.Data;

namespace EasyDAL.Exchange.Helper
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
                    return GetDefault(item.Param, 1, DbType.UInt16);
                }
                else
                {
                    return GetDefault(item.Param, 0, DbType.UInt16);
                }
            }
            else
            {
                return GetDefault(item.Param, item.Value.ToBool(), DbType.Boolean);
            }
        }

    }
}
