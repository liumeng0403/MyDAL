using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Impls.Base;
using MyDAL.Interfaces.IAsyncs;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Impls.ImplAsyncs
{
    internal sealed class SelectOneAsyncImpl<M>
         : Impler
         , ISelectOneAsync<M>
     where M : class
    {
        internal SelectOneAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<M> SelectOneAsync()
        {
            return (await new TopAsyncImpl<M>(DC).TopAsync(1)).FirstOrDefault();
        }
        public async Task<VM> SelectOneAsync<VM>()
            where VM : class
        {
            return (await new TopAsyncImpl<M>(DC).TopAsync<VM>(1)).FirstOrDefault();
        }
        public async Task<T> SelectOneAsync<T>(Expression<Func<M, T>> columnMapFunc)
        {
            return (await new TopAsyncImpl<M>(DC).TopAsync(1, columnMapFunc)).FirstOrDefault();
        }

    }

    internal sealed class SelectOneXAsyncImpl
         : Impler
         , ISelectOneXAsync
    {
        internal SelectOneXAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<M> SelectOneAsync<M>()
            where M : class
        {
            return (await new TopXAsyncImpl(DC).TopAsync<M>(1)).FirstOrDefault();
        }
        public async Task<T> SelectOneAsync<T>(Expression<Func<T>> columnMapFunc)
        {
            return (await new TopXAsyncImpl(DC).TopAsync(1, columnMapFunc)).FirstOrDefault();
        }

    }

    internal sealed class SelectOneSQLAsyncImpl
        : ImplerAsync
        , ISelectOneSQLAsync
    {
        public SelectOneSQLAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<T> SelectOneAsync<T>()
        {
            DC.Method = UiMethodEnum.QueryOne;
            if (typeof(T).IsSingleColumn())
            {
                return (await DSA.ExecuteReaderSingleColumnAsync<T>()).FirstOrDefault();
            }
            else
            {
                return (await DSA.ExecuteReaderMultiRowAsync<T>()).FirstOrDefault();
            }
        }

    }
}
