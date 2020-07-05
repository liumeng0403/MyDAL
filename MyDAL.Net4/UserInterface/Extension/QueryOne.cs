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

        #region QueryOne API

        public static async Task<bool> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, bool>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<bool?> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, bool?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<byte> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, byte>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<byte?> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, byte?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<char> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, char>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<char?> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, char?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<decimal> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, decimal>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<decimal?> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M,decimal?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<double> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, double>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<double?> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, double?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<float> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, float>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<float?> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, float?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<int> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, int>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<int?> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, int?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<long> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, long>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<long?> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, long?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<sbyte> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, sbyte>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<sbyte?> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, sbyte?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<short> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, short>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<short?> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, short?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<uint> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, uint>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<uint?> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, uint?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<ulong> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ulong>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<ulong?> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ulong?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<ushort> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M,ushort>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<ushort?> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ushort?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<DateTime> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, DateTime>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<DateTime?> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, DateTime?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<TimeSpan> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, TimeSpan>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<TimeSpan?> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, TimeSpan?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<Guid> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, Guid>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<Guid?> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, Guid?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<object> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, object>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<byte[]> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, byte[]>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }
        public static async Task<string> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, string>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<M> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<VM> QueryOneAsync<M, VM>(this XConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
            where VM : class
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync<VM>();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<T> QueryOneAsync<M, T>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, T>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }

        /*-------------------------------------------------------------*/

        public static async Task<T> QueryOneAsync<T>(this XConnection conn, string sql, List<XParam> dbParas = null)
        {
            CheckQuery(sql);
            var dc = DcForSQL(conn, sql, dbParas);
            return await new QueryOneSQLAsyncImpl(dc).QueryOneAsync<T>();
        }

        /*-------------------------------------------------------------*/

        public static bool QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, bool>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static bool? QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, bool?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static byte QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, byte>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static byte? QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, byte?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static char QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, char>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static char? QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, char?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static decimal QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, decimal>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static decimal? QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, decimal?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static double QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, double>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static double? QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, double?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static float QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, float>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static float? QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, float?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static int QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, int>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static int? QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, int?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static long QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, long>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static long? QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, long?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static sbyte QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, sbyte>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static sbyte? QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, sbyte?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static short QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, short>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static short? QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, short?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static uint QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, uint>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static uint? QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, uint?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static ulong QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ulong>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static ulong? QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ulong?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static ushort QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ushort>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static ushort? QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ushort?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static DateTime QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, DateTime>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static DateTime? QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, DateTime?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static TimeSpan QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, TimeSpan>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static TimeSpan? QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, TimeSpan?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static Guid QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, Guid>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static Guid? QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, Guid?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static object QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, object>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static byte[] QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, byte[]>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }
        public static string QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, string>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// Queryer 便捷-同步 QueryOneAsync 方法
        /// </summary>
        public static M QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne();
        }
        /// <summary>
        /// Queryer 便捷-同步 QueryOneAsync 方法
        /// </summary>
        public static VM QueryOne<M, VM>(this XConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
            where VM : class
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne<VM>();
        }
        /// <summary>
        /// Queryer 便捷-同步 QueryOneAsync 方法
        /// </summary>
        public static T QueryOne<M, T>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, T>> columnMapFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc);
        }

        /*-------------------------------------------------------------*/

        public static T QueryOne<T>(this XConnection conn, string sql, List<XParam> dbParas = null)
        {
            CheckQuery(sql);
            var dc = DcForSQL(conn, sql, dbParas);
            return new QueryOneSQLImpl(dc).QueryOne<T>();
        }

        #endregion

    }
}
