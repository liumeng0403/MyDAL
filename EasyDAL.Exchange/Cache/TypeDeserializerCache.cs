using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.Cache;
using EasyDAL.Exchange.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EasyDAL.Exchange.MapperX
{
    internal class TypeDeserializerCache
    {
        private TypeDeserializerCache(Type type)
        {
            this.type = type;
        }

        private static Hashtable byType { get; } = new Hashtable();
        private Type type { get; }
       

        internal static Func<IDataReader, object> GetReader(Type type, IDataReader reader, int startBound, int length, bool returnNullIfFirstMissing)
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
            return found.GetReader(reader, startBound, length, returnNullIfFirstMissing);
        }

        private Dictionary<DeserializerKey, Func<IDataReader, object>> readers { get; } = new Dictionary<DeserializerKey, Func<IDataReader, object>>();



        private Func<IDataReader, object> GetReader(IDataReader reader, int startBound, int length, bool returnNullIfFirstMissing)
        {
            if (length < 0)
            {
                length = reader.FieldCount - startBound;
            }
            int hash = SqlHelper. GetColumnHash(reader, startBound, length);
            if (returnNullIfFirstMissing)
            {
                hash *= -27;
            }
            // get a cheap key first: false means don't copy the values down
            var key = new DeserializerKey(hash, startBound, length, returnNullIfFirstMissing, reader, false);
            Func<IDataReader, object> deser;
            lock (readers)
            {
                if (readers.TryGetValue(key, out deser))
                {
                    return deser;
                }
            }
            deser = SqlHelper. GetTypeDeserializerImpl(type, reader, startBound, length, returnNullIfFirstMissing);
            // get a more expensive key: true means copy the values down so it can be used as a key later
            key = new DeserializerKey(hash, startBound, length, returnNullIfFirstMissing, reader, true);
            lock (readers)
            {
                readers[key] = deser;
                return deser;
            }
        }
    }
}
