using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.Base;
using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core.Update;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Core
{
    public class UpdateOperation<M> : DbOperation
    {
        public UpdateOperation(IDbConnection conn) 
            : base(conn)
        {
            
        }

        ///*
        // * 不用实现的方法
        // */
        //public override Task<int> UpdateAsync()
        //{
        //    throw new NotImplementedException();
        //}



    }
}
