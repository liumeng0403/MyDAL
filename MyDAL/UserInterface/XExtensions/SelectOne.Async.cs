using MyDAL.Impls.ImplAsyncs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static partial class XExtension
    {

        #region SelectOne API

        public static async Task<bool> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, bool>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<bool?> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, bool?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<byte> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, byte>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<byte?> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, byte?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<char> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, char>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<char?> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, char?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<decimal> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, decimal>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<decimal?> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, decimal?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<double> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, double>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<double?> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, double?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<float> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, float>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<float?> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, float?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<int> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, int>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<int?> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, int?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<long> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, long>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<long?> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, long?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<sbyte> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, sbyte>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<sbyte?> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, sbyte?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<short> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, short>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<short?> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, short?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<uint> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, uint>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<uint?> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, uint?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<ulong> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ulong>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<ulong?> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ulong?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<ushort> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ushort>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<ushort?> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ushort?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<DateTime> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, DateTime>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<DateTime?> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, DateTime?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<TimeSpan> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, TimeSpan>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<TimeSpan?> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, TimeSpan?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<Guid> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, Guid>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<Guid?> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, Guid?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<object> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, object>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<byte[]> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, byte[]>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }
        public static async Task<string> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, string>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// 请参阅: <see langword=".SelectOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<M> SelectOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<VM> SelectOneAsync<M, VM>(this XConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
            where VM : class
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync<VM>();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<T> SelectOneAsync<M, T>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, T>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectOneAsync(columnMapFunc);
        }

        /*-------------------------------------------------------------*/

        public static async Task<T> SelectOneAsync<T>(this XConnection conn, string sql, List<XParam> dbParas = null)
        {
            CheckQuery(sql);
            var dc = DcForSQL(conn, sql, dbParas);
            return await new SelectOneSQLAsyncImpl(dc).SelectOneAsync<T>();
        }

        /*-------------------------------------------------------------*/

        /*-------------------------------------------------------------*/

        /*-------------------------------------------------------------*/

        #endregion

    }
}
