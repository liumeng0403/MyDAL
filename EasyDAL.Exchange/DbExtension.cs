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

        public static CreateOperation<M> Creater<M>(this IDbConnection connection)
        {
            var operation = new CreateOperation<M>(connection);
            return operation;
        }

        public static Core.Delete.Deleter<M> Deleter<M>(this IDbConnection connection)
        {
            var operation = new DeleteOperation<M>(connection);
            return new Core.Delete.Deleter<M>(operation.DC);
        }

        public static Core.Update.Setter<M> Updater<M>(this IDbConnection connection)
        {
            var operation = new UpdateOperation<M>(connection);
            return new Core.Update.Setter<M>(operation.DC);
        }

        public static SelectOperation<M> Selecter<M>(this IDbConnection connection)
        {
            var operation = new SelectOperation<M>(connection);
            return operation;
        }

    }
}
