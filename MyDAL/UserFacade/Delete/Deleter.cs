﻿using System;
using System.Threading.Tasks;
using MyDAL.Core.Bases;
using MyDAL.Impls;
using MyDAL.Interfaces;

namespace MyDAL.UserFacade.Delete
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class Deleter<M> 
        : Operator, IDelete
        where M : class
    {
        internal Deleter(Context dc)
            : base(dc)
        { }

        /// <summary>
        /// 单表数据删除
        /// </summary>
        [Obsolete("警告：此 API 会删除表中所有数据！！！",false)]
        public async Task<int> DeleteAsync()
        {
            return await new DeleteImpl<M>(DC).DeleteAsync();
        }
    }
}
