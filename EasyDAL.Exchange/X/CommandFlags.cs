using System;
using System.Collections.Generic;
using System.Text;

namespace Yunyong.DataExchange.X
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
        /// <summary>
        /// Can async queries be pipelined?
        /// </summary>
        Pipelined = 2,
        /// <summary>
        /// Should the plan cache be bypassed?
        /// </summary>
        NoCache = 4,
    }
}
