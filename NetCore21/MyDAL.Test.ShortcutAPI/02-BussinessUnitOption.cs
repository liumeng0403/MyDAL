using MyDAL.Test.Entities.EasyDal_Exchange;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.ShortcutAPI
{
    public class _02_BussinessUnitOption:TestBase
    {

        [Fact]
        public async Task test()
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
                        .Queryer<WechatUserInfo>()
                        .Where(it => it.OpenId == "xxxyyyyzzz")
                        .FirstOrDefaultAsync();

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
                        .Queryer<WechatUserInfo>()
                        .Where(it => it.OpenId == "xxxyyyyzzz")
                        .FirstOrDefaultAsync();
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
