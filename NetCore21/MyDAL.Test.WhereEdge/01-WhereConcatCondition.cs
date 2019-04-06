using MyDAL.ModelTools;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.WhereEdge
{
    public class _01_WhereConcatCondition
        : TestBase
    {
        [Fact]
        public async Task Concat_None_ST()
        {
            xx = string.Empty;

            var where = Conn.Queryer<Agent>().WHERE;

            var res1 = await where.TopAsync(1);

            Assert.NotNull(res1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;

        }

        [Fact]
        public async Task Concat_Multi_ST()
        {
            xx = string.Empty;

            var userId = "08d6036b-0a7e-b07d-b9bd-af03841b3baa";
            var firstName = "伏";

            var where = Conn.Queryer<Agent>().WHERE;

            // 条件1
            if(!userId.IsNullStr())
            {
                where = where.And(it => it.UserId == Guid.Parse(userId));
            }
            // 条件2
            if(!firstName.IsNullStr())
            {
                where = where.And(it => it.Name.StartsWith(firstName));
            }

            var res1 = await where.QueryListAsync();

            Assert.True(res1.Count==1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }
    }
}
