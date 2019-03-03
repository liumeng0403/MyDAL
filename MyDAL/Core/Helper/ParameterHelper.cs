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

        private static DbType GetType(ParamTypeEnum colType, Type realType, Context dc)
        {
            if (realType == XConfig.TC.Int)
            {
                if (colType == ParamTypeEnum.MySQL_Int)//.Equals("int", StringComparison.OrdinalIgnoreCase))
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
                if (colType == ParamTypeEnum.MySQL_BigInt)//.Equals("bigint", StringComparison.OrdinalIgnoreCase))
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
                if (colType == ParamTypeEnum.MySQL_Decimal)//.Equals("decimal", StringComparison.OrdinalIgnoreCase))
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
                if (colType == ParamTypeEnum.MySQL_Bit)//.Equals("bit", StringComparison.OrdinalIgnoreCase))
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
                if (colType == ParamTypeEnum.MySQL_LongText)//.Equals("longtext", StringComparison.OrdinalIgnoreCase))
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
                if (colType == ParamTypeEnum.MySQL_DateTime)//.Equals("datetime", StringComparison.OrdinalIgnoreCase))
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
                if (colType == ParamTypeEnum.MySQL_Char)//.Equals("char", StringComparison.OrdinalIgnoreCase))
                {
                    return DbType.AnsiStringFixedLength;
                }
                else
                {
                    return DbType.Guid;
                }
            }
            else if (realType == null)
            {
                return DbType.AnsiString;
            }
            else if (realType.IsEnum)
            {
                return GetType(colType, Enum.GetUnderlyingType(realType), dc);
            }
            else if (realType == XConfig.TC.Byte)
            {
                if (colType == ParamTypeEnum.MySQL_TinyInt)//.Equals("tinyint", StringComparison.OrdinalIgnoreCase))
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
                if (colType == ParamTypeEnum.MySQL_VarChar)//.Equals("varchar", StringComparison.OrdinalIgnoreCase))
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
                if (colType == ParamTypeEnum.MySQL_Double)//.Equals("double", StringComparison.OrdinalIgnoreCase))
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
                if (colType == ParamTypeEnum.MySQL_Float)//.Equals("float", StringComparison.OrdinalIgnoreCase))
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
                if (colType == ParamTypeEnum.MySQL_TinyInt)//.Equals("tinyint", StringComparison.OrdinalIgnoreCase))
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
                if (colType == ParamTypeEnum.MySQL_SmallInt)//.Equals("smallint", StringComparison.OrdinalIgnoreCase))
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
                if (colType == ParamTypeEnum.MySQL_Int)//.Equals("int", StringComparison.OrdinalIgnoreCase))
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
                if (colType == ParamTypeEnum.MySQL_BigInt)//.Equals("bigint", StringComparison.OrdinalIgnoreCase))
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
                if (colType == ParamTypeEnum.MySQL_SmallInt)//.Equals("smallint", StringComparison.OrdinalIgnoreCase))
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
                if (colType == ParamTypeEnum.MySQL_Time)//.Equals("time", StringComparison.OrdinalIgnoreCase))
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
                throw dc.Exception(XConfig.EC._032, $"不支持的字段参数类型:[[{realType}]]!");
            }
        }
        private static ParamTypeEnum GetColType(string colType, Context dc)
        {
            switch (colType.ToLower())
            {
                case "tinyint":
                    return ParamTypeEnum.MySQL_TinyInt;
                case "smallint":
                    return ParamTypeEnum.MySQL_SmallInt;
                case "mediumint":
                    return ParamTypeEnum.MySQL_MediumInt;
                case "int":
                    return ParamTypeEnum.MySQL_Int;
                case "bigint":
                    return ParamTypeEnum.MySQL_BigInt;
                case "float":
                    return ParamTypeEnum.MySQL_Float;
                case "double":
                    return ParamTypeEnum.MySQL_Double;
                case "decimal":
                    return ParamTypeEnum.MySQL_Decimal;
                case "year":
                    return ParamTypeEnum.MySQL_Year;
                case "time":
                    return ParamTypeEnum.MySQL_Time;
                case "date":
                    return ParamTypeEnum.MySQL_Date;
                case "datetime":
                    return ParamTypeEnum.MySQL_DateTime;
                case "timestamp":
                    return ParamTypeEnum.MySQL_TimeStamp;
                case "char":
                    return ParamTypeEnum.MySQL_Char;
                case "varchar":
                    return ParamTypeEnum.MySQL_VarChar;
                case "tinytext":
                    return ParamTypeEnum.MySQL_TinyText;
                case "text":
                    return ParamTypeEnum.MySQL_Text;
                case "mediumtext":
                    return ParamTypeEnum.MySQL_MediumText;
                case "longtext":
                    return ParamTypeEnum.MySQL_LongText;
                case "enum":
                    return ParamTypeEnum.MySQL_Enum;
                case "set":
                    return ParamTypeEnum.MySQL_Set;
                case "bit":
                    return ParamTypeEnum.MySQL_Bit;
                case "binary":
                    return ParamTypeEnum.MySQL_Binary;
                case "varbinary":
                    return ParamTypeEnum.MySQL_VarBinary;
                case "tinyblob":
                    return ParamTypeEnum.MySQL_TinyBlob;
                case "blob":
                    return ParamTypeEnum.MySQL_Blob;
                case "mediumblob":
                    return ParamTypeEnum.MySQL_MediumBlob;
                case "longblob":
                    return ParamTypeEnum.MySQL_LongBlob;
                default:
                    throw dc.Exception(XConfig.EC._031, colType);
            }
        }
        private static ParamInfo BoolParam(DicParam ui, Type realType, Context dc)
        {
            var type = GetType(ui.ColumnType, realType, dc);
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
        private static ParamInfo ByteParam(DicParam ui, Type realType, Context dc)
        {
            var type = GetType(ui.ColumnType, realType, dc);
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
        private static ParamInfo CharParam(DicParam ui, Type realType, Context dc)
        {
            var type = GetType(ui.ColumnType, realType, dc);
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
        private static ParamInfo DecimalParam(DicParam ui, Type realType, Context dc)
        {
            var type = GetType(ui.ColumnType, realType, dc);
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
        private static ParamInfo DoubleParam(DicParam ui, Type realType, Context dc)
        {
            var type = GetType(ui.ColumnType, realType, dc);
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
        private static ParamInfo FloatParam(DicParam ui, Type realType, Context dc)
        {
            var type = GetType(ui.ColumnType, realType, dc);
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
        private static ParamInfo IntParam(DicParam ui, Type realType, Context dc)
        {
            var type = GetType(ui.ColumnType, realType, dc);
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
        private static ParamInfo LongParam(DicParam ui, Type realType, Context dc)
        {
            var type = GetType(ui.ColumnType, realType, dc);
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
        private static ParamInfo SbyteParam(DicParam ui, Type realType, Context dc)
        {
            var type = GetType(ui.ColumnType, realType, dc);
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
        private static ParamInfo ShortParam(DicParam ui, Type realType, Context dc)
        {
            var type = GetType(ui.ColumnType, realType, dc);
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
        private static ParamInfo UintParam(DicParam ui, Type realType, Context dc)
        {
            var type = GetType(ui.ColumnType, realType, dc);
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
        private static ParamInfo UlongParam(DicParam ui, Type realType, Context dc)
        {
            var type = GetType(ui.ColumnType, realType, dc);
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
        private static ParamInfo UshortParam(DicParam ui, Type realType, Context dc)
        {
            var type = GetType(ui.ColumnType, realType, dc);
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
        private static ParamInfo StringParam(DicParam ui, Type realType, Context dc)
        {
            var type = GetType(ui.ColumnType, realType, dc);
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
        private static ParamInfo DateTimeParam(DicParam ui, Type realType, Context dc)
        {
            var type = GetType(ui.ColumnType, realType, dc);
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
        private static ParamInfo TimeSpanParam(DicParam ui, Type realType, Context dc)
        {
            var type = GetType(ui.ColumnType, realType, dc);
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
        private static ParamInfo GuidParam(DicParam ui, Type realType, Context dc)
        {
            var type = GetType(ui.ColumnType, realType, dc);
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
        private static ParamInfo EnumParam(DicParam ui, Type realType, Context dc)
        {
            var type = GetType(ui.ColumnType, realType, dc);
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
        private static ParamInfo NullParam(DicParam ui, Type realType, Context dc)
        {
            var type = GetType(ui.ColumnType, realType, dc);
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
                if (ui.Option == OptionEnum.OneEqualOne)
                { }
                else if (ui.Action == ActionEnum.SQL)
                {
                    ui.ColumnType = ui.ParamUI.Type;
                }
                else
                {
                    var tbm = DC.XC.GetTableModel(ui.TbMType);
                    var col = tbm.TbCols.FirstOrDefault(it => it.ColumnName.Equals(ui.TbCol, StringComparison.OrdinalIgnoreCase));
                    if (col != null)
                    {
                        ui.ColumnType = GetColType(col.DataType, DC);
                    }
                }

                //
                var para = default(ParamInfo);
                if (realType == XConfig.TC.Int)
                {
                    para = IntParam(ui, realType, DC);
                }
                else if (realType == XConfig.TC.Long)
                {
                    para = LongParam(ui, realType, DC);
                }
                else if (realType == XConfig.TC.Decimal)
                {
                    para = DecimalParam(ui, realType, DC);
                }
                else if (realType == XConfig.TC.Bool)
                {
                    para = BoolParam(ui, realType, DC);
                }
                else if (realType == XConfig.TC.String)
                {
                    para = StringParam(ui, realType, DC);
                }
                else if (realType == XConfig.TC.DateTime)
                {
                    para = DateTimeParam(ui, realType, DC);
                }
                else if (realType == XConfig.TC.Guid)
                {
                    para = GuidParam(ui, realType, DC);
                }
                else if (realType == null
                    && ui.Action == ActionEnum.SQL)
                {
                    para = NullParam(ui, realType, DC);
                }
                else if (realType.IsEnum)
                {
                    para = EnumParam(ui, realType, DC);
                }
                else if (realType.IsNullable())
                {
                    var type = Nullable.GetUnderlyingType(realType);
                    if (ui.CsValue == null)
                    {
                        para = NullParam(ui, type, DC);
                    }
                    else
                    {
                        GetDbVal(ui, type);
                        return;
                    }
                }
                else if (realType == XConfig.TC.Byte)
                {
                    para = ByteParam(ui, realType, DC);
                }
                else if (realType == XConfig.TC.Char)
                {
                    para = CharParam(ui, realType, DC);
                }
                else if (realType == XConfig.TC.Double)
                {
                    para = DoubleParam(ui, realType, DC);
                }
                else if (realType == XConfig.TC.Float)
                {
                    para = FloatParam(ui, realType, DC);
                }
                else if (realType == XConfig.TC.Sbyte)
                {
                    para = SbyteParam(ui, realType, DC);
                }
                else if (realType == XConfig.TC.Short)
                {
                    para = ShortParam(ui, realType, DC);
                }
                else if (realType == XConfig.TC.Uint)
                {
                    para = UintParam(ui, realType, DC);
                }
                else if (realType == XConfig.TC.Ulong)
                {
                    para = UlongParam(ui, realType, DC);
                }
                else if (realType == XConfig.TC.Ushort)
                {
                    para = UshortParam(ui, realType, DC);
                }
                else if (realType == XConfig.TC.TimeSpan)
                {
                    para = TimeSpanParam(ui, realType, DC);
                }
                else
                {
                    throw DC.Exception(XConfig.EC._033, $"不支持的字段参数类型:[[{realType}]]!");
                }

                //
                ui.ParamInfo = para;
            }

        }

    }
}
