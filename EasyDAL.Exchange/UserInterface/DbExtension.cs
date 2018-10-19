using MyDAL.Core;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Helper;
using MyDAL.UserFacade.Create;
using MyDAL.UserFacade.Delete;
using MyDAL.UserFacade.Join;
using MyDAL.UserFacade.Query;
using MyDAL.UserFacade.Transaction;
using MyDAL.UserFacade.Update;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL
{
    /// <summary>
    /// This is ORM lite start point. 
    /// 博客:  https://www.cnblogs.com/Meng-NET/  
    /// GitHub:  https://github.com/liumeng0403/MyDAL  
    /// NuGet:  https://www.nuget.org/packages/MyDAL/ 
    /// </summary>
    public static class DbExtension
    {

        /// <summary>
        /// 新建数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Creater<M> Creater<M>(this IDbConnection conn)
        {
            var dc = new DbContext<M>(conn);
            dc.Crud = CrudTypeEnum.Create;
            return new Creater<M>(dc);
        }
        /// <summary>
        /// 删除数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Deleter<M> Deleter<M>(this IDbConnection conn)
        {
            var dc = new DbContext<M>(conn);
            dc.Crud = CrudTypeEnum.Delete;
            return new Deleter<M>(dc);
        }
        /// <summary>
        /// 修改数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Updater<M> Updater<M>(this IDbConnection conn)
        {
            var dc = new DbContext<M>(conn);
            dc.Crud = CrudTypeEnum.Update;
            return new Updater<M>(dc);
        }
        /// <summary>
        /// 单表查询 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Selecter<M> Selecter<M>(this IDbConnection conn)
        {
            var dc = new DbContext<M>(conn);
            dc.Crud = CrudTypeEnum.Query;
            return new Selecter<M>(dc);
        }

        /******************************************************************************************************************************/

        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Joiner Joiner<M1, M2>(this IDbConnection conn, out M1 table1, out M2 table2)
        {
            table1 = Activator.CreateInstance<M1>();
            table2 = Activator.CreateInstance<M2>();
            var dc = new DbContext<M1, M2>(conn);
            dc.Crud = CrudTypeEnum.Join;
            return new Joiner(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Joiner Joiner<M1, M2, M3>(this IDbConnection conn, out M1 table1, out M2 table2, out M3 table3)
        {
            table1 = Activator.CreateInstance<M1>();
            table2 = Activator.CreateInstance<M2>();
            table3 = Activator.CreateInstance<M3>();
            var dc = new DbContext<M1, M2, M3>(conn);
            dc.Crud = CrudTypeEnum.Join;
            return new Joiner(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Joiner Joiner<M1, M2, M3, M4>(this IDbConnection conn, out M1 table1, out M2 table2, out M3 table3, out M4 table4)
        {
            table1 = Activator.CreateInstance<M1>();
            table2 = Activator.CreateInstance<M2>();
            table3 = Activator.CreateInstance<M3>();
            table4 = Activator.CreateInstance<M4>();
            var dc = new DbContext<M1, M2, M3, M4>(conn);
            dc.Crud = CrudTypeEnum.Join;
            return new Joiner(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Joiner Joiner<M1, M2, M3, M4, M5>(this IDbConnection conn, out M1 table1, out M2 table2, out M3 table3, out M4 table4, out M5 table5)
        {
            table1 = Activator.CreateInstance<M1>();
            table2 = Activator.CreateInstance<M2>();
            table3 = Activator.CreateInstance<M3>();
            table4 = Activator.CreateInstance<M4>();
            table5 = Activator.CreateInstance<M5>();
            var dc = new DbContext<M1, M2, M3, M4, M5>(conn);
            dc.Crud = CrudTypeEnum.Join;
            return new Joiner(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Joiner Joiner<M1, M2, M3, M4, M5, M6>(this IDbConnection conn, out M1 table1, out M2 table2, out M3 table3, out M4 table4, out M5 table5, out M6 table6)
        {
            table1 = Activator.CreateInstance<M1>();
            table2 = Activator.CreateInstance<M2>();
            table3 = Activator.CreateInstance<M3>();
            table4 = Activator.CreateInstance<M4>();
            table5 = Activator.CreateInstance<M5>();
            table6 = Activator.CreateInstance<M6>();
            var dc = new DbContext<M1, M2, M3, M4, M5, M6>(conn);
            dc.Crud = CrudTypeEnum.Join;
            return new Joiner(dc);
        }

        /******************************************************************************************************************************/

        /// <summary>
        /// Creater 快速 CreateAsync 方法
        /// </summary>
        public static async Task<int> CreateAsync<M>(this IDbConnection conn, M m)
        {
            return await conn.Creater<M>().CreateAsync(m);
        }

        /// <summary>
        /// Creater 快速 CreateBatchAsync 方法
        /// </summary>
        public static async Task<int> CreateBatchAsync<M>(this IDbConnection conn, IEnumerable<M> mList)
        {
            return await conn.Creater<M>().CreateBatchAsync(mList);
        }

        /// <summary>
        /// Deleter 快速 DeleteAsync by pk 方法
        /// </summary>
        public static async Task<int> DeleteAsync<M>(this IDbConnection conn, int pkValue)
        {
            var deleter = conn.Deleter<M>();
            var option = new QuickOption().GetCondition() as IDictionary<string, object>;
            option[deleter.DC.SqlProvider.GetTablePK(typeof(M).FullName)] = pkValue;
            return await deleter.Where(option).DeleteAsync();
        }
        /// <summary>
        /// Deleter 快速 DeleteAsync by pk 方法
        /// </summary>
        public static async Task<int> DeleteAsync<M>(this IDbConnection conn, long pkValue)
        {
            var deleter = conn.Deleter<M>();
            var option = new QuickOption().GetCondition() as IDictionary<string, object>;
            option[deleter.DC.SqlProvider.GetTablePK(typeof(M).FullName)] = pkValue;
            return await deleter.Where(option).DeleteAsync();
        }
        /// <summary>
        /// Deleter 快速 DeleteAsync by pk 方法
        /// </summary>
        public static async Task<int> DeleteAsync<M>(this IDbConnection conn, string pkValue)
        {
            var deleter = conn.Deleter<M>();
            var option = new QuickOption().GetCondition() as IDictionary<string, object>;
            option[deleter.DC.SqlProvider.GetTablePK(typeof(M).FullName)] = pkValue;
            return await deleter.Where(option).DeleteAsync();
        }
        /// <summary>
        /// Deleter 快速 DeleteAsync by pk 方法
        /// </summary>
        public static async Task<int> DeleteAsync<M>(this IDbConnection conn, Guid pkValue)
        {
            var deleter = conn.Deleter<M>();
            var option = new QuickOption().GetCondition() as IDictionary<string, object>;
            option[deleter.DC.SqlProvider.GetTablePK(typeof(M).FullName)] = pkValue;
            return await deleter.Where(option).DeleteAsync();
        }

        /// <summary>
        /// Updater 快速 UpdateAsync update fields by pk 方法
        /// </summary>
        public static async Task<int> UpdateAsync<M>(this IDbConnection conn, int pkValue, object filedsObject)
        {
            var updater = conn.Updater<M>();
            var option = new QuickOption() as IDictionary<string, object>;
            option[updater.DC.SqlProvider.GetTablePK(typeof(M).FullName)] = pkValue;
            return await updater.Set(filedsObject).Where(option).UpdateAsync();
        }
        /// <summary>
        /// Updater 快速 UpdateAsync update fields by pk 方法
        /// </summary>
        public static async Task<int> UpdateAsync<M>(this IDbConnection conn, long pkValue, object filedsObject)
        {
            var updater = conn.Updater<M>();
            var option = new QuickOption() as IDictionary<string, object>;
            option[updater.DC.SqlProvider.GetTablePK(typeof(M).FullName)] = pkValue;
            return await updater.Set(filedsObject).Where(option).UpdateAsync();
        }
        /// <summary>
        /// Updater 快速 UpdateAsync update fields by pk 方法
        /// </summary>
        public static async Task<int> UpdateAsync<M>(this IDbConnection conn, string pkValue, object filedsObject)
        {
            var updater = conn.Updater<M>();
            var option = new QuickOption() as IDictionary<string, object>;
            option[updater.DC.SqlProvider.GetTablePK(typeof(M).FullName)] = pkValue;
            return await updater.Set(filedsObject).Where(option).UpdateAsync();
        }
        /// <summary>
        /// Updater 快速 UpdateAsync update fields by pk 方法
        /// </summary>
        public static async Task<int> UpdateAsync<M>(this IDbConnection conn, Guid pkValue, object filedsObject)
        {
            var updater = conn.Updater<M>();
            var option = new QuickOption() as IDictionary<string, object>;
            option[updater.DC.SqlProvider.GetTablePK(typeof(M).FullName)] = pkValue;
            return await updater.Set(filedsObject).Where(option).UpdateAsync();
        }

        /// <summary>
        /// Selecter 快速 QueryFirstOrDefaultAsync by pk 方法
        /// </summary>
        public static async Task<M> QueryFirstOrDefaultAsync<M>(this IDbConnection conn, int pkValue)
            where M : class
        {
            var selecter = conn.Selecter<M>();
            var option = new QuickOption().GetCondition() as IDictionary<string, object>;
            option[selecter.DC.SqlProvider.GetTablePK(typeof(M).FullName)] = pkValue;
            return await selecter.Where(option).QueryFirstOrDefaultAsync();
        }
        /// <summary>
        /// Selecter 快速 QueryFirstOrDefaultAsync by pk 方法
        /// </summary>
        public static async Task<M> QueryFirstOrDefaultAsync<M>(this IDbConnection conn, long pkValue)
            where M : class
        {
            var selecter = conn.Selecter<M>();
            var option = new QuickOption().GetCondition() as IDictionary<string, object>;
            option[selecter.DC.SqlProvider.GetTablePK(typeof(M).FullName)] = pkValue;
            return await selecter.Where(option).QueryFirstOrDefaultAsync();
        }
        /// <summary>
        /// Selecter 快速 QueryFirstOrDefaultAsync by pk 方法
        /// </summary>
        public static async Task<M> QueryFirstOrDefaultAsync<M>(this IDbConnection conn, string pkValue)
            where M : class
        {
            var selecter = conn.Selecter<M>();
            var option = new QuickOption().GetCondition() as IDictionary<string, object>;
            option[selecter.DC.SqlProvider.GetTablePK(typeof(M).FullName)] = pkValue;
            return await selecter.Where(option).QueryFirstOrDefaultAsync();
        }
        /// <summary>
        /// Selecter 快速 QueryFirstOrDefaultAsync by pk 方法
        /// </summary>
        public static async Task<M> QueryFirstOrDefaultAsync<M>(this IDbConnection conn, Guid pkValue)
            where M : class
        {
            var selecter = conn.Selecter<M>();
            var option = new QuickOption().GetCondition() as IDictionary<string, object>;
            option[selecter.DC.SqlProvider.GetTablePK(typeof(M).FullName)] = pkValue;
            return await selecter.Where(option).QueryFirstOrDefaultAsync();
        }

        /// <summary>
        /// Selecter 快速 QueryFirstOrDefaultAsync by pk 方法
        /// </summary>
        public static async Task<VM> QueryFirstOrDefaultAsync<M, VM>(this IDbConnection conn, int pkValue)
            where M : class
        {
            var selecter = conn.Selecter<M>();
            var option = new QuickOption().GetCondition() as IDictionary<string, object>;
            option[selecter.DC.SqlProvider.GetTablePK(typeof(M).FullName)] = pkValue;
            return await selecter.Where(option).QueryFirstOrDefaultAsync<VM>();
        }
        /// <summary>
        /// Selecter 快速 QueryFirstOrDefaultAsync by pk 方法
        /// </summary>
        public static async Task<VM> QueryFirstOrDefaultAsync<M, VM>(this IDbConnection conn, long pkValue)
            where M : class
        {
            var selecter = conn.Selecter<M>();
            var option = new QuickOption().GetCondition() as IDictionary<string, object>;
            option[selecter.DC.SqlProvider.GetTablePK(typeof(M).FullName)] = pkValue;
            return await selecter.Where(option).QueryFirstOrDefaultAsync<VM>();
        }
        /// <summary>
        /// Selecter 快速 QueryFirstOrDefaultAsync by pk 方法
        /// </summary>
        public static async Task<VM> QueryFirstOrDefaultAsync<M, VM>(this IDbConnection conn, string pkValue)
            where M : class
        {
            var selecter = conn.Selecter<M>();
            var option = new QuickOption().GetCondition() as IDictionary<string, object>;
            option[selecter.DC.SqlProvider.GetTablePK(typeof(M).FullName)] = pkValue;
            return await selecter.Where(option).QueryFirstOrDefaultAsync<VM>();
        }
        /// <summary>
        /// Selecter 快速 QueryFirstOrDefaultAsync by pk 方法
        /// </summary>
        public static async Task<VM> QueryFirstOrDefaultAsync<M, VM>(this IDbConnection conn, Guid pkValue)
            where M : class
        {
            var selecter = conn.Selecter<M>();
            var option = new QuickOption().GetCondition() as IDictionary<string, object>;
            option[selecter.DC.SqlProvider.GetTablePK(typeof(M).FullName)] = pkValue;
            return await selecter.Where(option).QueryFirstOrDefaultAsync<VM>();
        }

        /// <summary>
        /// Selecter 快速 QueryFirstOrDefaultAsync by pk 方法
        /// </summary>
        public static async Task<VM> QueryFirstOrDefaultAsync<M, VM>(this IDbConnection conn, int pkValue, Expression<Func<M, VM>> columnMapFunc)
            where M : class
        {
            var selecter = conn.Selecter<M>();
            var option = new QuickOption().GetCondition() as IDictionary<string, object>;
            option[selecter.DC.SqlProvider.GetTablePK(typeof(M).FullName)] = pkValue;
            return await selecter.Where(option).QueryFirstOrDefaultAsync<VM>(columnMapFunc);
        }
        /// <summary>
        /// Selecter 快速 QueryFirstOrDefaultAsync by pk 方法
        /// </summary>
        public static async Task<VM> QueryFirstOrDefaultAsync<M, VM>(this IDbConnection conn, long pkValue, Expression<Func<M, VM>> columnMapFunc)
            where M : class
        {
            var selecter = conn.Selecter<M>();
            var option = new QuickOption().GetCondition() as IDictionary<string, object>;
            option[selecter.DC.SqlProvider.GetTablePK(typeof(M).FullName)] = pkValue;
            return await selecter.Where(option).QueryFirstOrDefaultAsync<VM>(columnMapFunc);
        }
        /// <summary>
        /// Selecter 快速 QueryFirstOrDefaultAsync by pk 方法
        /// </summary>
        public static async Task<VM> QueryFirstOrDefaultAsync<M, VM>(this IDbConnection conn, string pkValue, Expression<Func<M, VM>> columnMapFunc)
            where M : class
        {
            var selecter = conn.Selecter<M>();
            var option = new QuickOption().GetCondition() as IDictionary<string, object>;
            option[selecter.DC.SqlProvider.GetTablePK(typeof(M).FullName)] = pkValue;
            return await selecter.Where(option).QueryFirstOrDefaultAsync<VM>(columnMapFunc);
        }
        /// <summary>
        /// Selecter 快速 QueryFirstOrDefaultAsync by pk 方法
        /// </summary>
        public static async Task<VM> QueryFirstOrDefaultAsync<M, VM>(this IDbConnection conn, Guid pkValue, Expression<Func<M, VM>> columnMapFunc)
            where M : class
        {
            var selecter = conn.Selecter<M>();
            var option = new QuickOption().GetCondition() as IDictionary<string, object>;
            option[selecter.DC.SqlProvider.GetTablePK(typeof(M).FullName)] = pkValue;
            return await selecter.Where(option).QueryFirstOrDefaultAsync<VM>(columnMapFunc);
        }

        /// <summary>
        /// Selecter 快速 QueryFirstOrDefaultAsync 方法
        /// </summary>
        public static async Task<M> QueryFirstOrDefaultAsync<M>(this IDbConnection conn, QueryOption option)
            where M : class
        {
            return await conn.Selecter<M>().Where(option).QueryFirstOrDefaultAsync();
        }
        /// <summary>
        /// Selecter 快速 QueryFirstOrDefaultAsync 方法
        /// </summary>
        public static async Task<VM> QueryFirstOrDefaultAsync<M, VM>(this IDbConnection conn, QueryOption option)
            where M : class
        {
            return await conn.Selecter<M>().Where(option).QueryFirstOrDefaultAsync<VM>();
        }
        /// <summary>
        /// Selecter 快速 QueryFirstOrDefaultAsync 方法
        /// </summary>
        public static async Task<VM> QueryFirstOrDefaultAsync<M, VM>(this IDbConnection conn, QueryOption option, Expression<Func<M, VM>> columnMapFunc)
            where M : class
        {
            return await conn.Selecter<M>().Where(option).QueryFirstOrDefaultAsync<VM>(columnMapFunc);
        }

        /// <summary>
        /// Selecter 快速 QueryListAsync 方法
        /// </summary>
        public static async Task<List<M>> QueryListAsync<M>(this IDbConnection conn, QueryOption option)
            where M : class
        {
            return await conn.Selecter<M>().Where(option).QueryListAsync();
        }
        /// <summary>
        /// Selecter 快速 QueryListAsync 方法
        /// </summary>
        public static async Task<List<VM>> QueryListAsync<M, VM>(this IDbConnection conn, QueryOption option)
            where M : class
        {
            return await conn.Selecter<M>().Where(option).QueryListAsync<VM>();
        }
        /// <summary>
        /// Selecter 快速 QueryListAsync 方法
        /// </summary>
        public static async Task<List<VM>> QueryListAsync<M, VM>(this IDbConnection conn, QueryOption option, Expression<Func<M, VM>> columnMapFunc)
            where M : class
        {
            return await conn.Selecter<M>().Where(option).QueryListAsync<VM>(columnMapFunc);
        }

        /// <summary>
        /// Selecter 快速 QueryPagingListAsync 方法
        /// </summary>
        public static async Task<PagingList<M>> QueryPagingListAsync<M>(this IDbConnection conn, PagingQueryOption option)
            where M : class
        {
            return await conn.Selecter<M>().Where(option).QueryPagingListAsync(option);
        }
        /// <summary>
        /// Selecter 快速 QueryPagingListAsync 方法
        /// </summary>
        public static async Task<PagingList<VM>> QueryPagingListAsync<M, VM>(this IDbConnection conn, PagingQueryOption option)
            where M : class
        {
            return await conn.Selecter<M>().Where(option).QueryPagingListAsync<VM>(option);
        }
        /// <summary>
        /// Selecter 快速 QueryPagingListAsync 方法
        /// </summary>
        public static async Task<PagingList<VM>> QueryPagingListAsync<M, VM>(this IDbConnection conn, PagingQueryOption option, Expression<Func<M, VM>> columnMapFunc)
            where M : class
        {
            return await conn.Selecter<M>().Where(option).QueryPagingListAsync<VM>(option, columnMapFunc);
        }

        /******************************************************************************************************************************/

        /// <summary>
        /// 事务单元
        /// </summary>
        public static Transactioner Transactioner(this IDbConnection conn)
        {
            return new Transactioner(new DbContext(conn));
        }

        /******************************************************************************************************************************/

        public static IDbConnection OpenDB(this IDbConnection conn)
        {
            conn.Open();
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
        public static IDbConnection OpenCodeFirst(this IDbConnection conn, string modelsNamespace)
        {
            XConfig.IsCodeFirst = true;
            XConfig.TablesNamespace = modelsNamespace;
            if (XConfig.IsNeedChangeDb)
            {
                (new CodeFirstHelper(new DbContext(conn)).CodeFirstProcess(conn)).GetAwaiter().GetResult();
            }
            return conn;
        }

    }
}
