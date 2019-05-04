using HPC.DAL.Core.Bases;
using System.Data;

namespace HPC.DAL.Core
{
    internal class XContext
        : Context
    {
        internal XContext(IDbConnection conn,IDbTransaction tran)
        {
            Init(conn,tran);
        }
    }

    internal class XContext<M1>
        : Context
        where M1 : class
    {
        internal XContext(IDbConnection conn,IDbTransaction tran)
        {
            Init(conn,tran);
            SetMTCache<M1>();
        }
    }

    internal class XContext<M1, M2>
        : Context
        where M1 : class
        where M2 : class
    {
        internal XContext(IDbConnection conn,IDbTransaction tran)
        {
            Init(conn,tran);
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
        internal XContext(IDbConnection conn,IDbTransaction tran)
        {
            Init(conn,tran);
            SetMTCache<M1>();
            SetMTCache<M2>();
            SetMTCache<M3>();
        }
    }

    internal class XContext<M1, M2, M3, M4>
        : Context
        where M1 : class
        where M2 : class
        where M3 : class
        where M4 : class
    {
        internal XContext(IDbConnection conn,IDbTransaction tran)
        {
            Init(conn,tran);
            SetMTCache<M1>();
            SetMTCache<M2>();
            SetMTCache<M3>();
            SetMTCache<M4>();
        }
    }

    internal class XContext<M1, M2, M3, M4, M5>
        : Context
        where M1 : class
        where M2 : class
        where M3 : class
        where M4 : class
        where M5 : class
    {
        internal XContext(IDbConnection conn,IDbTransaction tran)
        {
            Init(conn,tran);
            SetMTCache<M1>();
            SetMTCache<M2>();
            SetMTCache<M3>();
            SetMTCache<M4>();
            SetMTCache<M5>();
        }
    }

    internal class XContext<M1, M2, M3, M4, M5, M6>
        : Context
        where M1 : class
        where M2 : class
        where M3 : class
        where M4 : class
        where M5 : class
        where M6 : class
    {
        internal XContext(IDbConnection conn,IDbTransaction tran)
        {
            Init(conn,tran);
            SetMTCache<M1>();
            SetMTCache<M2>();
            SetMTCache<M3>();
            SetMTCache<M4>();
            SetMTCache<M5>();
            SetMTCache<M6>();
        }
    }
}
