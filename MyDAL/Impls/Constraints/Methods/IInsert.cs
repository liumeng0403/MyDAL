namespace MyDAL.Impls.Constraints.Methods
{
    internal interface IInsert<M>
        where M : class
    {
        int Insert(M m);
    }
}
