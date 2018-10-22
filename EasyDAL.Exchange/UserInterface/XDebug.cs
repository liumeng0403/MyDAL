using MyDAL.Core;
using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MyDAL
{
    public sealed class XDebug
    {

        private static object _lock { get; } = new object();
        private static List<string> _sql { get; set; } = new List<string>();
        private static List<string> _parameters { get; set; } = new List<string>();
        
        internal static void SetValue(Context dc,List<string> list)
        {
            if (XConfig.IsDebug)
            {
                XDebug.SQL = list;
                var parax = dc.DbConditions.Where(it => dc.IsParameter(it)).ToList();
                XDebug.Parameters = parax
                    .Select(dbM =>
                    {
                        var uiM = dc.UiConditions.FirstOrDefault(ui => dbM.Param.Equals(ui.Param, StringComparison.OrdinalIgnoreCase));
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
                XDebug.SqlWithParams = new List<string>();
                foreach (var sql in XDebug.SQL)
                {
                    var sqlStr = sql;
                    foreach (var par in parax)
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
                    XDebug.SqlWithParams.Add(sqlStr);
                }
            }
        }
        
        /// <summary>
        /// 准确,SQL 集合
        /// </summary>
        public static List<string> SQL
        {
            get
            {
                lock (_lock)
                {
                    return _sql;
                }
            }
            set
            {
                lock (_lock)
                {
                    _sql = value;
                }
            }
        }
        /// <summary>
        /// 准确,SQL 参数集合
        /// </summary>
        public static List<string> Parameters
        {
            get
            {
                lock (_lock)
                {
                    return _parameters;
                }
            }
            set
            {
                lock (_lock)
                {
                    _parameters = value;
                }
            }
        }
        /// <summary>
        /// 不一定准确,仅供参考!
        /// </summary>
        public static List<string> SqlWithParams { get; set; }

    }
}
