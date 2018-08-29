using EasyDAL.Exchange.AdoNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;

namespace EasyDAL.Exchange.Handler
{
    /// <summary>
    /// Not intended for direct usage
    /// </summary>
    /// <typeparam name="T">The type to have a cache for.</typeparam>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class TypeHandlerCache<T>
    {
        /// <summary>
        /// Not intended for direct usage.
        /// </summary>
        /// <param name="value">The object to parse.</param>
        public static T Parse(object value) => 
            (T)handler.Parse(typeof(T), value);
        
        private static ITypeHandler handler { get; set; }
    }
}
