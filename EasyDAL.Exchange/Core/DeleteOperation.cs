using EasyDAL.Exchange.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;
using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Enums;

namespace EasyDAL.Exchange.Core
{
    public class DeleteOperation<M> : DbOperation
    {
        public DeleteOperation(IDbConnection conn)
            : base(conn)
        {
        }
        
    }
}
