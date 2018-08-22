using EasyDAL.Exchange.Base;
using EasyDAL.Exchange.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EasyDAL.Exchange
{
    public static class DbExtension
    {

        public static CreateOperation Creater(this IDbConnection connection)
        {
            var operation = new CreateOperation(connection);
            return operation;
        }

        public static DeleteOperation Delete(this IDbConnection connection)
        {
            var operation = new DeleteOperation(connection);
            return operation;
        }

        public static UpdateOperation<M> Updater<M>(this IDbConnection connection)
        {
            var operation = new UpdateOperation<M>(connection);
            return operation;
        }

        public static QueryOperation Query(this IDbConnection connection)
        {
            var operation = new QueryOperation(connection);
            return operation;
        }

    }
}
