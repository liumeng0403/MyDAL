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
        Select,

        /// <summary>
        /// ""
        /// </summary>
        [Description("")]
        Insert,

        ///// <summary>
        ///// "="
        ///// </summary>
        //[Description("=")]
        //Set,

        ///// <summary>
        ///// ""
        ///// </summary>
        //[Description("")]
        //Change,

        /// <summary>
        /// 
        /// </summary>
        [Description("")]
        Update,

        /// <summary>
        /// ""
        /// </summary>
        [Description("")]
        From,

        /// <summary>
        /// " inner join "
        /// </summary>
        [Description(" inner join ")]
        InnerJoin,

        /// <summary>
        /// " left join "
        /// </summary>
        [Description(" left join ")]
        LeftJoin,

        /// <summary>
        /// " on "
        /// </summary>
        [Description(" on ")]
        On,

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
