using System;
using System.Linq.Expressions;

namespace Yunyong.DataExchange
{
    /// <summary>
    /// &gt;=
    /// </summary>
    public class GreaterThanOrEqual<M>
    {
        internal Expression<Func<M, object>> Func;
        
        internal string Field { get; set; }
        internal object Value { get; set; }
        public GreaterThanOrEqual(Expression<Func<M, object>> field, object value)
        {
            Value = value;
            Func = field;
        }
    }
}
