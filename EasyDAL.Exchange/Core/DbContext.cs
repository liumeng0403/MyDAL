
using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.Cache;
using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.ExpressionX;
using EasyDAL.Exchange.Extensions;
using EasyDAL.Exchange.Helper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Core
{
    internal class DbContext : Context
    {
        internal DbContext(IDbConnection conn)
        {
            Init(conn);
        }
    }

    internal class DbContext<M1>:Context
    {
        internal DbContext(IDbConnection conn)
        {
            Init(conn);
            SetMTCache<M1>();
        }
    }

    internal class DbContext<M1,M2> : Context
    {
        internal DbContext(IDbConnection conn)
        {
            Init(conn);
            SetMTCache<M1>();
            SetMTCache<M2>();
        }
    }

    internal class DbContext<M1, M2,M3> : Context
    {
        internal DbContext(IDbConnection conn)
        {
            Init(conn);
            SetMTCache<M1>();
            SetMTCache<M2>();
            SetMTCache<M3>();
        }
    }

    internal class DbContext<M1, M2, M3,M4> : Context
    {
        internal DbContext(IDbConnection conn)
        {
            Init(conn);
            SetMTCache<M1>();
            SetMTCache<M2>();
            SetMTCache<M3>();
            SetMTCache<M4>();
        }
    }

    internal class DbContext<M1, M2, M3, M4,M5> : Context
    {
        internal DbContext(IDbConnection conn)
        {
            Init(conn);
            SetMTCache<M1>();
            SetMTCache<M2>();
            SetMTCache<M3>();
            SetMTCache<M4>();
            SetMTCache<M5>();
        }
    }

    internal class DbContext<M1, M2, M3, M4, M5,M6> : Context
    {
        internal DbContext(IDbConnection conn)
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
