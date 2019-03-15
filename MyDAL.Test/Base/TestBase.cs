using MyDAL.Test.Entities;
using MyDAL.Test.Enums;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MyDAL.Test
{
    public abstract class TestBase
    {
        protected string xx { get; set; }
        protected (List<string>, List<string>, List<string>) tuple { get; set; }

        protected WhereTestModel WhereTest
        {
            get
            {
                return new WhereTestModel
                {
                    CreatedOn = Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30),
                    StartTime = Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30),
                    EndTime = DateTime.Now,
                    AgentLevelNull = null,
                    ContainStr2 = "~00-d-3-",
                    In_Array_枚举 = new AgentLevel?[]
                    {
                        AgentLevel.CityAgent,
                        AgentLevel.DistiAgent
                    },
                    In_Array_String = new string[]
                    {
                        "黄银凤",
                        "刘建芬"
                    }
                };
            }
        }

        /// <summary>
        /// MySQL
        /// </summary>
        protected IDbConnection Conn
        {
            get
            {
                return GetMySQLConnection();
            }
        }

        /// <summary>
        /// SqlServer 2012+
        /// </summary>
        protected IDbConnection Conn2
        {
            get
            {
                return GetTSQLConnection_2012Plus();
            }
        }

        /// <summary>
        /// SqlServer 2008R2
        /// </summary>
        protected IDbConnection Conn3
        {
            get
            {
                return GetTSQLConnection_2008R2();
            }
        }

        private static IDbConnection GetMySQLConnection()
        {
            //
            // Nuget : Package : MySql.Data
            //
            // 不同版本 mysql 连接字符串一般如下：
            // "Server=localhost; Database=MyDAL_TestDB; Uid=SkyUser; Pwd=Sky@4321;"
            // "Server=localhost; Database=MyDAL_TestDB; Uid=SkyUser; Pwd=Sky@4321;SslMode=none;"
            // "Server=localhost; Database=MyDAL_TestDB; Uid=SkyUser; Pwd=Sky@4321;SslMode=none;allowPublicKeyRetrieval=true;"
            //
            return
                new MySqlConnection("Server=localhost; Database=MyDAL_TestDB; Uid=SkyUser; Pwd=Sky@4321;SslMode=none;allowPublicKeyRetrieval=true;")
                .OpenDebug()  // 全局 debug 配置, 生产环境不要开启 
                              //.OpenAsync()  // 建议 每次新实例并打开,以获得更好的性能体验, 但是 用完要注意手动释放, 防止 连接池 资源耗尽!!!
                ;
        }
        private static IDbConnection GetTSQLConnection_2012Plus()
        {
            //
            // Nuget : Package : System.Data.SqlClient
            //
            return
                new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=MyDAL_TestDB;User Id=sa;Password=1010;")
                .OpenDebug()  // 全局 debug 配置, 生产环境不要开启 
                              //.OpenAsync()  // 建议 每次新实例并打开,以获得更好的性能体验, 但是 用完要注意手动释放, 防止 连接池 资源耗尽!!!
                ;
        }
        private static IDbConnection GetTSQLConnection_2008R2()
        {
            //
            // Nuget : Package : System.Data.SqlClient
            //
            return
                new SqlConnection("Data Source=127.0.0.1;Initial Catalog=MyDAL_TestDB;User Id=sa;Password=1010;")
                .OpenDebug()  // 全局 debug 配置, 生产环境不要开启 
                              //.OpenAsync()  // 建议 每次新实例并打开,以获得更好的性能体验, 但是 用完要注意手动释放, 防止 连接池 资源耗尽!!!
                ;
        }

    }

}
