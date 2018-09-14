using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Yunyong.DataExchange.Enums
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
        /// ""
        /// </summary>
        [Description("")]
        InsertTVP,

        /// <summary>
        /// =
        /// </summary>
        [Description("=")]
        Set,

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
        /// ""
        /// </summary>
        [Description("")]
        Column,
        ColumnAs,

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
        InHelper,

        /// <summary>
        /// " count"
        /// </summary>
        [Description(" count")]
        Count,

        /// <summary>
        /// " char_length"
        /// </summary>
        [Description(" char_length")]
        CharLength,

        /// <summary>
        /// ""
        /// </summary>
        [Description("")]
        OneEqualOne,

        /// <summary>
        /// " asc "
        /// </summary>
        [Description(" asc ")]
        Asc,

        /// <summary>
        /// " desc "
        /// </summary>
        [Description(" desc ")]
        Desc
    }
}
