using HPC.DAL.AdoNet;
using HPC.DAL.Core;
using System;
using System.Data;
using System.Threading.Tasks;

namespace HPC.DAL
{
    public sealed class XConnection
        : IDisposable
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
            /*
             * 仅限 组件外 调用。
             */
            Conn = conn;
            AutoClose = false;
            NeedClose();
        }

        /// <summary>
        /// 打开 DB 连接
        /// </summary>
        public void Open()
        {
            /*
             * 仅限 组件外 调用。
             */
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
            /*
             * 仅限 组件外 调用。
             */
            if (AutoClose)
            {
                await new DataSourceAsync().OpenAsync(Conn);
                AutoClose = false;
            }
        }
        public void Close()
        {
            /*
             * 仅限 组件外 调用。
             */
            if (!AutoClose)
            {
                Conn.Close();
                AutoClose = true;
            }
        }

        public void BeginTransaction()
        {
            /*
             * 仅限 组件外 调用。
             */
            if (Tran != null)
            {
                throw XConfig.EC.Exception(XConfig.EC._090, "上下文中事务已开启，无需再次开启！");
            }
            if (AutoClose) { Conn.Open(); }
            Tran = Conn.BeginTransaction();
        }
        public void CommitTransaction()
        {
            /*
             * 仅限 组件外 调用。
             */
            if (Tran == null)
            {
                throw XConfig.EC.Exception(XConfig.EC._088, "请检查: 1-上下文是否已调用【void BeginTransaction()】开启事务！; 2-在事务范围内使用的【XConnection】对象是否为同一实例！");
            }
            Tran.Commit();
            if (AutoClose) { Conn.Close(); }
        }
        public void RollbackTransaction()
        {
            /*
             * 仅限 组件外 调用。
             */
            if (Tran == null)
            {
                throw XConfig.EC.Exception(XConfig.EC._089, "请检查: 1-上下文是否已调用【void BeginTransaction()】开启事务！; 2-在事务范围内使用的【XConnection】对象是否为同一实例！");
            }
            Tran.Rollback();
            if (AutoClose) { Conn.Close(); }
        }

        internal bool IsDebug { get; private set; } = false;
        internal DebugEnum DebugType { get; private set; } = DebugEnum.Output;
        /// <summary>
        /// 请参阅: <see langword=".OpenDebug() 与 Visual Studio 输出窗口 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public XConnection OpenDebug(DebugEnum type = DebugEnum.Output)
        {
            IsDebug = true;
            DebugType = type;
            return this;
        }

        public void Dispose()
        {
            if (Tran != null) { using (Tran) { } }
            if (Conn != null) { if (AutoClose) { using (Conn) { } } }
        }
    }
}
