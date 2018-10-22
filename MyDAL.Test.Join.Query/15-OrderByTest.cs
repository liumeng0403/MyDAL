using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MyDAL;
using System.Linq;

namespace MyDAL.Test.Join.Query
{
    public class _15_OrderByTest :TestBase
    {

        [Fact]
        public async Task test()
        {

            /*********************************************************************************************************************************************************/

            var xx1 = "";

            var res1 = await Conn
                .Joiner<AspnetUsers, AspnetUserRoles, AspnetRoles>(out var user1, out var userRole1, out var role1)
                .From(() => user1)
                .InnerJoin(() => userRole1).On(() => user1.Id == userRole1.UserId)
                .InnerJoin(() => role1).On(() => userRole1.RoleId == role1.Id)
                .OrderBy(() => user1.UserName)
                .QueryListAsync<AspnetUsers>();
            Assert.True(res1.Count == 29180);
            Assert.True(res1.First().UserName == "45285586990");

            var tuple1 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*********************************************************************************************************************************************************/

            var xx2 = "";

            // order by id  -- 手动查看
            var res2 = await Conn
                .Joiner<AspnetUsers, AspnetUserRoles, AspnetRoles>(out var user2, out var userRole2, out var role2)
                .From(() => user2)
                .InnerJoin(() => userRole2).On(() => user2.Id == userRole2.UserId)
                .InnerJoin(() => role2).On(() => userRole2.RoleId == role2.Id)
                .QueryListAsync<AspnetUsers>();
            Assert.True(res2.Count == 29180);

            var tuple2 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*********************************************************************************************************************************************************/

            var xx3 = "";

            // order by createdon -- 手动查看
            var res3 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent3, out var record3)
                .From(() => agent3)
                .InnerJoin(() => record3).On(() => agent3.Id == record3.AgentId)
                .QueryListAsync<Agent>();
            Assert.True(res3.Count == 574);

            var tuple3 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*********************************************************************************************************************************************************/

            var xx4 = "";

            var res4 = await Conn
                .Joiner<AspnetUsers, AspnetUserRoles, AspnetRoles>(out var user4, out var userRole4, out var role4)
                .From(() => user4)
                .InnerJoin(() => userRole4).On(() => user4.Id == userRole4.UserId)
                .InnerJoin(() => role4).On(() => userRole4.RoleId == role4.Id)
                .OrderBy(() => user4.UserName)
                .ThenOrderBy(() => user4.AgentLevel, OrderByEnum.Asc)
                .QueryListAsync<AspnetUsers>();
            Assert.True(res4.Count == 29180);

            var tuple4 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*********************************************************************************************************************************************************/

            var xx = "";

        }

    }
}
