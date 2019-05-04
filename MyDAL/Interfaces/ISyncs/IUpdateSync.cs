using System.Data;

namespace HPC.DAL.Interfaces.ISyncs
{
    internal interface IUpdate<M>
    where M : class
    {
        int Update(SetEnum set = SetEnum.AllowedNull);
    }
}
