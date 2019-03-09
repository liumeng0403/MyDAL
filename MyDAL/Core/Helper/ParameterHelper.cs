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
            if (realType == null)
            {
                return DbType.AnsiString;
            }
            else if (XConfig.TypeFuncs.TryGetValue(realType, out var func))
            {
                return func(dc, colType);
            }
            else if (realType.IsEnum)
            {
                return GetType(colType, Enum.GetUnderlyingType(realType), dc);
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
            if (!XConfig.MySQLTypes.TryGetValue(colType.ToLower(), out var type))
            {
                throw dc.Exception(XConfig.EC._031, colType);
            }
            return type;
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
                throw dc.Exception(XConfig.EC._034, $"不支持的字段参数类型:[[{realType}]]!");
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
                throw dc.Exception(XConfig.EC._035, $"不支持的字段参数类型:[[{realType}]]!");
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
                throw dc.Exception(XConfig.EC._036, $"不支持的字段参数类型:[[{realType}]]!");
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
                throw dc.Exception(XConfig.EC._037, $"不支持的字段参数类型:[[{realType}]]!");
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
                throw dc.Exception(XConfig.EC._038, $"不支持的字段参数类型:[[{realType}]]!");
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
                throw dc.Exception(XConfig.EC._039, $"不支持的字段参数类型:[[{realType}]]!");
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
                throw dc.Exception(XConfig.EC._040, $"不支持的字段参数类型:[[{realType}]]!");
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
                throw dc.Exception(XConfig.EC._041, $"不支持的字段参数类型:[[{realType}]]!");
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
                throw dc.Exception(XConfig.EC._042, $"不支持的字段参数类型:[[{realType}]]!");
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
                throw dc.Exception(XConfig.EC._043, $"不支持的字段参数类型:[[{realType}]]!");
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
                throw dc.Exception(XConfig.EC._044, $"不支持的字段参数类型:[[{realType}]]!");
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
                throw dc.Exception(XConfig.EC._045, $"不支持的字段参数类型:[[{realType}]]!");
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
                throw dc.Exception(XConfig.EC._046, $"不支持的字段参数类型:[[{realType}]]!");
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
                throw dc.Exception(XConfig.EC._047, $"不支持的字段参数类型:[[{realType}]]!");
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
                throw dc.Exception(XConfig.EC._048, $"不支持的字段参数类型:[[{realType}]]!");
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
                throw dc.Exception(XConfig.EC._049, $"不支持的字段参数类型:[[{realType}]]!");
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
                throw dc.Exception(XConfig.EC._050, $"不支持的字段参数类型:[[{realType}]]!");
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
                throw dc.Exception(XConfig.EC._051, $"不支持的字段参数类型:[[{realType}]]!");
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
                    ui.ColumnType = ui.ParamUI.ParamType;
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
