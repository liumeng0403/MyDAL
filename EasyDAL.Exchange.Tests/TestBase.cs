using System;
using System.Data;
using Xunit;
using EasyDAL.Exchange.AdoNet;
using MySql.Data.MySqlClient;
using EasyDAL.Exchange.Tests.Entities;
using EasyDAL.Exchange.Tests.Enums;

namespace EasyDAL.Exchange.Tests
{
    public abstract class TestBase : IDisposable
    {
        protected static readonly bool IsAppVeyor = Environment.GetEnvironmentVariable("Appveyor")?.ToUpperInvariant() == "TRUE";

        public WhereTestModel testH
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


        protected IDbConnection _connection;
        protected IDbConnection connection => _connection ?? (_connection = GetOpenConnection());

        protected IDbConnection Conn = GetOpenConnection();
        public static IDbConnection GetOpenConnection(bool mars = false)
        {
            /*
             * CREATE DATABASE `rainbow_test_db20180817` 
            */
            var conn = new MySqlConnection("Server=localhost; Database=Rainbow_Test_DB20180817; Uid=SkyUser; Pwd=Sky@4321;SslMode=none;");
            conn.Open();
            return conn;
        }



 

        static TestBase()
        {
            Console.WriteLine("Dapper: " + typeof(SqlMapper).AssemblyQualifiedName);
            Console.WriteLine(".NET: " + Environment.Version);
            Console.Write("Loading native assemblies for SQL types...");
            try
            {
                SqlServerTypesLoader.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
                Console.WriteLine("done.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("failed.");
                Console.Error.WriteLine(ex.Message);
            }
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }

    [CollectionDefinition(Name, DisableParallelization = true)]
    public class NonParallelDefinition : TestBase
    {
        public const string Name = "NonParallel";
    }
}
