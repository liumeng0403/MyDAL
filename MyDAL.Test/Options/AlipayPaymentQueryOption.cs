using System;
using System.Collections.Generic;
using System.Text;

namespace MyDAL.Test.Options
{
    public class AlipayPaymentQueryOption : QueryOption
    {
        [QueryColumn("CreatedOn", CompareEnum.GreaterThanOrEqual)]
        public DateTime StartTime { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
