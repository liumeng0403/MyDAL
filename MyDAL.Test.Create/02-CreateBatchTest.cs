using MyDAL.Test.Entities.EasyDal_Exchange;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Create
{
    public class _02_CreateBatchTest:TestBase
    {
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


        [Fact]
        public async Task Test()
        {

            /********************************************************************************************************************************/

            var list3 = await PreCreateBatch();

            var xx3 = "";

            var res3 = await Conn
                .Creater<AddressInfo>()
                .CreateBatchAsync(list3);
            Assert.True(res3 == 10);

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************************************/

            var json = File.ReadAllText(@"C:\Users\liume\Desktop\工作\DalTestDB\ProfileData.json");
            var list = JsonConvert.DeserializeObject<List<UserInfo>>(json);
            foreach (var item in list)
            {
                item.Id = Guid.NewGuid();
                item.CreatedOn = DateTime.Now;
            }

            var xx4 = "";

            Assert.True(!list.Any(it => it.RootUser));
            Assert.True(!list.Any(it => it.InvitedCount > 0));
            Assert.True(!list.Any(it => !string.IsNullOrWhiteSpace(it.ArrangePathId)));
            Assert.True(!list.Any(it => it.IsVIP));
            Assert.True(!list.Any(it => it.IsActived));
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
