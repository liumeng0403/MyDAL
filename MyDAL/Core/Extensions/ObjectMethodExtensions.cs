using MyDAL.Core.Common;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyDAL.Core.Extensions
{
    internal static class ObjectMethodExtensions
    {
        /// <summary>
        /// JSON(string) ---> Object(T)
        /// </summary>
        public static T JsonDeserialize<T>(this string jsonStr)
        {
            var result = default(T);
            try
            {
                if (jsonStr.IsNullStr())
                {
                    return result;
                }

                result = JsonConvert.DeserializeObject<T>(jsonStr);
            }
            catch (Exception ex)
            {
                throw new Exception("方法:JsonDeserialize<T>出错" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Object(T) ---> JSON(string)   
        /// </summary>
        public static string JsonSerialize<T>(this T jsonObj)
            where T : class, new()
        {
            var result = string.Empty;
            try
            {
                if (jsonObj == null)
                {
                    return result;
                }

                result = JsonConvert.SerializeObject(jsonObj);
            }
            catch (Exception ex)
            {
                throw new Exception("方法:JsonSerialize<T>出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 深度复制 (值类型/包装类型/引用类型/序列化/非序列化/标识序列化/非标识序列化,皆可深度复制)
        /// </summary>
        internal static T DeepClone<T>(this T obj)
        {
            var result = default(T);
            try
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.SurrogateSelector = new SurrogateSelector();
                formatter.SurrogateSelector.ChainSelector(new NonSerialiazableTypeSurrogateSelector());
                var ms = new MemoryStream();
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                result = (T)formatter.Deserialize(ms);
            }
            catch (Exception ex)
            {
                throw new Exception("方法:T DeepClone<T>(this T obj)出错.", ex);
            }
            return result;
        }

        /*****************************************************************************************************************************************/

        internal static bool ToBool(this object obj)
        {
            var result = false;
            try
            {
                result = Convert.ToBoolean(obj);
            }
            catch (Exception ex)
            {
                throw new Exception($"bool ToBool(this object obj) -- {obj?.ToString()}", ex);
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
                throw new Exception($"byte ToByte(this object obj) -- {obj?.ToString()}", ex);
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
                throw new Exception($"decimal ToDecimal(this object obj) -- {obj?.ToString()}", ex);
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
                throw new Exception($"double ToDouble(this object obj) -- {obj?.ToString()}", ex);
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
                throw new Exception($"float ToFloat(this object obj) -- {obj?.ToString()}", ex);
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
                throw new Exception($"int ToInt(this object obj) -- {obj?.ToString()}", ex);
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
                throw new Exception($"long ToLong(this object obj) -- {obj?.ToString()}", ex);
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
                throw new Exception($"short ToShort(this object obj) -- {obj?.ToString()}", ex);
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
                throw new Exception($"sbyte ToSbtye(this object obj) -- {obj?.ToString()}", ex);
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
                throw new Exception($"uint ToUint(this object obj) -- {obj?.ToString()}", ex);
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
                throw new Exception($"ulong ToUlong(this object obj) -- {obj?.ToString()}", ex);
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
                throw new Exception($"ushort ToUshort(this object obj) -- {obj?.ToString()}", ex);
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
                throw new Exception($"DateTime ToDateTime(this object obj) -- {obj?.ToString()}", ex);
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
                    if(format.Equals("yyyy",StringComparison.OrdinalIgnoreCase))
                    {
                        return new DateTime(obj.ToInt(), 1, 1).ToString(format);
                    }
                }
                return Convert.ToDateTime(obj).ToString(format);
            }
            catch (Exception ex)
            {
                throw new Exception($"string ToDateTimeStr(this object obj) -- {obj?.ToString()}", ex);
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
                throw new Exception($"Guid ToGuid(this object obj) -- {obj?.ToString()}", ex);
            }
            return result;
        }

    }
}
