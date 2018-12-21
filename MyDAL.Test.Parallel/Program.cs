using System;

namespace MyDAL.Test.QueryParallel
{
    class Program
    {
        static void Main(string[] args)
        {
            new FirstOrDefaultAsync().FirstOrDefaultAsyncTest();      //  01 
            //new HttpTest().HttpApiTest();

        }
    }
}
