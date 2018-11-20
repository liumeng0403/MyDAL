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
    }
}
