using EasyDAL.Exchange.Tests.Entities.EasyDal_Exchange;
using EasyDAL.Exchange.Tests.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EasyDAL.Exchange.Tests
{
    public class InTest:TestBase
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

            var xx2 = "";
            
            // where in -- obj.prop List<enum>
            var res2 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => WhereTest.In_List_枚举.Contains(it.AgentLevel))
                .QueryListAsync();

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

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

            var xx = "";
        }

        [Fact]
        public async Task ListT_String_Test()
        {
            var xx1 = "";

            var names = new List<string>
            {
                "黄银凤",
                "刘建芬"
            };
            // where in  --  variable  List<string>  
            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => names.Contains(it.Name))
                .QueryListAsync();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var xx2 = "";

            // where in -- obj.prop List<string>
            var res2 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => WhereTest.In_List_String.Contains(it.Name))
                .QueryListAsync();

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            var xx3 = "";

            StringList = names;
            // where in -- this.prop List<string>
            var res3 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => StringList.Contains(it.Name))
                .QueryListAsync();

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            Assert.True(res1.Count == res2.Count);
            Assert.True(res2.Count == res3.Count);

            var xx = "";
        }

        [Fact]
        public async Task ListT_Init_Test()
        {
            await PereValue();

            var xx1 = "";

            // where in -- List<int>  init 
            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => new List<int> { 5, 10 }.Contains(it.DirectorStarCount))
                .QueryListAsync();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var xx2 = "";

            // where in -- List<long> init
            var res2 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => new List<long> { 5, 10 }.Contains(it.DirectorStarCount))
                .QueryListAsync();

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            var xx3 = "";

            // where in -- List<short> init
            var res3 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => new List<short> { 5, 10 }.Contains((short)(it.DirectorStarCount)))
                .QueryListAsync();

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            Assert.True(res1.Count == res2.Count);
            Assert.True(res2.Count == res3.Count);

            var xx4 = "";

            // where in -- List<string> init
            var res4 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => new List<string> { "黄银凤", "刘建芬" }.Contains(it.Name))
                .QueryListAsync();

            var tuple4 = (XDebug.SQL, XDebug.Parameters);

            var xx5 = "";

            // where in -- List<enum> init
            var res5 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => new List<AgentLevel> { AgentLevel.CityAgent, AgentLevel.DistiAgent }.Contains(it.AgentLevel))
                .QueryListAsync();

            var tuple5 = (XDebug.SQL, XDebug.Parameters);

            var xx = "";
        }

        [Fact]
        public async Task Array_Enum_Test()
        {
            var xx1 = "";

            var enums = new AgentLevel[]
            {
                AgentLevel.CityAgent,
                AgentLevel.DistiAgent
            };
            // where in  --  variable  enum[]  
            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => enums.Contains(it.AgentLevel))
                .QueryListAsync();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var xx2 = "";

            // where in -- obj.prop enum[]
            var res2 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => WhereTest.In_Array_枚举.Contains(it.AgentLevel))
                .QueryListAsync();

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            var xx3 = "";

            EnumArray = enums;
            // where in -- this.prop enum[]
            var res3 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => EnumArray.Contains(it.AgentLevel))
                .QueryListAsync();

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            Assert.True(res1.Count == res2.Count);
            Assert.True(res2.Count == res3.Count);

            var xx = "";
        }

        [Fact]
        public async Task Array_String_Test()
        {
            var xx1 = "";

            var names = new string[]
            {
                "黄银凤",
                "刘建芬"
            };
            // where in  --  variable  string[]  
            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => names.Contains(it.Name))
                .QueryListAsync();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var xx2 = "";

            // where in -- obj.prop string[]
            var res2 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => WhereTest.In_Array_String.Contains(it.Name))
                .QueryListAsync();

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            var xx3 = "";

            StringArray = names;
            // where in -- this.prop string[]
            var res3 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => StringArray.Contains(it.Name))
                .QueryListAsync();

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            Assert.True(res1.Count == res2.Count);
            Assert.True(res2.Count == res3.Count);

            var xx = "";
        }

        [Fact]
        public async Task Array_Init_Test()
        {
            await PereValue();

            var xx1 = "";

            // where in -- int[]  init 
            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => new int[] { 5, 10 }.Contains(it.DirectorStarCount))
                .QueryListAsync();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var xx2 = "";

            // where in -- long[] init
            var res2 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => new long[] { 5L, 10L }.Contains(it.DirectorStarCount))
                .QueryListAsync();

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            var xx3 = "";

            // where in -- short[] init
            var res3 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => new short[] { 5, 10 }.Contains((short)(it.DirectorStarCount)))
                .QueryListAsync();

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            Assert.True(res1.Count == res2.Count);
            Assert.True(res2.Count == res3.Count);

            var xx4 = "";

            // where in -- string[] init
            var res4 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => new string[] { "黄银凤", "刘建芬" }.Contains(it.Name))
                .QueryListAsync();

            var tuple4 = (XDebug.SQL, XDebug.Parameters);

            var xx5 = "";

            // where in -- enum[] init
            var res5 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => new AgentLevel[] { AgentLevel.CityAgent, AgentLevel.DistiAgent }.Contains(it.AgentLevel))
                .QueryListAsync();

            var tuple5 = (XDebug.SQL, XDebug.Parameters);

            var xx6 = "";

            // where in -- enum[] init
            var res6 = await Conn.OpenDebug()
                .Joiner<Agent,AgentInventoryRecord>(out var agent,out var record)
                .From(()=>agent)
                .InnerJoin(()=>record)
                .On(()=>agent.Id==record.AgentId)
                .Where(() => new AgentLevel[] { AgentLevel.CityAgent, AgentLevel.DistiAgent }.Contains(agent.AgentLevel))
                .QueryListAsync<Agent>();

            var tuple6 = (XDebug.SQL, XDebug.Parameters);

            var xx = "";
        }

    }
}
