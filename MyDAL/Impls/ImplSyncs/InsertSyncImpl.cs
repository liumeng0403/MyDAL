using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Impls.Base;
using MyDAL.Interfaces.ISyncs;
using System.Collections.Generic;

namespace MyDAL.Impls.ImplSyncs
{
    internal sealed class InsertImpl<M>
        : ImplerSync
        , IInsert<M>
        where M : class
    {
        public InsertImpl(Context dc)
            : base(dc)
        { }

        public int Insert(M m)
        {
            DC.Action = ActionEnum.Insert;
            CreateMHandle(new List<M> { m });
            PreExecuteHandle(UiMethodEnum.Create);
            return DSS.ExecuteNonQuery<M>(new List<M>()
            {
                m
            });
        }
    }
}
