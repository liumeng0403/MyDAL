using System.ComponentModel;

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
        /// ""
        /// </summary>
        InsertTVP,

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
        /// </summary>
        Column,
        ColumnAs,

        /// <summary>
        /// ""
        /// </summary>
        Compare,

        /// <summary>
        /// " like "
        /// </summary>
        Like,

        /// <summary>
        /// " in "
        /// </summary>
        In,
        InHelper,
        /// <summary>
        /// " not in "
        /// </summary>
        NotIn,

        /// <summary>
        /// " count"
        /// </summary>
        Count,

        /// <summary>
        /// " sum"
        /// </summary>
        Sum,

        /// <summary>
        /// " char_length"
        /// </summary>
        CharLength,

        /// <summary>
        /// " trim"
        /// </summary>
        Trim,

        /// <summary>
        /// " ltrim"
        /// </summary>
        LTrim,

        /// <summary>
        /// " rtrim"
        /// </summary>
        RTrim,

        /// <summary>
        /// " DATE_FORMAT"
        /// </summary>
        DateFormat,

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
