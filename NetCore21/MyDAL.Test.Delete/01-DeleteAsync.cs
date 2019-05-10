using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Delete
{
    public class _01_DeleteAsync
        : TestBase
    {
        private async Task<BodyFitRecord> PreDelete()
        {
            xx = string.Empty;

            // 造数据 
            var m = new BodyFitRecord
            {
                Id = Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"),
                CreatedOn = DateTime.Now,
                UserId = Guid.NewGuid(),
                BodyMeasureProperty = "{xxx:yyy,mmm:nnn}"
            };

            var res = await Conn
                .Deleter<BodyFitRecord>()
                .Where(it => it.Id == m.Id)
                .DeleteAsync();

            var res0 = await Conn.CreateAsync(m);

            return m;
        }

        [Fact]
        public async Task History_01()
        {
            xx = string.Empty;

            var pk2 = Guid.Parse("72d551bf-d9f4-4817-800f-01655794cf42");
            var res2 = await Conn.DeleteAsync<AlipayPaymentRecord>(it => it.Id == pk2);
            Assert.True(res2 == 1);

            

            var res21 = await Conn.QueryOneAsync<AlipayPaymentRecord>(it => it.Id == pk2);

            Assert.Null(res21);

            /****************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task History_02()
        {
            xx = string.Empty;

            var path = "~00-c-1-2-1-1-1-1-1-4-1-1-1-4-1-2-1-7";
            var level = 2;
            // where and
            var res3 = await Conn
                .Deleter<Agent>()
                .Where(it => it.PathId == path)
                .And(it => it.AgentLevel == (AgentLevel)level)
                .DeleteAsync();
            Assert.True(res3 == 1);

            

            xx = string.Empty;

            // where or
            var res2 = await Conn
                .Deleter<Agent>()
                .Where(it => it.PathId == path)
                .Or(it => it.AgentLevel == (AgentLevel)level)
                .DeleteAsync();
            Assert.True(res2 == 28063);

            

            var xx4 = string.Empty;

            // where and or
            var res4 = await Conn
                .Deleter<Agent>()
                .Where(it => it.PathId == path)
                .And(it => it.AgentLevel == (AgentLevel)level)
                .Or(it => it.CreatedOn >= WhereTest.StartTime)
                .DeleteAsync();

            

            xx = string.Empty;
        }

        [Fact]
        public async Task Mock_DeleteAll_Shortcut()
        {
            xx = string.Empty;

            var res1 = await Conn.DeleteAsync<WechatUserInfo>(it=>true);   //  WechatUserInfo -- 空表 

            Assert.True(res1 == 0);

            

            xx = string.Empty;
        }

        [Fact]
        public async Task Delete_Shortcut()
        {

            xx = string.Empty;
            
            var res1 = await Conn.DeleteAsync<AlipayPaymentRecord>(it => it.Id == Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d"));

            Assert.True(res1 == 1);

            

            var res11 = await Conn.QueryOneAsync<AlipayPaymentRecord>(it => it.Id == Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d"));

            Assert.Null(res11);

            /****************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task Mock_DeleteAll_ST()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Deleter<WechatUserInfo>()  //  WechatUserInfo -- 空表 
                .DeleteAsync();

            Assert.True(res1 == 0);

            

            xx = string.Empty;
        }

        [Fact]
        public async Task Delete_ST()
        {

            var m = await PreDelete();

            xx = string.Empty;

            // where 
            var res1 = await Conn
                .Deleter<BodyFitRecord>()
                .Where(it => it.Id == m.Id)
                .DeleteAsync();

            Assert.True(res1 == 1);

            

            xx = string.Empty;
        }

        [Fact]
        public async Task Mock_DeleteNoneCondition_MT()
        {
            /*
             * 多表连接方式删除表数据，自己写 SQL
             * 然后使用方法： IDbConnection.ExecuteNonQueryAsync(string sql, List<XParam> dbParas = null)
             * 方法命名空间：using MyDAL;
             * 方法描述： async Task<int> ExecuteNonQueryAsync(this IDbConnection conn, string sql, List<XParam> dbParas = null)
             */

            try
            {
                await None();
            }
            catch { }
        }

        [Fact]
        public async Task Delete_MT()
        {
            /*
             * 多表连接方式删除表数据，自己写 SQL
             * 然后使用方法： IDbConnection.ExecuteNonQueryAsync(string sql, List<XParam> dbParas = null)
             * 方法命名空间：using MyDAL;
             * 方法描述： async Task<int> ExecuteNonQueryAsync(this IDbConnection conn, string sql, List<XParam> dbParas = null)
             */

            try
            {
                await None();
            }
            catch { }
        }
    }
}
