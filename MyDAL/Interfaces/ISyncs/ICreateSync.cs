using System.Data;

namespace HPC.DAL.Interfaces.ISyncs
{
    internal interface ICreate<M>
        where M : class
    {
        int Create(M m);
    }
}
