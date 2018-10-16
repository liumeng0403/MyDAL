using System;
using System.Data;
using Yunyong.DataExchange.AdoNet;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Extensions;

namespace Yunyong.DataExchange.Core.Helper
{
    internal class ParameterHandle : ClassInstance<ParameterHandle>
    {
        private static ParamInfo GetDefault(string name, object value, DbType? dbType)
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
        private static DbType GetType(string colType, Type realType)
        {
            var type = default(DbType);
            if (realType == typeof(bool))
            {
                if (colType.IsNullStr())
                {
                    type = DbType.UInt16;
                }
                else if (colType.Equals("bit", StringComparison.OrdinalIgnoreCase))
                {
                    type = DbType.UInt16;
                }
                else
                {
                    type = DbType.Boolean;
                }
            }
            else if (realType == typeof(byte))
            {
                if (colType.IsNullStr())
                {
                    type = DbType.Byte;
                }
                else if (colType.Equals("tinyint", StringComparison.OrdinalIgnoreCase))
                {
                    type = DbType.Byte;
                }
                else
                {
                    type = DbType.Byte;
                }
            }
            else if (realType == typeof(char))
            {
                if (colType.IsNullStr())
                {
                    type = DbType.AnsiString;
                }
                else if(colType.Equals("varchar",StringComparison.OrdinalIgnoreCase))
                {
                    type = DbType.AnsiString;
                }
                else
                {
                    type = DbType.AnsiString;
                }
            }
            else if (realType == typeof(decimal))
            {
                if (colType.IsNullStr())
                {
                    type = DbType.Decimal;
                }
                else if (colType.Equals("decimal",StringComparison.OrdinalIgnoreCase))
                {
                    type = DbType.Decimal;
                }
                else
                {
                    type = DbType.Decimal;
                }
            }
            else if (realType == typeof(double))
            {
                if (colType.IsNullStr())
                {
                    type = DbType.Double;
                }
                else if (colType.Equals("double",StringComparison.OrdinalIgnoreCase))
                {
                    type = DbType.Double;
                }
                else
                {
                    type = DbType.Double;
                }
            }
            else if (realType == typeof(float))
            {
                if (colType.IsNullStr())
                {
                    type = DbType.Single;
                }
                else if (colType.Equals("float",StringComparison.OrdinalIgnoreCase))
                {
                    type = DbType.Single;
                }
                else
                {
                    type = DbType.Single ;
                }
            }
            else if (realType == typeof(int))
            {
                if (colType.IsNullStr())
                {
                    type = DbType.Int32;
                }
                else if (colType.Equals("int",StringComparison.OrdinalIgnoreCase))
                {
                    type = DbType.Int32;
                }
                else
                {
                    type = DbType.Int32;
                }
            }
            else if (realType == typeof(long))
            {
                if (colType.IsNullStr())
                {
                    type = DbType.Int64;
                }
                else if (colType.Equals("bigint",StringComparison.OrdinalIgnoreCase))
                {
                    type = DbType.Int64;
                }
                else
                {
                    type = DbType.Int64;
                }
            }
            else if (realType == typeof(sbyte))
            {
                if (colType.IsNullStr())
                {
                    type = DbType.Int16;
                }
                else if (colType.Equals("tinyint",StringComparison.OrdinalIgnoreCase))
                {
                    type = DbType.Int16;
                }
                else
                {
                    type = DbType.SByte;
                }
            }
            else if (realType == typeof(short))
            {
                if (colType.IsNullStr())
                {
                    type = DbType.Int16;
                }
                else if (colType.Equals("smallint",StringComparison.OrdinalIgnoreCase))
                {
                    type = DbType.Int16;
                }
                else
                {
                    type = DbType.Int16;
                }
            }
            else if (realType == typeof(uint))
            {
                if (colType.IsNullStr())
                {
                    type = DbType.UInt32;
                }
                else if (colType.Equals("int",StringComparison.OrdinalIgnoreCase))
                {
                    type = DbType.UInt32;
                }
                else
                {
                    type = DbType.UInt32;
                }
            }
            else if (realType == typeof(ulong))
            {
                if (colType.IsNullStr())
                {
                    type = DbType.UInt64;
                }
                else if (colType.Equals("bigint",StringComparison.OrdinalIgnoreCase))
                {
                    type = DbType.UInt64;
                }
                else
                {
                    type = DbType.UInt64;
                }
            }
            else if (realType == typeof(ushort))
            {
                if (colType.IsNullStr())
                {
                    type = DbType.UInt16;
                }
                else if (colType.Equals("smallint",StringComparison.OrdinalIgnoreCase))
                {
                    type = DbType.UInt16;
                }
                else
                {
                    type = DbType.UInt16;
                }
            }
            else if (realType == typeof(string))
            {
                if (colType.IsNullStr())
                {
                    type = DbType.String;
                }
                else if(colType.Equals("longtext",StringComparison.OrdinalIgnoreCase))
                {
                    type = DbType.String;
                }
                else
                {
                    type = DbType.AnsiString;
                }
            }
            else if (realType == typeof(DateTime))
            {
                if (colType.IsNullStr())
                {
                    type = DbType.DateTime2;
                }
                else if (colType.Equals("datetime",StringComparison.OrdinalIgnoreCase))
                {
                    type = DbType.DateTime2;
                }
                else
                {
                    type = DbType.DateTime;
                }
            }
            else if (realType == typeof(TimeSpan))
            {
                if (colType.IsNullStr())
                {
                    type = DbType.Time;
                }
                else if (colType.Equals("time",StringComparison.OrdinalIgnoreCase))
                {
                    type = DbType.Time;
                }
                else
                {
                    type = DbType.Time;
                }
            }
            else if (realType == typeof(Guid))
            {
                if (colType.IsNullStr())
                {
                    type = DbType.AnsiStringFixedLength;
                }
                else if (colType.Equals("char",StringComparison.OrdinalIgnoreCase))
                {
                    type = DbType.AnsiStringFixedLength;
                }
                else
                {
                    type = DbType.Guid;
                }
            }
            else if (realType.IsEnum)
            {
                if (colType.IsNullStr())
                {
                    type = DbType.Int32;
                }
                else if (colType.Equals("int",StringComparison.OrdinalIgnoreCase))
                {
                    type = DbType.Int32;
                }
                else
                {
                    type = DbType.Int32;
                }
            }
            else
            {
                throw new Exception($"[[DbType GetType(string colType, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return type;
        }

        /*************************************************************************************************************************************/

        internal ParamInfo BoolParam(string colType, DicModelUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            var val = default(object);

            //
            if (type == DbType.UInt16)
            {
                if (ui.CsValue.ToBool())
                {
                    val = 1;// return GetDefault(item.Param, 1, DbType.UInt16);
                }
                else
                {
                    val = 0; // return GetDefault(item.Param, 0, DbType.UInt16);
                }
            }
            else if (type == DbType.Boolean)
            {
                val = ui.CsValue.ToBool();  // GetDefault(item.Param, item.CsValue.ToBool(), DbType.Boolean);
            }
            else
            {
                throw new Exception($"[[ParamInfo BoolParamHandle(string colType, DicModelUI item, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }

        internal ParamInfo ByteParam(string colType, DicModelUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            var val = default(object);

            //
            if(type== DbType.Byte)
            {
                val = ui.CsValue.ToByte();
            }
            else
            {
                throw new Exception($"[[ParamInfo ByteParam(string colType, DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }

        internal ParamInfo CharParam(string colType, DicModelUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            var val = default(object);

            //
            if(type== DbType.AnsiString)
            {
                val = ui.CsValue?.ToString();
            }
            else
            {
                throw new Exception($"[[ParamInfo CharParam(string colType, DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }

        internal ParamInfo DecimalParam(string colType, DicModelUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            var val = default(object);
            
            //
            if(type == DbType.Decimal)
            {
                val = ui.CsValue.ToDecimal();
            }
            else
            {
                throw new Exception($"[[ParamInfo DecimalParam(string colType, DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }

        internal ParamInfo DoubleParam(string colType, DicModelUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            var val = default(object);

            //
            if (type == DbType.Double)
            {
                val = ui.CsValue.ToDouble();
            }
            else
            {
                throw new Exception($"[[ParamInfo DoubleParam(string colType, DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }

        internal ParamInfo FloatParam(string colType, DicModelUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            var val = default(object);

            //
            if (type == DbType.Single)
            {
                val = ui.CsValue.ToFloat();
            }
            else
            {
                throw new Exception($"[[ParamInfo FloatParam(string colType, DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }

        internal ParamInfo IntParam(string colType, DicModelUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            var val = default(object);

            //
            if (type == DbType.Int32)
            {
                val = ui.CsValue.ToInt();
            }
            else
            {
                throw new Exception($"[[ParamInfo IntParam(string colType, DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }

        internal ParamInfo LongParam(string colType, DicModelUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            var val = default(object);

            //
            if (type == DbType.Int64)
            {
                val = ui.CsValue.ToLong();
            }
            else
            {
                throw new Exception($"[[ParamInfo LongParam(string colType, DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }

        internal ParamInfo SbyteParam(string colType, DicModelUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            var val = default(object);

            //
            if (type == DbType.Int16)
            {
                val = ui.CsValue.ToShort();
            }
            else if(type == DbType.SByte)
            {
                val = ui.CsValue.ToSbtye();
            }
            else
            {
                throw new Exception($"[[ParamInfo SbyteParam(string colType, DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }

        internal ParamInfo ShortParam(string colType, DicModelUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            var val = default(object);

            //
            if (type == DbType.Int16)
            {
                val = ui.CsValue.ToShort();
            }
            else
            {
                throw new Exception($"[[ParamInfo ShortParam(string colType, DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }

        internal ParamInfo UintParam(string colType, DicModelUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            var val = default(object);

            //
            if (type == DbType.UInt32)
            {
                val = ui.CsValue.ToUint();
            }
            else
            {
                throw new Exception($"[[ParamInfo UintParam(string colType, DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }

        internal ParamInfo UlongParam(string colType, DicModelUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            var val = default(object);

            //
            if (type == DbType.UInt64)
            {
                val = ui.CsValue.ToUlong();
            }
            else
            {
                throw new Exception($"[[ParamInfo UlongParam(string colType, DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }

        internal ParamInfo UshortParam(string colType, DicModelUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            var val = default(object);

            //
            if (type ==  DbType.UInt16)
            {
                val = ui.CsValue.ToUshort();
            }
            else
            {
                throw new Exception($"[[ParamInfo UshortParam(string colType, DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }

        internal ParamInfo StringParam(string colType, DicModelUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            var val = default(object);

            //
            if (type == DbType.String)
            {
                val = ui.CsValue?.ToString();
            }
            else if(type== DbType.AnsiString)
            {
                val = ui.CsValue?.ToString();
            }
            else
            {
                throw new Exception($"[[ParamInfo StringParam(string colType, DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }

        internal ParamInfo DateTimeParam(string colType, DicModelUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            var val = default(object);

            //
            if (type == DbType.DateTime2 )
            {
                val = ui.CsValue.ToDateTime();
            }
            else if (type == DbType.DateTime)
            {
                val = ui.CsValue.ToDateTime();
            }
            else
            {
                throw new Exception($"[[ParamInfo DateTimeParam(string colType, DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }

        internal ParamInfo TimeSpanParam(string colType, DicModelUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            var val = default(object);

            //
            if (type ==DbType.Time)
            {            
                val = ui.CsValue.ToDateTime();                    
            }
            else
            {
                throw new Exception($"[[ParamInfo TimeSpanParam(string colType, DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }

        internal ParamInfo GuidParam(string colType, DicModelUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            var val = default(object);

            //
            if (type == DbType.AnsiStringFixedLength)
            {
                val = ui.CsValue?.ToString();
            }
            else if(type == DbType.Guid)
            {
                val = ui.CsValue.ToGuid();
            }
            else
            {
                throw new Exception($"[[ParamInfo GuidParam(string colType, DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }

        internal ParamInfo EnumParam(string colType, DicModelUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            var val = default(object);

            //
            if (type== DbType.Int32)
            {
                if (ui.CsValue is string)
                {
                    val = (int)(Enum.Parse(realType, ui.CsValue?.ToString(), true));
                }
                else if (ui.CsValue == null)
                {
                    val = null;
                }
                else if (ui.CsValue is int)
                {
                    val = ui.CsValue.ToInt();
                }
                else
                {
                    val = ui.CsValue;
                }
            }
            else
            {
                throw new Exception($"[[ParamInfo EnumParam(string colType, DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }

        public ParamInfo NullParam(string colType, DicModelUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            return GetDefault(ui.Param, null, type);
        }

    }
}
