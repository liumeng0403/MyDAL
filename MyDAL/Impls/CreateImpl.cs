﻿using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal sealed class CreateImpl<M>
        : Impler
        , ICreateAsync<M>, ICreate<M>
        where M : class
    {
        public CreateImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<int> CreateAsync(M m)
        {
            DC.Action = ActionEnum.Insert;
            CreateMHandle(new List<M> { m });
            PreExecuteHandle(UiMethodEnum.CreateAsync);
            return await DC.DSA.ExecuteNonQueryAsync();
        }

        public int Create(M m)
        {
            DC.Action = ActionEnum.Insert;
            CreateMHandle(new List<M> { m });
            PreExecuteHandle(UiMethodEnum.CreateAsync);
            return DC.DSS.ExecuteNonQuery();
        }
    }
}
