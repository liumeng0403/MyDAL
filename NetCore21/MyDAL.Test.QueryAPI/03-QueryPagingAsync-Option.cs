using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using MyDAL.Test.Options;
using MyDAL.Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryAPI
{
    public class _03_QueryPagingAsync_Option
        : TestBase
    {

        [Fact]
        public async Task History_01()
        {

            /*************************************************************************************************************************/

            xx = string.Empty;

            var option2 = new AgentQueryOption();
            option2.Id = Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b");
            option2.Name = "樊士芹";
            // where method
            var res2 = await Conn
                .Queryer<Agent>()
                .Where(option2)
                .QueryPagingAsync();

            Assert.True(res2.TotalCount == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

            xx = string.Empty;

            var option3 = new AgentQueryOption();
            option3.StartTime = Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30);
            option3.EndTime = DateTime.Now;

            //   >=   <=  
            var res3 = await Conn
                .Queryer<Agent>()
                .Where(option3)
                .QueryPagingAsync();

            Assert.True(res3.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

            xx = string.Empty;

            var option4 = new AgentQueryOption();
            option4.Name = "张";

            //   like  
            var res4 = await Conn
                .Queryer<Agent>()
                .Where(option4)
                .QueryPagingAsync();

            Assert.True(res4.TotalCount == 2002);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

            xx = string.Empty;

            var option5 = new AgentQueryOption();
            option5.EnumListIn = new List<AgentLevel>
            {
                AgentLevel.CityAgent,
                AgentLevel.DistiAgent
            };

            // in
            var res5 = await Conn
                .Queryer<Agent>()
                .Where(option5)
                .QueryPagingAsync();

            Assert.True(res5.TotalCount == 555);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

            xx = string.Empty;

            var option6 = new AgentQueryOption();
            option6.EnumListNotIn = new List<AgentLevel>
            {
                AgentLevel.CityAgent,
                AgentLevel.DistiAgent
            };

            // in
            var res6 = await Conn
                .Queryer<Agent>()
                .Where(option6)
                .QueryPagingAsync();

            Assert.True(res6.TotalCount == 28064 || res6.TotalCount == 28065);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task History_02()
        {

            /*************************************************************************************************************************/

            xx = string.Empty;

            var option = new AgentQueryOption
            {
                Id = Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"),
                Name = "樊士芹"
            };
            // where method -- option orderby 
            var res3 = await Conn
                .Queryer<Agent>()
                .Where(option)
                .OrderBy(it => it.Name, OrderByEnum.Desc)
                .QueryPagingAsync<AgentVM>();

            Assert.True(res3.TotalCount == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

        }

        /*********************************************************************************************************************************/

        [Fact]
        public async Task Option_Paging_ST()
        {

            xx = string.Empty;

            var op1 = new Single_PagingEdgeOption();
            op1.Phone = "19900000218";

            // Where --> PagingListAsync
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(op1)
                .QueryPagingAsync();

            Assert.True(res1.TotalCount == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

        }

        [Fact]
        public async Task Option_OrderBy_Paging_ST()
        {

            xx = string.Empty;

            var op2 = new Single_PagingEdgeOption();
            op2.PhoneNotEqual = "19900000218";

            // Where --> OrderBy --> PagingListAsync
            var res2 = await Conn
                .Queryer<Agent>()
                .Where(op2)
                .OrderBy(it => it.Name, OrderByEnum.Asc)
                .QueryPagingAsync();

            Assert.True(res2.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

        }

        [Fact]
        public async Task Option_OrderBy_ThenOrderBy_Paging_ST()
        {

            xx = string.Empty;

            var op3 = new Single_PagingEdgeOption();
            op3.PhoneNotEqual = "19900000218";

            // Where --> OrderBy --> ThenOrderBy --> PagingListAsync
            var res3 = await Conn
                .Queryer<Agent>()
                .Where(op3)
                .OrderBy(it => it.AgentLevel)
                    .ThenOrderBy(it => it.Name, OrderByEnum.Asc)
                .QueryPagingAsync();

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
                .QueryPagingAsync();

            Assert.True(res4.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

        }

        [Fact]
        public async Task Option_Distinct_Paging_ST()
        {

            xx = string.Empty;

            var op5 = new Single_PagingEdgeOption();
            op5.PhoneNotEqual = "19900000218";

            // Where --> Distinct --> PagingListAsync
            var res5 = await Conn
                .Queryer<Agent>()
                .Where(op5)
                .Distinct()
                .QueryPagingAsync(it => it.AgentLevel);

            Assert.True(res5.TotalCount == 3);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

        }

        [Fact]
        public async Task Option_Distinct_OrderBy_Paging_ST()
        {

            xx = string.Empty;

            var op6 = new Single_PagingEdgeOption();
            op6.PhoneNotEqual = "19900000218";

            // Where --> Distinct --> OrderBy --> PagingListAsync
            var res6 = await Conn
                .Queryer<Agent>()
                .Where(op6)
                .Distinct()
                .OrderBy(it => it.Name)
                .QueryPagingAsync();

            Assert.True(res6.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

        }

        [Fact]
        public async Task Option_Distinct_OrderBy_ThenOrderBy_Paging_ST()
        {

            xx = string.Empty;

            var op7 = new Single_PagingEdgeOption();
            op7.PhoneNotEqual = "19900000218";

            // Where -->  Distinct --> OrderBy --> ThenOrderBy -->PagingListAsync
            var res7 = await Conn
                .Queryer<Agent>()
                .Where(op7)
                .Distinct()
                .OrderBy(it => it.AgentLevel)
                    .ThenOrderBy(it => it.Name, OrderByEnum.Asc)
                .QueryPagingAsync<AgentVM>();

            Assert.True(res7.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            var op8 = new Single_PagingEdgeOption();
            op8.PhoneNotEqual = "19900000218";

            // Where --> Distinct --> OrderBy --> ThenOrderBy --> ... ... --> PagingListAsync
            var res8 = await Conn
                .Queryer<Agent>()
                .Where(op8)
                .Distinct()
                .OrderBy(it => it.PathId)
                    .ThenOrderBy(it => it.Name)
                    .ThenOrderBy(it => it.CreatedOn)
                .QueryPagingAsync(it => new AgentVM
                {
                    XXXX = it.Name,
                    YYYY = it.PathId
                });

            Assert.True(res8.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

        }

        /*********************************************************************************************************************************/

        [Fact]
        public async Task XQuery_Equal_ST()
        {

            xx = string.Empty;

            // CompareEnum.Equal
            var op1 = new Single_PagingEdgeOption();
            op1.PhoneEqual = "19900000218";

            var res1 = await Conn
                .Queryer<Agent>()
                .Where(op1)
                .QueryPagingAsync();

            Assert.True(res1.TotalCount == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

        }

        [Fact]
        public async Task XQuery_NotEqual_ST()
        {

            xx = string.Empty;

            // CompareEnum.NotEqual
            var op2 = new Single_PagingEdgeOption();
            op2.PhoneNotEqual = "19900000218";

            var res2 = await Conn
                .Queryer<Agent>()
                .Where(op2)
                .QueryPagingAsync();

            Assert.True(res2.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

        }

        [Fact]
        public async Task XQuery_LessThan_ST()
        {

            xx = string.Empty;

            // CompareEnum.LessThan
            var op3 = new Single_PagingEdgeOption();
            op3.CreatedOnLessThan = DateTime.Parse("2018-08-16 19:23:07.542265");

            var res3 = await Conn
                .Queryer<Agent>()
                .Where(op3)
                .QueryPagingAsync();

            Assert.True(res3.TotalCount == 9849);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

        }

        [Fact]
        public async Task XQuery_LessThanOrEqual_ST()
        {

            xx = string.Empty;

            // CompareEnum.LessThanOrEqual
            var op4 = new Single_PagingEdgeOption();
            op4.CreatedOnLessThanOrEqual = DateTime.Parse("2018-08-16 19:23:07.542265");

            var res4 = await Conn
                .Queryer<Agent>()
                .Where(op4)
                .QueryPagingAsync();

            Assert.True(res4.TotalCount == 9850);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

        }

        [Fact]
        public async Task XQuery_GreaterThan_ST()
        {

            xx = string.Empty;

            // CompareEnum.GreaterThan
            var op5 = new Single_PagingEdgeOption();
            op5.CreatedOnGreaterThan = DateTime.Parse("2018-08-16 19:23:07.542265");

            var res5 = await Conn
                .Queryer<Agent>()
                .Where(op5)
                .QueryPagingAsync();

            Assert.True(res5.TotalCount == 18770);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

        }

        [Fact]
        public async Task XQuery_GreaterThanOrEqual_ST()
        {

            xx = string.Empty;

            // CompareEnum.GreaterThanOrEqual
            var op6 = new Single_PagingEdgeOption();
            op6.CreatedOnGreaterThanOrEqual = DateTime.Parse("2018-08-16 19:23:07.542265");

            var res6 = await Conn
                .Queryer<Agent>()
                .Where(op6)
                .QueryPagingAsync();

            Assert.True(res6.TotalCount == 18771);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

        }

        [Fact]
        public async Task XQuery_Like_ST()
        {

            xx = string.Empty;

            // CompareEnum.Like
            var op7 = new Single_PagingEdgeOption();
            op7.NameLike = "雪";

            var res7 = await Conn
                .Queryer<Agent>()
                .Where(op7)
                .QueryPagingAsync();

            Assert.True(res7.TotalCount == 316);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

        }

        [Fact]
        public async Task XQuery_Like_StartsWith_ST()
        {

            xx = string.Empty;

            // CompareEnum.Like_StartsWith
            var op8 = new Single_PagingEdgeOption();
            op8.NameLike_StartsWith = "马";

            var res8 = await Conn
                .Queryer<Agent>()
                .Where(op8)
                .QueryPagingAsync();

            Assert.True(res8.TotalCount == 285);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

        }

        [Fact]
        public async Task XQuery_Like_EndsWith_ST()
        {

            xx = string.Empty;

            // CompareEnum.Like_EndsWith
            var op9 = new Single_PagingEdgeOption();
            op9.NameLike_EndsWith = "山";

            var res9 = await Conn
                .Queryer<Agent>()
                .Where(op9)
                .QueryPagingAsync();

            Assert.True(res9.TotalCount == 40);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

        }

        [Fact]
        public async Task XQuery_NotLike_ST()
        {

            xx = string.Empty;

            // CompareEnum.NotLike
            var op10 = new Single_PagingEdgeOption();
            op10.NameNotLike = "山";

            var res10 = await Conn
                .Queryer<Agent>()
                .Where(op10)
                .QueryPagingAsync();

            Assert.True(res10.TotalCount == 28577);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

        }

        [Fact]
        public async Task XQuery_NotLike_StartsWith_ST()
        {

            xx = string.Empty;

            // CompareEnum.NotLike_StartsWith
            var op11 = new Single_PagingEdgeOption();
            op11.NameNotLike_StartsWith = "刘";

            var res11 = await Conn
                .Queryer<Agent>()
                .Where(op11)
                .QueryPagingAsync();

            Assert.True(res11.TotalCount == 27163);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

        }

        [Fact]
        public async Task XQuery_NotLike_EndsWith_ST()
        {

            xx = string.Empty;

            // CompareEnum.NotLike_EndsWith
            var op12 = new Single_PagingEdgeOption();
            op12.NameNotLike_EndsWith = "民";

            var res12 = await Conn
                .Queryer<Agent>()
                .Where(op12)
                .QueryPagingAsync();

            Assert.True(res12.TotalCount == 28549);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

        }

        [Fact]
        public async Task XQuery_In_ST()
        {

            xx = string.Empty;

            // CompareEnum.In
            var op13 = new Single_PagingEdgeOption();
            op13.AgentLevelIn = new List<AgentLevel?> { AgentLevel.CityAgent, AgentLevel.DistiAgent };

            var res13 = await Conn
                .Queryer<Agent>()
                .Where(op13)
                .QueryPagingAsync();

            Assert.True(res13.TotalCount == 555);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

        }

        [Fact]
        public async Task XQuery_NotIn_ST()
        {

            xx = string.Empty;

            // CompareEnum.NotIn
            var op14 = new Single_PagingEdgeOption();
            op14.AgentLevelNotIn = new List<AgentLevel?> { AgentLevel.CityAgent, AgentLevel.DistiAgent };

            var res14 = await Conn
                .Queryer<Agent>()
                .Where(op14)
                .QueryPagingAsync();

            Assert.True(res14.TotalCount == 28065);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

        }

        /*********************************************************************************************************************************/

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
