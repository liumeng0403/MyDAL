using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using System.Collections.Generic;
using MyDAL.Impls.Constraints.Methods;
using MyDAL.Impls.Implers.Base;

namespace MyDAL.Impls.Implers
{
    internal sealed class InsertImpl<M>
        : ImplerBase
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
