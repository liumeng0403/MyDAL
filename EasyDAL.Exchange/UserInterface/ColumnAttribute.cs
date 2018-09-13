using System;

namespace EasyDAL.Exchange
{
    public class ColumnAttribute: Attribute
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string Name { get; set; }

    }
}
