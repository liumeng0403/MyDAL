using MyDAL.AdoNet;
using MyDAL.Cache;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Extensions;
using MyDAL.Core.MySql.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyDAL.Core.Helper
{
    internal class CodeFirstHelper
    {
        private Context DC { get; set; }

        internal CodeFirstHelper(Context dc)
        {
            DC = dc;
        }

        /********************************************************************************************************************/

        private List<NameTypeModel> GetTableAndTypes(string key)
        {
            var cmTypes = new List<NameTypeModel>();

            //
            var ass = StaticCache.Instance.GetAssembly(key);
            var types = ass.GetTypes();
            foreach (var type in types)
            {
                if (type.FullName.StartsWith(XConfig.TablesNamespace))
                {
                    var table = DC.AH.GetAttribute<XTableAttribute>(type) as XTableAttribute;
                    if (table != null)
                    {
                        cmTypes.Add(new NameTypeModel
                        {
                            Name = table.Name,
                            Type = type
                        });
                    }
                }
            }

            return cmTypes;
        }

        /**********************************************************************************************************************************************/

        private async Task CompareDb(IDbConnection conn, string targetDb)
        {
            var dbs = await DataSource.Instance.ExecuteReaderMultiRowAsync<DbModel>(conn, " show databases; ", null);
            if (dbs.Any(it => it.Database.Equals(targetDb, StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }
            else
            {
                try
                {
                    var res = await DataSource.Instance.ExecuteNonQueryAsync(conn, $" create database if not exists {targetDb} default charset utf8; ", null);
                }
                catch (Exception ex)
                {
                    throw new Exception("CodeFirst 创建数据库失败!", ex);
                }
            }
        }
        private async Task CompareTable(IDbConnection conn)
        {
            var key = StaticCache.Instance.GetKey(XConfig.TablesNamespace, conn.Database);
            var dtNames = new List<TableModel>();
            var tupleCs = GetTableAndTypes(key);
            var ctNames = tupleCs.Select(it=>it.Name);
            var cmTypes = tupleCs.Select(it=>it.Type);

            //
            var sql = $@"
                                    select table_name as TableName
                                    from information_schema.tables
                                    where table_schema='{conn.Database}'
                                            and table_type='base table';
                                ";
            dtNames =(await DataSource.Instance.ExecuteReaderMultiRowAsync<TableModel>(conn, sql, null)).ToList();

            //
            var createList = new List<string>();
            var existList = new List<string>();
            var dropList = new List<string>();
            foreach (var ct in ctNames)
            {
                if (dtNames.Any(it => it.TableName.Equals(ct, StringComparison.OrdinalIgnoreCase)))
                {
                    existList.Add(ct);
                    continue;
                }

                createList.Add(ct);
            }
            foreach(var dt in dtNames)
            {
                if(ctNames.Any(it=>it.Equals(dt.TableName,StringComparison.OrdinalIgnoreCase)))
                {
                    continue;
                }

                dropList.Add(dt.TableName);
            }

            //
            foreach(var c in createList)
            {
                var table = tupleCs.First(it => it.Name.Equals(c, StringComparison.OrdinalIgnoreCase));
                var tProps = GenericHelper.Instance.GetPropertyInfos(table.Type);

            }
        }
        private void CompareField()
        {

        }

        internal async Task CodeFirstProcess(IDbConnection conn)
        {
            XConfig.IsNeedChangeDb = false;

            //
            var tran = default(IDbTransaction);
            var dbConn = conn.DeepClone();
            var connStr = conn.ConnectionString;
            var sIndex = connStr.IndexOf("Database", StringComparison.OrdinalIgnoreCase);
            var eIndex = connStr.IndexOf(";", sIndex);
            dbConn.ConnectionString = connStr.Substring(0, sIndex) + connStr.Substring(eIndex);

            //
            using (dbConn)
            {
                if (dbConn.State == ConnectionState.Closed)
                {
                    dbConn.Open();
                }
                using (tran = dbConn.BeginTransaction())
                {
                    try
                    {
                        await CompareDb(dbConn, conn.Database);
                        await CompareTable(dbConn);
                        CompareField();

                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        XConfig.IsNeedChangeDb = true;
                        throw ex;
                    }
                }
            }
            XConfig.IsNeedChangeDb = false;
        }

    }
}
