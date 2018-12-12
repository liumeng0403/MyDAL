using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.Core;
using Yunyong.DataExchange.AdoNet;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.UserFacade.Create;
using Yunyong.DataExchange.UserFacade.Delete;
using Yunyong.DataExchange.UserFacade.Join;
using Yunyong.DataExchange.UserFacade.Query;
using Yunyong.DataExchange.UserFacade.Transaction;
using Yunyong.DataExchange.UserFacade.Update;

namespace Yunyong.DataExchange
{
    /// <summary>
    /// 请参阅: <see langword="简介&amp;安装&amp;快速使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static class XExtension
    {

        /// <summary>
        /// 新建数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Creater<M> Creater<M>(this IDbConnection conn)
            where M : class
        {
            var dc = new XContext<M>(conn);
            dc.Crud = CrudTypeEnum.Create;
            return new Creater<M>(dc);
        }
        /// <summary>
        /// 删除数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Deleter<M> Deleter<M>(this IDbConnection conn)
        {
            var dc = new XContext<M>(conn);
            dc.Crud = CrudTypeEnum.Delete;
            return new Deleter<M>(dc);
        }
        /// <summary>
        /// 修改数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Updater<M> Updater<M>(this IDbConnection conn)
        {
            var dc = new XContext<M>(conn);
            dc.Crud = CrudTypeEnum.Update;
            return new Updater<M>(dc);
        }

        /// <summary>
        /// 单表查询 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        [Obsolete("过期方法,建议使用 Queryer ,用法一致,方法名替换即可!!!")]
        public static Queryer<M> Selecter<M>(this IDbConnection conn)
            where M : class
        {
            return conn.Queryer<M>();
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        [Obsolete("过期方法,建议使用 Queryer ,用法一致,方法名替换即可!!!")]
        public static Queryer Joiner<M1, M2>(this IDbConnection conn, out M1 table1, out M2 table2)
        {
            return conn.Queryer(out table1, out table2);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        [Obsolete("过期方法,建议使用 Queryer ,用法一致,方法名替换即可!!!")]
        public static Queryer Joiner<M1, M2, M3>(this IDbConnection conn, out M1 table1, out M2 table2, out M3 table3)
        {
            return conn.Queryer(out table1, out table2, out table3);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        [Obsolete("过期方法,建议使用 Queryer ,用法一致,方法名替换即可!!!")]
        public static Queryer Joiner<M1, M2, M3, M4>(this IDbConnection conn, out M1 table1, out M2 table2, out M3 table3, out M4 table4)
        {
            return conn.Queryer(out table1, out table2, out table3, out table4);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        [Obsolete("过期方法,建议使用 Queryer ,用法一致,方法名替换即可!!!")]
        public static Queryer Joiner<M1, M2, M3, M4, M5>(this IDbConnection conn, out M1 table1, out M2 table2, out M3 table3, out M4 table4, out M5 table5)
        {
            return conn.Queryer(out table1, out table2, out table3, out table4, out table5);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        [Obsolete("过期方法,建议使用 Queryer ,用法一致,方法名替换即可!!!")]
        public static Queryer Joiner<M1, M2, M3, M4, M5, M6>(this IDbConnection conn, out M1 table1, out M2 table2, out M3 table3, out M4 table4, out M5 table5, out M6 table6)
        {
            return conn.Queryer(out table1, out table2, out table3, out table4, out table5, out table6);
        }

        /******************************************************************************************************************************/

        /// <summary>
        /// 单表查询 方法簇
        /// </summary>
        /// <typeparam name="M1">M1:与DB Table 一 一对应</typeparam>
        public static Queryer<M1> Queryer<M1>(this IDbConnection conn)
            where M1 : class
        {
            var dc = new XContext<M1>(conn);
            dc.Crud = CrudTypeEnum.Query;
            return new Queryer<M1>(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Queryer Queryer<M1, M2>(this IDbConnection conn, out M1 table1, out M2 table2)
        {
            table1 = Activator.CreateInstance<M1>();
            table2 = Activator.CreateInstance<M2>();
            var dc = new XContext<M1, M2>(conn);
            dc.Crud = CrudTypeEnum.Join;
            return new Queryer(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Queryer Queryer<M1, M2, M3>(this IDbConnection conn, out M1 table1, out M2 table2, out M3 table3)
        {
            table1 = Activator.CreateInstance<M1>();
            table2 = Activator.CreateInstance<M2>();
            table3 = Activator.CreateInstance<M3>();
            var dc = new XContext<M1, M2, M3>(conn);
            dc.Crud = CrudTypeEnum.Join;
            return new Queryer(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Queryer Queryer<M1, M2, M3, M4>(this IDbConnection conn, out M1 table1, out M2 table2, out M3 table3, out M4 table4)
        {
            table1 = Activator.CreateInstance<M1>();
            table2 = Activator.CreateInstance<M2>();
            table3 = Activator.CreateInstance<M3>();
            table4 = Activator.CreateInstance<M4>();
            var dc = new XContext<M1, M2, M3, M4>(conn);
            dc.Crud = CrudTypeEnum.Join;
            return new Queryer(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Queryer Queryer<M1, M2, M3, M4, M5>(this IDbConnection conn, out M1 table1, out M2 table2, out M3 table3, out M4 table4, out M5 table5)
        {
            table1 = Activator.CreateInstance<M1>();
            table2 = Activator.CreateInstance<M2>();
            table3 = Activator.CreateInstance<M3>();
            table4 = Activator.CreateInstance<M4>();
            table5 = Activator.CreateInstance<M5>();
            var dc = new XContext<M1, M2, M3, M4, M5>(conn);
            dc.Crud = CrudTypeEnum.Join;
            return new Queryer(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Queryer Queryer<M1, M2, M3, M4, M5, M6>(this IDbConnection conn, out M1 table1, out M2 table2, out M3 table3, out M4 table4, out M5 table5, out M6 table6)
        {
            table1 = Activator.CreateInstance<M1>();
            table2 = Activator.CreateInstance<M2>();
            table3 = Activator.CreateInstance<M3>();
            table4 = Activator.CreateInstance<M4>();
            table5 = Activator.CreateInstance<M5>();
            table6 = Activator.CreateInstance<M6>();
            var dc = new XContext<M1, M2, M3, M4, M5, M6>(conn);
            dc.Crud = CrudTypeEnum.Join;
            return new Queryer(dc);
        }

        /******************************************************************************************************************************/

        /// <summary>
        /// Creater 便捷 CreateAsync 方法
        /// </summary>
        public static async Task<int> CreateAsync<M>(this IDbConnection conn, M m)
            where M : class
        {
            return await conn.Creater<M>().CreateAsync(m);
        }

        /// <summary>
        /// Creater 便捷 CreateBatchAsync 方法
        /// </summary>
        public static async Task<int> CreateBatchAsync<M>(this IDbConnection conn, IEnumerable<M> mList)
            where M : class
        {
            return await conn.Creater<M>().CreateBatchAsync(mList);
        }

        /// <summary>
        /// Deleter 便捷 DeleteAsync 方法
        /// </summary>
        public static async Task<int> DeleteAsync<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            return await conn.Deleter<M>().Where(compareFunc).DeleteAsync();
        }

        /// <summary>
        /// Updater 便捷 UpdateAsync update fields 方法
        /// </summary>
        public static async Task<int> UpdateAsync<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, dynamic filedsObject, SetEnum set = SetEnum.AllowedNull)
            where M : class
        {
            return await conn.Updater<M>().Set(filedsObject as object).Where(compareFunc).UpdateAsync(set);
        }

        /// <summary>
        /// Queryer 便捷 FirstOrDefaultAsync 方法
        /// </summary>
        public static async Task<M> FirstOrDefaultAsync<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            return await conn.Queryer<M>().Where(compareFunc).FirstOrDefaultAsync();
        }
        /// <summary>
        /// Queryer 便捷 FirstOrDefaultAsync 方法
        /// </summary>
        public static async Task<VM> FirstOrDefaultAsync<M, VM>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class
            where VM : class
        {
            return await conn.Queryer<M>().Where(compareFunc).FirstOrDefaultAsync<VM>();
        }
        /// <summary>
        /// Queryer 便捷 FirstOrDefaultAsync 方法
        /// </summary>
        public static async Task<T> FirstOrDefaultAsync<M, T>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, T>> columnMapFunc)
            where M : class
        {
            return await conn.Queryer<M>().Where(compareFunc).FirstOrDefaultAsync(columnMapFunc);
        }
        /// <summary>
        /// Queryer 便捷-同步 FirstOrDefaultAsync 方法
        /// </summary>
        public static M FirstOrDefault<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            return (conn.FirstOrDefaultAsync(compareFunc)).GetAwaiter().GetResult();
        }
        /// <summary>
        /// Queryer 便捷-同步 FirstOrDefaultAsync 方法
        /// </summary>
        public static VM FirstOrDefault<M, VM>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class
            where VM : class
        {
            return (conn.FirstOrDefaultAsync<M, VM>(compareFunc)).GetAwaiter().GetResult();
        }
        /// <summary>
        /// Queryer 便捷-同步 FirstOrDefaultAsync 方法
        /// </summary>
        public static T FirstOrDefault<M, T>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, T>> columnMapFunc)
            where M : class
        {
            return (conn.FirstOrDefaultAsync(compareFunc, columnMapFunc)).GetAwaiter().GetResult();
        }


        /// <summary>
        /// Queryer 便捷 ListAsync 方法
        /// </summary>
        public static async Task<List<M>> ListAsync<M>(this IDbConnection conn, QueryOption option)
            where M : class
        {
            return await conn.Queryer<M>().Where(option).ListAsync();
        }
        /// <summary>
        /// Queryer 便捷 ListAsync 方法
        /// </summary>
        public static async Task<List<VM>> ListAsync<M, VM>(this IDbConnection conn, QueryOption option)
            where M : class
            where VM : class
        {
            return await conn.Queryer<M>().Where(option).ListAsync<VM>();
        }
        /// <summary>
        /// Queryer 便捷 ListAsync 方法
        /// </summary>
        public static async Task<List<VM>> ListAsync<M, VM>(this IDbConnection conn, QueryOption option, Expression<Func<M, VM>> columnMapFunc)
            where M : class
            where VM : class
        {
            return await conn.Queryer<M>().Where(option).ListAsync<VM>(columnMapFunc);
        }

        /// <summary>
        /// Queryer 便捷 ListAsync 方法
        /// </summary>
        public static async Task<List<M>> ListAsync<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            return await conn.Queryer<M>().Where(compareFunc).ListAsync();
        }
        /// <summary>
        /// Queryer 便捷 ListAsync 方法
        /// </summary>
        public static async Task<List<VM>> ListAsync<M, VM>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class
            where VM : class
        {
            return await conn.Queryer<M>().Where(compareFunc).ListAsync<VM>();
        }
        /// <summary>
        /// Queryer 便捷 ListAsync 方法
        /// </summary>
        public static async Task<List<VM>> ListAsync<M, VM>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, VM>> columnMapFunc)
            where M : class
            where VM : class
        {
            return await conn.Queryer<M>().Where(compareFunc).ListAsync<VM>(columnMapFunc);
        }

        /// <summary>
        /// Queryer 便捷 PagingListAsync 方法
        /// </summary>
        public static async Task<PagingList<M>> PagingListAsync<M>(this IDbConnection conn, PagingQueryOption option)
            where M : class
        {
            return await conn.Queryer<M>().Where(option).PagingListAsync(option);
        }
        /// <summary>
        /// Queryer 便捷 PagingListAsync 方法
        /// </summary>
        public static async Task<PagingList<VM>> PagingListAsync<M, VM>(this IDbConnection conn, PagingQueryOption option)
            where M : class
            where VM : class
        {
            return await conn.Queryer<M>().Where(option).PagingListAsync<VM>(option);
        }
        /// <summary>
        /// Queryer 便捷 PagingListAsync 方法
        /// </summary>
        public static async Task<PagingList<VM>> PagingListAsync<M, VM>(this IDbConnection conn, PagingQueryOption option, Expression<Func<M, VM>> columnMapFunc)
            where M : class
            where VM : class
        {
            return await conn.Queryer<M>().Where(option).PagingListAsync<VM>(option, columnMapFunc);
        }

        /// <summary>
        /// Queryer 便捷 AllAsync 方法
        /// </summary>
        public static async Task<List<M>> AllAsync<M>(this IDbConnection conn)
            where M : class
        {
            return await conn.Queryer<M>().AllAsync();
        }
        /// <summary>
        /// Queryer 便捷 AllAsync 方法
        /// </summary>
        public static async Task<List<VM>> AllAsync<M, VM>(this IDbConnection conn)
            where M : class
            where VM : class
        {
            return await conn.Queryer<M>().AllAsync<VM>();
        }
        /// <summary>
        /// Queryer 便捷 AllAsync 方法
        /// </summary>
        public static async Task<List<T>> AllAsync<M, T>(this IDbConnection conn, Expression<Func<M, T>> propertyFunc)
            where M : class
        {
            return await conn.Queryer<M>().AllAsync(propertyFunc);
        }

        /// <summary>
        /// Queryer 便捷 ExistAsync 方法
        /// </summary>
        public static async Task<bool> ExistAsync<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            return await conn.Queryer<M>().Where(compareFunc).ExistAsync();
        }
        /// <summary>
        /// Queryer 便捷-同步 ExistAsync 方法
        /// </summary>
        public static bool Exist<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            return (conn.ExistAsync(compareFunc)).GetAwaiter().GetResult();
        }


        /// <summary>
        /// Queryer 便捷 CountAsync 方法
        /// </summary>
        public static async Task<int> CountAsync<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            return await conn.Queryer<M>().Where(compareFunc).CountAsync();
        }

        /******************************************************************************************************************************/

        /// <summary>
        /// 事务单元
        /// </summary>
        public static Transactioner Transactioner(this IDbConnection conn)
        {
            return new Transactioner(new XContext(conn));
        }

        /******************************************************************************************************************************/

        public static IDbConnection OpenDB(this IDbConnection conn)
        {
            if (conn.State == ConnectionState.Closed)
            {
                new DataSource().OpenAsync(conn).GetAwaiter().GetResult();
            }
            return conn;
        }
        /// <summary>
        /// Sql 调试跟踪 开启
        /// </summary>
        public static IDbConnection OpenDebug(this IDbConnection conn)
        {
            XConfig.IsDebug = true;
            return conn;
        }

    }
}
