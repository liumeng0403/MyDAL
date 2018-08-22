using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDAL.Exchange.Common
{
    /// <summary>
    ///     Object 实例
    /// </summary>
    public class ClassInstance<T>
        where T : class, new()
    {
        /// <summary>
        ///     T 实例
        /// </summary>
        public static T Instance
        {
            get => new T();
        }
    }
}
