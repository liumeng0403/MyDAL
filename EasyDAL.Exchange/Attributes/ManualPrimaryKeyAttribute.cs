using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDAL.Exchange.Attributes
{
    /// <summary>
    /// PK - 手动赋值
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ManualPrimaryKeyAttribute : Attribute
    {
    }
}
