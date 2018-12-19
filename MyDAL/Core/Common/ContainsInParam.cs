using System.Linq.Expressions;

namespace Yunyong.DataExchange.Core.Common
{
    internal class ContainsInParam
    {
        internal bool Flag { get; set; }
        internal ExpressionType Type { get; set; }
        internal Expression Key { get; set; }
        internal Expression Val { get; set; }
    }
}
