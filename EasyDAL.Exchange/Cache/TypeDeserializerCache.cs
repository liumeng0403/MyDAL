using MyDAL.Core.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace MyDAL.Cache
{
    internal class TypeDeserializerCache
    {
        private TypeDeserializerCache(Type type)
        {
            this.type = type;
        }

        private static Hashtable byType { get; } = new Hashtable();
        private Type type { get; }


        internal static Func<IDataReader, object> GetReader(Type type, IDataReader reader, bool returnNullIfFirstMissing)
        {
            var found = (TypeDeserializerCache)byType[type];
            if (found == null)
            {
                lock (byType)
                {
                    found = (TypeDeserializerCache)byType[type];
                    if (found == null)
                    {
                        byType[type] = found = new TypeDeserializerCache(type);
                    }
                }
            }
            return found.GetReader(reader, returnNullIfFirstMissing);
        }

        private Dictionary<DeserializerKey, Func<IDataReader, object>> readers { get; } = new Dictionary<DeserializerKey, Func<IDataReader, object>>();



        private Func<IDataReader, object> GetReader(IDataReader reader, bool returnNullIfFirstMissing)
        {
            var length = reader.FieldCount;
            int hash = AdoNetHelper.GetColumnHash(reader);
            if (returnNullIfFirstMissing)
            {
                hash *= -27;
            }
            // get a cheap key first: false means don't copy the values down
            var key = new DeserializerKey(hash, length, returnNullIfFirstMissing, reader, false);
            Func<IDataReader, object> deser;
            lock (readers)
            {
                if (readers.TryGetValue(key, out deser))
                {
                    return deser;
                }
            }
            deser = AdoNetHelper.RowDeserializer(type, reader, length, returnNullIfFirstMissing);
            // get a more expensive key: true means copy the values down so it can be used as a key later
            key = new DeserializerKey(hash, length, returnNullIfFirstMissing, reader, true);
            lock (readers)
            {
                readers[key] = deser;
                return deser;
            }
        }
    }
}
