using MyDAL.AdoNet;
using System;
using System.Threading.Tasks;

namespace MyDAL
{
    public sealed partial class XConnection
        : IDisposable
    {
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
    }
}
