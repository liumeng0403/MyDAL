using System;
using System.Collections.Generic;
using System.Text;

namespace MyDAL.Test.Common
{
    /// <summary>
    /// T 实例
    /// </summary>
    internal class ClassInstance<T>
        where T : class, new()
    {
        /// <summary>
        /// T 实例
        /// </summary>
        internal static T Instance
        {
            get
            {
                return new T();
            }
        }
    }
}
