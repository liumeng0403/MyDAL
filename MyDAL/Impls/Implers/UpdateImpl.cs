using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Impls.Constraints.Methods;
using MyDAL.Impls.Implers.Base;

namespace MyDAL.Impls.Implers
{
    internal sealed class UpdateImpl<M>
        : ImplerBase
        , IUpdate<M>
        where M : class
    {
        internal UpdateImpl(Context dc)
            : base(dc)
        { }

        public int Update()
        {
            PreExecuteHandle(UiMethodEnum.Update);
            return DSS.ExecuteNonQuery<M>(null);
        }
    }
}
