using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dapper.Handler
{
    internal abstract class XmlTypeHandler<T> : StringTypeHandler<T>
    {
        public override void SetValue(IDbDataParameter parameter, T value)
        {
            base.SetValue(parameter, value);
            parameter.DbType = DbType.Xml;
        }
    }
}
