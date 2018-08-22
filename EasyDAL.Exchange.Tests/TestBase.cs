using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using Xunit;
using EasyDAL.Exchange.AdoNet;
using MySql.Data.MySqlClient;
#if !NETCOREAPP1_0
using System.Threading;
#endif

namespace EasyDAL.Exchange.Tests
{
    public abstract class TestBase : IDisposable
    {
        protected static readonly bool IsAppVeyor = Environment.GetEnvironmentVariable("Appveyor")?.ToUpperInvariant() == "TRUE";

        public static string ConnectionString
        {
            get
            {
                var builder = new MySqlConnectionStringBuilder
                {
                    Server = "localhost",
                    Database = "Rainbow_Test_DB20180817",
                    UserID = "SkyUser",
                    Password = "Sky@4321",
                    SslMode = MySqlSslMode.None,
                    CharacterSet = "utf8mb4"
                };
                return builder.ConnectionString;
            }
        }

        protected IDbConnection _connection;
        protected IDbConnection connection => _connection ?? (_connection = GetOpenConnection());

        public static IDbConnection GetOpenConnection(bool mars = false)
        {
            var conn = new MySqlConnection("Server=localhost; Database=Rainbow_Test_DB20180817; Uid=SkyUser; Pwd=Sky@4321;SslMode=none;");
            conn.Open();
            return conn;
        }

        public SqlConnection GetClosedConnection()
        {
            var conn = new SqlConnection(ConnectionString);
            if (conn.State != ConnectionState.Closed) throw new InvalidOperationException("should be closed!");
            return conn;
        }

        protected static CultureInfo ActiveCulture
        {
            get { return Thread.CurrentThread.CurrentCulture; }
            set { Thread.CurrentThread.CurrentCulture = value; }
        }

        static TestBase()
        {
            Console.WriteLine("Dapper: " + typeof(SqlMapper).AssemblyQualifiedName);
            Console.WriteLine("Using Connectionstring: {0}", ConnectionString);
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
