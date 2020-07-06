using System.Diagnostics;

namespace MyDAL.Core.Common.Tools
{
    internal sealed class RuntimeInfo
    {
        private bool _IsDebug { get; set; }

        internal RuntimeInfo()
        {
            this._IsDebug = false;
            ChangeStatus();
        }

        [Conditional("TRACE")]
        private void ChangeStatus()
        {
            this._IsDebug = true;
        }

        internal bool IsDebug
        {
            get
            {
                return this._IsDebug;
            }
        }

    }
}
