using System.Collections.Generic;
using MyDAL.AdoNet;
using MyDAL.Core.Bases;

namespace MyDAL.Impls.Implers.Base
{
    internal abstract class ImplerBase
                : Impler
    {
        protected ImplerBase(Context dc)
            : base(dc)
        {
            DSS = new DataSourceSync(dc);
        }

        /// <summary>
        /// 数据源 同步方式 实现
        /// </summary>
        internal DataSourceSync DSS { get; private set; }

    }
}
