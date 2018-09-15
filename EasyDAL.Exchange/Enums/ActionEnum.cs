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
        /// 
        /// </summary>
        [Description("")]
        Update,

        /// <summary>
        /// ""
        /// </summary>
        [Description("")]
        Select,

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
        /// " where "
        /// </summary>
        [Description(" \r\n where ")]
        Where,

        /// <summary>
        /// " and "
        /// </summary>
        [Description(" \r\n and ")]
        And,

        /// <summary>
        /// " or "
        /// </summary>
        [Description(" \r\n or ")]
        Or,

        /// <summary>
        /// ""
        /// </summary>
        [Description("")]
        OrderBy
    }
}
