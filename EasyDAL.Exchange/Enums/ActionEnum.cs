using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EasyDAL.Exchange.Enums
{
    internal enum ActionEnum
    {
        /// <summary>
        /// ""
        /// </summary>
        [Description("")]
        None,

        /// <summary>
        /// ""
        /// </summary>
        [Description("")]
        Insert,

        /// <summary>
        /// "="
        /// </summary>
        [Description("=")]
        Set,

        /// <summary>
        /// ""
        /// </summary>
        [Description("")]
        Change,

        /// <summary>
        /// " "
        /// </summary>
        [Description(" ")]
        Where,

        /// <summary>
        /// " and "
        /// </summary>
        [Description(" and ")]
        And,

        /// <summary>
        /// " or "
        /// </summary>
        [Description(" or ")]
        Or
    }
}
