using MyDAL.Core.Common;
using MyDAL.Core.Extensions;
using System;
using System.Data;
using System.Threading.Tasks;

namespace MyDAL.Core.Helper
{
    internal class CodeFirstHelper
        : ClassInstance<CodeFirstHelper>
    {

        private async  Task CompareDb(IDbConnection conn)
        {
            //var dbs= 
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
                        await CompareDb(dbConn);
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
