
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Core.Sql;
using System.Data;

namespace EasyDAL.Exchange
{
    public static class DbExtension
    {

        public static Core.Create.Creater<M> Creater<M>(this IDbConnection connection)
        {
            return new Core.Create.Creater<M>(new DbContext(connection));
        }

        public static Core.Delete.Deleter<M> Deleter<M>(this IDbConnection connection)
        {
            return new Core.Delete.Deleter<M>(new DbContext(connection));
        }

        public static Core.Update.Setter<M> Updater<M>(this IDbConnection connection)
        {
            return new Core.Update.Setter<M>(new DbContext(connection));
        }

        public static Core.Query.Selecter<M> Selecter<M>(this IDbConnection connection)
        {
            return new Core.Query.Selecter<M>(new DbContext(connection));
        }

        public static Core.Join.FromX Joiner(this IDbConnection connection)
        {
            return new Core.Join.FromX(new DbContext(connection));
        }

        public static IDbConnection OpenHint(this IDbConnection connection)
        {
            Hints.Hint = true;
            return connection;
        }

    }
}
