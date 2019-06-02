using MyDAL.AdoNet;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Extensions;
using MyDAL.DataRainbow.XCommon.Bases;
using MyDAL.ModelTools;
using MyDAL.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
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
            if (type == DbType.UInt16
                || type == DbType.UInt32
                || type == DbType.UInt64
                || type == DbType.Int16
                || type == DbType.Int32
                || type == DbType.Int64)
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
                val = ui.CsValue;
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
                val = ui.CsValue;
            }

            //
            return dc.PH.GetDefault(ui.Param, val, type);
        }
        internal ParamInfo ByteArryParam(DicParam ui, Type realType, Context dc)
        {
            var type = dc.PH.GetType(ui.ColumnType, realType, dc);
            var val = default(object);

            //
            if (type == DbType.Binary)
            {
                val = ui.CsValue;
            }
            else
            {
                val = ui.CsValue;
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
                val = ui.CsValue;
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
                val = ui.CsValue;
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
                val = ui.CsValue;
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
                val = ui.CsValue;
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
                val = ui.CsValue;
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
                val = ui.CsValue;
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
                val = ui.CsValue;
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
                val = ui.CsValue;
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
                val = ui.CsValue;
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
                val = ui.CsValue;
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
                val = ui.CsValue;
            }

            //
            return dc.PH.GetDefault(ui.Param, val, type);
        }
        internal ParamInfo StringParam(DicParam ui, Type realType, Context dc)
        {
            var type = dc.PH.GetType(ui.ColumnType, realType, dc);
            var val = default(object);

            //
            if (type == DbType.String
                || type == DbType.AnsiString)
            {
                val = ui.CsValue?.ToString();
            }
            else
            {
                val = ui.CsValue;
            }

            //
            return dc.PH.GetDefault(ui.Param, val, type);
        }
        internal ParamInfo ListStringParam(DicParam ui, Type realType, Context dc)
        {
            var type = dc.PH.GetType(ui.ColumnType, realType, dc);
            var val = default(object);

            //
            if (type == DbType.String
                && ui.CsValue is IList
                && ui.ColumnType== ParamTypeEnum.Set_MySQL)
            {
                val = string.Join(XSQL.CommaChar.ToString(), ui.CsValue as List<string>);
            }
            else
            {
                val = ui.CsValue;
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
                    val = ui.CsValue;
                    break;
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
                val = ui.CsValue;
            }
            else
            {
                val = ui.CsValue;
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
                val = ui.CsValue;
            }

            //
            return dc.PH.GetDefault(ui.Param, val, type);
        }
    }
}
