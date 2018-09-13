using EasyDAL.Exchange.Tests.Entities.EasyDal_Exchange;
using System.Threading.Tasks;
using Xunit;

namespace EasyDAL.Exchange.Tests
{
    public class TransactionTest : TestBase
    {
        [Fact]
        public async Task test01()
        {
            // return string
            var tuple1 = await Conn
                .Transactioner()
                .BusinessUnitOption(async () => "xxxxyyyyzzzzz");

            // return M data
            var tuple2 = await Conn
                .Transactioner()
                .BusinessUnitOption(async () =>
                {

                    //
                    var dbRecord = await Conn
                        .Selecter<WechatUserInfo>()
                        .Where(it => it.OpenId == "xxxyyyyzzz")
                        .QueryFirstOrDefaultAsync();

                    return dbRecord;

                });

            // return (string errMsg, M data)
            var tuple3 = await Conn
                .Transactioner()
                .BusinessUnitOption(async () =>
                {
                    //
                    var dbRecord = await Conn
                        .Selecter<WechatUserInfo>()
                        .Where(it => it.OpenId == "xxxyyyyzzz")
                        .QueryFirstOrDefaultAsync();
                    if (dbRecord != null) // 记录存在
                    {
                        return (string.Empty, true);
                    }

                    return (string.Empty, false);
                });
        }

    }
}
