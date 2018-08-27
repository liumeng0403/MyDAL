using System.Data;
using System.Linq;
using Xunit;
using EasyDAL.Exchange.AdoNet;

namespace EasyDAL.Exchange.Tests
{
    public class EnumTests : TestBase
    {

 



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

        //[Fact]
        //public void DapperEnumValue_SqlServer() => Common.DapperEnumValue(connection);

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
