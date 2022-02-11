﻿using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.WhereEdge
{
    public class _08_WhereDollarString 
        : TestBase
    {
        [Fact]
        public async Task test()
        {
            xx = string.Empty;

            var res1 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.Name == $"{"樊士芹"}")
                .QueryOneAsync();

            Assert.NotNull(res1);

            

            /*******************************************************************************************************************/

            xx = string.Empty;

            var name2 = "樊士芹";
            var res2 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.Name == $"{name2}")
                .QueryOneAsync();

            Assert.NotNull(res2);

            

            /*******************************************************************************************************************/

            xx = string.Empty;

            var res3 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.CreatedOn > DateTime.Parse($"{Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30).AddDays(-10)}"))
                .QueryListAsync();
            Assert.NotNull(res3);
            Assert.True(res3.Count == 28619);

            

            /*******************************************************************************************************************/

            xx = string.Empty;

            var name4 = "张";
            var res4 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.Name.Contains($"{name4}%"))
                .QueryListAsync();
            Assert.NotNull(res4);
            Assert.True(res4.Count == 1996);

            

            /*******************************************************************************************************************/

            xx = string.Empty;

            var res5 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.PathId.Contains($"{WhereTest.ContainStr2}%"))
                .QueryListAsync();
            Assert.NotNull(res5);
            Assert.True(res5.Count == 20016);

            

            /*******************************************************************************************************************/

            xx = string.Empty;

            var like61 = "李";
            var like62 = "张";
            var res6 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.Name.Contains($"{like61}%") || it.Name.Contains($"{like62}%"))
                .QueryListAsync();
            var res61 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.Name.Contains($"{like61}%"))
                .QueryListAsync();
            var res62 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.Name.Contains($"{like62}%"))
                .QueryListAsync();
            Assert.True(res61.Count != 0);
            Assert.True(res62.Count != 0);
            Assert.True(res6.Count == res61.Count + res62.Count);

            

            /*******************************************************************************************************************/

            xx = string.Empty;
        }
    }
}
