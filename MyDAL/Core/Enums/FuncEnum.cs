using System.ComponentModel;

namespace Yunyong.DataExchange.Core.Enums
{
    internal enum FuncEnum
    {
        /// <summary>
        /// ""
        /// </summary>
        None,
        
        /// <summary>
        /// " char_length"
        /// </summary>
        CharLength,
        
        /// <summary>
        /// " DATE_FORMAT"
        /// </summary>
        DateFormat,

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
        /// " in "
        /// </summary>
        In,
        /// <summary>
        /// " not in "
        /// </summary>
        NotIn,
        InHelper,

        /// <summary>
        /// " count"
        /// </summary>
        Count,

        /// <summary>
        /// " sum"
        /// </summary>
        Sum

    }
}
