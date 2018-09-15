using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;

namespace EasyDAL.Exchange.UserFacade.Join
{
    public class OnX : Operator, IMethodObject
    {

        internal OnX(DbContext dc)
            : base(dc)
        { }



    }
}
