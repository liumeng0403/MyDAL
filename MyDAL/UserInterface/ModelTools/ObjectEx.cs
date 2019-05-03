using HPC.DAL.Core;
using HPC.DAL.Core.Common;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace HPC.DAL.ModelTools
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static class ObjectEx
    {
        /// <summary>
        /// 请参阅: <see langword="引用类型对象 .DeepClone() 深度克隆[深度复制] 工具 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static T DeepClone<T>(this T obj)
            where T : class
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
                throw XConfig.EC.Exception(XConfig.EC._079, $"不支持类型【{typeof(T).FullName}】的深度复制!!! InnerException:{ex.Message}");
            }
            return result;
        }

    }
}
