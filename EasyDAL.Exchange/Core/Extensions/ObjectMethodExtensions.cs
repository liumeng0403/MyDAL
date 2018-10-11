using MyDAL.Core.Common;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyDAL.Core.Extensions
{
    internal static class ObjectMethodExtensions
    {

        /// <summary>
        ///  obj --> datetime -->str
        /// </summary>
        internal static string ToDatetimeStr(this object obj)
        {
            var result = string.Empty;
            try
            {
                result = Convert.ToDateTime(obj).ToString("yyyy-MM-dd HH:mm:ss.ffffff");
            }
            catch (Exception ex)
            {
                throw new Exception("string ToDatetimeStr(this object obj)", ex);
            }
            return result;
        }

        internal static bool ToBool(this object obj)
        {
            var result = false;
            try
            {
                result = Convert.ToBoolean(obj);
            }
            catch (Exception ex)
            {
                throw new Exception("bool ToBool(this object obj) -- error", ex);
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

    }
}
