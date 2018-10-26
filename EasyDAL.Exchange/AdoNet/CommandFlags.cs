using System;

namespace MyDAL.AdoNet
{
    /// <summary>
    /// Additional state flags that control command behaviour
    /// </summary>
    [Flags]
    internal enum CommandFlags
    {
        /// <summary>
        /// No additional flags
        /// </summary>
        None = 0,
        /// <summary>
        /// Should data be buffered before returning?
        /// </summary>
        Buffered = 1,
    }
}
