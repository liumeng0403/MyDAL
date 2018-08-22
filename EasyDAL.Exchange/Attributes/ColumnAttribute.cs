using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Attributes
{
    public class ColumnAttribute: Attribute
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string Name { get; set; }

    }
}
