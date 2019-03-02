using MyDAL.Core;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MyDAL
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
                    if (dbM.Crud == CrudEnum.Query
                        || dbM.Crud == CrudEnum.Update
                        || dbM.Crud == CrudEnum.Create
                        || dbM.Crud == CrudEnum.Delete)
                    {
                        field = dbM.TbCol;
                    }
                    else if (dbM.Crud == CrudEnum.Join)
                    {
                        field = $"{dbM.TbAlias}.{dbM.TbCol}";
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

                    //
                    if (dbM.Action == ActionEnum.SQL)
                    {
                        return $"参数:【{dbM.ParamInfo.Name}】-->【{dbVal}】.";
                    }
                    else
                    {
                        return $"字段:【{field}】-->【{csVal}】;参数:【{dbM.Param}】-->【{dbVal}】.";
                    }
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
                        if (par.Action == ActionEnum.SQL)
                        {
                            sqlStr = sqlStr.Replace($"@{par.ParamInfo.Name}", par.ParamInfo.Value == DBNull.Value ? "null" : par.ParamInfo.Value.ToString());
                        }
                        else
                        {
                            sqlStr = sqlStr.Replace($"@{par.Param}", par.ParamInfo.Value == DBNull.Value ? "null" : par.ParamInfo.Value.ToString());
                        }
                    }
                    else
                    {
                        if (par.Action == ActionEnum.SQL)
                        {
                            sqlStr = sqlStr.Replace($"@{par.ParamInfo.Name}", par.ParamInfo.Value == DBNull.Value ? "null" : $"'{par.ParamInfo.Value.ToString()}'");
                        }
                        else
                        {
                            sqlStr = sqlStr.Replace($"@{par.Param}", par.ParamInfo.Value == DBNull.Value ? "null" : $"'{par.ParamInfo.Value.ToString()}'");
                        }
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
