using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Tools
{
    public class _02_Transaction
        : TestBase
    {
        [Fact]
        public async Task Tran_Inner_Conn_Manual_Using()
        {
            xx = string.Empty;

            using (var conn = Conn)
            {

                await conn.OpenAsync();

                conn.BeginTransaction();

                conn.CommitTransaction();

                conn.Close();

            }

            xx = string.Empty;
        }

        [Fact]
        public async Task Tran_Outer_Conn_Manual_Using()
        {
            xx = string.Empty;

            var conn = Conn;

            await conn.OpenAsync();

            using (conn)
            {

                conn.BeginTransaction();

                conn.CommitTransaction();

            }

            conn.Close();

            xx = string.Empty;
        }

        [Fact]
        public void Tran_Conn_Auto_Using()
        {
            xx = string.Empty;

            var conn = Conn;

            using (conn)
            {

                conn.BeginTransaction();

                conn.CommitTransaction();

            }

            xx = string.Empty;
        }

        [Fact]
        public void Tran_Outer_Conn_Manual()
        {
            xx = string.Empty;

            var conn = Conn;

            conn.Open();

            conn.BeginTransaction();

            conn.CommitTransaction();

            conn.Close();

            xx = string.Empty;
        }

        [Fact]
        public void Tran_Conn_Auto()
        {
            xx = string.Empty;

            var conn = Conn;

            conn.BeginTransaction();

            conn.CommitTransaction();

            xx = string.Empty;
        }
    }
}
