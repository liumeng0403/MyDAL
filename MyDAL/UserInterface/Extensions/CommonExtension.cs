using MyDAL.Core.Common;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyDAL
{
    public static class CommonExtension
    {

        /// <summary>
        /// 深度复制 (值类型/包装类型/引用类型/序列化/非序列化/标识序列化/非标识序列化,皆可深度复制)
        /// </summary>
        public static T DeepClone<T>(this T obj)
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

        /// <summary>
        /// Is null/empty/whitespace ?
        /// </summary>
        public static bool IsNullStr(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /************************************************************************************************************************************************/

    }
}
