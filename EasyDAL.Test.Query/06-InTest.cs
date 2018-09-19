using EasyDAL.Exchange;
using EasyDAL.Test.Entities.EasyDal_Exchange;
using EasyDAL.Test.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EasyDAL.Test.Query
{
    public class _06_InTest : TestBase
    {
        private List<AgentLevel> EnumList { get; set; }
        private AgentLevel[] EnumArray { get; set; }
        private List<string> StringList { get; set; }
        private string[] StringArray { get; set; }
        private async Task PereValue()
        {
            var res1 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Id == Guid.Parse("03f0a7b4-acd3-4003-b686-01654436e906"))
                .QueryFirstOrDefaultAsync();
            var res2 = await Conn
                .Updater<Agent>()
                .Set(it => it.DirectorStarCount, 10)
                .Where(it => it.Id == res1.Id)
                .UpdateAsync();
            var res3 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Id == Guid.Parse("03fc18e2-4b1e-4aa2-832b-0165443388bd"))
                .QueryFirstOrDefaultAsync();
            var res4 = await Conn
                .Updater<Agent>()
                .Set(it => it.DirectorStarCount, 5)
                .Where(it => it.Id == res3.Id)
                .UpdateAsync();
        }

        [Fact]
        public async Task ListT_Enum_Test()
        {

            /*******************************************************************************************************************/

            var xx1 = "";

            var enums = new List<AgentLevel>
            {
                AgentLevel.CityAgent,
                AgentLevel.DistiAgent
            };
            // where in  --  variable  List<enum>  
            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => enums.Contains(it.AgentLevel))
                .QueryListAsync();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            /*******************************************************************************************************************/

            var xx2 = "";

            // where in -- obj.prop List<enum>
            var res2 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => WhereTest.In_List_枚举.Contains(it.AgentLevel))
                .QueryListAsync();

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            /*******************************************************************************************************************/

            var xx3 = "";

            EnumList = enums;
            // where in -- this.prop List<enum>
            var res3 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => EnumList.Contains(it.AgentLevel))
                .QueryListAsync();

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            Assert.True(res1.Count == res2.Count);
            Assert.True(res2.Count == res3.Count);
            Assert.True(res1.Count == 555);

            /*******************************************************************************************************************/

            var xx4 = "";

            var names = new List<string>
            {
                "黄银凤",
                "刘建芬"
            };
            // where in  --  variable  List<string>  
            var res4 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => names.Contains(it.Name))
                .QueryListAsync();

            var tuple4 = (XDebug.SQL, XDebug.Parameters);

            /*******************************************************************************************************************/

            var xx5 = "";

            // where in -- obj.prop List<string>
            var res5 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => WhereTest.In_List_String.Contains(it.Name))
                .QueryListAsync();

            var tuple5 = (XDebug.SQL, XDebug.Parameters);

            /*******************************************************************************************************************/

            var xx6 = "";

            StringList = names;
            // where in -- this.prop List<string>
            var res6 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => StringList.Contains(it.Name))
                .QueryListAsync();

            var tuple6 = (XDebug.SQL, XDebug.Parameters);

            Assert.True(res4.Count == res5.Count);
            Assert.True(res5.Count == res6.Count);
            Assert.True(res4.Count == 2);

            /*******************************************************************************************************************/


            await PereValue();

            /*******************************************************************************************************************/

            var xx7 = "";

            // where in -- List<int>  init 
            var res7 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => new List<int> { 5, 10 }.Contains(it.DirectorStarCount))
                .QueryListAsync();

            var tuple7 = (XDebug.SQL, XDebug.Parameters);

            /*******************************************************************************************************************/

            var xx8 = "";

            // where in -- List<long> init
            var res8 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => new List<long> { 5, 10 }.Contains(it.DirectorStarCount))
                .QueryListAsync();

            var tuple8 = (XDebug.SQL, XDebug.Parameters);

            /*******************************************************************************************************************/

            var xx9 = "";

            // where in -- List<short> init
            var res9 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => new List<short> { 5, 10 }.Contains((short)(it.DirectorStarCount)))
                .QueryListAsync();

            var tuple9 = (XDebug.SQL, XDebug.Parameters);

            Assert.True(res7.Count == res8.Count);
            Assert.True(res8.Count == res9.Count);
            Assert.True(res7.Count == 2);

            /*******************************************************************************************************************/

            var xx10 = "";

            // where in -- List<string> init
            var res10 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => new List<string> { "黄银凤", "刘建芬" }.Contains(it.Name))
                .QueryListAsync();
            Assert.True(res10.Count == 2);

            var tuple10 = (XDebug.SQL, XDebug.Parameters);

            /*******************************************************************************************************************/

            var xx11 = "";

            // where in -- List<enum> init
            var res11 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => new List<AgentLevel> { AgentLevel.CityAgent, AgentLevel.DistiAgent }.Contains(it.AgentLevel))
                .QueryListAsync();
            Assert.True(res11.Count == 555);

            var tuple11 = (XDebug.SQL, XDebug.Parameters);

            /*******************************************************************************************************************/

            var xx12 = "";

            var enumArray = new AgentLevel[]
            {
                AgentLevel.CityAgent,
                AgentLevel.DistiAgent
            };
            // where in  --  variable  enum[]  
            var res12 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => enumArray.Contains(it.AgentLevel))
                .QueryListAsync();

            var tuple12 = (XDebug.SQL, XDebug.Parameters);

            /*******************************************************************************************************************/

            var xx13 = "";

            // where in -- obj.prop enum[]
            var res13 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => WhereTest.In_Array_枚举.Contains(it.AgentLevel))
                .QueryListAsync();

            var tuple13 = (XDebug.SQL, XDebug.Parameters);

            /*******************************************************************************************************************/

            var xx14 = "";

            EnumArray = enumArray;
            // where in -- this.prop enum[]
            var res14 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => EnumArray.Contains(it.AgentLevel))
                .QueryListAsync();

            var tuple14 = (XDebug.SQL, XDebug.Parameters);

            Assert.True(res12.Count == res13.Count);
            Assert.True(res13.Count == res14.Count);
            Assert.True(res12.Count == 555);

            /*******************************************************************************************************************/

            var xx15 = "";

            var nameArray = new string[]
            {
                "黄银凤",
                "刘建芬"
            };
            // where in  --  variable  string[]  
            var res15 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => nameArray.Contains(it.Name))
                .QueryListAsync();

            var tuple15 = (XDebug.SQL, XDebug.Parameters);

            /*******************************************************************************************************************/

            var xx16 = "";

            // where in -- obj.prop string[]
            var res16 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => WhereTest.In_Array_String.Contains(it.Name))
                .QueryListAsync();

            var tuple16 = (XDebug.SQL, XDebug.Parameters);

            /*******************************************************************************************************************/

            var xx17 = "";

            StringArray = nameArray;
            // where in -- this.prop string[]
            var res17 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => StringArray.Contains(it.Name))
                .QueryListAsync();

            var tuple17 = (XDebug.SQL, XDebug.Parameters);

            Assert.True(res15.Count == res16.Count);
            Assert.True(res16.Count == res17.Count);
            Assert.True(res15.Count == 2);

            /*******************************************************************************************************************/

            await PereValue();

            /*******************************************************************************************************************/

            var xx18 = "";

            // where in -- int[]  init 
            var res18 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => new int[] { 5, 10 }.Contains(it.DirectorStarCount))
                .QueryListAsync();

            var tuple18 = (XDebug.SQL, XDebug.Parameters);

            /*******************************************************************************************************************/

            var xx19 = "";

            // where in -- long[] init
            var res19 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => new long[] { 5L, 10L }.Contains(it.DirectorStarCount))
                .QueryListAsync();

            var tuple19 = (XDebug.SQL, XDebug.Parameters);

            /*******************************************************************************************************************/

            var xx20 = "";

            // where in -- short[] init
            var res20 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => new short[] { 5, 10 }.Contains((short)(it.DirectorStarCount)))
                .QueryListAsync();

            var tuple20 = (XDebug.SQL, XDebug.Parameters);

            Assert.True(res18.Count == res19.Count);
            Assert.True(res19.Count == res20.Count);
            Assert.True(res18.Count == 2);

            /*******************************************************************************************************************/

            var xx21 = "";

            // where in -- string[] init
            var res21 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => new string[] { "黄银凤", "刘建芬" }.Contains(it.Name))
                .QueryListAsync();
            Assert.True(res21.Count == 2);

            var tuple21 = (XDebug.SQL, XDebug.Parameters);

            /*******************************************************************************************************************/

            var xx22 = "";

            // where in -- enum[] init
            var res22 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => new AgentLevel[] { AgentLevel.CityAgent, AgentLevel.DistiAgent }.Contains(it.AgentLevel))
                .QueryListAsync();
            Assert.True(res22.Count == 555);

            var tuple22 = (XDebug.SQL, XDebug.Parameters);

            /*******************************************************************************************************************/

            var xx23 = "";

            // where in -- enum[] init
            var res23 = await Conn.OpenDebug()
                .Joiner<Agent, AgentInventoryRecord>(out var agent, out var record)
                .From(() => agent)
                .InnerJoin(() => record)
                .On(() => agent.Id == record.AgentId)
                .Where(() => new AgentLevel[] { AgentLevel.CityAgent, AgentLevel.DistiAgent }.Contains(agent.AgentLevel))
                .QueryListAsync<Agent>();
            Assert.True(res23.Count == 574);

            var tuple23 = (XDebug.SQL, XDebug.Parameters);

            /*******************************************************************************************************************/

            var xx = "";
        }

    }
}
