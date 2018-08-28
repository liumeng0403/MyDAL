using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EasyDAL.Exchange.Cache
{
    /// <summary>
    /// 一个小型缓存; 
    /// 适用于条目个数可控的情况,
    /// 只可以追加; 
    /// 其内的值不能改变. 
    /// All key matches are on **REFERENCE** equality. 
    /// 该类线程安全.
    /// </summary>
    /// <typeparam name="TKey">The type to cache.</typeparam>
    /// <typeparam name="TValue">The value type of the cache.</typeparam>
    internal class Link<TKey, TValue>
        where TKey : class
    {
        public static bool TryGet(Link<TKey, TValue> link, TKey key, out TValue value)
        {
            while (link != null)
            {
                if ((object)key == (object)link.Key)
                {
                    value = link.Value;
                    return true;
                }
                link = link.Tail;
            }
            value = default(TValue);
            return false;
        }

        public static bool TryAdd(ref Link<TKey, TValue> head, TKey key, ref TValue value)
        {
            bool tryAgain;
            do
            {
                var snapshot = Interlocked.CompareExchange(ref head, null, null);
                if (TryGet(snapshot, key, out TValue found))
                {
                    // existing match; report the existing value instead
                    value = found;
                    return false;
                }
                var newNode = new Link<TKey, TValue>(key, value, snapshot);
                // did somebody move our cheese?
                tryAgain = Interlocked.CompareExchange(ref head, newNode, snapshot) != snapshot;
            } while (tryAgain);
            return true;
        }

        private Link(TKey key, TValue value, Link<TKey, TValue> tail)
        {
            Key = key;
            Value = value;
            Tail = tail;
        }

        public TKey Key { get; }
        public TValue Value { get; }
        public Link<TKey, TValue> Tail { get; }
    }
}
