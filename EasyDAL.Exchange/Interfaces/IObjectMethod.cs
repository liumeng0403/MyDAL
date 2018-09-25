using System;
using System.ComponentModel;

namespace EasyDAL.Exchange.Interfaces
{
    internal interface IObjectMethod
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
