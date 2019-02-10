﻿using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Options;
using MyDAL.Test.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.ShortcutAPI
{
    public class _02_QueryListAsync : TestBase
    {
        [Fact]
        public async Task test()
        {
            /****************************************************************************************/

            xx = string.Empty;
            var date = DateTime.Parse("2018-08-20");

            var res2 = await Conn.QueryListAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(it => it.CreatedOn >= date);
            Assert.True(res2.Count == 29);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            xx = string.Empty;

            var res3 = await Conn.QueryListAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(it => it.CreatedOn >= date,
                it => new AlipayPaymentRecordVM
                {
                    TotalAmount = it.TotalAmount,
                    Description = it.Description
                });
            Assert.True(res3.Count == 29);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);
            
            xx = string.Empty;

            var res7 = await Conn.QueryListAsync<Agent, string>(it => it.Name.StartsWith("张"), it => it.Name);
            Assert.True(res7.Count == 1996);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            xx = string.Empty;

        }
    }
}
