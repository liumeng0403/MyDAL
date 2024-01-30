using MyDAL.Test;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Tools
{
    public class _02_Transaction
        : TestBase
    {


        [Fact]
        public void Tran_Outer_Conn_Manual_Using()
        {
            xx = string.Empty;

            var conn = MyDAL_TestDB;
            
            conn.Open();

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

            var conn = MyDAL_TestDB;

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

            using (var conn = MyDAL_TestDB)
            {
                
                conn.Open();

                conn.BeginTransaction();

                conn.CommitTransaction();

                conn.Close();
            }
            
            xx = string.Empty;
        }

        [Fact]
        public void Tran_Conn_Auto()
        {
            xx = string.Empty;

            var conn = MyDAL_TestDB;

            conn.BeginTransaction();

            conn.CommitTransaction();

            xx = string.Empty;
        }
    }
}
