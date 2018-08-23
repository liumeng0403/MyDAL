using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDAL.Exchange.Attributes
{

    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {

        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }

    }
}
