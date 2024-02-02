﻿using MyDAL.Core;
using System;
using System.Collections;

namespace MyDAL.Tools
{
    public static class ObjectEx
    {

        public static bool IsNull(this object obj)
        {
            if(null == obj)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool NonNull(this object obj)
        {
            return !obj.IsNull();
        }

        public static bool ToBool(this object obj)
        {
            var result = false;
            try
            {
                //
                if(null == obj)
                {
                    return false;
                }
                else if (obj is string)
                {
                    if (obj.IsNull())
                    {
                        return false;
                    }
                    else
                    {
                        var str = ((string)obj).Trim().ToLower();
                        if ("1".Equals(str)
                            || "true".Equals(str))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else if (obj is int || obj is long)
                {
                    if (0 == (long)obj)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (obj is IEnumerable)   //  如  Microsoft.Extensions.Primitives.StringValues
                {
                    return obj.ToString().ToBool();
                }

                //
                result = Convert.ToBoolean(obj);
            }
            catch (Exception ex)
            {
                throw XConfig.EC.Exception(XConfig.EC._059, $"bool ToBool(this object obj) -- {obj?.ToString()}，InnerExeception：{ex.Message}");
            }
            return result;
        }

        public static byte ToByte(this object obj)
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

        public static decimal ToDecimal(this object obj)
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

        public static double ToDouble(this object obj)
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

        public static float ToFloat(this object obj)
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

        public static short ToShort(this object obj)
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

        public static sbyte ToSbtye(this object obj)
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

        public static uint ToUint(this object obj)
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

        public static ulong ToUlong(this object obj)
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

        public static ushort ToUshort(this object obj)
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

        public static DateTime ToDateTime(this object obj)
        {
            try
            {
                if (obj is string)
                {
                    return ((string)obj).ToDateTime();
                }
                else if (obj is IEnumerable)    //   如 Microsoft.Extensions.Primitives.StringValues
                {
                    return obj.ToString().ToDateTime();
                }
                return Convert.ToDateTime(obj);
            }
            catch (Exception ex)
            {
                throw XConfig.EC.Exception(XConfig.EC._071, $"DateTime ToDateTime(this object obj) -- {obj?.ToString()}，InnerExeception：{ex.Message}");
            }
        }
        public static DateTime? ToDateTimeNull(this object obj)
        {
            try
            {
                if (obj == null)
                {
                    return null;
                }
                else if (obj is string)
                {
                    var str = ((string)obj);
                    if (str.IsBlank())
                    {
                        return null;
                    }
                    else
                    {
                        return str.ToDateTime();
                    }
                }
                else if (obj is IEnumerable)    //   如 Microsoft.Extensions.Primitives.StringValues
                {
                    var str = obj.ToString();
                    if (str.IsBlank())
                    {
                        return null;
                    }
                    else
                    {
                        return str.ToDateTime();
                    }
                }
                return Convert.ToDateTime(obj);
            }
            catch (Exception ex)
            {
                throw XConfig.EC.Exception(XConfig.EC._124, $"DateTime ToDateTime(this object obj) -- {obj?.ToString()}，InnerExeception：{ex.Message}");
            }
        }
        public static string ToDateTimeStr(this object obj, string format = "")
        {
            try
            {
                if (format.IsBlank())
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

        public static Guid ToGuid(this object obj)
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
