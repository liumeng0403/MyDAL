using MyDAL.Test.Entities.MyDAL_TestDB;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.ShortcutAPI
{
    public class _02_BussinessUnitOption : TestBase
    {

        [Fact]
        public async Task test()
        {

            /***********************************************************************************************************/

            // return string
            var ts1 = await Conn
                .Transactioner()
                .BusinessUnitOption(async () => "xxxxyyyyzzzzz");
            Assert.Equal("xxxxyyyyzzzzz", ts1);

            /***********************************************************************************************************/

            // return M data
            var ts2 = await Conn
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
            Assert.Null(ts2.data);

            /***********************************************************************************************************/

            // return (string errMsg, M data)
            var ts3 = await Conn
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
            Assert.False(ts3.data);

            /***********************************************************************************************************/

            xx = string.Empty;

        }

    }
}
