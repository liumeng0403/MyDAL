using System;
using System.Data;

namespace MyDAL.Cache
{
    internal struct DeserializerKey : IEquatable<DeserializerKey>
    {
        private readonly int length;
        private readonly bool returnNullIfFirstMissing;
        private readonly IDataReader reader;
        private readonly string[] names;
        private readonly Type[] types;
        private readonly int hashCode;

        public DeserializerKey(int hashCode, int length, bool returnNullIfFirstMissing, IDataReader reader, bool copyDown)
        {
            this.hashCode = hashCode;
            this.length = length;
            this.returnNullIfFirstMissing = returnNullIfFirstMissing;

            if (copyDown)
            {
                this.reader = null;
                names = new string[length];
                types = new Type[length];
                int index = 0;
                for (int i = 0; i < length; i++)
                {
                    names[i] = reader.GetName(index);
                    types[i] = reader.GetFieldType(index++);
                }
            }
            else
            {
                this.reader = reader;
                names = null;
                types = null;
            }
        }

        public override int GetHashCode() => hashCode;

        public override bool Equals(object obj)
        {
            return obj is DeserializerKey && Equals((DeserializerKey)obj);
        }

        public bool Equals(DeserializerKey other)
        {
            if (hashCode != other.hashCode
                || length != other.length
                || returnNullIfFirstMissing != other.returnNullIfFirstMissing)
            {
                return false; // clearly different
            }
            for (int i = 0; i < length; i++)
            {
                if ((names?[i] ?? reader?.GetName(i)) != (other.names?[i] ?? other.reader?.GetName(i))
                    ||
                    (types?[i] ?? reader?.GetFieldType(i)) != (other.types?[i] ?? other.reader?.GetFieldType(i))
                    )
                {
                    return false; // different column name or type
                }
            }
            return true;
        }
    }
}
