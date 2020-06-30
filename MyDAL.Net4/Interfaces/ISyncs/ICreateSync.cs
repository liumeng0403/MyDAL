using System.Data;

namespace MyDAL.Interfaces.ISyncs
{
    internal interface ICreate<M>
        where M : class
    {
        int Create(M m);
    }
}
