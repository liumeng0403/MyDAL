using System;
using System.Data;
using Xunit;
using EasyDAL.Exchange.AdoNet;
using MySql.Data.MySqlClient;
using EasyDAL.Exchange.Tests.Entities;
using EasyDAL.Exchange.Tests.Enums;

namespace EasyDAL.Exchange.Tests
{
    public abstract class TestBase:IDisposable
    {
        protected WhereTestModel testH
        {
            get
            {
                return new WhereTestModel
                {
                    CreatedOn = DateTime.Now.AddDays(-10),
                    StartTime = DateTime.Now.AddDays(-10),
                    EndTime = DateTime.Now,
                    AgentLevelXX = AgentLevel.DistiAgent,
                    ContainStr = "~00-d-3-1-"
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
            return conn;
        }

        public void Dispose()
        {
            Conn.Close();
        }
    }

}
