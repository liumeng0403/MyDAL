using HPC.DAL;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Compare
{
    public class _06_LessThanOrEqual
        : TestBase
    {

        [Fact]
        public async Task LessThanOrEqual()
        {

            xx = string.Empty;

            // <= --> <=
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn <= DateTime.Parse("2018-08-16 19:20:35.867228"))
                .QueryListAsync();

            Assert.True(res1.Count == 6842);

            

            xx = string.Empty;

        }

        [Fact]
        public async Task NotLessThanOrEqual()
        {
            xx = string.Empty;

            // !(<=) --> >
            var res1 = await Conn.QueryListAsync<Agent>(it => !(it.CreatedOn <= DateTime.Parse("2018-08-16 19:20:35.867228")));

            Assert.True(res1.Count == 21778);

            

            /***********************************************************************************************************************************************/

            xx = string.Empty;

            // > --> >
            var res2 = await Conn.QueryListAsync<Agent>(it => it.CreatedOn > DateTime.Parse("2018-08-16 19:20:35.867228"));

            Assert.True(res2.Count == 21778);

            

            /***********************************************************************************************************************************************/

            xx = string.Empty;
        }

    }
}
