using MyDAL.AdoNet;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.ModelTools;
using System;
using System.Data;
using System.Linq;

namespace MyDAL.Core.Helper
{
    internal class ParameterHelper
    {

        private Context DC { get; set; }
        private static object DbNull(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }

            return value;
        }
        internal ParameterHelper(Context dc)
        {
            DC = dc;
        }

        /**********************************************************************************************************/

        private static DbType GetType(string colType, Type realType)
        {
            if (realType == XConfig.TC.Int)
            {
                if (colType.IsNullStr())
                {
                    return DbType.Int32;
                }
                else if (colType.Equals("int", StringComparison.OrdinalIgnoreCase))
                {
                    return DbType.Int32;
                }
                else
                {
                    return DbType.Int32;
                }
            }
            else if (realType == XConfig.TC.Long)
            {
                if (colType.IsNullStr())
                {
                    return DbType.Int64;
                }
                else if (colType.Equals("bigint", StringComparison.OrdinalIgnoreCase))
                {
                    return DbType.Int64;
                }
                else
                {
                    return DbType.Int64;
                }
            }
            else if (realType == XConfig.TC.Decimal)
            {
                if (colType.IsNullStr())
                {
                    return DbType.Decimal;
                }
                else if (colType.Equals("decimal", StringComparison.OrdinalIgnoreCase))
                {
                    return DbType.Decimal;
                }
                else
                {
                    return DbType.Decimal;
                }
            }
            else if (realType == XConfig.TC.Bool)
            {
                if (colType.IsNullStr())
                {
                    return DbType.UInt16;
                }
                else if (colType.Equals("bit", StringComparison.OrdinalIgnoreCase))
                {
                    return DbType.UInt16;
                }
                else
                {
                    return DbType.Boolean;
                }
            }
            else if (realType == XConfig.TC.String)
            {
                if (colType.IsNullStr())
                {
                    return DbType.String;
                }
                else if (colType.Equals("longtext", StringComparison.OrdinalIgnoreCase))
                {
                    return DbType.String;
                }
                else
                {
                    return DbType.AnsiString;
                }
            }
            else if (realType == XConfig.TC.DateTime)
            {
                if (colType.IsNullStr())
                {
                    return DbType.AnsiString;
                }
                else if (colType.Equals("datetime", StringComparison.OrdinalIgnoreCase))
                {
                    return DbType.AnsiString;
                }
                else
                {
                    return DbType.DateTime2;
                }
            }
            else if (realType == XConfig.TC.Guid)
            {
                if (colType.IsNullStr())
                {
                    return DbType.AnsiStringFixedLength;
                }
                else if (colType.Equals("char", StringComparison.OrdinalIgnoreCase))
                {
                    return DbType.AnsiStringFixedLength;
                }
                else
                {
                    return DbType.Guid;
                }
            }
            else if (realType.IsEnum)
            {
                return GetType(colType, Enum.GetUnderlyingType(realType));
            }
            else if (realType == XConfig.TC.Byte)
            {
                if (colType.IsNullStr())
                {
                    return DbType.Byte;
                }
                else if (colType.Equals("tinyint", StringComparison.OrdinalIgnoreCase))
                {
                    return DbType.Byte;
                }
                else
                {
                    return DbType.Byte;
                }
            }
            else if (realType == XConfig.TC.ByteArray)
            {
                return DbType.Binary;
            }
            else if (realType == XConfig.TC.Char)
            {
                if (colType.IsNullStr())
                {
                    return DbType.AnsiString;
                }
                else if (colType.Equals("varchar", StringComparison.OrdinalIgnoreCase))
                {
                    return DbType.AnsiString;
                }
                else
                {
                    return DbType.AnsiString;
                }
            }
            else if (realType == XConfig.TC.Double)
            {
                if (colType.IsNullStr())
                {
                    return DbType.Double;
                }
                else if (colType.Equals("double", StringComparison.OrdinalIgnoreCase))
                {
                    return DbType.Double;
                }
                else
                {
                    return DbType.Double;
                }
            }
            else if (realType == XConfig.TC.Float)
            {
                if (colType.IsNullStr())
                {
                    return DbType.Single;
                }
                else if (colType.Equals("float", StringComparison.OrdinalIgnoreCase))
                {
                    return DbType.Single;
                }
                else
                {
                    return DbType.Single;
                }
            }
            else if (realType == XConfig.TC.Sbyte)
            {
                if (colType.IsNullStr())
                {
                    return DbType.Int16;
                }
                else if (colType.Equals("tinyint", StringComparison.OrdinalIgnoreCase))
                {
                    return DbType.Int16;
                }
                else
                {
                    return DbType.SByte;
                }
            }
            else if (realType == XConfig.TC.Short)
            {
                if (colType.IsNullStr())
                {
                    return DbType.Int16;
                }
                else if (colType.Equals("smallint", StringComparison.OrdinalIgnoreCase))
                {
                    return DbType.Int16;
                }
                else
                {
                    return DbType.Int16;
                }
            }
            else if (realType == XConfig.TC.Uint)
            {
                if (colType.IsNullStr())
                {
                    return DbType.UInt32;
                }
                else if (colType.Equals("int", StringComparison.OrdinalIgnoreCase))
                {
                    return DbType.UInt32;
                }
                else
                {
                    return DbType.UInt32;
                }
            }
            else if (realType == XConfig.TC.Ulong)
            {
                if (colType.IsNullStr())
                {
                    return DbType.UInt64;
                }
                else if (colType.Equals("bigint", StringComparison.OrdinalIgnoreCase))
                {
                    return DbType.UInt64;
                }
                else
                {
                    return DbType.UInt64;
                }
            }
            else if (realType == XConfig.TC.Ushort)
            {
                if (colType.IsNullStr())
                {
                    return DbType.UInt16;
                }
                else if (colType.Equals("smallint", StringComparison.OrdinalIgnoreCase))
                {
                    return DbType.UInt16;
                }
                else
                {
                    return DbType.UInt16;
                }
            }
            else if (realType == XConfig.TC.TimeSpan)
            {
                if (colType.IsNullStr())
                {
                    return DbType.Time;
                }
                else if (colType.Equals("time", StringComparison.OrdinalIgnoreCase))
                {
                    return DbType.Time;
                }
                else
                {
                    return DbType.Time;
                }
            }
            else if (realType == XConfig.TC.DateTimeOffset)
            {
                return DbType.DateTimeOffset;
            }
            else if (realType == XConfig.TC.Object)
            {
                return DbType.Object;
            }
            else if (realType.FullName == XConfig.TC.LinqBinary)
            {
                return DbType.Binary;
            }
            else if (XConfig.TC.IEnumerableT.IsAssignableFrom(realType))
            {
                return (DbType)(-1);
            }
            else
            {
                throw new Exception($"[[DbType GetType(ui.ColumnTypeType realType)]]不支持的字段参数类型:[[{realType}]]!");
            }
        }
        private static ParamInfo BoolParam(DicParam ui, Type realType)
        {
            var type = GetType(ui.ColumnType, realType);
            var val = default(object);

            //
            if (type == DbType.UInt16)
            {
                if (ui.CsValue.ToBool())
                {
                    val = 1;
                }
                else
                {
                    val = 0;
                }
            }
            else if (type == DbType.Boolean)
            {
                val = ui.CsValue.ToBool();
            }
            else
            {
                throw new Exception($"[[ParamInfo BoolParamHandle(ui.ColumnTypeDicModelUI item, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private static ParamInfo ByteParam(DicParam ui, Type realType)
        {
            var type = GetType(ui.ColumnType, realType);
            var val = default(object);

            //
            if (type == DbType.Byte)
            {
                val = ui.CsValue.ToByte();
            }
            else
            {
                throw new Exception($"[[ParamInfo ByteParam(DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private static ParamInfo CharParam(DicParam ui, Type realType)
        {
            var type = GetType(ui.ColumnType, realType);
            var val = default(object);

            //
            if (type == DbType.AnsiString)
            {
                val = ui.CsValue?.ToString();
            }
            else
            {
                throw new Exception($"[[ParamInfo CharParam(DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private static ParamInfo DecimalParam(DicParam ui, Type realType)
        {
            var type = GetType(ui.ColumnType, realType);
            var val = default(object);

            //
            if (type == DbType.Decimal)
            {
                val = ui.CsValue.ToDecimal();
            }
            else
            {
                throw new Exception($"[[ParamInfo DecimalParam(ui.ColumnTypeDicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private static ParamInfo DoubleParam(DicParam ui, Type realType)
        {
            var type = GetType(ui.ColumnType, realType);
            var val = default(object);

            //
            if (type == DbType.Double)
            {
                val = ui.CsValue.ToDouble();
            }
            else
            {
                throw new Exception($"[[ParamInfo DoubleParam(ui.ColumnTypeDicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private static ParamInfo FloatParam(DicParam ui, Type realType)
        {
            var type = GetType(ui.ColumnType, realType);
            var val = default(object);

            //
            if (type == DbType.Single)
            {
                val = ui.CsValue.ToFloat();
            }
            else
            {
                throw new Exception($"[[ParamInfo FloatParam(ui.ColumnTypeDicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private static ParamInfo IntParam(DicParam ui, Type realType)
        {
            var type = GetType(ui.ColumnType, realType);
            var val = default(object);

            //
            if (type == DbType.Int32)
            {
                val = ui.CsValue.ToInt();
            }
            else
            {
                throw new Exception($"[[ParamInfo IntParam(ui.ColumnTypeDicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private static ParamInfo LongParam(DicParam ui, Type realType)
        {
            var type = GetType(ui.ColumnType, realType);
            var val = default(object);

            //
            if (type == DbType.Int64)
            {
                val = ui.CsValue.ToLong();
            }
            else
            {
                throw new Exception($"[[ParamInfo LongParam(ui.ColumnTypeDicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private static ParamInfo SbyteParam(DicParam ui, Type realType)
        {
            var type = GetType(ui.ColumnType, realType);
            var val = default(object);

            //
            if (type == DbType.Int16)
            {
                val = ui.CsValue.ToShort();
            }
            else if (type == DbType.SByte)
            {
                val = ui.CsValue.ToSbtye();
            }
            else
            {
                throw new Exception($"[[ParamInfo SbyteParam(ui.ColumnTypeDicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private static ParamInfo ShortParam(DicParam ui, Type realType)
        {
            var type = GetType(ui.ColumnType, realType);
            var val = default(object);

            //
            if (type == DbType.Int16)
            {
                val = ui.CsValue.ToShort();
            }
            else
            {
                throw new Exception($"[[ParamInfo ShortParam(ui.ColumnTypeDicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private static ParamInfo UintParam(DicParam ui, Type realType)
        {
            var type = GetType(ui.ColumnType, realType);
            var val = default(object);

            //
            if (type == DbType.UInt32)
            {
                val = ui.CsValue.ToUint();
            }
            else
            {
                throw new Exception($"[[ParamInfo UintParam(ui.ColumnTypeDicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private static ParamInfo UlongParam(DicParam ui, Type realType)
        {
            var type = GetType(ui.ColumnType, realType);
            var val = default(object);

            //
            if (type == DbType.UInt64)
            {
                val = ui.CsValue.ToUlong();
            }
            else
            {
                throw new Exception($"[[ParamInfo UlongParam(ui.ColumnTypeDicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private static ParamInfo UshortParam(DicParam ui, Type realType)
        {
            var type = GetType(ui.ColumnType, realType);
            var val = default(object);

            //
            if (type == DbType.UInt16)
            {
                val = ui.CsValue.ToUshort();
            }
            else
            {
                throw new Exception($"[[ParamInfo UshortParam(ui.ColumnTypeDicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private static ParamInfo StringParam(DicParam ui, Type realType)
        {
            var type = GetType(ui.ColumnType, realType);
            var val = default(object);

            //
            if (type == DbType.String)
            {
                val = ui.CsValue?.ToString();
            }
            else if (type == DbType.AnsiString)
            {
                val = ui.CsValue?.ToString();
            }
            else
            {
                throw new Exception($"[[ParamInfo StringParam(ui.ColumnTypeDicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private static ParamInfo DateTimeParam(DicParam ui, Type realType)
        {
            var type = GetType(ui.ColumnType, realType);
            var val = default(object);

            //
            var flag = ((!ui.CsValueStr.IsNullStr()) && ui.Format.IsNullStr());
            if (type == DbType.AnsiString)
            {
                if (flag)
                {
                    val = ui.CsValueStr;
                }
                else
                {
                    val = ui.CsValue;
                }
            }
            else if (type == DbType.DateTime2)
            {
                if (flag)
                {
                    val = ui.CsValueStr.ToDateTime();
                }
                else
                {
                    val = ui.CsValue.ToDateTime();
                }
            }
            else
            {
                throw new Exception($"[[ParamInfo DateTimeParam(ui.ColumnTypeDicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private static ParamInfo TimeSpanParam(DicParam ui, Type realType)
        {
            var type = GetType(ui.ColumnType, realType);
            var val = default(object);

            //
            if (type == DbType.Time)
            {
                val = ui.CsValueStr.ToDateTime();
            }
            else
            {
                throw new Exception($"[[ParamInfo TimeSpanParam(ui.ColumnTypeDicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private static ParamInfo GuidParam(DicParam ui, Type realType)
        {
            var type = GetType(ui.ColumnType, realType);
            var val = default(object);

            //
            if (type == DbType.AnsiStringFixedLength)
            {
                val = ui.CsValue?.ToString();
            }
            else if (type == DbType.Guid)
            {
                val = ui.CsValue.ToGuid();
            }
            else
            {
                throw new Exception($"[[ParamInfo GuidParam(ui.ColumnTypeDicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private static ParamInfo EnumParam(DicParam ui, Type realType)
        {
            var type = GetType(ui.ColumnType, realType);
            var val = default(object);

            //
            if (type == DbType.Int32)
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
                    val = (int)(ui.CsValue);
                }
            }
            else
            {
                throw new Exception($"[[ParamInfo EnumParam(ui.ColumnTypeDicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private static ParamInfo NullParam(DicParam ui, Type realType)
        {
            var type = GetType(ui.ColumnType, realType);
            return GetDefault(ui.Param, null, type);
        }

        /*************************************************************************************************************************************/

        internal static ParamInfo GetDefault(string paraName, object dbVal, DbType dbType)
        {
            return new ParamInfo
            {
                Name = paraName,
                Value = DbNull(dbVal),
                Type = dbType,
                ParameterDirection = ParameterDirection.Input,
                Size = null,
                Precision = null,
                Scale = null
            };
        }
        internal void GetDbVal(DicParam ui, Type realType)
        {

            //
            if (DC.IsParameter(ui.Action))
            {
                //
                if (ui.Option != OptionEnum.OneEqualOne)
                {
                    var tbm = DC.XC.GetTableModel(ui.TbMType);
                    var col = tbm.TbCols.FirstOrDefault(it => it.ColumnName.Equals(ui.TbCol, StringComparison.OrdinalIgnoreCase));
                    if (col != null)
                    {
                        ui.ColumnType = col.DataType;
                    }
                }

                //
                var para = default(ParamInfo);
                if (realType == XConfig.TC.Int)
                {
                    para = IntParam(ui, realType);
                }
                else if (realType == XConfig.TC.Long)
                {
                    para = LongParam(ui, realType);
                }
                else if (realType == XConfig.TC.Decimal)
                {
                    para = DecimalParam(ui, realType);
                }
                else if (realType == XConfig.TC.Bool)
                {
                    para = BoolParam(ui, realType);
                }
                else if (realType == XConfig.TC.String)
                {
                    para = StringParam(ui, realType);
                }
                else if (realType == XConfig.TC.DateTime)
                {
                    para = DateTimeParam(ui, realType);
                }
                else if (realType == XConfig.TC.Guid)
                {
                    para = GuidParam(ui, realType);
                }
                else if (realType.IsEnum)
                {
                    para = EnumParam(ui, realType);
                }
                else if (realType.IsNullable())
                {
                    var type = Nullable.GetUnderlyingType(realType);
                    if (ui.CsValue == null)
                    {
                        para = NullParam(ui, type);
                    }
                    else
                    {
                        GetDbVal(ui, type);
                        return;
                    }
                }
                else if (realType == XConfig.TC.Byte)
                {
                    para = ByteParam(ui, realType);
                }
                else if (realType == XConfig.TC.Char)
                {
                    para = CharParam(ui, realType);
                }
                else if (realType == XConfig.TC.Double)
                {
                    para = DoubleParam(ui, realType);
                }
                else if (realType == XConfig.TC.Float)
                {
                    para = FloatParam(ui, realType);
                }
                else if (realType == XConfig.TC.Sbyte)
                {
                    para = SbyteParam(ui, realType);
                }
                else if (realType == XConfig.TC.Short)
                {
                    para = ShortParam(ui, realType);
                }
                else if (realType == XConfig.TC.Uint)
                {
                    para = UintParam(ui, realType);
                }
                else if (realType == XConfig.TC.Ulong)
                {
                    para = UlongParam(ui, realType);
                }
                else if (realType == XConfig.TC.Ushort)
                {
                    para = UshortParam(ui, realType);
                }
                else if (realType == XConfig.TC.TimeSpan)
                {
                    para = TimeSpanParam(ui, realType);
                }
                else
                {
                    throw new Exception($"不支持的字段参数类型:[[{realType}]]!");
                }

                //
                ui.ParamInfo = para;
            }

        }

    }
}
