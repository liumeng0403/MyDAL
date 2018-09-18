using System.ComponentModel;

namespace EasyDAL.Exchange
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
