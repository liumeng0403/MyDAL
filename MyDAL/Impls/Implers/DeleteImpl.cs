using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Impls.Constraints.Methods;
using MyDAL.Impls.Implers.Base;

namespace MyDAL.Impls.Implers
{
    internal sealed class DeleteImpl<M>
        : ImplerBase
        , IDelete
        where M : class
    {
        internal DeleteImpl(Context dc)
            : base(dc)
        { }

        public int Delete()
        {
            PreExecuteHandle(UiMethodEnum.Delete);
            return DSS.ExecuteNonQuery<M>(null);
        }

    }
}
