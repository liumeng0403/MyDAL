using System.Linq.Expressions;

namespace MyDAL.Core.Models.ExpPara
{
    internal class EqualsParam
    {
        internal bool Flag { get; set; }
        internal ExpressionType Type { get; set; }
        internal Expression Val { get; set; }
    }
}