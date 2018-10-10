using MyDAL.Common;
using System;
using System.Data;

namespace MyDAL.Helper
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
