using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Dapper.Handler
{
    internal sealed class XmlDocumentHandler : XmlTypeHandler<XmlDocument>
    {
        protected override XmlDocument Parse(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc;
        }

        protected override string Format(XmlDocument xml)=>   xml.OuterXml;
    }
}
