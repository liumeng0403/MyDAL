using System;
using Yunyong.Core;
using Yunyong.DataExchange;

namespace MyDAL.Test.Options
{
    public class AlipayPaymentQueryOption : QueryOption
    {
        [QueryColumn("CreatedOn", CompareEnum.GreaterThanOrEqual)]
        public DateTime StartTime { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
