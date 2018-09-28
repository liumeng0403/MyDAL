using System;
using System.Linq.Expressions;

namespace Yunyong.DataExchange
{
    /// <summary>
    /// &lt;=
    /// </summary>
    public class LessThanOrEqual<M>
    {
        internal Expression<Func<M, object>> Func;

        internal string Field { get; set; }
        internal object Value { get; set; }
        public LessThanOrEqual(Expression<Func<M, object>> field, object value)
        {
            Value = value;
            Func = field;
        }
    }
}
