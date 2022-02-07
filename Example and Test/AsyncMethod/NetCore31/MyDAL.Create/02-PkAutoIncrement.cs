using System;
using System.Threading.Tasks;
using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using Xunit;

namespace MyDAL.Create
{
    public class _02_PkAutoIncrement
    : TestBase{
        
        
        [Fact]
        public async Task insert_one()
        {

            /********************************************************************************************************************************/

            var m1 = new AutoIncrementDemo()
            {
                col1 = "111",
                col2 = "3333"
            };

            xx = string.Empty;

            var res1 = await MyDAL_TestDB.InsertAsync(m1);

            Assert.True(m1.id == 1);
            
            xx = string.Empty;

            /********************************************************************************************************************************/

            var m2 = new AutoIncrementDemo()
            {
                col1 = "111",
                col2 = "3333"
            };

            xx = string.Empty;

            var res2 = await MyDAL_TestDB.InsertAsync(m2);

            Assert.True(m2.id == 2);
            
            xx = string.Empty;


            /********************************************************************************************************************************/

            var m5 = new AutoIncrementDemo()
            {
                id = 5,
                col1 = "111",
                col2 = "3333"
            };

            xx = string.Empty;

            var res5 = await MyDAL_TestDB.InsertAsync(m5);

            Assert.True(m5.id == 5);
            
            xx = string.Empty;
            
            
            /********************************************************************************************************************************/

            var m6 = new AutoIncrementDemo()
            {
                col1 = "111",
                col2 = "3333"
            };

            xx = string.Empty;

            var res6 = await MyDAL_TestDB.InsertAsync(m6);

            Assert.True(m6.id == 6);
            
            xx = string.Empty;
            
        }
        
    }
}