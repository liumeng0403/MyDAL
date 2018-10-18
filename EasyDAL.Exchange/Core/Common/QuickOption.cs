using System.Collections.Generic;

namespace MyDAL.Core.Common
{
    internal class QuickOption
        : IQueryOption
    {
        public List<OrderBy> OrderBys { get; set; }
    }
}
