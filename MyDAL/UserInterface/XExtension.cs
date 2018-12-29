using MyDAL.AdoNet;
using MyDAL.Core;
using MyDAL.Core.Enums;
using MyDAL.UserFacade.Create;
using MyDAL.UserFacade.Delete;
using MyDAL.UserFacade.Join;
using MyDAL.UserFacade.Query;
using MyDAL.UserFacade.Update;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL
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
            where M : class, new()
        {
            var dc = new XContext<M>(conn);
            dc.Crud = CrudEnum.Create;
            return new Creater<M>(dc);
        }
        /// <summary>
        /// 删除数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Deleter<M> Deleter<M>(this IDbConnection conn)
            where M : class, new()
        {
            var dc = new XContext<M>(conn);
            dc.Crud = CrudEnum.Delete;
            return new Deleter<M>(dc);
        }
        /// <summary>
        /// 修改数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Updater<M> Updater<M>(this IDbConnection conn)
            where M : class, new()
        {
            var dc = new XContext<M>(conn);
            dc.Crud = CrudEnum.Update;
            return new Updater<M>(dc);
        }

        /******************************************************************************************************************************/

        /// <summary>
        /// 单表查询 方法簇
        /// </summary>
        /// <typeparam name="M1">M1:与DB Table 一 一对应</typeparam>
        public static Queryer<M1> Queryer<M1>(this IDbConnection conn)
            where M1 : class, new()
        {
            var dc = new XContext<M1>(conn);
            dc.Crud = CrudEnum.Query;
            return new Queryer<M1>(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Queryer Queryer<M1, M2>(this IDbConnection conn, out M1 table1, out M2 table2)
            where M1 : class, new()
            where M2 : class, new()
        {
            table1 = new M1();
            table2 = new M2();
            var dc = new XContext<M1, M2>(conn);
            dc.Crud = CrudEnum.Join;
            return new Queryer(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Queryer Queryer<M1, M2, M3>(this IDbConnection conn, out M1 table1, out M2 table2, out M3 table3)
            where M1 : class, new()
            where M2 : class, new()
            where M3 : class, new()
        {
            table1 = new M1();
            table2 = new M2();
            table3 = new M3();
            var dc = new XContext<M1, M2, M3>(conn);
            dc.Crud = CrudEnum.Join;
            return new Queryer(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Queryer Queryer<M1, M2, M3, M4>(this IDbConnection conn, out M1 table1, out M2 table2, out M3 table3, out M4 table4)
            where M1 : class, new()
            where M2 : class, new()
            where M3 : class, new()
            where M4 : class, new()
        {
            table1 = new M1();
            table2 = new M2();
            table3 = new M3();
            table4 = new M4();
            var dc = new XContext<M1, M2, M3, M4>(conn);
            dc.Crud = CrudEnum.Join;
            return new Queryer(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Queryer Queryer<M1, M2, M3, M4, M5>(this IDbConnection conn, out M1 table1, out M2 table2, out M3 table3, out M4 table4, out M5 table5)
            where M1 : class, new()
            where M2 : class, new()
            where M3 : class, new()
            where M4 : class, new()
            where M5 : class, new()
        {
            table1 = new M1();
            table2 = new M2();
            table3 = new M3();
            table4 = new M4();
            table5 = new M5();
            var dc = new XContext<M1, M2, M3, M4, M5>(conn);
            dc.Crud = CrudEnum.Join;
            return new Queryer(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Queryer Queryer<M1, M2, M3, M4, M5, M6>(this IDbConnection conn, out M1 table1, out M2 table2, out M3 table3, out M4 table4, out M5 table5, out M6 table6)
            where M1 : class, new()
            where M2 : class, new()
            where M3 : class, new()
            where M4 : class, new()
            where M5 : class, new()
            where M6 : class, new()
        {
            table1 = new M1();
            table2 = new M2();
            table3 = new M3();
            table4 = new M4();
            table5 = new M5();
            table6 = new M6();
            var dc = new XContext<M1, M2, M3, M4, M5, M6>(conn);
            dc.Crud = CrudEnum.Join;
            return new Queryer(dc);
        }

        /******************************************************************************************************************************/

        /// <summary>
        /// Creater 便捷 CreateAsync 方法
        /// </summary>
        public static async Task<int> CreateAsync<M>(this IDbConnection conn, M m)
            where M : class, new()
        {
            return await conn.Creater<M>().CreateAsync(m);
        }

        /// <summary>
        /// Creater 便捷 CreateBatchAsync 方法
        /// </summary>
        public static async Task<int> CreateBatchAsync<M>(this IDbConnection conn, IEnumerable<M> mList)
            where M : class, new()
        {
            return await conn.Creater<M>().CreateBatchAsync(mList);
        }

        /// <summary>
        /// Deleter 便捷 DeleteAsync 方法
        /// </summary>
        public static async Task<int> DeleteAsync<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
        {
            return await conn.Deleter<M>().Where(compareFunc).DeleteAsync();
        }

        /// <summary>
        /// Updater 便捷 UpdateAsync update fields 方法
        /// </summary>
        public static async Task<int> UpdateAsync<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, dynamic filedsObject, SetEnum set = SetEnum.AllowedNull)
            where M : class, new()
        {
            return await conn.Updater<M>().Set(filedsObject as object).Where(compareFunc).UpdateAsync(set);
        }

        /// <summary>
        /// 请参阅: <see langword=".FirstOrDefaultAsync() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<M> FirstOrDefaultAsync<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).FirstOrDefaultAsync();
        }
        /// <summary>
        /// 请参阅: <see langword=".FirstOrDefaultAsync() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<VM> FirstOrDefaultAsync<M, VM>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
            where VM : class
        {
            return await conn.Queryer<M>().Where(compareFunc).FirstOrDefaultAsync<VM>();
        }
        /// <summary>
        /// 请参阅: <see langword=".FirstOrDefaultAsync() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<T> FirstOrDefaultAsync<M, T>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, T>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).FirstOrDefaultAsync(columnMapFunc);
        }
        /// <summary>
        /// Queryer 便捷-同步 FirstOrDefaultAsync 方法
        /// </summary>
        public static M FirstOrDefault<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
        {
            return (conn.FirstOrDefaultAsync(compareFunc)).GetAwaiter().GetResult();
        }
        /// <summary>
        /// Queryer 便捷-同步 FirstOrDefaultAsync 方法
        /// </summary>
        public static VM FirstOrDefault<M, VM>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
            where VM : class
        {
            return (conn.FirstOrDefaultAsync<M, VM>(compareFunc)).GetAwaiter().GetResult();
        }
        /// <summary>
        /// Queryer 便捷-同步 FirstOrDefaultAsync 方法
        /// </summary>
        public static T FirstOrDefault<M, T>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, T>> columnMapFunc)
            where M : class, new()
        {
            return (conn.FirstOrDefaultAsync(compareFunc, columnMapFunc)).GetAwaiter().GetResult();
        }


        /// <summary>
        /// 请参阅: <see langword=".ListAsync() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<List<M>> ListAsync<M>(this IDbConnection conn, QueryOption option)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(option).ListAsync();
        }
        /// <summary>
        /// Queryer 便捷 ListAsync 方法
        /// </summary>
        public static async Task<List<VM>> ListAsync<M, VM>(this IDbConnection conn, QueryOption option)
            where M : class, new()
            where VM : class
        {
            return await conn.Queryer<M>().Where(option).ListAsync<VM>();
        }
        /// <summary>
        /// Queryer 便捷 ListAsync 方法
        /// </summary>
        public static async Task<List<VM>> ListAsync<M, VM>(this IDbConnection conn, QueryOption option, Expression<Func<M, VM>> columnMapFunc)
            where M : class, new()
            where VM : class
        {
            return await conn.Queryer<M>().Where(option).ListAsync<VM>(columnMapFunc);
        }

        /// <summary>
        /// 请参阅: <see langword=".ListAsync() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<List<M>> ListAsync<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).ListAsync();
        }
        /// <summary>
        /// 请参阅: <see langword=".ListAsync() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<List<VM>> ListAsync<M, VM>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
            where VM : class
        {
            return await conn.Queryer<M>().Where(compareFunc).ListAsync<VM>();
        }
        /// <summary>
        /// 请参阅: <see langword=".ListAsync() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<List<T>> ListAsync<M, T>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, T>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).ListAsync(columnMapFunc);
        }

        /// <summary>
        /// Queryer 便捷 PagingListAsync 方法
        /// </summary>
        public static async Task<PagingList<M>> PagingListAsync<M>(this IDbConnection conn, PagingQueryOption option)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(option).PagingListAsync(option);
        }
        /// <summary>
        /// Queryer 便捷 PagingListAsync 方法
        /// </summary>
        public static async Task<PagingList<VM>> PagingListAsync<M, VM>(this IDbConnection conn, PagingQueryOption option)
            where M : class, new()
            where VM : class
        {
            return await conn.Queryer<M>().Where(option).PagingListAsync<VM>(option);
        }
        /// <summary>
        /// Queryer 便捷 PagingListAsync 方法
        /// </summary>
        public static async Task<PagingList<VM>> PagingListAsync<M, VM>(this IDbConnection conn, PagingQueryOption option, Expression<Func<M, VM>> columnMapFunc)
            where M : class, new()
            where VM : class
        {
            return await conn.Queryer<M>().Where(option).PagingListAsync<VM>(option, columnMapFunc);
        }

        /// <summary>
        /// Queryer 便捷 AllAsync 方法
        /// </summary>
        public static async Task<List<M>> QueryAllAsync<M>(this IDbConnection conn)
            where M : class, new()
        {
            return await conn.Queryer<M>().QueryAllAsync();
        }
        /// <summary>
        /// Queryer 便捷 AllAsync 方法
        /// </summary>
        public static async Task<List<VM>> QueryAllAsync<M, VM>(this IDbConnection conn)
            where M : class, new()
            where VM : class
        {
            return await conn.Queryer<M>().QueryAllAsync<VM>();
        }
        /// <summary>
        /// Queryer 便捷 AllAsync 方法
        /// </summary>
        public static async Task<List<T>> QueryAllAsync<M, T>(this IDbConnection conn, Expression<Func<M, T>> propertyFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().QueryAllAsync(propertyFunc);
        }

        /// <summary>
        /// Queryer 便捷 ExistAsync 方法
        /// </summary>
        public static async Task<bool> ExistAsync<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).ExistAsync();
        }
        /// <summary>
        /// Queryer 便捷-同步 ExistAsync 方法
        /// </summary>
        public static bool Exist<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
        {
            return (conn.ExistAsync(compareFunc)).GetAwaiter().GetResult();
        }


        /// <summary>
        /// Queryer 便捷 CountAsync 方法
        /// </summary>
        public static async Task<int> CountAsync<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).CountAsync();
        }

        /******************************************************************************************************************************/

        /// <summary>
        /// 异步打开 DB 连接
        /// </summary>
        public static IDbConnection OpenAsync(this IDbConnection conn)
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
