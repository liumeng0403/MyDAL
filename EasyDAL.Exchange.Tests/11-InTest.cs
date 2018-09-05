using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Tests.Entities;
using EasyDAL.Exchange.Tests.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EasyDAL.Exchange.Tests
{
    public class InTest:TestBase
    {
        private List<AgentLevel> EnumList { get; set; }
        private List<string> StringList { get; set; }
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
                .Where(it => WhereTest.In_枚举.Contains(it.AgentLevel))
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
                .Where(it => WhereTest.In_String.Contains(it.Name))
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

            var xx = "";
        }

        [Fact]
        public async Task ListT_Value_Test()
        {
            await PereValue();

            var xx1 = "";

            // where in -- List<int>
            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => new List<int> { 5, 10 }.Contains(it.DirectorStarCount))
                .QueryListAsync();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);


            var xx = "";
        }
    }
}
