using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.DataBase;
using EasyDAL.Exchange.DynamicParameter;
using EasyDAL.Exchange.MapperX;

namespace EasyDAL.Exchange.Reader
{
    /// <summary>
    /// 多结果查询处理
    /// </summary>
    public class GridReader : IDisposable
    {
        private IDataReader reader;
        private readonly Identity identity;
        private readonly bool addToCache;

        internal GridReader(IDbCommand command, IDataReader reader, Identity identity, IParameterCallbacks callbacks, bool addToCache)
        {
            Command = command;
            this.reader = reader;
            this.identity = identity;
            this.callbacks = callbacks;
            this.addToCache = addToCache;
        }
        private readonly CancellationToken cancel;
        internal GridReader(IDbCommand command, IDataReader reader, Identity identity, DynamicParameters dynamicParams, bool addToCache, CancellationToken cancel)
            : this(command, reader, identity, dynamicParams, addToCache)
        {
            this.cancel = cancel;
        }
        
        private T ReadRow<T>(Type type, Row row)
        {
            if (reader == null)
            {
                throw new ObjectDisposedException(GetType().FullName, "The reader has been disposed; this can happen after all data has been consumed");
            }
            if (IsConsumed)
            {
                throw new InvalidOperationException("Query results must be consumed in the correct order, and each result can only be consumed once");
            }
            IsConsumed = true;

            T result = default(T);
            if (reader.Read() && reader.FieldCount != 0)
            {
                var typedIdentity = identity.ForGrid(type, gridIndex);
                CacheInfo cache = SqlMapper. GetCacheInfo(typedIdentity, null, addToCache);
                var deserializer = cache.Deserializer;

                int hash = SqlMapper. GetColumnHash(reader);
                if (deserializer.Func == null || deserializer.Hash != hash)
                {
                    deserializer = new DeserializerState(hash, SqlMapper. GetDeserializer(type, reader, 0, -1, false));
                    cache.Deserializer = deserializer;
                }
                object val = deserializer.Func(reader);
                if (val == null || val is T)
                {
                    result = (T)val;
                }
                else
                {
                    var convertToType = Nullable.GetUnderlyingType(type) ?? type;
                    result = (T)Convert.ChangeType(val, convertToType, CultureInfo.InvariantCulture);
                }
                if ((row & Row.Single) != 0 && reader.Read())
                {
                    SqlMapper. ThrowMultipleRows(row);
                }
                while (reader.Read()) { /* ignore subsequent rows */ }
            }
            else if ((row & Row.FirstOrDefault) == 0) // demanding a row, and don't have one
            {
                SqlMapper. ThrowZeroRows(row);
            }
            NextResultAsync().GetAwaiter().GetResult();
            return result;
        }
        
        private IEnumerable<TReturn> MultiReadInternal<TReturn>(Type[] types, Func<object[], TReturn> map, string splitOn)
        {
            var identity = this.identity.ForGrid(typeof(TReturn), types, gridIndex);
            try
            {
                foreach (var r in SqlMapper. MultiMapImpl<TReturn>(null, default(CommandDefinition), types, map, splitOn, reader, identity, false))
                {
                    yield return r;
                }
            }
            finally
            {
                NextResultAsync().GetAwaiter().GetResult();
            }
        }
        
        private async Task<T> ReadRowAsyncImplViaDbReader<T>(DbDataReader reader, Type type, Row row)
        {
            if (reader == null) throw new ObjectDisposedException(GetType().FullName, "The reader has been disposed; this can happen after all data has been consumed");
            if (IsConsumed) throw new InvalidOperationException("Query results must be consumed in the correct order, and each result can only be consumed once");

            IsConsumed = true;
            T result = default(T);
            if (await reader.ReadAsync(cancel).ConfigureAwait(false) && reader.FieldCount != 0)
            {
                var typedIdentity = identity.ForGrid(type, gridIndex);
                CacheInfo cache =SqlMapper. GetCacheInfo(typedIdentity, null, addToCache);
                var deserializer = cache.Deserializer;

                int hash =SqlMapper. GetColumnHash(reader);
                if (deserializer.Func == null || deserializer.Hash != hash)
                {
                    deserializer = new DeserializerState(hash, SqlMapper. GetDeserializer(type, reader, 0, -1, false));
                    cache.Deserializer = deserializer;
                }
                result = (T)deserializer.Func(reader);
                if ((row & Row.Single) != 0 && await reader.ReadAsync(cancel).ConfigureAwait(false))
                {
                    SqlMapper. ThrowMultipleRows(row);
                }
                while (await reader.ReadAsync(cancel).ConfigureAwait(false)) { /* ignore subsequent rows */ }
            }
            else if ((row & Row.FirstOrDefault) == 0) // demanding a row, and don't have one
            {
                SqlMapper. ThrowZeroRows(row);
            }
            await NextResultAsync().ConfigureAwait(false);
            return result;
        }
        
        private int gridIndex, readCount;
        private readonly IParameterCallbacks callbacks;

        /// <summary>
        /// Has the underlying reader been consumed?
        /// </summary>
        public bool IsConsumed { get; private set; }

        /// <summary>
        /// The command associated with the reader
        /// </summary>
        public IDbCommand Command { get; set; }

        private async Task NextResultAsync()
        {
            if (await ((DbDataReader)reader).NextResultAsync(cancel).ConfigureAwait(false))
            {
                readCount++;
                gridIndex++;
                IsConsumed = false;
            }
            else
            {
                // happy path; close the reader cleanly - no
                // need for "Cancel" etc
                reader.Dispose();
                reader = null;
                callbacks?.OnCompleted();
                Dispose();
            }
        }

        /// <summary>
        /// Dispose the grid, closing and disposing both the underlying reader and command.
        /// </summary>
        public void Dispose()
        {
            if (reader != null)
            {
                if (!reader.IsClosed) Command?.Cancel();
                reader.Dispose();
                reader = null;
            }
            if (Command != null)
            {
                Command.Dispose();
                Command = null;
            }
        }
    }
}
