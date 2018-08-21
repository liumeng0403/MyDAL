using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper.DataBase;
using Dapper.DynamicParameter;

namespace Dapper.Reader
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

        /// <summary>
        /// Read the next grid of results, returned as a dynamic object
        /// </summary>
        /// <remarks>Note: each row can be accessed via "dynamic", or by casting to an IDictionary&lt;string,object&gt;</remarks>
        /// <param name="buffered">Whether to buffer the results.</param>
        public Task<IEnumerable<dynamic>> ReadAsync(bool buffered = true) => ReadAsyncImpl<dynamic>(typeof(DapperRow), buffered);

        /// <summary>
        /// Read an individual row of the next grid of results, returned as a dynamic object
        /// </summary>
        /// <remarks>Note: the row can be accessed via "dynamic", or by casting to an IDictionary&lt;string,object&gt;</remarks>
        public Task<dynamic> ReadFirstAsync() => ReadRowAsyncImpl<dynamic>(typeof(DapperRow), Row.First);

        /// <summary>
        /// Read an individual row of the next grid of results, returned as a dynamic object
        /// </summary>
        /// <remarks>Note: the row can be accessed via "dynamic", or by casting to an IDictionary&lt;string,object&gt;</remarks>
        public Task<dynamic> ReadFirstOrDefaultAsync() => ReadRowAsyncImpl<dynamic>(typeof(DapperRow), Row.FirstOrDefault);

        /// <summary>
        /// Read an individual row of the next grid of results, returned as a dynamic object
        /// </summary>
        /// <remarks>Note: the row can be accessed via "dynamic", or by casting to an IDictionary&lt;string,object&gt;</remarks>
        public Task<dynamic> ReadSingleAsync() => ReadRowAsyncImpl<dynamic>(typeof(DapperRow), Row.Single);

        /// <summary>
        /// Read an individual row of the next grid of results, returned as a dynamic object
        /// </summary>
        /// <remarks>Note: the row can be accessed via "dynamic", or by casting to an IDictionary&lt;string,object&gt;</remarks>
        public Task<dynamic> ReadSingleOrDefaultAsync() => ReadRowAsyncImpl<dynamic>(typeof(DapperRow), Row.SingleOrDefault);

        /// <summary>
        /// Read the next grid of results
        /// </summary>
        /// <param name="type">The type to read.</param>
        /// <param name="buffered">Whether to buffer the results.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public Task<IEnumerable<object>> ReadAsync(Type type, bool buffered = true)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return ReadAsyncImpl<object>(type, buffered);
        }

        /// <summary>
        /// Read an individual row of the next grid of results
        /// </summary>
        /// <param name="type">The type to read.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public Task<object> ReadFirstAsync(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return ReadRowAsyncImpl<object>(type, Row.First);
        }

        /// <summary>
        /// Read an individual row of the next grid of results.
        /// </summary>
        /// <param name="type">The type to read.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public Task<object> ReadFirstOrDefaultAsync(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return ReadRowAsyncImpl<object>(type, Row.FirstOrDefault);
        }

        /// <summary>
        /// Read an individual row of the next grid of results.
        /// </summary>
        /// <param name="type">The type to read.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public Task<object> ReadSingleAsync(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return ReadRowAsyncImpl<object>(type, Row.Single);
        }

        /// <summary>
        /// Read an individual row of the next grid of results.
        /// </summary>
        /// <param name="type">The type to read.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public Task<object> ReadSingleOrDefaultAsync(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return ReadRowAsyncImpl<object>(type, Row.SingleOrDefault);
        }

        /// <summary>
        /// Read the next grid of results.
        /// </summary>
        /// <typeparam name="T">The type to read.</typeparam>
        /// <param name="buffered">Whether the results should be buffered in memory.</param>
        public Task<IEnumerable<T>> ReadAsync<T>(bool buffered = true) => ReadAsyncImpl<T>(typeof(T), buffered);

        /// <summary>
        /// Read an individual row of the next grid of results.
        /// </summary>
        /// <typeparam name="T">The type to read.</typeparam>
        public Task<T> ReadFirstAsync<T>() => ReadRowAsyncImpl<T>(typeof(T), Row.First);

        /// <summary>
        /// Read an individual row of the next grid of results.
        /// </summary>
        /// <typeparam name="T">The type to read.</typeparam>
        public Task<T> ReadFirstOrDefaultAsync<T>() => ReadRowAsyncImpl<T>(typeof(T), Row.FirstOrDefault);

        /// <summary>
        /// Read an individual row of the next grid of results.
        /// </summary>
        /// <typeparam name="T">The type to read.</typeparam>
        public Task<T> ReadSingleAsync<T>() => ReadRowAsyncImpl<T>(typeof(T), Row.Single);

        /// <summary>
        /// Read an individual row of the next grid of results.
        /// </summary>
        /// <typeparam name="T">The type to read.</typeparam>
        public Task<T> ReadSingleOrDefaultAsync<T>() => ReadRowAsyncImpl<T>(typeof(T), Row.SingleOrDefault);

        private Task<IEnumerable<T>> ReadAsyncImpl<T>(Type type, bool buffered)
        {
            if (reader == null) throw new ObjectDisposedException(GetType().FullName, "The reader has been disposed; this can happen after all data has been consumed");
            if (IsConsumed) throw new InvalidOperationException("Query results must be consumed in the correct order, and each result can only be consumed once");
            var typedIdentity = identity.ForGrid(type, gridIndex);
            SqlMapper. CacheInfo cache = SqlMapper. GetCacheInfo(typedIdentity, null, addToCache);
            var deserializer = cache.Deserializer;

            int hash =SqlMapper. GetColumnHash(reader);
            if (deserializer.Func == null || deserializer.Hash != hash)
            {
                deserializer = new SqlMapper. DeserializerState(hash, SqlMapper. GetDeserializer(type, reader, 0, -1, false));
                cache.Deserializer = deserializer;
            }
            IsConsumed = true;
            if (buffered && reader is DbDataReader)
            {
                return ReadBufferedAsync<T>(gridIndex, deserializer.Func);
            }
            else
            {
                var result = ReadDeferred<T>(gridIndex, deserializer.Func, type);
                if (buffered) result = result.ToList(); // for the "not a DbDataReader" scenario
                return Task.FromResult(result);
            }
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
                SqlMapper. CacheInfo cache = SqlMapper. GetCacheInfo(typedIdentity, null, addToCache);
                var deserializer = cache.Deserializer;

                int hash = SqlMapper. GetColumnHash(reader);
                if (deserializer.Func == null || deserializer.Hash != hash)
                {
                    deserializer = new SqlMapper. DeserializerState(hash, SqlMapper. GetDeserializer(type, reader, 0, -1, false));
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
        
        /// <summary>
        /// Read multiple objects from a single record set on the grid
        /// </summary>
        /// <typeparam name="TReturn">The type to return from the record set.</typeparam>
        /// <param name="types">The types to read from the result set.</param>
        /// <param name="map">The mapping function from the read types to the return type.</param>
        /// <param name="splitOn">The field(s) we should split and read the second object from (defaults to "id")</param>
        /// <param name="buffered">Whether to buffer results in memory.</param>
        public IEnumerable<TReturn> Read<TReturn>(Type[] types, Func<object[], TReturn> map, string splitOn = "id", bool buffered = true)
        {
            var result = MultiReadInternal(types, map, splitOn);
            return buffered ? result.ToList() : result;
        }

        private IEnumerable<T> ReadDeferred<T>(int index, Func<IDataReader, object> deserializer, Type effectiveType)
        {
            try
            {
                var convertToType = Nullable.GetUnderlyingType(effectiveType) ?? effectiveType;
                while (index == gridIndex && reader.Read())
                {
                    object val = deserializer(reader);
                    if (val == null || val is T)
                    {
                        yield return (T)val;
                    }
                    else
                    {
                        yield return (T)Convert.ChangeType(val, convertToType, CultureInfo.InvariantCulture);
                    }
                }
            }
            finally // finally so that First etc progresses things even when multiple rows
            {
                if (index == gridIndex)
                {
                    NextResultAsync().GetAwaiter().GetResult();
                }
            }
        }

        private Task<T> ReadRowAsyncImpl<T>(Type type, Row row)
        {
            if (reader is DbDataReader dbReader)
            {
                return ReadRowAsyncImplViaDbReader<T>(dbReader, type, row);
            }

            // no async API available; use non-async and fake it
            return Task.FromResult(ReadRow<T>(type, row));
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
                SqlMapper. CacheInfo cache =SqlMapper. GetCacheInfo(typedIdentity, null, addToCache);
                var deserializer = cache.Deserializer;

                int hash =SqlMapper. GetColumnHash(reader);
                if (deserializer.Func == null || deserializer.Hash != hash)
                {
                    deserializer = new SqlMapper. DeserializerState(hash, SqlMapper. GetDeserializer(type, reader, 0, -1, false));
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

        private async Task<IEnumerable<T>> ReadBufferedAsync<T>(int index, Func<IDataReader, object> deserializer)
        {
            try
            {
                var reader = (DbDataReader)this.reader;
                var buffer = new List<T>();
                while (index == gridIndex && await reader.ReadAsync(cancel).ConfigureAwait(false))
                {
                    buffer.Add((T)deserializer(reader));
                }
                return buffer;
            }
            finally // finally so that First etc progresses things even when multiple rows
            {
                if (index == gridIndex)
                {
                    await NextResultAsync().ConfigureAwait(false);
                }
            }
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
