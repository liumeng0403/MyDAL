using MyDAL.AdoNet;
using MyDAL.Core;
using MyDAL.Tools;
using System;
using System.Data;

namespace MyDAL
{
    public sealed class XConnection
        : IDisposable
    {
        private XConnectionBuilder _Builder { get; }
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
        private XConnection() { }

        // ************************************************************************************

        internal IDbConnection Conn { get; private set; }
        internal IDbTransaction Tran { get; private set; }
        internal bool IsDebug { get; private set; } = false;
        internal DebugEnum DebugType { get; private set; } = DebugEnum.Output;

        // ************************************************************************************

        private XConnection(IDbConnection conn)
        {
            /*
             * 仅限 组件外 调用。
             */
            Conn = conn;
            AutoClose = false;
            NeedClose();
        }
        internal XConnection(XConnectionBuilder builder)
        {
            this._Builder = builder;
        }
        public static XConnectionBuilder Builder()
        {
            return new XConnectionBuilder();
        }
        /// <summary>
        /// 上下文线程安全的 MySQL DB 操作实例
        /// </summary>
        public XConnection GetDB()
        {
            try
            {
                IDbConnection obj = (IDbConnection)Activator.CreateInstance(_Builder.DriverType, _Builder.ConnStr);
                return new XConnection(obj);
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message.IsNullStr() ? string.Empty : ex.Message;
                string innerErrMsg = ex.InnerException == null ? 
                    string.Empty : 
                    ex.InnerException.Message.IsNullStr() ? string.Empty : ex.InnerException.Message;
                throw XConfig.EC.Exception(XConfig.EC._100, "MySQL 驱动异常: [1]" + errMsg + ". [2]" + innerErrMsg);
            }
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
