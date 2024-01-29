namespace MyDAL.Core.Enums
{
    internal enum OptionEnum
    {
        /// <summary>
        /// none
        /// </summary>
        None,

        /// <summary>
        /// ""
        /// </summary>
        Insert,

        /// <summary>
        /// =
        /// </summary>
        Set,

        /// <summary>
        /// +
        /// </summary>
        ChangeAdd,

        /// <summary>
        /// -
        /// </summary>
        ChangeMinus,

        /// <summary>
        /// ""
        /// ColumnForMydalFunc - MyDAL 原生函数
        /// </summary>
        Column,
        ColumnAs,
        ColumnOther,
        ColumnForMydalFunc,
        
        /// <summary>
        /// ""
        /// </summary>
        Compare,

        /// <summary>
        /// ""
        /// </summary>
        Function,

        /// <summary>
        /// ""
        /// </summary>
        OneEqualOne,

        /// <summary>
        /// " is null "
        /// </summary>
        IsNull,

        /// <summary>
        /// " is not null "
        /// </summary>
        IsNotNull,

        /// <summary>
        /// " asc "
        /// </summary>
        Asc,

        /// <summary>
        /// " desc "
        /// </summary>
        Desc
    }
}
