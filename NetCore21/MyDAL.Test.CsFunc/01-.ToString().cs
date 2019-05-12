<<<<<<< HEAD
﻿using MyDAL.Test.Entities.MySql;
=======
﻿using HPC.DAL;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Entities.MySql;
using System;
using System.Text;
>>>>>>> 95401e8... [add]  to string & query one api
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.CsFunc
{
    public class _01_
        : TestBase
    {

        [Fact]
        public async Task QueryOne_SingleColumn_ST()
        {
            var pk = 'A';
            await MySQL_PreData(pk, 1);

            xx = string.Empty;

            var res1 = await Conn.QueryOneAsync<MySQL_EveryType>(it => it.Char == $"{pk}-char");
            Assert.NotNull(res1);

            var res_char = await Conn.QueryOneAsync<MySQL_EveryType, string>(it => it.Char == $"{pk}-char", it => it.Char.ToString());
            Assert.Equal(res_char, $"{pk}-char");

            var res_VarChar = await Conn.QueryOneAsync<MySQL_EveryType, string>(it => it.Char == $"{pk}-char", it => it.VarChar.ToString());
            Assert.Equal(res_VarChar, $"{pk}-var char");

            var res_TinyText = await Conn.QueryOneAsync<MySQL_EveryType, string>(it => it.Char == $"{pk}-char", it => it.TinyText.ToString());
            Assert.Equal(res_TinyText, $"{pk}-tiny text");

            var res_Text = await Conn.QueryOneAsync<MySQL_EveryType, string>(it => it.Char == $"{pk}-char", it => it.Text.ToString());
            Assert.Equal(res_Text, $"{pk}-text");

            var res_MediumText = await Conn.QueryOneAsync<MySQL_EveryType, string>(it => it.Char == $"{pk}-char", it => it.MediumText.ToString());
            Assert.Equal(res_MediumText, $"{pk}-medium text");

            var res_LongText = await Conn.QueryOneAsync<MySQL_EveryType, string>(it => it.Char == $"{pk}-char", it => it.LongText.ToString());
            Assert.Equal(res_LongText, $"{pk}-long text");

            //var res_TinyBlob = await Conn.QueryOneAsync<MySQL_EveryType, byte[]>(it => it.Char == $"{pk}-char", it => it.TinyBlob);
            //Assert.Equal(Encoding.UTF8.GetString(res_TinyBlob), $"{pk}-tiny blob");

            xx = string.Empty;
        }

        [Fact]
        public async Task QueryOne_VmColumn_ST()
        {

        }

        [Fact]
        public async Task QueryOne_SingleColumn_MT()
        {

        }

        [Fact]
        public async Task QueryOne_VmColumn_MT()
        {

        }

        [Fact]
        public async Task DateTime_yyyy_MM_dd()
        {
            var date = DateTime.Parse("2018-08-16 12:03:47.225916");

            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn.ToString("yyyy-MM-dd") == date.ToString("yyyy-MM-dd"))
                .QueryListAsync();

            Assert.True(res1.Count == 28619);

            xx = string.Empty;
        }

        [Fact]
        public async Task DateTime_yyyy_MM()
        {
            var date = DateTime.Parse("2018-08-16 12:03:47.225916");

            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn.ToString("yyyy-MM") == date.ToString("yyyy-MM"))
                .QueryListAsync();

            Assert.True(res1.Count == 28619);

            xx = string.Empty;
        }

        [Fact]
        public async Task DateTime_yyyy()
        {
            var date = DateTime.Parse("2018-08-16 12:03:47.225916");

            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn.ToString("yyyy") == date.ToString("yyyy"))
                .QueryListAsync();

            Assert.True(res1.Count == 28619);

            xx = string.Empty;
        }

        [Fact]
        public async Task DateTime_Null_yyyy_MM_dd()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.ActivedOn != null && it.ActivedOn.Value.ToString("yyyy-MM-dd") == DateTime.Parse("2018-08-19 12:05:45.560984").ToString("yyyy-MM-dd"))
                .QueryListAsync();

            Assert.True(res1.Count == 554);

            xx = string.Empty;
        }
    }
}
