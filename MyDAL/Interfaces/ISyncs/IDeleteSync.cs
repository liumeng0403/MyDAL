using System.Data;

namespace MyDAL.Interfaces.ISyncs
{
    internal interface IDelete
    {
        int Delete(IDbTransaction tran = null);
    }
}
