﻿using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using Xunit;

namespace MyDAL.Compare
{
    public class _05_LessThan
        :TestBase
    {

        [Fact]
        public void LessThan()
        {

            xx = string.Empty;

            // < --> <
            var res1 = MyDAL_TestDB.SelectList<Agent>(it => it.CreatedOn < DateTime.Parse("2019-02-10"));

            Assert.True(res1.Count == 28620);

            

            xx = string.Empty;

        }

        [Fact]
        public void NotLessThan()
        {
            xx = string.Empty;

            // !(<) --> >=
            var res1 = MyDAL_TestDB.SelectList<Agent>(it => !(it.CreatedOn < DateTime.Parse("2019-02-10")));

            Assert.True(res1.Count == 0);

            

            /*********************************************************************************************************************************/

            xx = string.Empty;

            // >= --> >=
            var res2 = MyDAL_TestDB.SelectList<AlipayPaymentRecord>(it => it.CreatedOn >= DateTime.Parse("2018-08-20"));

            Assert.True(res2.Count == 29);

            

            /****************************************************************************************/
            
            xx = string.Empty;
        }

    }
}
