using MyDAL.AdoNet;
using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using System;
using System.Data;

namespace MyDAL.Impls.Base
{
    internal abstract class ImplerSync
                : Impler
    {
        protected ImplerSync(Context dc)
            : base(dc)
        {
            DSS = new DataSourceSync(dc);
        }

        internal DataSourceSync DSS { get; private set; }

    }
}
