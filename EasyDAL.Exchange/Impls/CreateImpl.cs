using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using EasyDAL.Exchange.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Impls
{
    internal class CreateImpl<M>
        : Impler, ICreate<M>
    {
        public CreateImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<int> CreateAsync(M m)
        {
            DC.GetProperties(m);
            return await SqlHelper.ExecuteAsync(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.CreateAsync)[0],
                DC.GetParameters());
        }
    }
}
