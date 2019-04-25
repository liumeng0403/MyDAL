﻿using System;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HPC.DAL.Interfaces.IAsyncs
{
    internal interface ISumAsync<M>
        where M : class
    {
        Task<F> SumAsync<F>(Expression<Func<M, F>> propertyFunc, IDbTransaction tran = null)
            where F : struct;

        Task<Nullable<F>> SumAsync<F>(Expression<Func<M, Nullable<F>>> propertyFunc, IDbTransaction tran = null)
            where F : struct;
    }
}