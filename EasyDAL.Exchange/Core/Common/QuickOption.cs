using System.Collections.Generic;
using Yunyong.Core;

namespace Yunyong.DataExchange.Core.Common
{
    internal class QuickOption
        : IQueryOption
    {
        public List<OrderBy> OrderBys { get; set; }
    }
}
