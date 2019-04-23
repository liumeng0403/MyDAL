using HPC.DAL.Core.Bases;
using HPC.DAL.Impls.Base;
using HPC.DAL.Interfaces.ISyncs;
using System.Data;

namespace HPC.DAL.Impls.ImplSyncs
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
