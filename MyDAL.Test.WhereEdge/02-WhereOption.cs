using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using MyDAL.Test.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.WhereEdge
{
    public class _02_WhereOption : TestBase
    {
        [Fact]
        public async Task test()
        {

            /*****************************************************************************************************************************/

            var xx1 = "";

            var option1 = new AgentQueryOption();
            option1.StartTime = WhereTest.CreatedOn;
            option1.EndTime = DateTime.Now;
            option1.AgentLevel = AgentLevel.DistiAgent;

            //   =    >=    <=
            var res1 = await Conn
                .Selecter<Agent>()
                .Where(option1)
                .ListAsync();
            Assert.True(res1.Count == 555);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            /*****************************************************************************************************************************/

            var xx2 = "";

            var option2 = new AgentQueryOption();
            option2.StartTime = WhereTest.CreatedOn;
            option2.EndTime = DateTime.Now;

            //   >=   <=  
            var res2 = await Conn
                .Selecter<Agent>()
                .Where(option2)
                .ListAsync();
            Assert.True(res2.Count == 28619);

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            /*****************************************************************************************************************************/

            var xx3 = "";

            var option3 = new AgentQueryOption();
            option3.Name = "张";

            //   like  
            var res3 = await Conn
                .Selecter<Agent>()
                .Where(option3)
                .ListAsync();
            Assert.True(res3.Count == 2002);

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            /*****************************************************************************************************************************/

            var xx4 = "";

            var option4 = new AgentQueryOption();
            option4.EnumListIn = new List<AgentLevel>
            {
                AgentLevel.CityAgent,
                AgentLevel.DistiAgent
            };

            // in
            var res4 = await Conn
                .Selecter<Agent>()
                .Where(option4)
                .ListAsync();
            Assert.True(res4.Count == 555);

            var tuple4 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

            var xx5 = "";

            var option5 = new AgentQueryOption();
            option5.EnumListNotIn = new List<AgentLevel>
            {
                AgentLevel.CityAgent,
                AgentLevel.DistiAgent
            };

            // in
            var res5 = await Conn
                .Selecter<Agent>()
                .Where(option5)
                .ListAsync();
            Assert.True(res5.Count == 28064 || res5.Count == 28065);

            var tuple5 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

            var xx = "";

        }
    }
}
