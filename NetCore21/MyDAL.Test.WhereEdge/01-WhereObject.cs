using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using MyDAL.Test.Options;
using MyDAL.Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.WhereEdge
{
    public class _01_WhereObject:TestBase
    {
        [Fact]
        public async Task test()
        {

            /*************************************************************************************************************************/

            xx = string.Empty;

            // where object
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(new
                {
                    Id = Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"),
                    Name = "樊士芹",
                    xxx = "xxx"
                })
                .QueryListAsync();
            Assert.True(res1.Count == 1);
            Assert.True(res1.First().Name == "樊士芹");

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /*************************************************************************************************************************/

            xx = string.Empty;

            var option = new AgentQueryOption();
            option.Id = Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b");
            option.Name = "樊士芹";
            // where method
            var res2 = await Conn
                .Queryer<Agent>()
                .Where(option.GetCondition())
                .PagingListAsync(option);
            Assert.True(res2.TotalCount == 1);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /*************************************************************************************************************************/

            xx=string.Empty;

            option.OrderBys = new List<OrderBy>
            {
                new OrderBy
                {
                    Field="Name",
                    Desc=true
                }
            };
            // where method -- option orderby 
            var res3 = await Conn
                .Queryer<Agent>()
                .Where(option.GetCondition())
                .PagingListAsync<AgentVM>(option);
            Assert.True(res3.TotalCount == 1);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /*************************************************************************************************************************/

            var xx4 = string.Empty;

            // where object --> no where
            var res4 = await Conn
                .Queryer<Agent>()
                .Where(new
                {
                    //Id = Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"),
                    //Name = "樊士芹",
                    xxx = "xxx"
                })
                .QueryListAsync();
            Assert.True(res4.Count == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            // no where --> and or
            var res41 = await Conn
                .Queryer<Agent>()
                .Where(new
                {
                    //Id = Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"),
                    //Name = "樊士芹",
                    xxx = "xxx"
                })
                .And(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .Or(it => it.AgentLevel == AgentLevel.DistiAgent)
                .QueryListAsync();
            Assert.True(res41.Count == 556);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            // no where --> or and 
            var res42 = await Conn
                .Queryer<Agent>()
                .Where(new
                {
                    //Id = Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"),
                    //Name = "樊士芹",
                    xxx = "xxx"
                })
                .Or(it => it.AgentLevel == AgentLevel.Customer)
                .And(it => it.Name == "金月琴")
                .QueryListAsync();
            Assert.True(res42.Count == 1);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /*************************************************************************************************************************/

            var xx5 = string.Empty;

            var option2 = new ProductQueryOption
            {
                VipProduct = null     // true fals null 
            };
            // where method -- option orderby 
            var res5 = await Conn
                .Queryer<Product>()
                .Where(option2.GetCondition())
                .PagingListAsync(option2);
            Assert.True(res5.Data.Count == 4);

            option2.VipProduct = false;
            var res51 = await Conn
                .Queryer<Product>()
                .Where(option2.GetCondition())
                .PagingListAsync(option2);
            Assert.True(res51.Data.Count == 4);

            option2.VipProduct = true;
            var res52 = await Conn
                .Queryer<Product>()
                .Where(option2.GetCondition())
                .PagingListAsync(option2);
            Assert.True(res52.Data.Count == 0);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

        }
    }
}
