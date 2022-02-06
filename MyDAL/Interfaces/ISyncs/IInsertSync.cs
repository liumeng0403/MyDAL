namespace MyDAL.Interfaces.ISyncs
{
    internal interface ICreate<M>
        where M : class
    {
        int Insert(M m);
    }
}
