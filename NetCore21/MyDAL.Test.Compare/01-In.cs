using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Compare
{
    public class _01_In
        : TestBase
    {
        private List<AgentLevel?> EnumList { get; set; }
        private AgentLevel?[] EnumArray { get; set; }
        private List<string> StringList { get; set; }
        private string[] StringArray { get; set; }
        private async Task PereValue()
        {
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Id == Guid.Parse("03f0a7b4-acd3-4003-b686-01654436e906"))
                .QueryOneAsync();

            var res2 = await Conn
                .Updater<Agent>()
                .Set(it => it.DirectorStarCount, 10)
                .Where(it => it.Id == res1.Id)
                .UpdateAsync();
            var res3 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Id == Guid.Parse("03fc18e2-4b1e-4aa2-832b-0165443388bd"))
                .QueryOneAsync();

            var res4 = await Conn
                .Updater<Agent>()
                .Set(it => it.DirectorStarCount, 5)
                .Where(it => it.Id == res3.Id)
                .UpdateAsync();
        }

        [Fact]
        public async Task History()
        {

            var enums = new List<AgentLevel?>
            {
                AgentLevel.CityAgent,
                AgentLevel.DistiAgent
            };

            /*******************************************************************************************************************/

            xx = string.Empty;

            EnumList = enums;
            // where in -- this.prop List<enum>
            var res3 = await Conn
                .Queryer<Agent>()
                .Where(it => EnumList.Contains(it.AgentLevel))
                .QueryListAsync();
             
            Assert.True(res3.Count == 555);

            /*******************************************************************************************************************/

            xx = string.Empty;

            var names = new List<string>
            {
                "黄银凤",
                "刘建芬"
            };
            // where in  --  variable  List<string>  
            var res4 = await Conn
                .Queryer<Agent>()
                .Where(it => names.Contains(it.Name))
                .QueryListAsync();

            

            /*******************************************************************************************************************/

            xx = string.Empty;

            StringList = names;
            // where in -- this.prop List<string>
            var res6 = await Conn
                .Queryer<Agent>()
                .Where(it => StringList.Contains(it.Name))
                .QueryListAsync();

            

            Assert.True(res4.Count == res6.Count);
            Assert.True(res4.Count == 2);

            /*******************************************************************************************************************/


            await PereValue();

            /*******************************************************************************************************************/

            xx = string.Empty;

            // where in -- List<int>  init 
            var res7 = await Conn
                .Queryer<Agent>()
                .Where(it => new List<int> { 5, 10 }.Contains(it.DirectorStarCount))
                .QueryListAsync();

            

            /*******************************************************************************************************************/

            xx = string.Empty;

            // where in -- List<long> init
            var res8 = await Conn
                .Queryer<Agent>()
                .Where(it => new List<long> { 5, 10 }.Contains(it.DirectorStarCount))
                .QueryListAsync();

            

            /*******************************************************************************************************************/

            xx = string.Empty;

            // where in -- List<short> init
            var res9 = await Conn
                .Queryer<Agent>()
                .Where(it => new List<short> { 5, 10 }.Contains((short)(it.DirectorStarCount)))
                .QueryListAsync();

            

            Assert.True(res7.Count == res8.Count);
            Assert.True(res8.Count == res9.Count);
            Assert.True(res7.Count == 2);

            /*******************************************************************************************************************/

            xx = string.Empty;

            // where in -- List<string> init
            var res10 = await Conn
                .Queryer<Agent>()
                .Where(it => new List<string> { "黄银凤", "刘建芬" }.Contains(it.Name))
                .QueryListAsync();
            Assert.True(res10.Count == 2);

            

            /*******************************************************************************************************************/

            xx = string.Empty;

            // where in -- List<enum> init
            var res11 = await Conn
                .Queryer<Agent>()
                .Where(it => new List<AgentLevel?> { AgentLevel.CityAgent, AgentLevel.DistiAgent }.Contains(it.AgentLevel))
                .QueryListAsync();
            Assert.True(res11.Count == 555);

            

            /*******************************************************************************************************************/
            var enumArray = new AgentLevel?[]
            {
                AgentLevel.CityAgent,
                AgentLevel.DistiAgent
            };

            xx = string.Empty;

            // where in -- obj.prop enum[]
            var res13 = await Conn
                .Queryer<Agent>()
                .Where(it => WhereTest.In_Array_枚举.Contains(it.AgentLevel))
                .QueryListAsync();

            

            /*******************************************************************************************************************/

            xx = string.Empty;

            EnumArray = enumArray;
            // where in -- this.prop enum[]
            var res14 = await Conn
                .Queryer<Agent>()
                .Where(it => EnumArray.Contains(it.AgentLevel))
                .QueryListAsync();

            

            Assert.True(res13.Count == res14.Count);
            Assert.True(res13.Count == 555);

            /*******************************************************************************************************************/

            xx = string.Empty;

            var nameArray = new string[]
            {
                "黄银凤",
                "刘建芬"
            };
            // where in  --  variable  string[]  
            var res15 = await Conn
                .Queryer<Agent>()
                .Where(it => nameArray.Contains(it.Name))
                .QueryListAsync();

            

            /*******************************************************************************************************************/

            xx = string.Empty;

            // where in -- obj.prop string[]
            var res16 = await Conn
                .Queryer<Agent>()
                .Where(it => WhereTest.In_Array_String.Contains(it.Name))
                .QueryListAsync();

            

            /*******************************************************************************************************************/

            xx = string.Empty;

            StringArray = nameArray;
            // where in -- this.prop string[]
            var res17 = await Conn
                .Queryer<Agent>()
                .Where(it => StringArray.Contains(it.Name))
                .QueryListAsync();

            

            Assert.True(res15.Count == res16.Count);
            Assert.True(res16.Count == res17.Count);
            Assert.True(res15.Count == 2);

            /*******************************************************************************************************************/

            await PereValue();

            /*******************************************************************************************************************/

            xx = string.Empty;

            // where in -- int[]  init 
            var res18 = await Conn
                .Queryer<Agent>()
                .Where(it => new int[] { 5, 10 }.Contains(it.DirectorStarCount))
                .QueryListAsync();

            

            /*******************************************************************************************************************/

            var xx19 = string.Empty;

            // where in -- long[] init
            var res19 = await Conn
                .Queryer<Agent>()
                .Where(it => new long[] { 5L, 10L }.Contains(it.DirectorStarCount))
                .QueryListAsync();

            

            /*******************************************************************************************************************/

            xx = string.Empty;

            // where in -- short[] init
            var res20 = await Conn
                .Queryer<Agent>()
                .Where(it => new short[] { 5, 10 }.Contains((short)(it.DirectorStarCount)))
                .QueryListAsync();

            

            Assert.True(res18.Count == res19.Count);
            Assert.True(res19.Count == res20.Count);
            Assert.True(res18.Count == 2);

            /*******************************************************************************************************************/

            xx = string.Empty;

            // where in -- string[] init
            var res21 = await Conn
                .Queryer<Agent>()
                .Where(it => new string[] { "黄银凤", "刘建芬" }.Contains(it.Name))
                .QueryListAsync();
            Assert.True(res21.Count == 2);

            

            /*******************************************************************************************************************/

            var xx22 = string.Empty;

            // where in -- enum[] init
            var res22 = await Conn
                .Queryer<Agent>()
                .Where(it => new AgentLevel?[] { AgentLevel.CityAgent, AgentLevel.DistiAgent }.Contains(it.AgentLevel))
                .QueryListAsync();
            Assert.True(res22.Count == 555);

            

            /*******************************************************************************************************************/

            var guid241 = Guid.Parse("0048793b-ca61-457e-a2b4-0165442f3684");
            var guid242 = Guid.Parse("004f4290-9576-43b9-903f-01654434da0f");
            // where in -- string[] init
            var res24 = await Conn
                .Queryer<Agent>()
                .Where(it => new string[] { "黄银凤", "刘建芬" }.Contains(it.Name) || new List<Guid> { guid241, guid242 }.Contains(it.Id))
                .QueryListAsync();
            Assert.True(res24.Count == 4);

            

            /*******************************************************************************************************************/

            var xx25 = string.Empty;

            // where in -- string[] init
            var res25 = await Conn
                .Queryer<Agent>()
                .Where(it => new string[] { "刘建芬" }.Contains(it.Name))
                .QueryListAsync();
            Assert.True(res25.Count == 1);

            

            /*******************************************************************************************************************/

            var xx26 = string.Empty;

            // where in -- string[] init
            var res26 = await Conn
                .Queryer<Agent>()
                .Where(it => new string[] { "刘建芬" }.Contains(it.Name) || new List<Guid> { guid241 }.Contains(it.Id))
                .QueryListAsync();
            Assert.True(res26.Count == 2);

            

            /*******************************************************************************************************************/

            var xx27 = string.Empty;

            try
            {
                // where in -- string[] init
                var res27 = await Conn
                    .Queryer<Agent>()
                    .Where(it => new List<Guid> { }.Contains(it.Id))
                    .QueryListAsync();
            }
            catch (Exception ex)
            {
                var errStr = "【ERR-050】 -- [[【new List`1() {}.Contains(it.Id)】 中 集合为空!!!]] ，请 EMail: --> liumeng0403@163.com <--";
                Assert.Equal(errStr, ex.Message, ignoreCase: true);
            }

            /*******************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task In_Shortcut()
        {

            xx = string.Empty;

            // in
            var res2 = await Conn.QueryListAsync<Agent>(it => new List<AgentLevel?> { AgentLevel.CityAgent, AgentLevel.DistiAgent }.Contains(it.AgentLevel));

            Assert.True(res2.Count == 555);

            

            /*******************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task NotIn_Shortcut()
        {
            xx = string.Empty;

            // in
            var res2 = await Conn.QueryListAsync<Agent>(it => !new List<AgentLevel?> { AgentLevel.CityAgent, AgentLevel.DistiAgent }.Contains(it.AgentLevel));

            Assert.True(res2.Count == 28065);

            

            /*******************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task In_ST()
        {

            xx = string.Empty;

            // in
            var res5 = await Conn
                .Queryer<Agent>()
                .Where(it => new List<string> { "黄银凤", "刘建芬" }.Contains(it.Name))
                .QueryListAsync();

            Assert.True(res5.Count == 2);

            

            /*******************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task NotIn_ST()
        {
            xx = string.Empty;

            // in
            var res5 = await Conn
                .Queryer<Agent>()
                .Where(it => !new List<string> { "黄银凤", "刘建芬" }.Contains(it.Name))
                .QueryListAsync();

            Assert.True(res5.Count == 28618);

            

            /*******************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task In_Array_MT()
        {

            xx = string.Empty;

            // in
            var res1 = await Conn
                .Queryer(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .Where(() => new AgentLevel?[] { AgentLevel.CityAgent, AgentLevel.DistiAgent }.Contains(agent.AgentLevel))
                .QueryListAsync<Agent>();

            Assert.True(res1.Count == 574);

            

            /*******************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task NotIn_MT()
        {

            xx = string.Empty;

            // not in
            var res1 = await Conn
                .Queryer(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .Where(() => !new AgentLevel?[] { AgentLevel.CityAgent, AgentLevel.DistiAgent }.Contains(agent.AgentLevel))
                .QueryListAsync<Agent>();

            Assert.True(res1.Count == 0);

            

            /*******************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task In_List()
        {
            xx = string.Empty;

            var enums = new List<AgentLevel?>
            {
                AgentLevel.CityAgent,
                AgentLevel.DistiAgent
            };

            // in
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => enums.Contains(it.AgentLevel))
                .QueryListAsync();

            Assert.True(res1.Count == 555);

            xx = string.Empty;
        }

        [Fact]
        public async Task NotIn_List()
        {

            xx = string.Empty;

            // not in
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => !new List<AgentLevel?> { AgentLevel.CityAgent, AgentLevel.DistiAgent }.Contains(it.AgentLevel))
                .QueryListAsync();

            Assert.True(res1.Count == 28065 || res1.Count == 28064);

            

            xx = string.Empty;

        }

        [Fact]
        public async Task In_Array()
        {

            xx = string.Empty;

            var enumArray = new AgentLevel?[]
            {
                AgentLevel.CityAgent,
                AgentLevel.DistiAgent
            };

            // in
            var res12 = await Conn
                .Queryer<Agent>()
                .Where(it => enumArray.Contains(it.AgentLevel))
                .QueryListAsync();

            Assert.True(res12.Count == 555);

            

            /*******************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task NotIn_Array()
        {

            xx = string.Empty;

            // not in
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => !new AgentLevel?[] { AgentLevel.CityAgent, AgentLevel.DistiAgent }.Contains(it.AgentLevel))
                .QueryListAsync();

            Assert.True(res1.Count == 28065 || res1.Count == 28064);

            

            xx = string.Empty;

        }

        [Fact]
        public async Task In_IEnumerable_ST()
        {

            xx = string.Empty;

            var enums = new List<AgentLevel?>
            {
                AgentLevel.CityAgent,
                AgentLevel.DistiAgent
            }.Select(it=>it);

            // in
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => enums.Contains(it.AgentLevel))
                .QueryListAsync();

            Assert.True(res1.Count == 555);

            xx = string.Empty;

        }

        [Fact]
        public async Task In_IEnumerable_MT()
        {
            xx = string.Empty;

            var arr = new AgentLevel?[] { AgentLevel.CityAgent, AgentLevel.DistiAgent }.Where(it => true);

            // in
            var res1 = await Conn
                .Queryer(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .Where(() => arr.Contains(agent.AgentLevel))
                .QueryListAsync<Agent>();

            Assert.True(res1.Count == 574);

            xx = string.Empty;
        }
    }
}
