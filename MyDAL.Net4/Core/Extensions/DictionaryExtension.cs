using System.Collections.Generic;

namespace MyDAL.Core.Extensions
{
    internal static class DictionaryExtension
    {

        /// <summary>
        /// 不存在，添加；存在，替换
        /// </summary>
        public static Dictionary<K, V> AddOrReplace<K, V>(this Dictionary<K, V> dic, K key, V value)
        {
            dic[key] = value;
            return dic;
        }
    }
}
