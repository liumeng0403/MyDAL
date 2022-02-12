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

        /*-------------------------------------------------------------*/


        /*-------------------------------------------------------------*/


        /*-------------------------------------------------------------*/

        public static List<bool> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, bool>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<bool?> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, bool?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<byte> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, byte>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<byte?> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, byte?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<char> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, char>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<char?> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, char?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<decimal> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, decimal>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<decimal?> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, decimal?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<double> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, double>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<double?> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, double?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<float> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, float>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<float?> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, float?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<int> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, int>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<int?> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, int?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<long> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, long>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<long?> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, long?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<sbyte> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, sbyte>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<sbyte?> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, sbyte?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<short> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, short>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<short?> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, short?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<uint> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, uint>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<uint?> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, uint?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<ulong> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ulong>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<ulong?> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ulong?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<ushort> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ushort>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<ushort?> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, ushort?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<DateTime> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, DateTime>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<DateTime?> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, DateTime?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<TimeSpan> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, TimeSpan>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<TimeSpan?> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, TimeSpan?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<Guid> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, Guid>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<Guid?> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, Guid?>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<object> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, object>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<byte[]> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, byte[]>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }
        public static List<string> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, string>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static List<M> SelectList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static List<VM> SelectList<M, VM>(this XConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
            where VM : class
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList<VM>();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static List<T> SelectList<M, T>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, T>> columnMapFunc)
            where M : class, new()
        {
            return conn.Selecter<M>().Where(compareFunc).SelectList(columnMapFunc);
        }

        /*-------------------------------------------------------------*/

        public static List<T> SelectList<T>(this XConnection conn, string sql, List<XParam> dbParas = null)
        {
            CheckQuery(sql);
            var dc = DcForSQL(conn, sql, dbParas);
            return new SelectListSQLImpl(dc).SelectList<T>();
        }
        
        #endregion

    }
}
