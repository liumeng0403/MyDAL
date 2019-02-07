using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using MyDAL.Test.Options;
using MyDAL.Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.WhereEdge
{
    public class _01_WherePaging
        : TestBase
    {

        [Fact]
        public async Task TestST01()
        {

            /*
             * 单表
             */

            /************************************************************************************************************************************/

            xx = string.Empty;

            var op1 = new Single_PagingEdgeOption();
            op1.Phone = "19900000218";

            // Where --> PagingListAsync
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(op1)
                .PagingListAsync();

            Assert.True(res1.TotalCount == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            var op2 = new Single_PagingEdgeOption();
            op2.PhoneNotEqual = "19900000218";

            // Where --> OrderBy --> PagingListAsync
            var res2 = await Conn
                .Queryer<Agent>()
                .Where(op2)
                .OrderBy(it => it.Name, OrderByEnum.Asc)
                .PagingListAsync();

            Assert.True(res2.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            var op3 = new Single_PagingEdgeOption();
            op3.PhoneNotEqual = "19900000218";

            // Where --> OrderBy --> ThenOrderBy --> PagingListAsync
            var res3 = await Conn
                .Queryer<Agent>()
                .Where(op3)
                .OrderBy(it => it.AgentLevel)
                    .ThenOrderBy(it => it.Name, OrderByEnum.Asc)
                .PagingListAsync();

            Assert.True(res3.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            var op4 = new Single_PagingEdgeOption();
            op4.PhoneNotEqual = "19900000218";

            // Where --> OrderBy --> ThenOrderBy --> ... ... --> PagingListAsync
            var res4 = await Conn
                .Queryer<Agent>()
                .Where(op4)
                .OrderBy(it => it.AgentLevel)
                    .ThenOrderBy(it => it.Name)
                    .ThenOrderBy(it => it.PathId)
                .PagingListAsync();

            Assert.True(res4.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            var op5 = new Single_PagingEdgeOption();
            op5.PhoneNotEqual = "19900000218";

            // Where --> Distinct --> PagingListAsync
            var res5 = await Conn
                .Queryer<Agent>()
                .Where(op5)
                .Distinct()
                .PagingListAsync(it => it.AgentLevel);

            Assert.True(res5.TotalCount == 3);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            var op6 = new Single_PagingEdgeOption();
            op6.PhoneNotEqual = "19900000218";

            // Where --> OrderBy --> Distinct --> PagingListAsync
            var res6 = await Conn
                .Queryer<Agent>()
                .Where(op6)
                .OrderBy(it => it.Name)
                .Distinct()
                .PagingListAsync();

            Assert.True(res6.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            var op7 = new Single_PagingEdgeOption();
            op7.PhoneNotEqual = "19900000218";

            // Where --> OrderBy --> ThenOrderBy --> Distinct --> PagingListAsync
            var res7 = await Conn
                .Queryer<Agent>()
                .Where(op7)
                .OrderBy(it => it.AgentLevel)
                    .ThenOrderBy(it => it.Name, OrderByEnum.Asc)
                .Distinct()
                .PagingListAsync<AgentVM>();

            Assert.True(res7.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            var op8 = new Single_PagingEdgeOption();
            op8.PhoneNotEqual = "19900000218";

            // Where --> OrderBy --> ThenOrderBy --> ... ... --> Distinct --> PagingListAsync
            var res8 = await Conn
                .Queryer<Agent>()
                .Where(op8)
                .OrderBy(it => it.PathId)
                    .ThenOrderBy(it => it.Name)
                    .ThenOrderBy(it => it.CreatedOn)
                .Distinct()
                .PagingListAsync<AgentVM>(it => new AgentVM
                {
                    XXXX = it.Name,
                    YYYY = it.PathId
                });

            Assert.True(res8.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task TestST02()
        {

            /*
             * 单表
             */

            /************************************************************************************************************************************/

            xx = string.Empty;

            // CompareEnum.Equal
            var op1 = new Single_PagingEdgeOption();
            op1.PhoneEqual = "19900000218";

            var res1 = await Conn
                .Queryer<Agent>()
                .Where(op1)
                .PagingListAsync();

            Assert.True(res1.TotalCount == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            // CompareEnum.NotEqual
            var op2 = new Single_PagingEdgeOption();
            op2.PhoneNotEqual = "19900000218";

            var res2 = await Conn
                .Queryer<Agent>()
                .Where(op2)
                .PagingListAsync();

            Assert.True(res2.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            // CompareEnum.LessThan
            var op3 = new Single_PagingEdgeOption();
            op3.CreatedOnLessThan = DateTime.Parse("2018-08-16 19:23:07.542265");

            var res3 = await Conn
                .Queryer<Agent>()
                .Where(op3)
                .PagingListAsync();

            Assert.True(res3.TotalCount == 9849);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            // CompareEnum.LessThanOrEqual
            var op4 = new Single_PagingEdgeOption();
            op4.CreatedOnLessThanOrEqual = DateTime.Parse("2018-08-16 19:23:07.542265");

            var res4 = await Conn
                .Queryer<Agent>()
                .Where(op4)
                .PagingListAsync();

            Assert.True(res4.TotalCount == 9850);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            // CompareEnum.GreaterThan
            var op5 = new Single_PagingEdgeOption();
            op5.CreatedOnGreaterThan = DateTime.Parse("2018-08-16 19:23:07.542265");

            var res5 = await Conn
                .Queryer<Agent>()
                .Where(op5)
                .PagingListAsync();

            Assert.True(res5.TotalCount == 18770);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            // CompareEnum.GreaterThanOrEqual
            var op6 = new Single_PagingEdgeOption();
            op6.CreatedOnGreaterThanOrEqual = DateTime.Parse("2018-08-16 19:23:07.542265");

            var res6 = await Conn
                .Queryer<Agent>()
                .Where(op6)
                .PagingListAsync();

            Assert.True(res6.TotalCount == 18771);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            // CompareEnum.Like
            var op7 = new Single_PagingEdgeOption();
            op7.NameLike = "雪";

            var res7 = await Conn
                .Queryer<Agent>()
                .Where(op7)
                .PagingListAsync();

            Assert.True(res7.TotalCount == 316);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            // CompareEnum.Like_StartsWith
            var op8 = new Single_PagingEdgeOption();
            op8.NameLike_StartsWith = "马";

            var res8 = await Conn
                .Queryer<Agent>()
                .Where(op8)
                .PagingListAsync();

            Assert.True(res8.TotalCount == 285);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            // CompareEnum.Like_EndsWith
            var op9 = new Single_PagingEdgeOption();
            op9.NameLike_EndsWith = "山";

            var res9 = await Conn
                .Queryer<Agent>()
                .Where(op9)
                .PagingListAsync();

            Assert.True(res9.TotalCount == 40);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            // CompareEnum.NotLike
            var op10 = new Single_PagingEdgeOption();
            op10.NameNotLike = "山";

            var res10 = await Conn
                .Queryer<Agent>()
                .Where(op10)
                .PagingListAsync();

            Assert.True(res10.TotalCount == 28577);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            // CompareEnum.NotLike_StartsWith
            var op11 = new Single_PagingEdgeOption();
            op11.NameNotLike_StartsWith = "刘";

            var res11 = await Conn
                .Queryer<Agent>()
                .Where(op11)
                .PagingListAsync();

            Assert.True(res11.TotalCount == 27163);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            // CompareEnum.NotLike_EndsWith
            var op12 = new Single_PagingEdgeOption();
            op12.NameNotLike_EndsWith = "民";

            var res12 = await Conn
                .Queryer<Agent>()
                .Where(op12)
                .PagingListAsync();

            Assert.True(res12.TotalCount == 28549);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            // CompareEnum.In
            var op13 = new Single_PagingEdgeOption();
            op13.AgentLevelIn = new List<AgentLevel?> { AgentLevel.CityAgent, AgentLevel.DistiAgent };

            var res13 = await Conn
                .Queryer<Agent>()
                .Where(op13)
                .PagingListAsync();

            Assert.True(res13.TotalCount == 555);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            // CompareEnum.NotIn
            var op14 = new Single_PagingEdgeOption();
            op14.AgentLevelNotIn = new List<AgentLevel?> { AgentLevel.CityAgent, AgentLevel.DistiAgent };

            var res14 = await Conn
                .Queryer<Agent>()
                .Where(op14)
                .PagingListAsync();

            Assert.True(res14.TotalCount == 28065);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task TestMT01()
        {

            /*
             * 多表
             */

            /************************************************************************************************************************************/

        }

    }
}
