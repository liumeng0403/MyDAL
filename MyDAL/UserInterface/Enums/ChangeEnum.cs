using System.ComponentModel;

namespace MyDAL
{
    public enum ChangeEnum
    {
        /// <summary>
        /// +
        /// </summary>
        [Description("+")]
        Add,
        /// <summary>
        /// -
        /// </summary>
        [Description("-")]
        Minus
    }
}
