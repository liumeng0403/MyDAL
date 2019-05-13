using MyDAL.Impls.ImplAsyncs;
using MyDAL.Impls.ImplSyncs;
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

        #region QueryList API

        public static async Task<List<bool>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, bool>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<bool?>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, bool?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<byte>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, byte>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<byte?>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, byte?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<char>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, char>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<char?>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, char?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<decimal>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, decimal>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<decimal?>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, decimal?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<double>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, double>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<double?>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, double?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<float>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, float>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<float?>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, float?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<int>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, int>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<int?>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, int?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<long>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, long>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<long?>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, long?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<sbyte>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, sbyte>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<sbyte?>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, sbyte?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<short>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, short>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<short?>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, short?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<uint>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, uint>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<uint?>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, uint?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<ulong>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ulong>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<ulong?>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ulong?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<ushort>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ushort>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<ushort?>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ushort?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<DateTime>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, DateTime>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<DateTime?>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, DateTime?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<TimeSpan>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, TimeSpan>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<TimeSpan?>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, TimeSpan?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<Guid>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, Guid>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<Guid?>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, Guid?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<object>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, object>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<byte[]>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, byte[]>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }
        public static async Task<List<string>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, string>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<List<M>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<List<VM>> QueryListAsync<M, VM>(this XConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
            where VM : class
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync<VM>();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<List<T>> QueryListAsync<M, T>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, T>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
        }

        /*-------------------------------------------------------------*/

        public static async Task<List<T>> QueryListAsync<T>(this XConnection conn, string sql, List<XParam> dbParas = null)
        {
            CheckQuery(sql);
            var dc = DcForSQL(conn, sql, dbParas);
            return await new QueryListSQLAsyncImpl(dc).QueryListAsync<T>();
        }

        /*-------------------------------------------------------------*/

        public static List<bool> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, bool>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<bool?> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, bool?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<byte> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, byte>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<byte?> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, byte?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<char> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, char>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<char?> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, char?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<decimal> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, decimal>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<decimal?> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, decimal?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<double> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, double>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<double?> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, double?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<float> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, float>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<float?> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, float?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<int> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, int>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<int?> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, int?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<long> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, long>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<long?> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, long?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<sbyte> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, sbyte>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<sbyte?> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, sbyte?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<short> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, short>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<short?> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, short?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<uint> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, uint>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<uint?> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, uint?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<ulong> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ulong>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<ulong?> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ulong?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<ushort> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ushort>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<ushort?> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ushort?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<DateTime> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, DateTime>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<DateTime?> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, DateTime?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<TimeSpan> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, TimeSpan>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<TimeSpan?> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, TimeSpan?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<Guid> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, Guid>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<Guid?> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, Guid?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<object> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, object>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<byte[]> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, byte[]>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }
        public static List<string> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, string>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static List<M> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static List<VM> QueryList<M, VM>(this XConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
            where VM : class
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList<VM>();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static List<T> QueryList<M, T>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, T>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc);
        }

        /*-------------------------------------------------------------*/

        public static List<T> QueryList<T>(this XConnection conn, string sql, List<XParam> dbParas = null)
        {
            CheckQuery(sql);
            var dc = DcForSQL(conn, sql, dbParas);
            return new QueryListSQLImpl(dc).QueryList<T>();
        }
        
        #endregion

    }
}
