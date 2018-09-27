using System.ComponentModel;

namespace MyDAL
{
    public enum CompareConditionEnum
    {
        /// <summary>
        /// " "
        /// </summary>
        [Description(" ")]
        None,

        /// <summary>
        /// =
        /// </summary>
        [Description("=")]
        Equal,

        /// <summary>
        /// <>
        /// </summary>
        [Description("<>")]
        NotEqual,

        /// <summary>
        /// &lt;
        /// </summary>
        [Description("<")]
        LessThan,

        /// <summary>
        /// &lt;=
        /// </summary>
        [Description("<=")]
        LessThanOrEqual,

        /// <summary>
        /// &gt;
        /// </summary>
        [Description(">")]
        GreaterThan,

        /// <summary>
        /// &gt;=
        /// </summary>
        [Description(">=")]
        GreaterThanOrEqual,


    }
}
