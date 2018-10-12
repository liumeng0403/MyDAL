using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Yunyong.DataExchange.AdoNet;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Extensions;
using Yunyong.DataExchange.Core.MySql.Models;

namespace Yunyong.DataExchange.Core.Helper
{
    internal class CodeFirstHelper
        : ClassInstance<CodeFirstHelper>
    {

        private async Task CompareDb(IDbConnection conn, string targetDb)
        {
            var dbs = await DataSource.Instance.ExecuteReaderMultiRowAsync<DbModel>(conn, " show databases; ", null);
            if(dbs.Any(it=>it.Database.Equals(targetDb,StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }
            else
            {
                try
                {
                    var res = await DataSource.Instance.ExecuteNonQueryAsync(conn, $" create database {targetDb} default charset utf8; ", null);
                }
                catch(Exception ex)
                {
                    throw new Exception("CodeFirst 创建数据库失败!", ex);
                }
            }
        }
        private void CompareTable()
        {

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
                        CompareTable();
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
