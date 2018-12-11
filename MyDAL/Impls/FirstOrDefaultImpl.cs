using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.Impls
{
    internal class FirstOrDefaultImpl<M>
        : Impler, IFirstOrDefault<M>
        where M : class
    {
        internal FirstOrDefaultImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<M> FirstOrDefaultAsync()
        {
            return (await new TopImpl<M>(DC).TopAsync(1)).FirstOrDefault();
        }

        public async Task<VM> FirstOrDefaultAsync<VM>()
            where VM : class
        {
            return (await new TopImpl<M>(DC).TopAsync<VM>(1)).FirstOrDefault();
        }

        public async Task<T> FirstOrDefaultAsync<T>(Expression<Func<M, T>> columnMapFunc)
        {
            return (await new TopImpl<M>(DC).TopAsync(1, columnMapFunc)).FirstOrDefault();
        }
    }

    internal class FirstOrDefaultXImpl
        : Impler, IFirstOrDefaultX
    {
        internal FirstOrDefaultXImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<M> FirstOrDefaultAsync<M>()
            where M : class
        {
            return (await new TopXImpl(DC).TopAsync<M>(1)).FirstOrDefault();
        }

        public async Task<T> FirstOrDefaultAsync<T>(Expression<Func<T>> columnMapFunc)
        {
            return (await new TopXImpl(DC).TopAsync(1, columnMapFunc)).FirstOrDefault();
        }
    }
}
