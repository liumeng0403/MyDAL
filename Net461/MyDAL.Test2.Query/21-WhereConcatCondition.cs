using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;
using HPC.DAL.Tools;
using HPC.DAL;

namespace MyDAL.Test2.Query
{
    [TestClass]
    public class _21_WhereConcatCondition
        :TestBase
    {

        [TestMethod]
        public async Task Concat_None_ST()
        {
            xx = string.Empty;

            var where = Conn2.Queryer<Agent>().WhereSegment;

            var res1 = await where.TopAsync(1);

            Assert.IsNotNull(res1);

            

            xx = string.Empty;

        }

        [TestMethod]
        public async Task Concat_Multi_ST()
        {
            xx = string.Empty;

            var userId = "08d6036b-0a7e-b07d-b9bd-af03841b3baa";
            var firstName = "伏";

            var where = Conn2.Queryer<Agent>().WhereSegment;

            // 条件1
            if (!userId.IsNullStr())
            {
                where = where.And(it => it.UserId == Guid.Parse(userId));
            }
            // 条件2
            if (!firstName.IsNullStr())
            {
                where = where.And(it => it.Name.StartsWith(firstName));
            }

            var res1 = await where.QueryListAsync();

            Assert.IsTrue(res1.Count == 1);

            

            xx = string.Empty;
        }

    }
}
