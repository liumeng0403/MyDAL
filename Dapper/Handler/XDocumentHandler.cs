using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Dapper.Handler
{
    internal sealed class XDocumentHandler : XmlTypeHandler<XDocument>
    {
        protected override XDocument Parse(string xml) => XDocument.Parse(xml);
        protected override string Format(XDocument xml) => xml.ToString();
    }
}
