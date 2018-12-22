using MyDAL.Test.Entities;
using MyDAL.Test.Enums;
using MyDAL.Test.TestModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Yunyong.DataExchange;

namespace MyDAL.Test
{
    public abstract class TestBase
    {
        protected WhereTestModel WhereTest
        {
            get
            {
                return new WhereTestModel
                {
                    CreatedOn = Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30),
                    StartTime = Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30),
                    EndTime = DateTime.Now,
                    AgentLevelXX = AgentLevel.DistiAgent,
                    AgentLevelNull = null,
                    ContainStr = "~00-d-3-1-",
                    ContainStr2 = "~00-d-3-",
                    In_List_枚举 = new List<AgentLevel?>
                    {
                        AgentLevel.CityAgent,
                        AgentLevel.DistiAgent
                    },
                    In_Array_枚举 = new AgentLevel?[]
                    {
                        AgentLevel.CityAgent,
                        AgentLevel.DistiAgent
                    },
                    In_List_String = new List<string>
                    {
                        "黄银凤",
                        "刘建芬"
                    },
                    In_Array_String = new string[]
                    {
                        "黄银凤",
                        "刘建芬"
                    }
                };
            }
        }

        protected LikeTestModel LikeTest
        {
            get
            {
                return new LikeTestModel
                {
                    无通配符 = "陈",
                    百分号 = "陈%",
                    下划线 = "王_",
                    百分号转义 = "刘/%_",
                    下划线转义 = "何/__"
                };
            }
        }

        protected IDbConnection Conn
        {
            get
            {
                return GetMySQLConnection();  // MySql
                //return GetTSQLConnection();  // SQL Server
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
                              //.OpenDB()  // 建议 每次新实例并打开,以获得更好的性能体验, 但是 用完要注意手动释放, 防止 连接池 资源耗尽!!!
                ;
        }
        private static IDbConnection GetTSQLConnection()
        {
            //
            // Nuget : Package : System.Data.SqlClient
            //
            return
                new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=MyDAL_TestDB;User Id=sa;Password=1010;")
                .OpenDebug()  // 全局 debug 配置, 生产环境不要开启 
                              //.OpenDB()  // 建议 每次新实例并打开,以获得更好的性能体验, 但是 用完要注意手动释放, 防止 连接池 资源耗尽!!!
                ;
        }

    }

}
