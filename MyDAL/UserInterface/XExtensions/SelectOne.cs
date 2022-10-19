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

        #region SelectOne API

        public static bool SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, bool>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static bool? SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, bool?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static byte SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, byte>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static byte? SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, byte?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static char SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, char>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static char? SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, char?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static decimal SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, decimal>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static decimal? SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, decimal?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static double SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, double>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static double? SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, double?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static float SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, float>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static float? SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, float?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static int SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, int>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static int? SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, int?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static long SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, long>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static long? SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, long?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static sbyte SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, sbyte>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static sbyte? SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, sbyte?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static short SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, short>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static short? SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, short?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static uint SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, uint>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static uint? SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, uint?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static ulong SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ulong>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static ulong? SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ulong?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static ushort SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ushort>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static ushort? SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ushort?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static DateTime SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, DateTime>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static DateTime? SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, DateTime?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static TimeSpan SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, TimeSpan>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static TimeSpan? SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, TimeSpan?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static Guid SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, Guid>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static Guid? SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, Guid?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static object SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, object>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static byte[] SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, byte[]>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }
        public static string SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, string>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// 请参阅: <see langword=".SelectOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static M SelectOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static VM SelectOne<M, VM>(this XConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
            where VM : class
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne<VM>();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static T SelectOne<M, T>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, T>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectOne(columnMapFunc);
        }

        /*-------------------------------------------------------------*/

        public static T SelectOne<T>(this XConnection conn, string sql, List<XParam> dbParas = null)
        {
            CheckQuery(sql);
            var dc = DcForSQL(conn, sql, dbParas);
            return new SelectOneSQLImpl(dc).SelectOne<T>();
        }

        #endregion

    }
}
