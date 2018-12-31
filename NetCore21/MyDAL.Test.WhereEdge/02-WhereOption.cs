using MyDAL.Test.Entities.MyDAL_TestDB;
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

            xx = string.Empty;

            var option1 = new AgentQueryOption();
            option1.StartTime = WhereTest.CreatedOn;
            option1.EndTime = DateTime.Now;
            option1.AgentLevel = AgentLevel.DistiAgent;

            //   =    >=    <=
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(option1)
                .QueryListAsync();
            Assert.True(res1.Count == 555);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

            xx = string.Empty;

            var option2 = new AgentQueryOption();
            option2.StartTime = WhereTest.CreatedOn;
            option2.EndTime = DateTime.Now;

            //   >=   <=  
            var res2 = await Conn
                .Queryer<Agent>()
                .Where(option2)
                .QueryListAsync();
            Assert.True(res2.Count == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

            var xx3 = string.Empty;

            var option3 = new AgentQueryOption();
            option3.Name = "张";

            //   like  
            var res3 = await Conn
                .Queryer<Agent>()
                .Where(option3)
                .QueryListAsync();
            Assert.True(res3.Count == 2002);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

            var xx4 = string.Empty;

            var option4 = new AgentQueryOption();
            option4.EnumListIn = new List<AgentLevel>
            {
                AgentLevel.CityAgent,
                AgentLevel.DistiAgent
            };

            // in
            var res4 = await Conn
                .Queryer<Agent>()
                .Where(option4)
                .QueryListAsync();
            Assert.True(res4.Count == 555);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

            var xx5 = string.Empty;

            var option5 = new AgentQueryOption();
            option5.EnumListNotIn = new List<AgentLevel>
            {
                AgentLevel.CityAgent,
                AgentLevel.DistiAgent
            };

            // in
            var res5 = await Conn
                .Queryer<Agent>()
                .Where(option5)
                .QueryListAsync();
            Assert.True(res5.Count == 28064 || res5.Count == 28065);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

            xx = string.Empty;

        }
    }
}
