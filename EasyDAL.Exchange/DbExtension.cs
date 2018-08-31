
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using System;
using System.Data;
using System.Threading.Tasks;

namespace EasyDAL.Exchange
{
    public static class DbExtension
    {
        
        public async static Task<int> CreateAsync<M>(this IDbConnection connection,M m)
        {
            var creater = new Core.Create.Creater<M>(new DbContext(connection));

            creater.DC.GetProperties(m);
            return await SqlHelper.ExecuteAsync(
                creater.DC.Conn,
                creater.DC.SqlProvider.GetSQL<M>(SqlTypeEnum.CreateAsync)[0],
                creater.DC.SqlProvider.GetParameters());
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
