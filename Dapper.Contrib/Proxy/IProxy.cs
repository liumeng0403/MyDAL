using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Contrib.Proxy
{
    /// <summary>
    /// Defined a proxy object with a possibly dirty state.
    /// </summary>
    public interface IProxy 
    {
        /// <summary>
        /// 标志对象是否发生过变化
        /// </summary>
        bool IsDirty { get; set; }
    }
}
