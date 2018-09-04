using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Extensions;
using EasyDAL.Exchange.Tests.Entities;
using EasyDAL.Exchange.Tests.ViewModels;
using Rainbow.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EasyDAL.Exchange.Tests
{
    public class WhereObjTest:TestBase
    {
        // yunyong_tech 分支专用
        public class AgentQueryOption:PagingQueryOption
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }
        }

        [Fact]
        public async Task WhereObjQueryOptionTest()
        {

            var xx1 = "";

            // where object
            var res1 = await Conn.OpenHint()
                .Selecter<Agent>()
                .Where(new
                {
                    Id = Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"),
                    Name = "樊士芹",
                    xxx = "xxx"
                })
                .QueryListAsync();

            var tuple1 = (Hints.SQL, Hints.Parameters);

            var xx2 = "";

            
            var option = new AgentQueryOption();
            option.Id = Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b");
            option.Name = "樊士芹";
            // where method
            var res2 = await Conn.OpenHint()
                .Selecter<Agent>()
                .Where(option.GetCondition())
                .QueryPagingListAsync(option);

            var tuple2 = (Hints.SQL, Hints.Parameters);

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
            var res3 = await Conn.OpenHint()
                .Selecter<Agent>()
                .Where(option.GetCondition())
                .QueryPagingListAsync<AgentVM>(option);

            var tuple3 = (Hints.SQL, Hints.Parameters);

            var xx4 = "";

            // where object --> no where
            var res4 = await Conn.OpenHint()
                .Selecter<Agent>()
                .Where(new
                {
                    //Id = Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"),
                    //Name = "樊士芹",
                    xxx = "xxx"
                })
                .QueryListAsync();

            var tuple4 = (Hints.SQL, Hints.Parameters);


            var xx = "";

        }

    }
}
