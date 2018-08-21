using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDAL.Exchange.DynamicParameter
{
    /// <summary>
    /// Extends IDynamicParameters with facilities for executing callbacks after commands have completed
    /// </summary>
    public interface IParameterCallbacks : IDynamicParameters
    {
        /// <summary>
        /// Invoked when the command has executed
        /// </summary>
        void OnCompleted();
    }
}
