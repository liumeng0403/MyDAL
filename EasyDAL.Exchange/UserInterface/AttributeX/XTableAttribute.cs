using System;

namespace Yunyong.DataExchange
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false,Inherited =false)]
    public sealed class XTableAttribute:Attribute
    {
        public XTableAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
