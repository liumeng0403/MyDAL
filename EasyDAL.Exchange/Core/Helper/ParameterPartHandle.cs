using System;
using System.Data;
using Yunyong.DataExchange.AdoNet;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Extensions;

namespace Yunyong.DataExchange.Core.Helper
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

        public ParamInfo BoolParamHandle(string colType, DicModelUI item)
        {
            if (!string.IsNullOrWhiteSpace(colType)
                && colType.Equals("bit", StringComparison.OrdinalIgnoreCase))
            {
                if (item.CsValue.ToBool())
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
                return GetDefault(item.Param, item.CsValue.ToBool(), DbType.Boolean);
            }
        }

        public ParamInfo EnumParamHandle(string colType, DicModelUI item,Type realType)
        {
            if (!string.IsNullOrWhiteSpace(colType)
                && (colType.Equals("int", StringComparison.OrdinalIgnoreCase)))
            {
                if (item.CsValue is string)
                {
                    var val = (int)(Enum.Parse(realType, item.CsValue.ToString(), true));
                    return GetDefault(item.Param, val, DbType.Int32);
                }
                else if(item.CsValue==null)
                {
                    return GetDefault(item.Param, null, DbType.Int32);
                }
                else
                {
                    var val = (int)item.CsValue;
                    return GetDefault(item.Param, val, DbType.Int32);
                }
            }
            else
            {
                return GetDefault(item.Param, item.CsValue, DbType.Int32);
            }
        }

    }
}
