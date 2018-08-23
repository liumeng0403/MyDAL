using System;
using System.Data;
using System.Data.Common;
using EasyDAL.Exchange.DynamicParameter;
using Xunit;
using EasyDAL.Exchange.AdoNet;

namespace EasyDAL.Exchange.Tests
{
    public static class Common
    {
        public static Type GetSomeType() => typeof(SomeType);

        public static void DapperEnumValue(IDbConnection connection)
        {
            // test passing as AsEnum, reading as int
            var v = (AnEnum)connection.QuerySingle<int>("select @v, @y, @z", new { v = AnEnum.B, y = (AnEnum?)AnEnum.B, z = (AnEnum?)null });
            Assert.Equal(AnEnum.B, v);

            var args = new DynamicParameters();
            args.Add("v", AnEnum.B);
            args.Add("y", AnEnum.B);
            args.Add("z", null);
            v = (AnEnum)connection.QuerySingle<int>("select @v, @y, @z", args);
            Assert.Equal(AnEnum.B, v);

            // test passing as int, reading as AnEnum
            var k = (int)connection.QuerySingle<AnEnum>("select @v, @y, @z", new { v = (int)AnEnum.B, y = (int?)(int)AnEnum.B, z = (int?)null });
            Assert.Equal(k, (int)AnEnum.B);

            args = new DynamicParameters();
            args.Add("v", (int)AnEnum.B);
            args.Add("y", (int)AnEnum.B);
            args.Add("z", null);
            k = (int)connection.QuerySingle<AnEnum>("select @v, @y, @z", args);
            Assert.Equal(k, (int)AnEnum.B);
        }



        private class NullableDatePerson
        {
            public int Id { get; set; }
            public DateTime? DoB { get; set; }
            public DateTime? DoB2 { get; set; }
        }
    }
}
