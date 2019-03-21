using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.TestData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Create
{
    public class _02_CreateBatchTest 
        : TestBase
    {
        [Fact]
        public async Task Test()
        {

            /********************************************************************************************************************************/

            var list1 = await new CreateData().PreCreateBatch(Conn);

            xx = string.Empty;

            var res1 = await Conn.CreateBatchAsync(list1);

            Assert.True(res1 == 10);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            var json = File.ReadAllText(@"C:\Users\liume\Desktop\Work\DalTestDB\ProfileData.json");
            var list = JsonConvert.DeserializeObject<List<UserInfo>>(json);
            foreach (var item in list)
            {
                item.Id = Guid.NewGuid();
                item.CreatedOn = DateTime.Now;
            }

            xx = string.Empty;

            Assert.True(!list.Any(it => it.RootUser));
            Assert.True(!list.Any(it => it.InvitedCount > 0));
            Assert.True(!list.Any(it => !string.IsNullOrWhiteSpace(it.ArrangePathId)));
            Assert.True(!list.Any(it => it.IsVIP));
            Assert.True(!list.Any(it => it.IsActived));

            var res4 = await Conn.CreateBatchAsync(list);

            Assert.True(res4 == list.Count);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            xx = string.Empty;

        }

    }
}
