using MyDAL.Test.Parallel;
using System;

namespace MyDAL.Test.QueryParallel
{
    class Program
    {
        static void Main(string[] args)
        {
            //new _01_QueryOneAsync().QueryOneAsyncTest();      //  01 
            //new HttpTest().HttpApiTest();
            new _03_XmlTest().TestXmlLoad();
        }
    }
}
