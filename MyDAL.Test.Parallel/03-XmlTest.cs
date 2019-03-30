using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyDAL.Test.Parallel
{
    public class _03_XmlTest
    {
        public void TestXmlLoad()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(@"C:\Users\liume\Desktop\Work\DalTestDB\xml.txt");
            //xml.NameTable
            //var item = xml.SelectSingleNode("/");
            //var txt01 = item.InnerText;
            var item = xml["xml"];
            var txt01 = item["appid"].InnerText;
        }
       
    }
}
