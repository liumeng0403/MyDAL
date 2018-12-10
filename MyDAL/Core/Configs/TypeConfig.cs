using System;
using System.Collections.Generic;

namespace MyDAL.Core.Configs
{
    internal class TypeConfig
    {

        /************************************************************************************************************/

        internal Type Bool { get; } = typeof(bool);
        internal Type Byte { get; } = typeof(byte);
        internal Type ByteArray { get; } = typeof(byte[]);
        internal string LinqBinary { get; } = "System.Data.Linq.Binary";
        internal Type Char { get; } = typeof(char);
        internal Type CharNull { get; } = typeof(char?);
        internal Type Decimal { get; } = typeof(decimal);
        internal Type Double { get; } = typeof(double);
        internal Type Float { get; } = typeof(float);
        internal Type Int { get; } = typeof(int);
        internal Type Long { get; } = typeof(long);
        internal Type Sbyte { get; } = typeof(sbyte);
        internal Type Short { get; } = typeof(short);
        internal Type Uint { get; } = typeof(uint);
        internal Type Ulong { get; } = typeof(ulong);
        internal Type Ushort { get; } = typeof(ushort);
        internal Type String { get; } = typeof(string);
        internal Type DateTime { get; } = typeof(DateTime);
        internal Type DateTimeNull { get; } = typeof(DateTime?);
        internal Type DateTimeOffset { get; } = typeof(DateTimeOffset);
        internal Type TimeSpan { get; } = typeof(TimeSpan);
        internal Type Guid { get; } = typeof(Guid);
        internal Type NullableT { get; } = typeof(Nullable<>);
        internal Type ListT { get; } = typeof(List<>);
        internal Type IEnumerableT { get; } = typeof(IEnumerable<>);
        internal Type Object { get; } = typeof(object);

    }
}
