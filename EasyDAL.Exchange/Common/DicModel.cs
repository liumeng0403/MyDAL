using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDAL.Exchange.Common
{
    public class DicModel<K, V>
    {
        public K key { get; set; }
        public V Value { get; set; }
        public OptionEnum Option { get; set; }
        public ActionEnum Action { get; set; }
    }
}
