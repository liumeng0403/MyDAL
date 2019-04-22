using System.Data;

namespace MyDAL.Interfaces.ISyncs
{
    internal interface IIsExist
    {
        bool IsExist(IDbTransaction tran = null);
    }

    internal interface IIsExistX
    {
        bool IsExist(IDbTransaction tran = null);
    }
}
