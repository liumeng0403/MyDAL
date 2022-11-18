using MyDAL.Core.Bases;
using MyDAL.Impls.Constraints.Methods;
using MyDAL.Impls.Implers.Base;

namespace MyDAL.Impls.Implers
{
    internal sealed class ExecuteNonQuerySQLImpl
        : ImplerBase
        , IExecuteNonQuerySQL
    {
        public ExecuteNonQuerySQLImpl(Context dc)
            : base(dc)
        { }

        public int ExecuteNonQuery()
        {
            return DSS.ExecuteNonQuery<None>(null);
        }
    }
}
