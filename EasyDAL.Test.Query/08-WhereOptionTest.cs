using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using MyDAL.Test.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Query
{
    public class _08_WhereOptionTest:TestBase
    {

        [Fact]
        public async Task OptionTest()
        {

            /***************************************************************************************************************************************/

            var xx1 = "";

            var option1 = new AgentQueryOption();
            option1.StartTime = WhereTest.CreatedOn;
            option1.EndTime = DateTime.Now;
            option1.AgentLevel = AgentLevel.DistiAgent;

            var con1 = option1.GetCondition() as IDictionary<string, object>;
            con1["StartTime"] = new GreaterThanOrEqual<Agent>(it => it.CreatedOn, con1["StartTime"]);
            con1["EndTime"] = new LessThanOrEqual<Agent>(it => it.CreatedOn, con1["EndTime"]);

            //var res1=await Conn
            //    .Selecter<Agent>()
            //    .Where(con1)

            /***************************************************************************************************************************************/



        }

    }
}
