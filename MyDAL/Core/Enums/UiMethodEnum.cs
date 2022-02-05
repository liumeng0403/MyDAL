namespace MyDAL.Core.Enums
{
    /// <summary>
    /// UI Interface SQL option type
    /// </summary>
    internal enum UiMethodEnum
    {
        None,
        Create,
        CreateBatch,
        Delete,
        Update,
        QueryOne,
        QueryList,
        QueryPaging,
        Top,
        IsExist,
        Count,
        Sum
    }
}
