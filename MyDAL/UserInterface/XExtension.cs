using MyDAL.Core;
using MyDAL.Core.Enums;
using MyDAL.Impls.ImplSyncs;
using MyDAL.Tools;
using MyDAL.UserFacade.Delete;
using MyDAL.UserFacade.Join;
using MyDAL.UserFacade.Query;
using MyDAL.UserFacade.Update;
using System.Collections.Generic;
using MyDAL.UserFacade.Insert;

namespace MyDAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static partial class XExtension
    {

        /*
         *  剔除 sql server 支持, 拥抱开源 , 仅支持 mysql 
         *  [EditorBrowsable(EditorBrowsableState.Never)]
         *  [Obsolete("Use Register(Component.For<T>().Lifestyle.Is(lifestyle)) instead.")] 
         */

        #region Internal

        /* 内部方法 */
        private static Inserter<M> Inserter<M>(this XConnection conn)
            where M : class, new()
        {
            var dc = new XContext<M>(conn)
            {
                Crud = CrudEnum.Create
            };
            return new Inserter<M>(dc);
        }

        /*-------------------------------------------------------------*/

        private static void CheckCreate(string sql)
        {
            if (sql.IsBlank())
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
            if (sql.IsBlank())
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
            if (sql.IsBlank())
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
            if (sql.IsBlank())
            {
                throw XConfig.EC.Exception(XConfig.EC._086, "Select SQL 语句不能为空！");
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
                throw XConfig.EC.Exception(XConfig.EC._087, "Select API 只能用来查询数据！");
            }
        }
        private static XContext DcForSQL(XConnection conn, string sql, List<XParam> dbParas)
        {
            var dc = new XContext(conn)
            {
                Crud = CrudEnum.SQL
            };
            dc.ParseSQL(sql);
            dc.ParseParam(dbParas);
            return dc;
        }
        private static int ExecuteNonQuery(this XConnection conn, string sql, List<XParam> dbParas = null)
        {
            var dc = DcForSQL(conn, sql, dbParas);
            return new ExecuteNonQuerySQLImpl(dc).ExecuteNonQuery();
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
            var dc = new XContext<M>(conn)
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
            var dc = new XContext<M>(conn)
            {
                Crud = CrudEnum.Update
            };
            return new Updater<M>(dc);
        }
        
        #endregion

        #region Selecter

        /// <summary>
        /// 单表查询 方法簇
        /// </summary>
        /// <typeparam name="M1">M1:与DB Table 一 一对应</typeparam>
        public static Queryer<M1> Selecter<M1>(this XConnection conn)
            where M1 : class, new()
        {
            var dc = new XContext<M1>(conn)
            {
                Crud = CrudEnum.Query
            };
            return new Queryer<M1>(dc);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Queryer Selecter<M1, M2>(this XConnection conn, out M1 table1, out M2 table2)
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
        public static Queryer Selecter<M1, M2, M3>(this XConnection conn, out M1 table1, out M2 table2, out M3 table3)
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
        public static Queryer Selecter<M1, M2, M3, M4>(this XConnection conn, out M1 table1, out M2 table2, out M3 table3, out M4 table4)
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
        public static Queryer Selecter<M1, M2, M3, M4, M5>(this XConnection conn, out M1 table1, out M2 table2, out M3 table3, out M4 table4, out M5 table5)
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
        public static Queryer Selecter<M1, M2, M3, M4, M5, M6>(this XConnection conn, out M1 table1, out M2 table2, out M3 table3, out M4 table4, out M5 table5, out M6 table6)
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

        #endregion

    }
}
