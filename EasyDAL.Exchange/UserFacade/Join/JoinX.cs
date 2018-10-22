using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyDAL.Core.Bases;
using MyDAL.Interfaces;

namespace MyDAL.UserFacade.Join
{
    public sealed class JoinX 
        : Operator
    {

        internal JoinX(Context dc)
            : base(dc)
        { }



    }
}
