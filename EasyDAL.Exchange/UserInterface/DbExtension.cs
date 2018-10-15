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
using System.Data;

namespace MyDAL
{
    public static class DbExtension
    {

        /******************************************************************************************************************************/

        /// <summary>
        /// 新建数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Creater<M> Creater<M>(this IDbConnection connection)
        {
            var dc = new DbContext<M>(connection);
            dc.Crud = CrudTypeEnum.Create;
            return new Creater<M>(dc);
        }

        /// <summary>
        /// 删除数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Deleter<M> Deleter<M>(this IDbConnection connection)
        {
            var dc = new DbContext<M>(connection);
            dc.Crud = CrudTypeEnum.Delete;
            return new Deleter<M>(dc);
        }

        /// <summary>
        /// 修改数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Updater<M> Updater<M>(this IDbConnection connection)
        {
            var dc = new DbContext<M>(connection);
            dc.Crud = CrudTypeEnum.Update;
            return new Updater<M>(dc);
        }

        /// <summary>
        /// 单表查询 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Selecter<M> Selecter<M>(this IDbConnection connection)
        {
            var dc = new DbContext<M>(connection);
            dc.Crud = CrudTypeEnum.Query;
            return new Selecter<M>(dc);
        }

        ///// <summary>
        ///// 连接查询 方法簇
        ///// </summary>
        //public static Joiner Joiner<M1>(this IDbConnection connection, out M1 table1)
        //{
        //    table1 = Activator.CreateInstance<M1>();
        //    return new Joiner(new DbContext<M1>(connection));
        //}
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Joiner Joiner<M1, M2>(this IDbConnection connection, out M1 table1, out M2 table2)
        {
            table1 = Activator.CreateInstance<M1>();
            table2 = Activator.CreateInstance<M2>();
            var dc = new DbContext<M1, M2>(connection);
            dc.Crud = CrudTypeEnum.Join;
            return new Joiner(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Joiner Joiner<M1, M2, M3>(this IDbConnection connection, out M1 table1, out M2 table2, out M3 table3)
        {
            table1 = Activator.CreateInstance<M1>();
            table2 = Activator.CreateInstance<M2>();
            table3 = Activator.CreateInstance<M3>();
            var dc = new DbContext<M1, M2, M3>(connection);
            dc.Crud = CrudTypeEnum.Join;
            return new Joiner(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Joiner Joiner<M1, M2, M3, M4>(this IDbConnection connection, out M1 table1, out M2 table2, out M3 table3, out M4 table4)
        {
            table1 = Activator.CreateInstance<M1>();
            table2 = Activator.CreateInstance<M2>();
            table3 = Activator.CreateInstance<M3>();
            table4 = Activator.CreateInstance<M4>();
            var dc = new DbContext<M1, M2, M3, M4>(connection);
            dc.Crud = CrudTypeEnum.Join;
            return new Joiner(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Joiner Joiner<M1, M2, M3, M4, M5>(this IDbConnection connection, out M1 table1, out M2 table2, out M3 table3, out M4 table4, out M5 table5)
        {
            table1 = Activator.CreateInstance<M1>();
            table2 = Activator.CreateInstance<M2>();
            table3 = Activator.CreateInstance<M3>();
            table4 = Activator.CreateInstance<M4>();
            table5 = Activator.CreateInstance<M5>();
            var dc = new DbContext<M1, M2, M3, M4, M5>(connection);
            dc.Crud = CrudTypeEnum.Join;
            return new Joiner(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Joiner Joiner<M1, M2, M3, M4, M5, M6>(this IDbConnection connection, out M1 table1, out M2 table2, out M3 table3, out M4 table4, out M5 table5, out M6 table6)
        {
            table1 = Activator.CreateInstance<M1>();
            table2 = Activator.CreateInstance<M2>();
            table3 = Activator.CreateInstance<M3>();
            table4 = Activator.CreateInstance<M4>();
            table5 = Activator.CreateInstance<M5>();
            table6 = Activator.CreateInstance<M6>();
            var dc = new DbContext<M1, M2, M3, M4, M5, M6>(connection);
            dc.Crud = CrudTypeEnum.Join;
            return new Joiner(dc);
        }

        /// <summary>
        /// 事务单元
        /// </summary>
        public static Transactioner Transactioner(this IDbConnection connection)
        {
            return new Transactioner(new DbContext(connection));
        }

        /******************************************************************************************************************************/

        public static IDbConnection OpenDB(this IDbConnection connection)
        {
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Sql 调试跟踪 开启
        /// </summary>
        public static IDbConnection OpenDebug(this IDbConnection connection)
        {
            XConfig.IsDebug = true;
            return connection;
        }

        public static IDbConnection OpenCodeFirst(this IDbConnection connection, string modelsNamespace)
        {
            XConfig.IsCodeFirst = true;
            XConfig.TablesNamespace = modelsNamespace;
            if (XConfig.IsNeedChangeDb)
            {
                (CodeFirstHelper.Instance.CodeFirstProcess(connection)).GetAwaiter().GetResult();
            }
            return connection;
        }



    }
}
