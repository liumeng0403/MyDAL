namespace MyDAL.Interfaces.ISyncs
{
    internal interface IInsert<M>
        where M : class
    {
        int Insert(M m);
    }
}
