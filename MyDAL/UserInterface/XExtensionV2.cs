using HPC.DAL.Core;
using HPC.DAL.Core.Enums;
using HPC.DAL.Impls.ImplAsyncs;
using HPC.DAL.Impls.ImplSyncs;
using HPC.DAL.Tools;
using HPC.DAL.UserFacade.Create;
using HPC.DAL.UserFacade.Delete;
using HPC.DAL.UserFacade.Join;
using HPC.DAL.UserFacade.Query;
using HPC.DAL.UserFacade.Update;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HPC.DAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static partial class XExtension
    {
        #region Internal
        /* 内部方法 */
        private static Creater<M> Creater<M>(this XConnection conn)
            where M : class, new()
        {
            var dc = new XContext<M>(conn.Conn)
            {
                Crud = CrudEnum.Create
            };
            return new Creater<M>(dc);
        }
        private static void CheckCreate(string sql)
        {
            if (sql.IsNullStr())
            {
                throw XConfig.EC.Exception(XConfig.EC._080, "Create SQL 语句不能为空！");
            }
            if (!sql.Contains("insert")
                && !sql.Contains("INSERT")
                && !sql.Contains("Insert"))
            {
                throw XConfig.EC.Exception(XConfig.EC._081, "Create API 只能用来新增数据！");
            }
        }
        private static void CheckDelete(string sql)
        {
            if (sql.IsNullStr())
            {
                throw XConfig.EC.Exception(XConfig.EC._082, "Delete SQL 语句不能为空！");
            }
            if (!sql.Contains("delete")
                && !sql.Contains("DELETE")
                && !sql.Contains("Delete"))
            {
                throw XConfig.EC.Exception(XConfig.EC._083, "Delete API 只能用来删除数据！");
            }
        }
        private static void CheckUpdate(string sql)
        {
            if (sql.IsNullStr())
            {
                throw XConfig.EC.Exception(XConfig.EC._084, "Update SQL 语句不能为空！");
            }
            if (!sql.Contains("update")
                && !sql.Contains("UPDATE")
                && !sql.Contains("Update"))
            {
                throw XConfig.EC.Exception(XConfig.EC._085, "Update API 只能用来更新数据！");
            }
        }
        private static void CheckQuery(string sql)
        {
            if (sql.IsNullStr())
            {
                throw XConfig.EC.Exception(XConfig.EC._086, "Query SQL 语句不能为空！");
            }
            if (sql.Contains("insert")
                || sql.Contains("delete")
                || sql.Contains("update")
                || sql.Contains("INSERT")
                || sql.Contains("DELETE")
                || sql.Contains("UPDATE")
                || sql.Contains("Insert")
                || sql.Contains("Delete")
                || sql.Contains("Update"))
            {
                throw XConfig.EC.Exception(XConfig.EC._087, "Query API 只能用来查询数据！");
            }
        }
        private static XContext DcForSQL(XConnection conn, string sql, List<XParam> dbParas)
        {
            var dc = new XContext(conn.Conn)
            {
                Crud = CrudEnum.SQL
            };
            dc.ParseSQL(sql);
            dc.ParseParam(dbParas);
            return dc;
        }
        [Obsolete("警告：此 API 后面会移除！！！", false)]
        public static async Task<int> ExecuteNonQueryAsync(this XConnection conn, string sql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            var dc = DcForSQL(conn, sql, dbParas);
            return await new ExecuteNonQuerySQLAsyncImpl(dc).ExecuteNonQueryAsync(tran);
        }
        [Obsolete("警告：此 API 后面会移除！！！", false)]
        public static int ExecuteNonQuery(this XConnection conn, string sql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            var dc = DcForSQL(conn, sql, dbParas);
            return new ExecuteNonQuerySQLImpl(dc).ExecuteNonQuery(tran);
        }

        /*-------------------------------------------------------------*/

        [Obsolete("警告：此 API 后面会移除！！！", false)]
        public static async Task<PagingResult<T>> QueryPagingAsync<T>
            (this XConnection conn, PagingResult<T> paging, string totalCountSql, string pageDataSql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            var dc = new XContext(conn.Conn)
            {
                Crud = CrudEnum.SQL
            };
            dc.PageIndex = paging.PageIndex;
            dc.PageSize = paging.PageSize;
            dc.ParseSQL(totalCountSql, pageDataSql);
            dc.ParseParam(dbParas);
            var result = await new QueryPagingSQLAsyncImpl(dc).QueryPagingAsync<T>(tran);
            paging.TotalCount = result.TotalCount;
            paging.Data = result.Data;
            return paging;
        }
        [Obsolete("警告：此 API 后面会移除！！！", false)]
        public static PagingResult<T> QueryPaging<T>
            (this XConnection conn, PagingResult<T> paging, string totalCountSql, string pageDataSql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            var dc = new XContext(conn.Conn)
            {
                Crud = CrudEnum.SQL
            };
            dc.PageIndex = paging.PageIndex;
            dc.PageSize = paging.PageSize;
            dc.ParseSQL(totalCountSql, pageDataSql);
            dc.ParseParam(dbParas);
            var result = new QueryPagingSQLImpl(dc).QueryPaging<T>(tran);
            paging.TotalCount = result.TotalCount;
            paging.Data = result.Data;
            return paging;
        }
        #endregion

        #region Deleter
        /// <summary>
        /// 删除数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Deleter<M> Deleter<M>(this XConnection conn)
            where M : class, new()
        {
            var dc = new XContext<M>(conn.Conn)
            {
                Crud = CrudEnum.Delete
            };
            return new Deleter<M>(dc);
        }
        #endregion

        #region Updater
        /// <summary>
        /// 修改数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Updater<M> Updater<M>(this XConnection conn)
            where M : class, new()
        {
            var dc = new XContext<M>(conn.Conn)
            {
                Crud = CrudEnum.Update
            };
            return new Updater<M>(dc);
        }
        #endregion

        #region Queryer
        /// <summary>
        /// 单表查询 方法簇
        /// </summary>
        /// <typeparam name="M1">M1:与DB Table 一 一对应</typeparam>
        public static Queryer<M1> Queryer<M1>(this XConnection conn)
            where M1 : class, new()
        {
            var dc = new XContext<M1>(conn.Conn)
            {
                Crud = CrudEnum.Query
            };
            return new Queryer<M1>(dc);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Queryer Queryer<M1, M2>(this XConnection conn, out M1 table1, out M2 table2)
            where M1 : class, new()
            where M2 : class, new()
        {
            table1 = new M1();
            table2 = new M2();
            var dc = new XContext<M1, M2>(conn.Conn)
            {
                Crud = CrudEnum.Join
            };
            return new Queryer(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Queryer Queryer<M1, M2, M3>(this XConnection conn, out M1 table1, out M2 table2, out M3 table3)
            where M1 : class, new()
            where M2 : class, new()
            where M3 : class, new()
        {
            table1 = new M1();
            table2 = new M2();
            table3 = new M3();
            var dc = new XContext<M1, M2, M3>(conn.Conn)
            {
                Crud = CrudEnum.Join
            };
            return new Queryer(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Queryer Queryer<M1, M2, M3, M4>(this XConnection conn, out M1 table1, out M2 table2, out M3 table3, out M4 table4)
            where M1 : class, new()
            where M2 : class, new()
            where M3 : class, new()
            where M4 : class, new()
        {
            table1 = new M1();
            table2 = new M2();
            table3 = new M3();
            table4 = new M4();
            var dc = new XContext<M1, M2, M3, M4>(conn.Conn)
            {
                Crud = CrudEnum.Join
            };
            return new Queryer(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Queryer Queryer<M1, M2, M3, M4, M5>(this XConnection conn, out M1 table1, out M2 table2, out M3 table3, out M4 table4, out M5 table5)
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
            var dc = new XContext<M1, M2, M3, M4, M5>(conn.Conn)
            {
                Crud = CrudEnum.Join
            };
            return new Queryer(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Queryer Queryer<M1, M2, M3, M4, M5, M6>(this XConnection conn, out M1 table1, out M2 table2, out M3 table3, out M4 table4, out M5 table5, out M6 table6)
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
            var dc = new XContext<M1, M2, M3, M4, M5, M6>(conn.Conn)
            {
                Crud = CrudEnum.Join
            };
            return new Queryer(dc);
        }
        #endregion

        #region Create API
        /// <summary>
        /// Creater 便捷 CreateAsync 方法
        /// </summary>
        public static async Task<int> CreateAsync<M>(this XConnection conn, M m, IDbTransaction tran = null)
            where M : class, new()
        {
            return await conn.Creater<M>().CreateAsync(m, tran);
        }

        /*-------------------------------------------------------------*/

        public static async Task<int> CreateAsync(this XConnection conn, string sql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            CheckCreate(sql);
            return await conn.ExecuteNonQueryAsync(sql, dbParas, tran);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// Creater 便捷 CreateAsync 方法
        /// </summary>
        public static int Create<M>(this XConnection conn, M m, IDbTransaction tran = null)
            where M : class, new()
        {
            return conn.Creater<M>().Create(m, tran);
        }

        /*-------------------------------------------------------------*/

        public static int Create(this XConnection conn, string sql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            CheckCreate(sql);
            return conn.ExecuteNonQuery(sql, dbParas, tran);
        }
        #endregion

        #region CreateBatch API
        /// <summary>
        /// Creater 便捷 CreateBatchAsync 方法
        /// </summary>
        public static async Task<int> CreateBatchAsync<M>(this XConnection conn, IEnumerable<M> mList, IDbTransaction tran = null)
            where M : class, new()
        {
            return await conn.Creater<M>().CreateBatchAsync(mList, tran);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// Creater 便捷 CreateBatchAsync 方法
        /// </summary>
        public static int CreateBatch<M>(this XConnection conn, IEnumerable<M> mList, IDbTransaction tran = null)
            where M : class, new()
        {
            return conn.Creater<M>().CreateBatch(mList, tran);
        }
        #endregion

        #region Delete API
        /// <summary>
        /// Deleter 便捷 DeleteAsync 方法
        /// </summary>
        public static async Task<int> DeleteAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return await conn.Deleter<M>().Where(compareFunc).DeleteAsync(tran);
        }

        /*-------------------------------------------------------------*/

        public static async Task<int> DeleteAsync(this XConnection conn, string sql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            CheckDelete(sql);
            return await conn.ExecuteNonQueryAsync(sql, dbParas, tran);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// Deleter 便捷 DeleteAsync 方法
        /// </summary>
        public static int Delete<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return conn.Deleter<M>().Where(compareFunc).Delete(tran);
        }

        /*-------------------------------------------------------------*/

        public static int Delete(this XConnection conn, string sql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            CheckDelete(sql);
            return conn.ExecuteNonQuery(sql, dbParas, tran);
        }
        #endregion

        #region Update API
        /// <summary>
        /// 请参阅: <see langword=".UpdateAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<int> UpdateAsync<M>
            (this XConnection conn, Expression<Func<M, bool>> compareFunc, dynamic filedsObject, IDbTransaction tran = null, SetEnum set = SetEnum.AllowedNull)
            where M : class, new()
        {
            return await conn.Updater<M>().Set(filedsObject as object).Where(compareFunc).UpdateAsync(tran, set);
        }

        /*-------------------------------------------------------------*/

        public static async Task<int> UpdateAsync(this XConnection conn, string sql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            CheckUpdate(sql);
            return await conn.ExecuteNonQueryAsync(sql, dbParas, tran);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// Updater 便捷 UpdateAsync update fields 方法
        /// </summary>
        public static int Update<M>
            (this XConnection conn, Expression<Func<M, bool>> compareFunc, dynamic filedsObject, IDbTransaction tran = null, SetEnum set = SetEnum.AllowedNull)
            where M : class, new()
        {
            return conn.Updater<M>().Set(filedsObject as object).Where(compareFunc).Update(tran, set);
        }

        /*-------------------------------------------------------------*/

        public static int Update(this XConnection conn, string sql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            CheckUpdate(sql);
            return conn.ExecuteNonQuery(sql, dbParas, tran);
        }
        #endregion

        #region QueryOne API
        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<M> QueryOneAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(tran);
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<VM> QueryOneAsync<M, VM>(this XConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
            where VM : class
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync<VM>(tran);
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<T> QueryOneAsync<M, T>
            (this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc, tran);
        }

        /*-------------------------------------------------------------*/

        public static async Task<T> QueryOneAsync<T>(this XConnection conn, string sql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            CheckQuery(sql);
            var dc = DcForSQL(conn, sql, dbParas);
            return await new QueryOneSQLAsyncImpl(dc).QueryOneAsync<T>(tran);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// Queryer 便捷-同步 QueryOneAsync 方法
        /// </summary>
        public static M QueryOne<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(tran);
        }
        /// <summary>
        /// Queryer 便捷-同步 QueryOneAsync 方法
        /// </summary>
        public static VM QueryOne<M, VM>(this XConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
            where VM : class
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne<VM>(tran);
        }
        /// <summary>
        /// Queryer 便捷-同步 QueryOneAsync 方法
        /// </summary>
        public static T QueryOne<M, T>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc, tran);
        }

        /*-------------------------------------------------------------*/

        public static T QueryOne<T>(this XConnection conn, string sql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            CheckQuery(sql);
            var dc = DcForSQL(conn, sql, dbParas);
            return new QueryOneSQLImpl(dc).QueryOne<T>(tran);
        }
        #endregion

        #region QueryList API
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<List<M>> QueryListAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(tran);
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<List<VM>> QueryListAsync<M, VM>(this XConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
            where VM : class
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync<VM>(tran);
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<List<T>> QueryListAsync<M, T>
            (this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc, tran);
        }

        /*-------------------------------------------------------------*/

        public static async Task<List<T>> QueryListAsync<T>(this XConnection conn, string sql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            CheckQuery(sql);
            var dc = DcForSQL(conn, sql, dbParas);
            return await new QueryListSQLAsyncImpl(dc).QueryListAsync<T>(tran);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static List<M> QueryList<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(tran);
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static List<VM> QueryList<M, VM>(this XConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
            where VM : class
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList<VM>(tran);
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static List<T> QueryList<M, T>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc, tran);
        }

        /*-------------------------------------------------------------*/

        public static List<T> QueryList<T>(this XConnection conn, string sql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            CheckQuery(sql);
            var dc = DcForSQL(conn, sql, dbParas);
            return new QueryListSQLImpl(dc).QueryList<T>(tran);
        }
        #endregion

        #region IsExist API
        /// <summary>
        /// 请参阅: <see langword=".IsExistAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<bool> IsExistAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).IsExistAsync(tran);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// Queryer 便捷-同步 IsExistAsync 方法
        /// </summary>
        public static bool IsExist<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).IsExist(tran);
        }
        #endregion

        #region Count API
        /// <summary>
        /// Queryer 便捷 CountAsync 方法
        /// </summary>
        public static async Task<int> CountAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).CountAsync(tran);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// Queryer 便捷 CountAsync 方法
        /// </summary>
        public static int Count<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).Count(tran);
        }
        #endregion

        #region Sum API
        public static async Task<F> SumAsync<M, F>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, F>> propertyFunc, IDbTransaction tran = null)
            where M : class, new()
            where F : struct
        {
            return await conn.Queryer<M>().Where(compareFunc).SumAsync(propertyFunc, tran);
        }
        public static async Task<Nullable<F>> SumAsync<M, F>
            (this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, Nullable<F>>> propertyFunc, IDbTransaction tran = null)
            where M : class, new()
            where F : struct
        {
            return await conn.Queryer<M>().Where(compareFunc).SumAsync(propertyFunc, tran);
        }

        /*-------------------------------------------------------------*/

        public static F Sum<M, F>(this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, F>> propertyFunc, IDbTransaction tran = null)
            where M : class, new()
            where F : struct
        {
            return conn.Queryer<M>().Where(compareFunc).Sum(propertyFunc, tran);
        }
        public static Nullable<F> Sum<M, F>
            (this XConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, Nullable<F>>> propertyFunc, IDbTransaction tran = null)
            where M : class, new()
            where F : struct
        {
            return conn.Queryer<M>().Where(compareFunc).Sum(propertyFunc, tran);
        }
        #endregion

        #region ForUser
        /// <summary>
        /// Sql 调试跟踪 开启
        /// </summary>
        public static XConnection OpenDebug(this XConnection conn, DebugEnum type = DebugEnum.Output)
        {
            XConfig.IsDebug = true;
            XConfig.DebugType = type;
            return conn;
        }
        #endregion
    }
}
