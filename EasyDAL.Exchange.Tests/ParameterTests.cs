using EasyDAL.Exchange.DynamicParameter;
using EasyDAL.Exchange.MapperX;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Xunit;

namespace EasyDAL.Exchange.Tests
{
    public class ParameterTests : TestBase
    {
        public class DbParams : IDynamicParameters, IEnumerable<IDbDataParameter>
        {
            private readonly List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            public IEnumerator<IDbDataParameter> GetEnumerator() { return parameters.GetEnumerator(); }
            IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
            public void Add(IDbDataParameter value)
            {
                parameters.Add(value);
            }

            void IDynamicParameters.AddParameters(IDbCommand command, Identity identity)
            {
                foreach (IDbDataParameter parameter in parameters)
                    command.Parameters.Add(parameter);
            }
        }

        private static List<Microsoft.SqlServer.Server.SqlDataRecord> CreateSqlDataRecordList(IEnumerable<int> numbers)
        {
            var number_list = new List<Microsoft.SqlServer.Server.SqlDataRecord>();

            // Create an SqlMetaData object that describes our table type.
            Microsoft.SqlServer.Server.SqlMetaData[] tvp_definition = { new Microsoft.SqlServer.Server.SqlMetaData("n", SqlDbType.Int) };

            foreach (int n in numbers)
            {
                // Create a new record, using the metadata array above.
                var rec = new Microsoft.SqlServer.Server.SqlDataRecord(tvp_definition);
                rec.SetInt32(0, n);    // Set the value.
                number_list.Add(rec);      // Add it to the list.
            }

            return number_list;
        }

    
        

        private class SO29596645_RuleTableValuedParameters : IDynamicParameters
        {
            private readonly string parameterName;

            public SO29596645_RuleTableValuedParameters(string parameterName)
            {
                this.parameterName = parameterName;
            }

            public void AddParameters(IDbCommand command, Identity identity)
            {
                Debug.WriteLine("> AddParameters");
                var lazy = (SqlCommand)command;
                lazy.Parameters.AddWithValue("Id", 7);
                var table = new DataTable
                {
                    Columns = { { "Id", typeof(int) } },
                    Rows = { { 4 }, { 9 } }
                };
                lazy.Parameters.AddWithValue("Rules", table);
                Debug.WriteLine("< AddParameters");
            }
        }

        private class SO29596645_OrganisationDTO
        {
            public SO29596645_RuleTableValuedParameters Rules { get; }

            public SO29596645_OrganisationDTO()
            {
                Rules = new SO29596645_RuleTableValuedParameters("@Rules");
            }
        }



        [Fact]
        public void SO30156367_DynamicParamsWithoutExec()
        {
            var dbParams = new DynamicParameters();
            dbParams.Add("Field1", 1);
            var value = dbParams.Get<int>("Field1");
            Assert.Equal(1, value);
        }

 
    }
}
