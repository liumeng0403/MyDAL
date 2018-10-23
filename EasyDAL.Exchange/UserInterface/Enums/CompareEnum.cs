using System.ComponentModel;

namespace Yunyong.DataExchange
{
    public enum CompareEnum
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
        /// !=
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

        /// <summary>
        /// " like "
        /// </summary>
        [Description(" like ")]
        Like,

        /// <summary>
        /// " in "
        /// </summary>
        [Description(" in ")]
        In,

        /// <summary>
        /// " not in "
        /// </summary>
        [Description(" not in ")]
        NotIn

    }
}
