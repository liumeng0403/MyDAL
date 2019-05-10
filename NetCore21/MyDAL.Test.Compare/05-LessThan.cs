using HPC.DAL;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Compare
{
    public class _05_LessThan
        :TestBase
    {

        [Fact]
        public async Task LessThan()
        {

            xx = string.Empty;

            // < --> <
            var res1 = await Conn.QueryListAsync<Agent>(it => it.CreatedOn < DateTime.Parse("2019-02-10"));

            Assert.True(res1.Count == 28620);

            

            xx = string.Empty;

        }

        [Fact]
        public async Task NotLessThan()
        {
            xx = string.Empty;

            // !(<) --> >=
            var res1 = await Conn.QueryListAsync<Agent>(it => !(it.CreatedOn < DateTime.Parse("2019-02-10")));

            Assert.True(res1.Count == 0);

            

            /*********************************************************************************************************************************/

            xx = string.Empty;

            // >= --> >=
            var res2 = await Conn.QueryListAsync<AlipayPaymentRecord>(it => it.CreatedOn >= DateTime.Parse("2018-08-20"));

            Assert.True(res2.Count == 29);

            

            /****************************************************************************************/
            
            xx = string.Empty;
        }

    }
}
