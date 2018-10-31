using MyDAL.Core;
using MyDAL.Core.Bases;
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

        internal static List<DicModelUI> UIs { get; set; }
        internal static List<DicModelDB> DBs { get; set; }
        internal static void SetValue()
        {
            if (XConfig.IsDebug)
            {
                Parameters = DBs
                    .Select(dbM =>
                    {
                        var uiM = UIs.FirstOrDefault(ui => dbM.Param.Equals(ui.Param, StringComparison.OrdinalIgnoreCase));
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
                        if (uiM.CsValue == null)
                        {
                            csVal = "Null";
                        }
                        else if (uiM.CsType == XConfig.DateTime)
                        {
                            csVal = uiM.CsValue.ToDateTimeStr();
                        }
                        else
                        {
                            csVal = uiM.CsValue.ToString();
                        }

                        //
                        var dbVal = string.Empty;
                        dbVal = dbM.DbValue == null ? "DbNull" : dbM.DbValue.ToString();
                        return $"字段:【{field}】-->【{csVal}】;参数:【{dbM.Param}】-->【{dbVal}】.";
                    })
                    .ToList();
                SqlWithParams = new List<string>();
                foreach (var sql in SQL)
                {
                    var sqlStr = sql;
                    foreach (var par in DBs)
                    {
                        if (par.DbType == DbType.Boolean
                            || par.DbType == DbType.Decimal
                            || par.DbType == DbType.Double
                            || par.DbType == DbType.Int16
                            || par.DbType == DbType.Int32
                            || par.DbType == DbType.Int64
                            || par.DbType == DbType.Single
                            || par.DbType == DbType.UInt16
                            || par.DbType == DbType.UInt32
                            || par.DbType == DbType.UInt64)
                        {
                            sqlStr = sqlStr.Replace($"@{par.Param}", par.DbValue == null ? "null" : par.DbValue.ToString());
                        }
                        else
                        {
                            sqlStr = sqlStr.Replace($"@{par.Param}", par.DbValue == null ? "null" : $"'{par.DbValue.ToString()}'");
                        }
                    }
                    SqlWithParams.Add(sqlStr);
                }
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
