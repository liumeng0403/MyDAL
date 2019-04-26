using HPC.DAL.Core;
using HPC.DAL.Core.Bases;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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

        internal static void OutPutSQL(List<string> sqlList, Context dc)
        {
            //
            if (!dc.FlatOutput)
            {
                return;
            }

            //
            foreach (var sql in sqlList)
            {
                var info = $@"
=================================================================  <--  参数化 SQL 开始，{(XConfig.IsDebug ? "Debug 模式已开启！" : "")}
{sql}
=================================================================  <--  参数化 SQL 结束
                                        ";
                switch (XConfig.DebugType)
                {
                    case DebugEnum.Output:
                        Trace.WriteLine(info);
                        break;
                    case DebugEnum.Console:
                        Console.WriteLine(info);
                        break;
                }
            }
        }
    }
}
