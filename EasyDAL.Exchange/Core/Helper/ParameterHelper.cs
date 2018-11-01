using MyDAL.AdoNet;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using System;
using System.Data;
using System.Linq;

namespace MyDAL.Core.Helper
{
    internal class ParameterHelper
    {

        private Context DC { get; set; }
        internal ParameterHelper(Context dc)
        {
            DC = dc;
        }

        /**********************************************************************************************************/

        internal static ParamInfo GetDefault(string paraName, object dbVal, DbType dbType)
        {
            return new ParamInfo
            {
                Name = paraName,
                Value = dbVal,
                Type = dbType,
                ParameterDirection = ParameterDirection.Input,
                Size = null,
                Precision = null,
                Scale = null
            };
        }
        private static DbType GetType(string colType, Type realType)
        {
            if (realType == XConfig.Int)
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
            else if (realType == XConfig.Long)
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
            else if (realType == XConfig.Decimal)
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
            else if (realType == XConfig.Bool)
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
            else if (realType == XConfig.String)
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
            else if (realType == XConfig.DateTime)
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
            else if (realType == XConfig.Guid)
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
            else if (realType == XConfig.Byte)
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
            else if (realType == XConfig.ByteArray)
            {
                return DbType.Binary;
            }
            else if (realType == XConfig.Char)
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
            else if (realType == XConfig.Double)
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
            else if (realType == XConfig.Float)
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
            else if (realType == XConfig.Sbyte)
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
            else if (realType == XConfig.Short)
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
            else if (realType == XConfig.Uint)
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
            else if (realType == XConfig.Ulong)
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
            else if (realType == XConfig.Ushort)
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
            else if (realType == XConfig.TimeSpan)
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
            else if (realType == XConfig.DateTimeOffset)
            {
                return DbType.DateTimeOffset;
            }
            else if (realType == XConfig.Object)
            {
                return DbType.Object;
            }
            else if(realType.FullName==XConfig.LinqBinary)
            {
                return DbType.Binary;
            }
            else if(XConfig.IEnumerableT.IsAssignableFrom(realType))
            {
                return (DbType)(-1);
            }
            else
            {
                throw new Exception($"[[DbType GetType(string colType, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }            
        }
        private ParamInfo BoolParam(string colType, DicUI ui, Type realType)
        {
            var type = GetType(colType, realType);
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
                throw new Exception($"[[ParamInfo BoolParamHandle(string colType, DicModelUI item, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private ParamInfo ByteParam(string colType, DicUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            var val = default(object);

            //
            if (type == DbType.Byte)
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
        internal ParamInfo CharParam(string colType, DicUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            var val = default(object);

            //
            if (type == DbType.AnsiString)
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
        private ParamInfo DecimalParam(string colType, DicUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            var val = default(object);

            //
            if (type == DbType.Decimal)
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
        private ParamInfo DoubleParam(string colType, DicUI ui, Type realType)
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
        private ParamInfo FloatParam(string colType, DicUI ui, Type realType)
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
        private ParamInfo IntParam(string colType, DicUI ui, Type realType)
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
        private ParamInfo LongParam(string colType, DicUI ui, Type realType)
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
        private ParamInfo SbyteParam(string colType, DicUI ui, Type realType)
        {
            var type = GetType(colType, realType);
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
                throw new Exception($"[[ParamInfo SbyteParam(string colType, DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private ParamInfo ShortParam(string colType, DicUI ui, Type realType)
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
        private ParamInfo UintParam(string colType, DicUI ui, Type realType)
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
        private ParamInfo UlongParam(string colType, DicUI ui, Type realType)
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
        private ParamInfo UshortParam(string colType, DicUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            var val = default(object);

            //
            if (type == DbType.UInt16)
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
        private ParamInfo StringParam(string colType, DicUI ui, Type realType)
        {
            var type = GetType(colType, realType);
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
                throw new Exception($"[[ParamInfo StringParam(string colType, DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private ParamInfo DateTimeParam(string colType, DicUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            var val = default(object);

            //
            if (type == DbType.AnsiString)
            {
                val = ui.CsValueStr;
            }
            else if (type == DbType.DateTime2)
            {
                val = ui.CsValueStr.ToDateTime();
            }
            else
            {
                throw new Exception($"[[ParamInfo DateTimeParam(string colType, DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private ParamInfo TimeSpanParam(string colType, DicUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            var val = default(object);

            //
            if (type == DbType.Time)
            {
                val = ui.CsValueStr.ToDateTime();
            }
            else
            {
                throw new Exception($"[[ParamInfo TimeSpanParam(string colType, DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private ParamInfo GuidParam(string colType, DicUI ui, Type realType)
        {
            var type = GetType(colType, realType);
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
                throw new Exception($"[[ParamInfo GuidParam(string colType, DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private ParamInfo EnumParam(string colType, DicUI ui, Type realType)
        {
            var type = GetType(colType, realType);
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
                throw new Exception($"[[ParamInfo EnumParam(string colType, DicModelUI ui, Type realType)]]不支持的字段参数类型:[[{realType}]]!");
            }

            //
            return GetDefault(ui.Param, val, type);
        }
        private ParamInfo NullParam(string colType, DicUI ui, Type realType)
        {
            var type = GetType(colType, realType);
            return GetDefault(ui.Param, null, type);
        }

        /*************************************************************************************************************************************/

        internal void GetDbVal(DicUI ui, DicDB db, Type realType)
        {

            //
            if (DC.IsParameter(ui.Action))
            {
                //
                if (ui.Option != OptionEnum.OneEqualOne)
                {
                    var columns = DC.SC.GetColumnInfos(DC.SC.GetModelKey(ui.ClassFullName));
                    var col = columns.FirstOrDefault(it => it.ColumnName.Equals(ui.ColumnOne, StringComparison.OrdinalIgnoreCase));
                    if (col != null)
                    {
                        db.ColumnType = col.DataType;
                    }
                }

                //
                var para = default(ParamInfo);
                if (realType == XConfig.Int)
                {
                    para = DC.PH.IntParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Long)
                {
                    para = DC.PH.LongParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Decimal)
                {
                    para = DC.PH.DecimalParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Bool)
                {
                    para = DC.PH.BoolParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.String)
                {
                    para = DC.PH.StringParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.DateTime)
                {
                    para = DC.PH.DateTimeParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Guid)
                {
                    para = DC.PH.GuidParam(db.ColumnType, ui, realType);
                }
                else if (realType.IsEnum)
                {
                    para = DC.PH.EnumParam(db.ColumnType, ui, realType);
                }
                else if (realType.IsNullable())
                {
                    //var type = realType.GetGenericArguments()[0];
                    var type = Nullable.GetUnderlyingType(realType);
                    if (ui.CsValue == null)
                    {
                        para = DC.PH.NullParam(db.ColumnType, ui, type);
                    }
                    else
                    {
                        GetDbVal(ui, db, type);
                        return;
                    }
                }
                else if (realType == XConfig.Byte)
                {
                    para = DC.PH.ByteParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Char)
                {
                    para = DC.PH.CharParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Double)
                {
                    para = DC.PH.DoubleParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Float)
                {
                    para = DC.PH.FloatParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Sbyte)
                {
                    para = DC.PH.SbyteParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Short)
                {
                    para = DC.PH.ShortParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Uint)
                {
                    para = DC.PH.UintParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Ulong)
                {
                    para = DC.PH.UlongParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.Ushort)
                {
                    para = DC.PH.UshortParam(db.ColumnType, ui, realType);
                }
                else if (realType == XConfig.TimeSpan)
                {
                    para = DC.PH.TimeSpanParam(db.ColumnType, ui, realType);
                }
                else
                {
                    throw new Exception($"不支持的字段参数类型:[[{realType}]]!");
                }

                //
                db.ParamInfo = para;
            }

        }

    }
}
