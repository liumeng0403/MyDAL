using System.Data;

namespace MyDAL.Interfaces.ISyncs
{
    internal interface IExecuteNonQuerySQL
    {
        int ExecuteNonQuery(IDbTransaction tran = null);
    }
}
