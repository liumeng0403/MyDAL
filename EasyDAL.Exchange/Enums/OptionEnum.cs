using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EasyDAL.Exchange.Enums
{
    internal enum OptionEnum
    {
        /// <summary>
        /// none
        /// </summary>
        [Description("<<<<<")]
        None,

        /// <summary>
        /// ""
        /// </summary>
        [Description("")]
        Insert,

        /// <summary>
        /// =
        /// </summary>
        [Description("=")]
        Equal,

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
        /// +
        /// </summary>
        [Description("+")]
        ChangeAdd,

        /// <summary>
        /// -
        /// </summary>
        [Description("-")]
        ChangeMinus,

        /// <summary>
        /// =
        /// </summary>
        [Description("=")]
        Set
    }
}
