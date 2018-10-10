using MyDAL.Common;
using MyDAL.Core;
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
            return new Creater<M>(new DbContext<M>(connection));
        }

        /// <summary>
        /// 删除数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Deleter<M> Deleter<M>(this IDbConnection connection)
        {
            return new Deleter<M>(new DbContext<M>(connection));
        }

        /// <summary>
        /// 修改数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Updater<M> Updater<M>(this IDbConnection connection)
        {
            return new Updater<M>(new DbContext<M>(connection));
        }

        /// <summary>
        /// 单表查询 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Selecter<M> Selecter<M>(this IDbConnection connection)
        {
            return new Selecter<M>(new DbContext<M>(connection));
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
            return new Joiner(new DbContext<M1, M2>(connection));
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Joiner Joiner<M1, M2, M3>(this IDbConnection connection, out M1 table1, out M2 table2, out M3 table3)
        {
            table1 = Activator.CreateInstance<M1>();
            table2 = Activator.CreateInstance<M2>();
            table3 = Activator.CreateInstance<M3>();
            return new Joiner(new DbContext<M1, M2, M3>(connection));
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
            return new Joiner(new DbContext<M1, M2, M3, M4>(connection));
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
            return new Joiner(new DbContext<M1, M2, M3, M4, M5>(connection));
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
            return new Joiner(new DbContext<M1, M2, M3, M4, M5, M6>(connection));
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

        public static IDbConnection OpenCodeFirst(this IDbConnection connection)
        {
            XConfig.IsCodeFirst = true;

            return connection;
        }



    }
}
