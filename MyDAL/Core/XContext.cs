using System.Data;
using Yunyong.DataExchange.Core.Bases;

namespace Yunyong.DataExchange.Core
{
    internal class XContext : Context
    {
        internal XContext(IDbConnection conn)
        {
            Init(conn);
        }
    }

    internal class XContext<M1>:Context
    {
        internal XContext(IDbConnection conn)
        {
            Init(conn);
            SetMTCache<M1>();
        }
    }

    internal class XContext<M1,M2> : Context
    {
        internal XContext(IDbConnection conn)
        {
            Init(conn);
            SetMTCache<M1>();
            SetMTCache<M2>();
        }
    }

    internal class XContext<M1, M2,M3> : Context
    {
        internal XContext(IDbConnection conn)
        {
            Init(conn);
            SetMTCache<M1>();
            SetMTCache<M2>();
            SetMTCache<M3>();
        }
    }

    internal class XContext<M1, M2, M3,M4> : Context
    {
        internal XContext(IDbConnection conn)
        {
            Init(conn);
            SetMTCache<M1>();
            SetMTCache<M2>();
            SetMTCache<M3>();
            SetMTCache<M4>();
        }
    }

    internal class XContext<M1, M2, M3, M4,M5> : Context
    {
        internal XContext(IDbConnection conn)
        {
            Init(conn);
            SetMTCache<M1>();
            SetMTCache<M2>();
            SetMTCache<M3>();
            SetMTCache<M4>();
            SetMTCache<M5>();
        }
    }

    internal class XContext<M1, M2, M3, M4, M5,M6> : Context
    {
        internal XContext(IDbConnection conn)
        {
            Init(conn);
            SetMTCache<M1>();
            SetMTCache<M2>();
            SetMTCache<M3>();
            SetMTCache<M4>();
            SetMTCache<M5>();
            SetMTCache<M6>();
        }
    }
}
