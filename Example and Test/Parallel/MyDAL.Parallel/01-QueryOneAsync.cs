using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Parallels;

namespace MyDAL.Parallel
{
    public class _01_QueryOneAsync
         : TestBase
    {
        public None test(None none)
        {
            var res5 = (Conn
                .Queryer<Agent>()
                .Where(it => it.Name == "刘中华")
                .Distinct()
                .QueryOneAsync()).GetAwaiter().GetResult();

            return none;
        }

        public void QueryOneAsyncTest()
        {
            var parallel = new XParallelTest();
            parallel.IsLookTask = true;
            parallel.Request = new None();
            parallel.Response = new None();
            parallel.TargetFunc = () => new _01_QueryOneAsync().test;
            parallel.ApiDebug();
            //parallel.Parallel_100_10000();
            //parallel.Parallel_90_10000();
            //parallel.Parallel_80_10000();
            //parallel.Parallel_70_10000();
            //parallel.Parallel_60_10000();
            parallel.Parallel_50_10000();
            //parallel.Parallel_40_10000();
            //parallel.Parallel_30_10000();
            //parallel.Parallel_20_10000();
            //parallel.Parallel_10_10000();
            //parallel.Parallel_5_10000();
            //parallel.Parallel_1_10000();
        }
    }
}
