using MyDAL.Core.Bases;
using MyDAL.Impls.Base;
using MyDAL.Interfaces.ISyncs;
using System.Data;

namespace MyDAL.Impls.ImplSyncs
{
    internal sealed class ExecuteNonQuerySQLImpl
        : ImplerSync
        , IExecuteNonQuerySQL
    {
        public ExecuteNonQuerySQLImpl(Context dc)
            : base(dc)
        { }

        public int ExecuteNonQuery(IDbTransaction tran = null)
        {
            DSS.Tran = tran;
            return DSS.ExecuteNonQuery();
        }
    }
}
