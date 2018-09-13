using EasyDAL.Exchange.Tests.Entities;
using EasyDAL.Exchange.Tests.Entities.EasyDal_Exchange;
using EasyDAL.Exchange.Tests.Enums;
using EasyDAL.Exchange.Tests.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EasyDAL.Exchange.Tests
{
    public class QueryTest : TestBase
    {   
    




        // 查询 是否存在
        [Fact]
        public async Task ExistAsyncTest()
        {
            var xx1 = "";

            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .ExistAsync();

            var tuple = (XDebug.SQL, XDebug.Parameters);

            var xx = "";
        }





    }
}
