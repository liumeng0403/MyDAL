using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EasyDAL.Exchange.MapperX
{
    /// <summary>
    /// Handles variances in features per DBMS
    /// </summary>
    internal class FeatureSupport
    {
        private static readonly FeatureSupport
            Default = new FeatureSupport(false),
            Postgres = new FeatureSupport(true);
        
        private FeatureSupport(bool arrays)
        {
            Arrays = arrays;
        }

        /// <summary>
        /// True if the db supports array columns e.g. Postgresql
        /// </summary>
        public bool Arrays { get; }
    }
}
