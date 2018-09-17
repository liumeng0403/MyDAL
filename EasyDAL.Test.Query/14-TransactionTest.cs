using EasyDAL.Exchange;
using EasyDAL.Test.Entities.EasyDal_Exchange;
using System.Threading.Tasks;
using Xunit;

namespace EasyDAL.Test.Query
{
    public class _14_TransactionTest : TestBase
    {
        [Fact]
        public async Task test01()
        {

            /***********************************************************************************************************/

            // return string
            var tuple1 = await Conn
                .Transactioner()
                .BusinessUnitOption(async () => "xxxxyyyyzzzzz");
            Assert.Equal("xxxxyyyyzzzzz", tuple1);

            /***********************************************************************************************************/

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
            Assert.Null(tuple2.data);

            /***********************************************************************************************************/

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
            Assert.False(tuple3.data);

            /***********************************************************************************************************/

            var xx = "";

        }

    }
}
