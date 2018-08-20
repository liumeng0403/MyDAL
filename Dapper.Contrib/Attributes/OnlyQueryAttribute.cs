using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Contrib.Attributes
{
    /// <summary>
    /// 仅对该字段从 DB 查询
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class OnlyQueryAttribute : Attribute
    {
    }
}
