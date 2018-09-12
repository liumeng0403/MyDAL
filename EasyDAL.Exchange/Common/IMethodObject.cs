using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Yunyong.DataExchange.Common
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IMethodObject
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object obj);
        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();
        [EditorBrowsable(EditorBrowsableState.Never)]
        Type GetType();
        [EditorBrowsable(EditorBrowsableState.Never)]
        string ToString();
    }
}
