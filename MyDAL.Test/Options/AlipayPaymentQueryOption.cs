using System;
using System.Collections.Generic;
using System.Text;

namespace MyDAL.Test.Options
{
    public class AlipayPaymentQueryOption : QueryOption
    {
        [XQuery(Name ="CreatedOn",Compare = CompareEnum.GreaterThanOrEqual)]
        public DateTime StartTime { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
