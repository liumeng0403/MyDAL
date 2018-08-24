using System.Data;
using System.Linq;
using Xunit;
using EasyDAL.Exchange.AdoNet;

namespace EasyDAL.Exchange.Tests
{
    public class EnumTests : TestBase
    {

 


        private enum EnumParam : short
        {
            None = 0,
            A = 1,
            B = 2
        }

        private class EnumParamObject
        {
            public EnumParam A { get; set; }
            public EnumParam? B { get; set; }
            public EnumParam? C { get; set; }
        }

        private class EnumParamObjectNonNullable
        {
            public EnumParam A { get; set; }
            public EnumParam? B { get; set; }
            public EnumParam? C { get; set; }
        }

        private enum TestEnum : byte
        {
            Bla = 1
        }

        private class TestEnumClass
        {
            public TestEnum? EnumEnum { get; set; }
        }

        private class TestEnumClassNoNull
        {
            public TestEnum EnumEnum { get; set; }
        }

        [Fact]
        public void AdoNetEnumValue()
        {
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "select @foo";
                var p = cmd.CreateParameter();
                p.ParameterName = "@foo";
                p.DbType = DbType.Int32; // it turns out that this is the key piece; setting the DbType
                p.Value = AnEnum.B;
                cmd.Parameters.Add(p);
                object value = cmd.ExecuteScalar();
                AnEnum val = (AnEnum)value;
                Assert.Equal(AnEnum.B, val);
            }
        }

        [Fact]
        public void DapperEnumValue_SqlServer() => Common.DapperEnumValue(connection);

        private enum SO27024806Enum
        {
            Foo = 0,
            Bar = 1
        }

        private class SO27024806Class
        {
            public SO27024806Class(SO27024806Enum myField)
            {
                MyField = myField;
            }

            public SO27024806Enum MyField { get; set; }
        }

    }
}
