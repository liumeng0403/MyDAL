using MyDAL.AdoNet;
using MyDAL.Core;
using MyDAL.Core.Enums;
using MyDAL.Impls;
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
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
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
            var dc = new XContext<M>(conn)
            {
                Crud = CrudEnum.Create
            };
            return new Creater<M>(dc);
        }
        /// <summary>
        /// 删除数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Deleter<M> Deleter<M>(this IDbConnection conn)
            where M : class, new()
        {
            var dc = new XContext<M>(conn)
            {
                Crud = CrudEnum.Delete
            };
            return new Deleter<M>(dc);
        }
        /// <summary>
        /// 修改数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Updater<M> Updater<M>(this IDbConnection conn)
            where M : class, new()
        {
            var dc = new XContext<M>(conn)
            {
                Crud = CrudEnum.Update
            };
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
            var dc = new XContext<M1>(conn)
            {
                Crud = CrudEnum.Query
            };
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
            var dc = new XContext<M1, M2>(conn)
            {
                Crud = CrudEnum.Join
            };
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
            var dc = new XContext<M1, M2, M3>(conn)
            {
                Crud = CrudEnum.Join
            };
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
            var dc = new XContext<M1, M2, M3, M4>(conn)
            {
                Crud = CrudEnum.Join
            };
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
            var dc = new XContext<M1, M2, M3, M4, M5>(conn)
            {
                Crud = CrudEnum.Join
            };
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
            var dc = new XContext<M1, M2, M3, M4, M5, M6>(conn)
            {
                Crud = CrudEnum.Join
            };
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
            if (compareFunc == null)
            {
                throw new NotImplementedException();//return await conn.Deleter<M>
            }
            else
            {
                return await conn.Deleter<M>().Where(compareFunc).DeleteAsync();
            }
        }

        /// <summary>
        /// Updater 便捷 UpdateAsync update fields 方法
        /// </summary>
        public static async Task<int> UpdateAsync<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, dynamic filedsObject, SetEnum set = SetEnum.AllowedNull)
            where M : class, new()
        {
            return await conn.Updater<M>().Set(filedsObject as object).Where(compareFunc).UpdateAsync(set);
        }

        /******************************************************************************************************************************/

        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<M> QueryOneAsync<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<VM> QueryOneAsync<M, VM>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
            where VM : class
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync<VM>();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<T> QueryOneAsync<M, T>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, T>> columnMapFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc);
        }

        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<List<M>> QueryListAsync<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
        {
            if (compareFunc == null)
            {
                return await conn.Queryer<M>().QueryListAsync();
            }
            else
            {
                return await conn.Queryer<M>().Where(compareFunc).QueryListAsync();
            }
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<List<VM>> QueryListAsync<M, VM>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
            where VM : class
        {
            if (compareFunc == null)
            {
                return await conn.Queryer<M>().QueryListAsync<VM>();
            }
            else
            {
                return await conn.Queryer<M>().Where(compareFunc).QueryListAsync<VM>();
            }
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<List<T>> QueryListAsync<M, T>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, T>> columnMapFunc)
            where M : class, new()
        {
            if (compareFunc == null)
            {
                return await conn.Queryer<M>().QueryListAsync(columnMapFunc);
            }
            else
            {
                return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc);
            }
        }

        /// <summary>
        /// 请参阅: <see langword=".IsExistAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<bool> IsExistAsync<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).IsExistAsync();
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
        /// Queryer 便捷-同步 QueryOneAsync 方法
        /// </summary>
        public static M QueryOne<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
        {
            return (conn.QueryOneAsync(compareFunc)).GetAwaiter().GetResult();
        }
        /// <summary>
        /// Queryer 便捷-同步 QueryOneAsync 方法
        /// </summary>
        public static VM QueryOne<M, VM>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
            where VM : class
        {
            return (conn.QueryOneAsync<M, VM>(compareFunc)).GetAwaiter().GetResult();
        }
        /// <summary>
        /// Queryer 便捷-同步 QueryOneAsync 方法
        /// </summary>
        public static T QueryOne<M, T>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, T>> columnMapFunc)
            where M : class, new()
        {
            return (conn.QueryOneAsync(compareFunc, columnMapFunc)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Queryer 便捷-同步 IsExistAsync 方法
        /// </summary>
        public static bool IsExist<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
        {
            return (conn.IsExistAsync(compareFunc)).GetAwaiter().GetResult();
        }

        /******************************************************************************************************************************/

        public static async Task<int> ExecuteNonQueryAsync(this IDbConnection conn, string sql, List<XParam> dbParas = null)
        {
            var dc = new XContext(conn)
            {
                Crud = CrudEnum.SQL
            };
            dc.ParseSQL(sql);
            dc.ParseParam(dbParas);
            return await new ExecuteNonQuerySQLImpl(dc).ExecuteNonQueryAsync();
        }

        public static async Task<T> QueryOneAsync<T>(this IDbConnection conn, string sql, List<XParam> dbParas = null)
        {
            var dc = new XContext(conn)
            {
                Crud = CrudEnum.SQL
            };
            dc.ParseSQL(sql);
            dc.ParseParam(dbParas);
            return await new QueryOneSQLImpl(dc).QueryOneAsync<T>();
        }

        public static async Task<List<T>> QueryListAsync<T>(this IDbConnection conn, string sql, List<XParam> dbParas = null)
        {
            var dc = new XContext(conn)
            {
                Crud = CrudEnum.SQL
            };
            dc.ParseSQL(sql);
            dc.ParseParam(dbParas);
            return await new QueryListSQLImpl(dc).QueryListAsync<T>();
        }

        public static async Task<PagingResult<T>> QueryPagingAsync<T>
            (this IDbConnection conn, int pageIndex, int pageSize, string totalCountSql, string pageDataSql, List<XParam> dbParas = null)
        {
            var dc = new XContext(conn)
            {
                Crud = CrudEnum.SQL
            };
            dc.PageIndex = pageIndex;
            dc.PageSize = pageSize;
            dc.ParseSQL(totalCountSql, pageDataSql);
            dc.ParseParam(dbParas);
            return await new QueryPagingSQLImpl(dc).QueryPagingAsync<T>();
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
