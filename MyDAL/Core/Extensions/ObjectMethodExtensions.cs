using HPC.DAL.Tools;
using System;

namespace HPC.DAL.Core.Extensions
{
    internal static class ObjectMethodExtensions
    {
        internal static bool ToBool(this object obj)
        {
            var result = false;
            try
            {
                result = Convert.ToBoolean(obj);
            }
            catch (Exception ex)
            {
                throw XConfig.EC.Exception(XConfig.EC._059, $"bool ToBool(this object obj) -- {obj?.ToString()}，InnerExeception：{ex.Message}");
            }
            return result;
        }

        internal static byte ToByte(this object obj)
        {
            var result = default(byte);
            try
            {
                result = Convert.ToByte(obj);
            }
            catch (Exception ex)
            {
                throw XConfig.EC.Exception(XConfig.EC._060, $"byte ToByte(this object obj) -- {obj?.ToString()}，InnerExeception：{ex.Message}");
            }
            return result;
        }

        internal static decimal ToDecimal(this object obj)
        {
            var result = default(decimal);
            try
            {
                result = Convert.ToDecimal(obj);
            }
            catch (Exception ex)
            {
                throw XConfig.EC.Exception(XConfig.EC._061, $"decimal ToDecimal(this object obj) -- {obj?.ToString()}，InnerExeception：{ex.Message}");
            }
            return result;
        }

        internal static double ToDouble(this object obj)
        {
            var result = default(double);
            try
            {
                result = Convert.ToDouble(obj);
            }
            catch (Exception ex)
            {
                throw XConfig.EC.Exception(XConfig.EC._062, $"double ToDouble(this object obj) -- {obj?.ToString()}，InnerExeception：{ex.Message}");
            }
            return result;
        }

        internal static float ToFloat(this object obj)
        {
            var result = default(float);
            try
            {
                result = Convert.ToSingle(obj);
            }
            catch (Exception ex)
            {
                throw XConfig.EC.Exception(XConfig.EC._063, $"float ToFloat(this object obj) -- {obj?.ToString()}，InnerExeception：{ex.Message}");
            }
            return result;
        }

        internal static int ToInt(this object obj)
        {
            var result = default(int);
            try
            {
                result = Convert.ToInt32(obj);
            }
            catch (Exception ex)
            {
                throw XConfig.EC.Exception(XConfig.EC._064, $"int ToInt(this object obj) -- {obj?.ToString()}，InnerExeception：{ex.Message}");
            }
            return result;
        }

        internal static long ToLong(this object obj)
        {
            var result = default(long);
            try
            {
                result = Convert.ToInt64(obj);
            }
            catch (Exception ex)
            {
                throw XConfig.EC.Exception(XConfig.EC._065, $"long ToLong(this object obj) -- {obj?.ToString()}，InnerExeception：{ex.Message}");
            }
            return result;
        }

        internal static short ToShort(this object obj)
        {
            var result = default(short);
            try
            {
                result = Convert.ToInt16(obj);
            }
            catch (Exception ex)
            {
                throw XConfig.EC.Exception(XConfig.EC._066, $"short ToShort(this object obj) -- {obj?.ToString()}，InnerExeception：{ex.Message}");
            }
            return result;
        }

        internal static sbyte ToSbtye(this object obj)
        {
            var result = default(sbyte);
            try
            {
                result = Convert.ToSByte(obj);
            }
            catch (Exception ex)
            {
                throw XConfig.EC.Exception(XConfig.EC._067, $"sbyte ToSbtye(this object obj) -- {obj?.ToString()}，InnerExeception：{ex.Message}");
            }
            return result;
        }

        internal static uint ToUint(this object obj)
        {
            var result = default(uint);
            try
            {
                result = Convert.ToUInt32(obj);
            }
            catch (Exception ex)
            {
                throw XConfig.EC.Exception(XConfig.EC._068, $"uint ToUint(this object obj) -- {obj?.ToString()}，InnerExeception：{ex.Message}");
            }
            return result;
        }

        internal static ulong ToUlong(this object obj)
        {
            var result = default(ulong);
            try
            {
                result = Convert.ToUInt64(obj);
            }
            catch (Exception ex)
            {
                throw XConfig.EC.Exception(XConfig.EC._069, $"ulong ToUlong(this object obj) -- {obj?.ToString()}，InnerExeception：{ex.Message}");
            }
            return result;
        }

        internal static ushort ToUshort(this object obj)
        {
            var result = default(ushort);
            try
            {
                result = Convert.ToUInt16(obj);
            }
            catch (Exception ex)
            {
                throw XConfig.EC.Exception(XConfig.EC._070, $"ushort ToUshort(this object obj) -- {obj?.ToString()}，InnerExeception：{ex.Message}");
            }
            return result;
        }

        internal static DateTime ToDateTime(this object obj)
        {
            var result = default(DateTime);
            try
            {
                result = Convert.ToDateTime(obj);
            }
            catch (Exception ex)
            {
                throw XConfig.EC.Exception(XConfig.EC._071, $"DateTime ToDateTime(this object obj) -- {obj?.ToString()}，InnerExeception：{ex.Message}");
            }
            return result;
        }
        internal static string ToDateTimeStr(this object obj, string format = "")
        {
            try
            {
                if (format.IsNullStr())
                {
                    format = "yyyy-MM-dd HH:mm:ss.ffffff";
                }
                else
                {
                    if (format.Equals("yyyy", StringComparison.OrdinalIgnoreCase))
                    {
                        return new DateTime(obj.ToInt(), 1, 1).ToString(format);
                    }
                }
                return Convert.ToDateTime(obj).ToString(format);
            }
            catch (Exception ex)
            {
                throw XConfig.EC.Exception(XConfig.EC._072, $"string ToDateTimeStr(this object obj) -- {obj?.ToString()}，InnerExeception：{ex.Message}");
            }
        }

        internal static Guid ToGuid(this object obj)
        {
            var result = Guid.Empty;
            try
            {
                result = Guid.Parse(obj.ToString());
            }
            catch (Exception ex)
            {
                throw XConfig.EC.Exception(XConfig.EC._073, $"Guid ToGuid(this object obj) -- {obj?.ToString()}，InnerExeception：{ex.Message}");
            }
            return result;
        }

    }
}
