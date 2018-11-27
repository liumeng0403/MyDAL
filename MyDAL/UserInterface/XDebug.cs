using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Core.Extensions;

namespace Yunyong.DataExchange
{
    /// <summary>
    /// 只适用于 单线程 调试 代码时 查看 MyDAL 生成的 sql 与 param .
    /// </summary>
    public sealed class XDebug
    {
        internal static object Lock { get; } = new object();

        internal static List<DicParam> Dics { get; set; }
        internal static void SetValue()
        {
            Parameters = Dics
                .Select(dbM =>
                {
                    var field = string.Empty;
                    if (dbM.Crud == CrudTypeEnum.Query
                        || dbM.Crud == CrudTypeEnum.Update
                        || dbM.Crud == CrudTypeEnum.Create
                        || dbM.Crud == CrudTypeEnum.Delete)
                    {
                        field = dbM.ColumnOne;
                    }
                    else if (dbM.Crud == CrudTypeEnum.Join)
                    {
                        field = $"{dbM.TableAliasOne}.{dbM.ColumnOne}";
                    }

                    //
                    var csVal = string.Empty;
                    if (dbM.CsValue == null)
                    {
                        csVal = "Null";
                    }
                    else if (dbM.CsType == XConfig.TC.DateTime)
                    {
                        try
                        {
                            csVal = dbM.CsValue.ToDateTimeStr();
                        }
                        catch
                        {
                            csVal = dbM.CsValue.ToString();
                        }
                    }
                    else
                    {
                        csVal = dbM.CsValue.ToString();
                    }

                    //
                    var dbVal = string.Empty;
                    dbVal = dbM.ParamInfo.Value == DBNull.Value ? "DbNull" : dbM.ParamInfo.Value.ToString();
                    return $"字段:【{field}】-->【{csVal}】;参数:【{dbM.Param}】-->【{dbVal}】.";
                })
                .ToList();
            SqlWithParams = new List<string>();
            foreach (var sql in SQL)
            {
                var sqlStr = sql;
                foreach (var par in Dics)
                {
                    if (par.ParamInfo.Type == DbType.Boolean
                        || par.ParamInfo.Type == DbType.Decimal
                        || par.ParamInfo.Type == DbType.Double
                        || par.ParamInfo.Type == DbType.Int16
                        || par.ParamInfo.Type == DbType.Int32
                        || par.ParamInfo.Type == DbType.Int64
                        || par.ParamInfo.Type == DbType.Single
                        || par.ParamInfo.Type == DbType.UInt16
                        || par.ParamInfo.Type == DbType.UInt32
                        || par.ParamInfo.Type == DbType.UInt64)
                    {
                        sqlStr = sqlStr.Replace($"@{par.Param}", par.ParamInfo.Value == DBNull.Value ? "null" : par.ParamInfo.Value.ToString());
                    }
                    else
                    {
                        sqlStr = sqlStr.Replace($"@{par.Param}", par.ParamInfo.Value == DBNull.Value ? "null" : $"'{par.ParamInfo.Value.ToString()}'");
                    }
                }
                SqlWithParams.Add(sqlStr);
            }
        }

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
