using System;
using Yunyong.Core;
using Yunyong.DataExchange;

namespace MyDAL.Test.Options
{
    public class AlipayPaymentPaggingQueryOption : PagingQueryOption
    {
        [QueryColumn("CreatedOn", CompareEnum.GreaterThanOrEqual)]
        public DateTime StartTime { get; set; }
    }
}
