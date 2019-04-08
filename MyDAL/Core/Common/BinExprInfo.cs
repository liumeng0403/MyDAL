using HPC.DAL.Core.Enums;
using System.Linq.Expressions;

namespace HPC.DAL.Core.Common
{
    internal class BinExprInfo
    {
        internal Expression Left { get; set; }
        internal Expression Right { get; set; }
        internal ExpressionType Node { get; set; }
        internal CompareXEnum Compare { get; set; }
    }
}
