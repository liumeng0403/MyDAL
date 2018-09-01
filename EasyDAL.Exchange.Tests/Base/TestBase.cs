using System;
using System.Data;
using Xunit;
using EasyDAL.Exchange.AdoNet;
using MySql.Data.MySqlClient;
using EasyDAL.Exchange.Tests.Entities;
using EasyDAL.Exchange.Tests.Enums;
using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Tests.TestModels;

namespace EasyDAL.Exchange.Tests
{
    public abstract class TestBase : IDisposable
    {
        protected WhereTestModel WhereTest
        {
            get
            {
                return new WhereTestModel
                {
                    CreatedOn = DateTime.Now.AddDays(-30),
                    DateTime_大于等于 = DateTime.Now.AddDays(-30),
                    DateTime_小于等于 = DateTime.Now,
                    AgentLevelXX = AgentLevel.DistiAgent,
                    ContainStr = "~00-d-3-1-"
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
                return GetOpenConnection();
            }
        }

        private static IDbConnection GetOpenConnection(bool mars = false)
        {
            /*
             * CREATE DATABASE `rainbow_test_db20180817` 
            */
            var conn = new MySqlConnection("Server=localhost; Database=Rainbow_Test_DB20180817; Uid=SkyUser; Pwd=Sky@4321;SslMode=none;");
            conn.Open();
            //Hints.Hint = true;  // 全局 Hint 配置, 生产环境不要开启 
            return conn;
        }

        public void Dispose()
        {
            Conn.Close();
        }
    }

}
