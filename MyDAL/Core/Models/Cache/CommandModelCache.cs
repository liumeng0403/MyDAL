using System;
using System.Collections.Generic;
using System.Reflection;

namespace MyDAL.Core.Models.Cache
{
    internal class CommandModelCache
    {
        /// <summary>
        /// command type
        /// </summary>
        internal Type CType { get; set; }
        
        /// <summary>
        /// command properties
        /// </summary>
        internal List<PropertyInfo> CProps { get; set; }
        
        internal PropertyInfo AutoValue { get; set; }
    }
}