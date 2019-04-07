using MyDAL.Core.Bases;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal sealed class ExecuteNonQuerySQLImpl
        : Impler
        , IExecuteNonQuerySQLAsync, IExecuteNonQuerySQL
    {
        public ExecuteNonQuerySQLImpl(Context dc) 
            : base(dc)
        {   }

        public async Task<int> ExecuteNonQueryAsync()
        {
            return await DC.DSA.ExecuteNonQueryAsync();
        }

        public int ExecuteNonQuery()
        {
            return DC.DSS.ExecuteNonQuery();
        }
    }
}
