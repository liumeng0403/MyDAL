using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Attributes
{
    /// <summary>
    /// PK - 自动赋值
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : Attribute
    {
    }
}
