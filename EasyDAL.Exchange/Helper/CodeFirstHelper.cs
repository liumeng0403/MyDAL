using System;
using System.Data;
using Yunyong.DataExchange.Common;

namespace Yunyong.DataExchange.Helper
{
    internal class CodeFirstHelper
        : ClassInstance<CodeFirstHelper>
    {

        private void CompareDb()
        {

        }
        private void CompareTable()
        {

        }
        private void CompareField()
        {

        }

        internal void CodeFirstProcess(IDbConnection conn)
        {
            XConfig.IsNeedChangeDb = false;

            //
            var needClose = conn.State == ConnectionState.Closed;
            if (needClose)
            {
                conn.Open();
            }

            //
            using (var tran = conn.BeginTransaction())
            {
                try
                {
                    CompareDb();
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
                finally
                {
                    if(needClose)
                    {
                        conn.Close();
                    }
                }
            }

            //
            XConfig.IsNeedChangeDb = false;
        }

    }
}
