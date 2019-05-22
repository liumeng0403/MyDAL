using HPC.DAL;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Entities.MySql;
using System;
using System.Collections.Generic;
using System.Text;
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

            var res_char = await Conn.QueryOneAsync<MySQL_EveryType>(it => it.Char == $"{pk}-char", it => it.Char.ToString());
            Assert.Equal(res_char, $"{pk}-char");

            var res_VarChar = await Conn.QueryOneAsync<MySQL_EveryType>(it => it.Char == $"{pk}-char", it => it.VarChar.ToString());
            Assert.Equal(res_VarChar, $"{pk}-var char");

            var res_TinyText = await Conn.QueryOneAsync<MySQL_EveryType>(it => it.Char == $"{pk}-char", it => it.TinyText.ToString());
            Assert.Equal(res_TinyText, $"{pk}-tiny text");

            var res_Text = await Conn.QueryOneAsync<MySQL_EveryType>(it => it.Char == $"{pk}-char", it => it.Text.ToString());
            Assert.Equal(res_Text, $"{pk}-text");

            var res_MediumText = await Conn.QueryOneAsync<MySQL_EveryType>(it => it.Char == $"{pk}-char", it => it.MediumText.ToString());
            Assert.Equal(res_MediumText, $"{pk}-medium text");

            var res_LongText = await Conn.QueryOneAsync<MySQL_EveryType>(it => it.Char == $"{pk}-char", it => it.LongText.ToString());
            Assert.Equal(res_LongText, $"{pk}-long text");

            var res_TinyBlob = await Conn.QueryOneAsync<MySQL_EveryType>(it => it.Char == $"{pk}-char", it => it.TinyBlob);
            Assert.Equal(Encoding.UTF8.GetString(res_TinyBlob), $"{pk}-tiny blob");

            try
            {
                var res_Blob = await Conn.QueryOneAsync<MySQL_EveryType>(it => it.Char == $"{pk}-char", it => it.Blob.ToString());
            }
            catch (Exception ex)
            {
                var err = "【ERR-093】 -- [[【byte[]】对应 DB column 不能使用 C# .ToString() 函数！表达式--【it.Blob.ToString()】]] ，请 EMail: --> liumeng0403@163.com <--";
                Assert.Equal(ex.Message, err);
            }

            var res_MediumBlob = await Conn.QueryOneAsync<MySQL_EveryType>(it => it.Char == $"{pk}-char", it => it.MediumBlob);
            Assert.Equal(Encoding.UTF8.GetString(res_MediumBlob), $"{pk}-medium blob");

            var res_LongBlob = await Conn.QueryOneAsync<MySQL_EveryType>(it => it.Char == $"{pk}-char", it => it.LongBlob);
            Assert.Equal(Encoding.UTF8.GetString(res_LongBlob), $"{pk}-long blob");

            var res_Binary = await Conn.QueryOneAsync<MySQL_EveryType>(it => it.Char == $"{pk}-char", it => it.Binary);
            Assert.StartsWith($"{pk}-binary",Encoding.UTF8.GetString(res_Binary));

            var res_VarBinary = await Conn.QueryOneAsync<MySQL_EveryType>(it => it.Char == $"{pk}-char", it => it.VarBinary);
            Assert.Equal($"{pk}-var binary", Encoding.UTF8.GetString(res_VarBinary));

            var res_Enum = await Conn.QueryOneAsync<MySQL_EveryType>(it => it.Char == $"{pk}-char", it => it.Enum.ToString());
            Assert.Equal(MySQL_Enum.A.ToString(), res_Enum);

            var res_Enum_Null = await Conn.QueryOneAsync<MySQL_EveryType>(it => it.Char == $"{pk}-char", it => it.Enum_Null.ToString());
            Assert.Equal(MySQL_Enum.B.ToString(), res_Enum_Null);

            var res_Set = await Conn.QueryOneAsync<MySQL_EveryType>(it => it.Char == $"{pk}-char", it => it.Set.ToString());
            Assert.Equal(string.Join(",", new List<string> { "music", "movie" }), res_Set);

            var res_Set_Null = await Conn.QueryOneAsync<MySQL_EveryType>(it => it.Char == $"{pk}-char", it => it.Set_Null.ToString());
            Assert.Equal(string.Join(",", new List<string> { "swimming" }), res_Set_Null);

            var res_TinyInt = await Conn.QueryOneAsync<MySQL_EveryType>(it => it.Char == $"{pk}-char", it => it.TinyInt.ToString());
            Assert.Equal("65", res_TinyInt);

            var res_TinyInt_Null = await Conn.QueryOneAsync<MySQL_EveryType>(it => it.Char == $"{pk}-char", it => it.TinyInt_Null.ToString());
            Assert.Equal("65", res_TinyInt_Null);

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
