using System;
using System.Collections.Generic;
using System.Text;

namespace MyDAL.Test.Options
{
    public class AlipayPaymentPaggingQueryOption : PagingQueryOption
    {
        [QueryColumn("CreatedOn", CompareEnum.GreaterThanOrEqual)]
        public DateTime StartTime { get; set; }
    }
}
