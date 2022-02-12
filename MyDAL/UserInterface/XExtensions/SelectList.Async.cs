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

        #region SelectList API

        public static async Task<List<bool>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, bool>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<bool?>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, bool?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<byte>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, byte>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<byte?>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, byte?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<char>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, char>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<char?>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, char?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<decimal>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, decimal>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<decimal?>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, decimal?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<double>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, double>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<double?>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, double?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<float>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, float>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<float?>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, float?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<int>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, int>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<int?>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, int?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<long>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, long>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<long?>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, long?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<sbyte>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, sbyte>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<sbyte?>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, sbyte?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<short>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, short>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<short?>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, short?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<uint>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, uint>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<uint?>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, uint?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<ulong>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ulong>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<ulong?>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ulong?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<ushort>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ushort>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<ushort?>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ushort?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<DateTime>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, DateTime>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<DateTime?>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, DateTime?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<TimeSpan>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, TimeSpan>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<TimeSpan?>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, TimeSpan?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<Guid>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, Guid>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<Guid?>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, Guid?>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<object>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, object>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<byte[]>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, byte[]>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }
        public static async Task<List<string>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, string>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<List<M>> SelectListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<List<VM>> SelectListAsync<M, VM>(this XConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
            where VM : class
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync<VM>();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<List<T>> SelectListAsync<M, T>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, T>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SelectListAsync(columnMapFunc);
        }

        /*-------------------------------------------------------------*/

        public static async Task<List<T>> SelectListAsync<T>(this XConnection conn, string sql, List<XParam> dbParas = null)
        {
            CheckQuery(sql);
            var dc = DcForSQL(conn, sql, dbParas);
            return await new SelectListSQLAsyncImpl(dc).SelectListAsync<T>();
        }

        /*-------------------------------------------------------------*/

        /*-------------------------------------------------------------*/


        /*-------------------------------------------------------------*/

       

        #endregion

    }
}
