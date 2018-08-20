using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Contrib.Attributes
{
    /// <summary>
    /// PK
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : Attribute
    {
    }
}
