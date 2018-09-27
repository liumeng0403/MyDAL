using MyDAL;
using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace MyDAL.Test.Create
{
    public class _01_CreateTest : TestBase
    {
        private async Task PreCreate(BodyFitRecord m)
        {
            // 清除数据

            var xx2 = "";

            var res2 = await Conn
                .Deleter<BodyFitRecord>()
                .Where(it => it.Id == Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"))
                .DeleteAsync();
        }
        private async Task<List<AddressInfo>> PreCreateBatch()
        {
            var res1 = await Conn
                .Deleter<AddressInfo>()
                .Where(a => true)
                .DeleteAsync();

            var list = new List<AddressInfo>();
            for (var i = 0; i < 10; i++)
            {
                if (i % 2 == 0)
                {
                    list.Add(new AddressInfo
                    {
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTime.Now,
                        ContactName = "Name_" + i.ToString(),
                        ContactPhone = "1800000000" + i.ToString(),
                        DetailAddress = "Address_" + i.ToString(),
                        IsDefault = true,   // f:bool c:bit(1)
                        UserId = Guid.NewGuid()
                    });
                }
                else
                {
                    list.Add(new AddressInfo
                    {
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTime.Now,
                        ContactName = "Name_" + i.ToString(),
                        ContactPhone = "1800000000" + i.ToString(),
                        DetailAddress = "Address_" + i.ToString(),
                        IsDefault = false,   // f:bool c:bit(1)
                        UserId = Guid.NewGuid()
                    });
                }
            }
            return list;
        }

        // 创建一个新对象
        [Fact]
        public async Task CreateAsyncTest()
        {

            /********************************************************************************************************************************/

            var m1 = new BodyFitRecord
            {
                Id = Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"),
                CreatedOn = DateTime.Now,
                UserId = Guid.NewGuid(),
                BodyMeasureProperty = "{xxx:yyy,mmm:nnn}"
            };
            await PreCreate(m1);

            var xx1 = "";

            // 新建
            var res1 = await Conn
                .Creater<BodyFitRecord>()
                .CreateAsync(m1);
            Assert.True(res1 == 1);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************************************/

            var m2 = new Agent
            {
                Id = Guid.NewGuid(),
                CreatedOn = DateTime.Now,
                UserId = Guid.NewGuid(),
                PathId = "x-xx-xxx-xxxx",
                Name = "张三",
                Phone = "18088889999",
                IdCardNo = "No.12345",
                CrmUserId = "yyyyy",
                AgentLevel = AgentLevel.DistiAgent,
                ActivedOn = null,   // DateTime?
                ActiveOrderId = null,  // Guid?
                DirectorStarCount = 5
            };

            var xx2 = "";

            var res2 = await Conn
                .Creater<Agent>()
                .CreateAsync(m2);
            Assert.True(res2 == 1);

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************************************/

            var list3 = await PreCreateBatch();

            var xx3 = "";

            var res3 = await Conn
                .Creater<AddressInfo>()
                .CreateBatchAsync(list3);
            Assert.True(res3 == 10);

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************************************/

            var json = File.ReadAllText(@"C:\Users\Administrator.DESKTOP-UH5FN5U\Desktop\工作\DalTestDB\ProfileData.json");
            var list = JsonConvert.DeserializeObject<List<UserInfo>>(json);
            foreach (var item in list)
            {
                item.Id = Guid.NewGuid();
                item.CreatedOn = DateTime.Now;
            }

            var xx4 = "";

            var res4 = await Conn
                .Creater<UserInfo>()
                .CreateBatchAsync(list);
            Assert.True(res4 == list.Count);

            var tuple4 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************************************/

            var xx = "";
        }

    }
}
