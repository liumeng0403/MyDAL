namespace MyDAL.Core.Enums
{
    internal enum CompareXEnum
    {
        /// <summary>
        /// " "
        /// </summary>
        None,

        /// <summary>
        /// =
        /// </summary>
        Equal,

        /// <summary>
        /// !=
        /// </summary>
        NotEqual,

        /// <summary>
        /// &lt;
        /// </summary>
        LessThan,

        /// <summary>
        /// &lt;=
        /// </summary>
        LessThanOrEqual,

        /// <summary>
        /// &gt;
        /// </summary>
        GreaterThan,

        /// <summary>
        /// &gt;=
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        /// " like "
        /// </summary>
        Like,

        /// <summary>
        /// " in "
        /// </summary>
        In,

        /// <summary>
        /// " not in "
        /// </summary>
        NotIn,

        /// <summary>
        /// " distinct "
        /// </summary>
        Distinct

    }
}
