﻿using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static partial class XExtension
    {

        #region Sum API

        public static async Task<int> SumAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, int>> propertyFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SumAsync(propertyFunc);
        }
        public static async Task<int?> SumAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, int?>> propertyFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SumAsync(propertyFunc);
        }
        public static async Task<long> SumAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, long>> propertyFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SumAsync(propertyFunc);
        }
        public static async Task<long?> SumAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, long?>> propertyFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SumAsync(propertyFunc);
        }
        public static async Task<decimal> SumAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, decimal>> propertyFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SumAsync(propertyFunc);
        }
        public static async Task<decimal?> SumAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, decimal?>> propertyFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SumAsync(propertyFunc);
        }
        public static async Task<float> SumAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, float>> propertyFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SumAsync(propertyFunc);
        }
        public static async Task<float?> SumAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, float?>> propertyFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SumAsync(propertyFunc);
        }
        public static async Task<double> SumAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, double>> propertyFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SumAsync(propertyFunc);
        }
        public static async Task<double?> SumAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, double?>> propertyFunc)
            where M : class, new()
        {
            return await conn.Selecter<M>().Where(compareFunc).SumAsync(propertyFunc);
        }

        /*-------------------------------------------------------------*/

        public static async Task<F> SumAsync<M, F>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, F>> propertyFunc)
            where M : class, new()
            where F : struct
        {
            return await conn.Selecter<M>().Where(compareFunc).SumAsync(propertyFunc);
        }
        public static async Task<F?> SumAsync<M, F>
            (this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, F?>> propertyFunc)
            where M : class, new()
            where F : struct
        {
            return await conn.Selecter<M>().Where(compareFunc).SumAsync(propertyFunc);
        }

        /*-------------------------------------------------------------*/


        /*-------------------------------------------------------------*/


        #endregion

    }
}
