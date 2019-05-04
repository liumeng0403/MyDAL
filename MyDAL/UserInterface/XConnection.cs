using System.Data;
using System.Threading.Tasks;

namespace MyDAL
{
    public sealed class XConnection
    {
        private bool AutoClose { get; set; }
        private void NeedClose()
        {
            /*
             * 仅限：
             * XConnection(IDbConnection conn)
             * 调用。
             */
            if (Conn.State == ConnectionState.Closed)
            {
                AutoClose = true;
            }
        }

        internal IDbConnection Conn { get; private set; }
        internal IDbTransaction Tran { get; private set; }

        private XConnection() { }
        public XConnection(IDbConnection conn)
        {
            Conn = conn;
            AutoClose = false;
            NeedClose();
        }

        /// <summary>
        /// 打开 DB 连接
        /// </summary>
        public void Open()
        {
            if (AutoClose)
            {
                new DataSourceSync().Open(Conn);
                AutoClose = false;
            }
        }
        /// <summary>
        /// 异步 打开 DB 连接
        /// </summary>
        public async Task OpenAsync()
        {
            if (AutoClose)
            {
                await new DataSourceAsync().OpenAsync(Conn);
                AutoClose = false;
            }
        }

        public void Close()
        {
            if (!AutoClose)
            {
                Conn.Close();
                AutoClose = true;
            }
        }

        public IDbTransaction BeginTransaction()
        {
            if (AutoClose) { this.Open(); }
            Tran = Conn.BeginTransaction();
            return Tran;
        }
        public void TransactionCommit()
        {
            Tran.Commit();
            if (AutoClose) { Conn.Close(); }
        }
        public void TransactionRollback()
        {
            Tran.Rollback();
            if (AutoClose) { Conn.Close(); }
        }

        ~XConnection()
        {
            if (Tran != null) { using (Tran) { } }
            if (Conn != null) { if (AutoClose) { using (Conn) { } } }
        }
    }
}
