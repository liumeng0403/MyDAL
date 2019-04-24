using MyDAL.Core;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyDAL
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

        internal static void OutPutSQL(List<string> sqlList)
        {
            foreach (var sql in sqlList)
            {
                switch (XConfig.DebugType)
                {
                    case DebugEnum.Debug:
                        Debug.WriteLine(sql);
                        break;
                    case DebugEnum.Trace:
                        Trace.WriteLine(sql);
                        break;
                    case DebugEnum.Console:
                        Console.WriteLine(sql);
                        break;
                }
            }
        }
    }
}
