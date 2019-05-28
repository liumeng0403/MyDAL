using HPC.DAL;
using HPC.DAL.ModelTools;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
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

            var where = Conn.Queryer<Agent>().WhereSegment;

            var res1 = await where.TopAsync(1);

            Assert.NotNull(res1);

            

            xx = string.Empty;

        }

        [Fact]
        public async Task Concat_Multi_ST()
        {
            xx = string.Empty;

            // 上下文条件 变量
            var userId = "08d6036b-0a7e-b07d-b9bd-af03841b3baa";
            var firstName = "伏";

            var where = Conn.Queryer<Agent>().WhereSegment;

            // 根据条件 判断 是否 拼接 UserId 字段 的 过滤条件
            if (!userId.IsNullStr())
            {
                where = where.And(it => it.UserId == Guid.Parse(userId));
            }

            // 根据条件 判断 是否 拼接 Name 字段 的 过滤条件
            if (!firstName.IsNullStr())
            {
                where = where.And(it => it.Name.StartsWith(firstName));
            }

            // 对 WhereSegment 设定的条件 进行 select 动作
            var res1 = await where.QueryListAsync();

            Assert.True(res1.Count == 1);

            

            xx = string.Empty;
        }

        [Fact]
        public async Task Concat_Multi_MT()
        {
            xx = string.Empty;

            // 上下文 分页 变量
            var pageIndex = 2;
            var pageSize = 10;

            // 上下文 条件 变量
            var level = (AgentLevel?)AgentLevel.DistiAgent;
            var pk1 = Guid.Parse("fbad4af4-c160-4e66-a8fc-0165443b4db0");

            // 可 自由混合书写 多个 inner join 或 left join 
            var where = Conn
                .Queryer(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .LeftJoin(() => record)
                        .On(() => agent.Id == record.AgentId).WhereSegment;

            // 根据条件 判断 是否 拼接 AgentLevel 字段 的 过滤条件
            if (level != null)
            {
                where = where.And(() => agent.AgentLevel == level);   // and demo
            }

            // 根据条件 判断 是否 拼接 Id 字段 的 过滤条件
            if (pk1 != Guid.Empty)
            {
                where = where.Or(() => agent.Id == pk1);   //  or demo
            }

            // 对 WhereSegment 设定的条件 进行 select 动作
            var res1 = await where.QueryPagingAsync<Agent>(pageIndex, pageSize);

            Assert.True(res1.Data.Count == 10);
            Assert.True(res1.TotalCount == 575);

            

            xx = string.Empty;
        }
    }
}
