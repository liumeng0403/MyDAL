using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Impls.Base;
using MyDAL.Interfaces.ISyncs;
using System.Collections.Generic;
using System.Data;

namespace MyDAL.Impls.ImplSyncs
{
    internal sealed class CreateImpl<M>
        : ImplerSync
        , ICreate<M>
        where M : class
    {
        public CreateImpl(Context dc)
            : base(dc)
        { }

        public int Create(M m)
        {
            DC.Action = ActionEnum.Insert;
            CreateMHandle(new List<M> { m });
            PreExecuteHandle(UiMethodEnum.CreateAsync);
            return DSS.ExecuteNonQuery();
        }
    }
}
