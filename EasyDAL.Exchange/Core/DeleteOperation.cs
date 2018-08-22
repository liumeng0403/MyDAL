using EasyDAL.Exchange.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EasyDAL.Exchange.Core
{
    public class DeleteOperation : DbOperation
    {
        public DeleteOperation(IDbConnection conn) 
            : base(conn)
        {
        }
    }
}
