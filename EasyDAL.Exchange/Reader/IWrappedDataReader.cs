using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EasyDAL.Exchange.Reader
{
    /// <summary>
    /// Describes a reader that controls the lifetime of both a command and a reader,
    /// exposing the downstream command/reader as properties.
    /// </summary>
    public interface IWrappedDataReader : IDataReader
    {
        /// <summary>
        /// Obtain the underlying reader
        /// </summary>
        IDataReader Reader { get; }
        /// <summary>
        /// Obtain the underlying command
        /// </summary>
        IDbCommand Command { get; }
    }
}
