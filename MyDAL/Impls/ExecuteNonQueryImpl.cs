﻿using MyDAL.Core.Bases;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class ExecuteNonQuerySQLImpl
        : Impler, IExecuteNonQuerySQL
    {
        public ExecuteNonQuerySQLImpl(Context dc) 
            : base(dc)
        {   }

        public async Task<int> ExecuteNonQueryAsync()
        {
            return await DC.DS.ExecuteNonQueryAsync();
        }
    }
}