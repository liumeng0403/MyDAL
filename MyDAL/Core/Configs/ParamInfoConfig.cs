using MyDAL.AdoNet;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Extensions;
using MyDAL.ModelTools;
using System;
using System.Data;

namespace MyDAL.Core.Configs
{
    internal class ParamInfoConfig
    {
        internal ParamInfo BoolParam(DicParam ui, Type realType, Context dc)
        {
            var type = dc.PH.GetType(ui.ColumnType, realType, dc);
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
            return dc.PH.GetDefault(ui.Param, val, type);
        }
        internal ParamInfo ByteParam(DicParam ui, Type realType, Context dc)
        {
            var type = dc.PH.GetType(ui.ColumnType, realType, dc);
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
            return dc.PH.GetDefault(ui.Param, val, type);
        }
        internal ParamInfo CharParam(DicParam ui, Type realType, Context dc)
        {
            var type = dc.PH.GetType(ui.ColumnType, realType, dc);
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
            return dc.PH.GetDefault(ui.Param, val, type);
        }
        internal ParamInfo DecimalParam(DicParam ui, Type realType, Context dc)
        {
            var type = dc.PH.GetType(ui.ColumnType, realType, dc);
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
            return dc.PH.GetDefault(ui.Param, val, type);
        }
        internal ParamInfo DoubleParam(DicParam ui, Type realType, Context dc)
        {
            var type = dc.PH.GetType(ui.ColumnType, realType, dc);
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
            return dc.PH.GetDefault(ui.Param, val, type);
        }
        internal ParamInfo FloatParam(DicParam ui, Type realType, Context dc)
        {
            var type = dc.PH.GetType(ui.ColumnType, realType, dc);
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
            return dc.PH.GetDefault(ui.Param, val, type);
        }
        internal ParamInfo IntParam(DicParam ui, Type realType, Context dc)
        {
            var type = dc.PH.GetType(ui.ColumnType, realType, dc);
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
            return dc.PH.GetDefault(ui.Param, val, type);
        }
        internal ParamInfo LongParam(DicParam ui, Type realType, Context dc)
        {
            var type = dc.PH.GetType(ui.ColumnType, realType, dc);
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
            return dc.PH.GetDefault(ui.Param, val, type);
        }
        internal ParamInfo SbyteParam(DicParam ui, Type realType, Context dc)
        {
            var type = dc.PH.GetType(ui.ColumnType, realType, dc);
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
            return dc.PH.GetDefault(ui.Param, val, type);
        }
        internal ParamInfo ShortParam(DicParam ui, Type realType, Context dc)
        {
            var type = dc.PH.GetType(ui.ColumnType, realType, dc);
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
            return dc.PH.GetDefault(ui.Param, val, type);
        }
        internal ParamInfo UintParam(DicParam ui, Type realType, Context dc)
        {
            var type = dc.PH.GetType(ui.ColumnType, realType, dc);
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
            return dc.PH.GetDefault(ui.Param, val, type);
        }
        internal ParamInfo UlongParam(DicParam ui, Type realType, Context dc)
        {
            var type = dc.PH.GetType(ui.ColumnType, realType, dc);
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
            return dc.PH.GetDefault(ui.Param, val, type);
        }
        internal ParamInfo UshortParam(DicParam ui, Type realType, Context dc)
        {
            var type = dc.PH.GetType(ui.ColumnType, realType, dc);
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
            return dc.PH.GetDefault(ui.Param, val, type);
        }
        internal ParamInfo StringParam(DicParam ui, Type realType, Context dc)
        {
            var type = dc.PH.GetType(ui.ColumnType, realType, dc);
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
            return dc.PH.GetDefault(ui.Param, val, type);
        }
        internal ParamInfo DateTimeParam(DicParam ui, Type realType, Context dc)
        {
            var type = dc.PH.GetType(ui.ColumnType, realType, dc);
            var val = default(object);
            var flag = ((!ui.CsValueStr.IsNullStr()) && ui.Format.IsNullStr());

            //
            switch (type)
            {
                case DbType.AnsiString:
                    if (flag)
                    {
                        val = ui.CsValueStr;
                    }
                    else
                    {
                        val = ui.CsValue;
                    }
                    break;
                case DbType.DateTime:
                case DbType.DateTime2:
                    if (flag)
                    {
                        val = ui.CsValueStr.ToDateTime();
                    }
                    else
                    {
                        val = ui.CsValue.ToDateTime();
                    }
                    break;
                default:
                    throw dc.Exception(XConfig.EC._048, $"不支持的字段参数类型:[[{realType}]]!");
            }

            return dc.PH.GetDefault(ui.Param, val, type);
        }
        internal ParamInfo TimeSpanParam(DicParam ui, Type realType, Context dc)
        {
            var type = dc.PH.GetType(ui.ColumnType, realType, dc);
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
            return dc.PH.GetDefault(ui.Param, val, type);
        }
        internal ParamInfo GuidParam(DicParam ui, Type realType, Context dc)
        {
            var type = dc.PH.GetType(ui.ColumnType, realType, dc);
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
            return dc.PH.GetDefault(ui.Param, val, type);
        }
    }
}
