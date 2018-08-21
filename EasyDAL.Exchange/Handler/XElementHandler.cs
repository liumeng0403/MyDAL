using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace EasyDAL.Exchange.Handler
{
    internal sealed class XElementHandler : XmlTypeHandler<XElement>
    {
        protected override XElement Parse(string xml) => XElement.Parse(xml);
        protected override string Format(XElement xml) => xml.ToString();
    }
}
