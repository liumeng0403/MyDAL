using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EasyDAL.Exchange.Handler
{
    /// <summary>
    /// Base-class for simple type-handlers
    /// </summary>
    /// <typeparam name="T">This <see cref="Type"/> this handler is for.</typeparam>
    public abstract class TypeHandler<T> : ITypeHandler
    {
        /// <summary>
        /// 设置 Param 的值 
        /// </summary>
        public abstract void SetValue(IDbDataParameter parameter, T value);

        /// <summary>
        /// Parse a database value back to a typed value
        /// </summary>
        /// <param name="value">The value from the database</param>
        /// <returns>The typed value</returns>
        public abstract T Parse(object value);

        void ITypeHandler.SetValue(IDbDataParameter parameter, object value)
        {
            if (value is DBNull)
            {
                parameter.Value = value;
            }
            else
            {
                SetValue(parameter, (T)value);
            }
        }

        /// <summary>
        /// ITypeHandler.Parse 方法
        /// </summary>
        object ITypeHandler.Parse(Type destinationType, object value)
        {
            return Parse(value);
        }
    }
}
