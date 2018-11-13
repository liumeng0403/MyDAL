using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.Core;
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
    /// This is ORM lite start point. 
    /// 博客:  https://www.cnblogs.com/Meng-NET/p/8963476.html  
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
            where M:class
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
        public static Selecter<M> Selecter<M>(this IDbConnection conn)
            where M:class
        {
            var dc = new XContext<M>(conn);
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
            var dc = new XContext<M1, M2>(conn);
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
            var dc = new XContext<M1, M2, M3>(conn);
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
            var dc = new XContext<M1, M2, M3, M4>(conn);
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
            var dc = new XContext<M1, M2, M3, M4, M5>(conn);
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
            var dc = new XContext<M1, M2, M3, M4, M5, M6>(conn);
            dc.Crud = CrudTypeEnum.Join;
            return new Joiner(dc);
        }

        /******************************************************************************************************************************/

        /// <summary>
        /// Creater 快速 CreateAsync 方法
        /// </summary>
        public static async Task<int> CreateAsync<M>(this IDbConnection conn, M m)
            where M:class
        {
            return await conn.Creater<M>().CreateAsync(m);
        }

        /// <summary>
        /// Creater 快速 CreateBatchAsync 方法
        /// </summary>
        public static async Task<int> CreateBatchAsync<M>(this IDbConnection conn, IEnumerable<M> mList)
            where M:class
        {
            return await conn.Creater<M>().CreateBatchAsync(mList);
        }

        /// <summary>
        /// Deleter 快速 DeleteAsync by pk 方法
        /// </summary>
        public static async Task<int> DeleteAsync<M>(this IDbConnection conn, object pkValue)
            where M:class
        {
            var deleter = conn.Deleter<M>();
            var option = new QuickOption().GetCondition() as IDictionary<string, object>;
            option[deleter.DC.SqlProvider.GetTablePK(typeof(M).FullName)] = pkValue;
            return await deleter.Where(option).DeleteAsync();
        }

        /// <summary>
        /// Updater 快速 UpdateAsync update fields by pk 方法
        /// </summary>
        public static async Task<int> UpdateAsync<M>(this IDbConnection conn, object pkValue, dynamic filedsObject)
            where M:class
        {
            var updater = conn.Updater<M>();
            var option = new QuickOption().GetCondition() as IDictionary<string, object>;
            option[updater.DC.SqlProvider.GetTablePK(typeof(M).FullName)] = pkValue;
            return await updater.Set(filedsObject as object).Where(option).UpdateAsync();
        }

        /// <summary>
        /// Selecter 快速 QueryFirstOrDefaultAsync by pk 方法
        /// </summary>
        public static async Task<M> GetAsync<M>(this IDbConnection conn, object pkValue)
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
        public static async Task<VM> GetAsync<M, VM>(this IDbConnection conn, object pkValue)
            where M : class
            where VM:class
        {
            var selecter = conn.Selecter<M>();
            var option = new QuickOption().GetCondition() as IDictionary<string, object>;
            option[selecter.DC.SqlProvider.GetTablePK(typeof(M).FullName)] = pkValue;
            return await selecter.Where(option).QueryFirstOrDefaultAsync<VM>();
        }

        /// <summary>
        /// Selecter 快速 QueryFirstOrDefaultAsync by pk 方法
        /// </summary>
        public static async Task<VM> GetAsync<M, VM>(this IDbConnection conn, object pkValue, Expression<Func<M, VM>> columnMapFunc)
            where M : class
            where VM:class
        {
            var selecter = conn.Selecter<M>();
            var option = new QuickOption().GetCondition() as IDictionary<string, object>;
            option[selecter.DC.SqlProvider.GetTablePK(typeof(M).FullName)] = pkValue;
            return await selecter.Where(option).QueryFirstOrDefaultAsync<VM>(columnMapFunc);
        }

        /// <summary>
        /// Selecter 快速 QueryFirstOrDefaultAsync 方法
        /// </summary>
        public static async Task<M> FirstOrDefaultAsync<M>(this IDbConnection conn, QueryOption option)
            where M : class
        {
            return await conn.Selecter<M>().Where(option).QueryFirstOrDefaultAsync();
        }
        /// <summary>
        /// Selecter 快速 QueryFirstOrDefaultAsync 方法
        /// </summary>
        public static async Task<VM> FirstOrDefaultAsync<M, VM>(this IDbConnection conn, QueryOption option)
            where M : class
            where VM:class
        {
            return await conn.Selecter<M>().Where(option).QueryFirstOrDefaultAsync<VM>();
        }
        /// <summary>
        /// Selecter 快速 QueryFirstOrDefaultAsync 方法
        /// </summary>
        public static async Task<VM> FirstOrDefaultAsync<M, VM>(this IDbConnection conn, QueryOption option, Expression<Func<M, VM>> columnMapFunc)
            where M : class
            where VM:class
        {
            return await conn.Selecter<M>().Where(option).QueryFirstOrDefaultAsync<VM>(columnMapFunc);
        }

        /// <summary>
        /// Selecter 快速 QueryFirstOrDefaultAsync 方法
        /// </summary>
        public static async Task<M> FirstOrDefaultAsync<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            return await conn.Selecter<M>().Where(compareFunc).QueryFirstOrDefaultAsync();
        }
        /// <summary>
        /// Selecter 快速 QueryFirstOrDefaultAsync 方法
        /// </summary>
        public static async Task<VM> FirstOrDefaultAsync<M, VM>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class
            where VM : class
        {
            return await conn.Selecter<M>().Where(compareFunc).QueryFirstOrDefaultAsync<VM>();
        }
        /// <summary>
        /// Selecter 快速 QueryFirstOrDefaultAsync 方法
        /// </summary>
        public static async Task<VM> FirstOrDefaultAsync<M, VM>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, VM>> columnMapFunc)
            where M : class
            where VM : class
        {
            return await conn.Selecter<M>().Where(compareFunc).QueryFirstOrDefaultAsync<VM>(columnMapFunc);
        }

        /// <summary>
        /// Selecter 快速 QueryListAsync 方法
        /// </summary>
        public static async Task<List<M>> ListAsync<M>(this IDbConnection conn, QueryOption option)
            where M : class
        {
            return await conn.Selecter<M>().Where(option).QueryListAsync();
        }
        /// <summary>
        /// Selecter 快速 QueryListAsync 方法
        /// </summary>
        public static async Task<List<VM>> ListAsync<M, VM>(this IDbConnection conn, QueryOption option)
            where M : class
            where VM:class
        {
            return await conn.Selecter<M>().Where(option).QueryListAsync<VM>();
        }
        /// <summary>
        /// Selecter 快速 QueryListAsync 方法
        /// </summary>
        public static async Task<List<VM>> ListAsync<M, VM>(this IDbConnection conn, QueryOption option, Expression<Func<M, VM>> columnMapFunc)
            where M : class
            where VM:class
        {
            return await conn.Selecter<M>().Where(option).QueryListAsync<VM>(columnMapFunc);
        }

        /// <summary>
        /// Selecter 快速 QueryListAsync 方法
        /// </summary>
        public static async Task<List<M>> ListAsync<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            return await conn.Selecter<M>().Where(compareFunc).QueryListAsync();
        }
        /// <summary>
        /// Selecter 快速 QueryListAsync 方法
        /// </summary>
        public static async Task<List<VM>> ListAsync<M, VM>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class
            where VM : class
        {
            return await conn.Selecter<M>().Where(compareFunc).QueryListAsync<VM>();
        }
        /// <summary>
        /// Selecter 快速 QueryListAsync 方法
        /// </summary>
        public static async Task<List<VM>> ListAsync<M, VM>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, VM>> columnMapFunc)
            where M : class
            where VM : class
        {
            return await conn.Selecter<M>().Where(compareFunc).QueryListAsync<VM>(columnMapFunc);
        }

        /// <summary>
        /// Selecter 快速 QueryPagingListAsync 方法
        /// </summary>
        public static async Task<PagingList<M>> PagingListAsync<M>(this IDbConnection conn, PagingQueryOption option)
            where M : class
        {
            return await conn.Selecter<M>().Where(option).QueryPagingListAsync(option);
        }
        /// <summary>
        /// Selecter 快速 QueryPagingListAsync 方法
        /// </summary>
        public static async Task<PagingList<VM>> PagingListAsync<M, VM>(this IDbConnection conn, PagingQueryOption option)
            where M : class
            where VM:class
        {
            return await conn.Selecter<M>().Where(option).QueryPagingListAsync<VM>(option);
        }
        /// <summary>
        /// Selecter 快速 QueryPagingListAsync 方法
        /// </summary>
        public static async Task<PagingList<VM>> PagingListAsync<M, VM>(this IDbConnection conn, PagingQueryOption option, Expression<Func<M, VM>> columnMapFunc)
            where M : class
            where VM:class
        {
            return await conn.Selecter<M>().Where(option).QueryPagingListAsync<VM>(option, columnMapFunc);
        }

        /// <summary>
        /// Selecter 快速 ExistAsync 方法
        /// </summary>
        public static async Task<bool> ExistAsync<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc)
            where M:class
        {
            return await conn.Selecter<M>().Where(compareFunc).ExistAsync();
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
        //public static IDbConnection OpenCodeFirst(this IDbConnection conn, string modelsNamespace)
        //{
        //    XConfig.IsCodeFirst = true;
        //    XConfig.TablesNamespace = modelsNamespace;
        //    if (XConfig.IsNeedChangeDb)
        //    {
        //        (new CodeFirstHelper(new DbContext(conn)).CodeFirstProcess(conn)).GetAwaiter().GetResult();
        //    }
        //    return conn;
        //}

    }
}
