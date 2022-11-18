namespace MyDAL.Impls.Constraints.Methods
{
    internal interface IUpdate<M>
    where M : class
    {
        int Update();
    }
}
