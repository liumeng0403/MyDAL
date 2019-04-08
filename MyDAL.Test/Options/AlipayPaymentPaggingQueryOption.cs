using HPC.DAL;
using System;

namespace MyDAL.Test.Options
{
    public class AlipayPaymentPaggingQueryOption 
        : PagingOption
    {
        [XQuery(Column = "CreatedOn", Compare = CompareEnum.GreaterThanOrEqual)]
        public DateTime StartTime { get; set; }
    }
}
