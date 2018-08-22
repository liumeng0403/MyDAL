using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDAL.Exchange.Common
{
    public class DicModel<K, V>
    {
        public K key { get; set; }
        public V Value { get; set; }
    }
}
