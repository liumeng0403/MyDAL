using MyDAL.Test.Entities.EasyDal_Exchange;
using Yunyong.DataExchange;

namespace MyDAL.Test.QueryParallel
{
    public class FirstOrDefaultAsync : TestBase
    {
        public None test(None none)
        {
            var res5 = (Conn
                .Selecter<Agent>()
                .Where(it => it.Name == "刘中华")
                .Distinct()
                .FirstOrDefaultAsync()).GetAwaiter().GetResult();
            return none;
        }

        public void FirstOrDefaultAsyncTest()
        {
            var parallel = new XParallelTest();
            parallel.IsLookTask = true;
            parallel.Request = new None();
            parallel.Response = new None();
            parallel.TargetFunc = new FirstOrDefaultAsync().test;
            parallel.ApiDebug();
            //parallel.Parallel_100_10000();
            //parallel.Parallel_90_10000();
            //parallel.Parallel_80_10000();
            //parallel.Parallel_70_10000();
            //parallel.Parallel_60_10000();
            //parallel.Parallel_50_10000();
            //parallel.Parallel_40_10000();
            //parallel.Parallel_30_10000();
            parallel.Parallel_20_10000();
            //parallel.Parallel_10_10000();
            //parallel.Parallel_5_10000();
            //parallel.Parallel_1_10000();
        }
    }
}
