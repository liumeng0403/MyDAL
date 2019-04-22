using HPC.DAL.AdoNet;
using HPC.DAL.Core.Bases;

namespace HPC.DAL.Impls.Base
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
