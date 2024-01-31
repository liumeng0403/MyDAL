using MyDAL.Core.Bases;
using System.Data;

namespace MyDAL.Core
{
    internal class XContext
        : Context
    {
        internal XContext(XConnection xConn)
        {
            Init(xConn);
        }
    }

    internal class XContext<M1>
        : Context
        where M1 : class
    {
        internal XContext(XConnection xConn)
        {
            Init(xConn);
            SetMTCache<M1>();
        }
    }

    internal class XContext<M1, M2>
        : Context
        where M1 : class
        where M2 : class
    {
        internal XContext(XConnection xConn)
        {
            Init(xConn);
            SetMTCache<M1>();
            SetMTCache<M2>();
        }
    }

    internal class XContext<M1, M2, M3>
        : Context
        where M1 : class
        where M2 : class
        where M3 : class
    {
        internal XContext(XConnection xConn)
        {
            Init(xConn);
            SetMTCache<M1>();
            SetMTCache<M2>();
            SetMTCache<M3>();
        }
    }
}
