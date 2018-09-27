using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using MyDAL.Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Yunyong.Core;
using Yunyong.DataExchange;

namespace MyDAL.Test.Query
{
    public class _08_WhereObjTest : TestBase
    {
        // yunyong_tech 分支专用
        public class AgentQueryOption : PagingQueryOption
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }

            public AgentLevel AgentLevel { get; set; }
        }

        public class ProductQueryOption : PagingQueryOption
        {
            public bool? VipProduct { get; set; }
        }

        [Fact]
        public async Task WhereObjQueryOptionTest()
        {

            /*************************************************************************************************************************/

            var xx1 = "";

            // where object
            var res1 = await Conn
                .Selecter<Agent>()
                .Where(new
                {
                    Id = Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"),
                    Name = "樊士芹",
                    xxx = "xxx"
                })
                .QueryListAsync();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            /*************************************************************************************************************************/

            var xx2 = "";

            var option = new AgentQueryOption();
            option.Id = Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b");
            option.Name = "樊士芹";
            // where method
            var res2 = await Conn
                .Selecter<Agent>()
                .Where(option.GetCondition())
                .QueryPagingListAsync(option);

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            /*************************************************************************************************************************/

            var xx3 = "";

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
                .Selecter<Agent>()
                .Where(option.GetCondition())
                .QueryPagingListAsync<AgentVM>(option);

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            /*************************************************************************************************************************/

            var xx4 = "";

            // where object --> no where
            var res4 = await Conn
                .Selecter<Agent>()
                .Where(new
                {
                    //Id = Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"),
                    //Name = "樊士芹",
                    xxx = "xxx"
                })
                .QueryListAsync();

            var tuple4 = (XDebug.SQL, XDebug.Parameters);

            // no where --> and or
            var res41 = await Conn
                .Selecter<Agent>()
                .Where(new
                {
                    //Id = Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"),
                    //Name = "樊士芹",
                    xxx = "xxx"
                })
                .And(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .Or(it => it.AgentLevel == AgentLevel.DistiAgent)
                .QueryListAsync();

            var tuple41 = (XDebug.SQL, XDebug.Parameters);

            // no where --> or and 
            var res42 = await Conn
                .Selecter<Agent>()
                .Where(new
                {
                    //Id = Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"),
                    //Name = "樊士芹",
                    xxx = "xxx"
                })
                .Or(it => it.AgentLevel == AgentLevel.Customer)
                .And(it => it.Name == "金月琴")
                .QueryListAsync();

            var tuple42 = (XDebug.SQL, XDebug.Parameters);

            /*************************************************************************************************************************/

            var xx5 = "";

            var option2 = new ProductQueryOption
            {
                VipProduct = null     // true fals null 
            };
            // where method -- option orderby 
            var res5 = await Conn
                .Selecter<Product>()
                .Where(option2.GetCondition())
                .QueryPagingListAsync(option2);

            var tuple5 = (XDebug.SQL, XDebug.Parameters);


            /*************************************************************************************************************************/

            var option6 = new AgentQueryOption();
            option6.AgentLevel = AgentLevel.DistiAgent;

            var xx6 = "";

            var res6 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent6, out var record6)
                .From(() => agent6)
                .InnerJoin(() => record6)
                .On(() => agent6.Id == record6.AgentId)
                .Where(() => agent6.AgentLevel == AgentLevel.DistiAgent)
                .QueryPagingListAsync<Agent>(option6);
            Assert.True(res6.TotalCount == 574);

            var tuple6 = (XDebug.SQL, XDebug.Parameters);

            /*************************************************************************************************************************/

            var xx7 = "";
            
            // where method -- option orderby 
            var res7 = await Conn
                .Selecter<Agent>()
                //.Where(option.GetCondition())
                .Where(it=>it.Name== "樊士芹")
                .QueryPagingListAsync(option, agent => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });

            var tuple7 = (XDebug.SQL, XDebug.Parameters);

            /*************************************************************************************************************************/

            var xx = "";

        }

    }
}
