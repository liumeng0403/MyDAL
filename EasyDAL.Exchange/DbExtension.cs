
using EasyDAL.Exchange.Core.Create;
using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using System;
using System.Data;
using System.Threading.Tasks;

namespace EasyDAL.Exchange
{
    public static class DbExtension
    {
        /// <summary>
        /// 新建数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Core.Create.Creater<M> Creater<M>(this IDbConnection connection)
        {
            return new Core.Create.Creater<M>(new DbContext(connection));
        }

        /// <summary>
        /// 删除数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Core.Delete.Deleter<M> Deleter<M>(this IDbConnection connection)
        {
            return new Core.Delete.Deleter<M>(new DbContext(connection));
        }

        /// <summary>
        /// 修改数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Core.Update.Setter<M> Updater<M>(this IDbConnection connection)
        {
            return new Core.Update.Setter<M>(new DbContext(connection));
        }

        /// <summary>
        /// 单表查询 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Core.Query.Selecter<M> Selecter<M>(this IDbConnection connection)
        {
            return new Core.Query.Selecter<M>(new DbContext(connection));
        }

        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Core.Join.FromX Joiner(this IDbConnection connection)
        {
            return new Core.Join.FromX(new DbContext(connection));
        }

        /// <summary>
        /// Sql 调试跟踪 开启
        /// </summary>
        public static IDbConnection OpenDebug(this IDbConnection connection)
        {
            Debug.Hint = true;
            return connection;
        }

    }
}
