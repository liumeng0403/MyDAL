using System;
using System.Data;
using System.Linq;
using EasyDAL.Exchange.DynamicParameter;
using Xunit;
using EasyDAL.Exchange.AdoNet;

namespace EasyDAL.Exchange.Tests
{
    public class DecimalTests : TestBase
    {


        [Fact]
        public void Issue261_Decimals_ADONET_SetViaBaseClass() => Issue261_Decimals_ADONET(true);

        [Fact]
        public void Issue261_Decimals_ADONET_SetViaConcreteClass() => Issue261_Decimals_ADONET(false);

        private void Issue261_Decimals_ADONET(bool setPrecisionScaleViaAbstractApi)
        {
            try
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "create proc #Issue261Direct @c decimal(10,5) OUTPUT as begin set @c=11.884 end";
                    cmd.ExecuteNonQuery();
                }
            }
            catch { /* we don't care that it already exists */ }

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "#Issue261Direct";
                var c = cmd.CreateParameter();
                c.ParameterName = "c";
                c.Direction = ParameterDirection.Output;
                c.Value = DBNull.Value;
                c.DbType = DbType.Decimal;

                if (setPrecisionScaleViaAbstractApi)
                {
                    IDbDataParameter baseParam = c;
                    baseParam.Precision = 10;
                    baseParam.Scale = 5;
                }
                else
                {
                    c.Precision = 10;
                    c.Scale = 5;
                }

                cmd.Parameters.Add(c);
                cmd.ExecuteNonQuery();
                decimal value = (decimal)c.Value;
                Assert.Equal(11.884M, value);
            }
        }






    }
}
