using System;

namespace MyDAL.Test.QueryParallel
{
    class Program
    {
        static void Main(string[] args)
        {
            new FirstOrDefaultAsync().FirstOrDefaultAsyncTest();
            //new HttpTest().HttpAsyncTest();

        }
    }
}
