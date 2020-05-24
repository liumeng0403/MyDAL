using System;

namespace MyDAL.Parallel
{
    class Program
    {
        static void Main(string[] args)
        {
            //new _01_QueryOneAsync().QueryOneAsyncTest();      //  01 
            //new _02_HttpTest().HttpApiTest();
            new _02_HttpTest_lv().HttpApiTest();
            //new _03_XmlTest().TestXmlLoad();
        }
    }
}
