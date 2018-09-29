﻿using MyDAL.Common;
using MyDAL.Core;
using MyDAL.Enums;
using MyDAL.Helper;
using MyDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class QueryListImpl<M>
        : Impler, IQueryList<M>
    {
        internal QueryListImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<List<M>> QueryListAsync()
        {
            return await QueryListAsyncHandle<M, M>();
        }

        public async Task<List<VM>> QueryListAsync<VM>()
        {
            return await QueryListAsyncHandle<M, VM>();
        }

        public async Task<List<VM>> QueryListAsync<VM>(Expression<Func<M, VM>> func)
        {
            SelectMHandle(func);
            return await QueryListAsyncHandle<M, VM>();
        }
    }

    internal class QueryListXImpl
        : Impler, IQueryListX
    {
        internal QueryListXImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<List<M>> QueryListAsync<M>()
        {
            SelectMHandle<M>();
            return (await SqlHelper.QueryAsync<M>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.JoinQueryListAsync)[0],
                DC.GetParameters())).ToList();
        }

        public async Task<List<VM>> QueryListAsync<VM>(Expression<Func<VM>> func)
        {
            SelectMHandle(func);
            return (await SqlHelper.QueryAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<VM>(UiMethodEnum.JoinQueryListAsync)[0],
                DC.GetParameters())).ToList();
        }
    }
}