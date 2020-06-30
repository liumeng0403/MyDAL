using MyDAL.AdoNet;
using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MyDAL.Impls.Base
{
    internal abstract class ImplerAsync
        : Impler
    {
        protected ImplerAsync(Context dc)
            : base(dc)
        {
            DSA = new DataSourceAsync(dc);
        }

        internal DataSourceAsync DSA { get; private set; }

    }
}
