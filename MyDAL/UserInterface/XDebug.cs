using HPC.DAL.Core;
using HPC.DAL.Core.Common;
using HPC.DAL.Core.Enums;
using HPC.DAL.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HPC.DAL
{
    /// <summary>
    /// 只适用于 单线程 调试 代码时 查看 MyDAL 生成的 sql 与 param .
    /// </summary>
    public sealed class XDebug
    {
        /// <summary>
        /// 准确,SQL 集合
        /// </summary>
        public static List<string> SQL { get; set; }
        /// <summary>
        /// 准确,SQL 参数集合
        /// </summary>
        public static List<string> Parameters { get; set; }
        /// <summary>
        /// 不一定准确,仅供参考!
        /// </summary>
        public static List<string> SqlWithParams { get; set; }
    }
}
