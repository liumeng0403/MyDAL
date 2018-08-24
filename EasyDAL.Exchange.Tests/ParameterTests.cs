using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Dynamic;
using System.Linq;
using Xunit;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Diagnostics;
using EasyDAL.Exchange.DynamicParameter;
using EasyDAL.Exchange.Parameter;
using EasyDAL.Exchange.MapperX;
using EasyDAL.Exchange.DataBase;
using EasyDAL.Exchange.AdoNet;

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

        private class IntDynamicParam : IDynamicParameters
        {
            private readonly IEnumerable<int> numbers;
            public IntDynamicParam(IEnumerable<int> numbers)
            {
                this.numbers = numbers;
            }

            public void AddParameters(IDbCommand command, Identity identity)
            {
                var sqlCommand = (SqlCommand)command;
                sqlCommand.CommandType = CommandType.StoredProcedure;

                var number_list = CreateSqlDataRecordList(numbers);

                // Add the table parameter.
                var p = sqlCommand.Parameters.Add("ints", SqlDbType.Structured);
                p.Direction = ParameterDirection.Input;
                p.TypeName = "int_list_type";
                p.Value = number_list;
            }
        }

        private class IntCustomParam : ICustomQueryParameter
        {
            private readonly IEnumerable<int> numbers;
            public IntCustomParam(IEnumerable<int> numbers)
            {
                this.numbers = numbers;
            }

            public void AddParameter(IDbCommand command, string name)
            {
                var sqlCommand = (SqlCommand)command;
                sqlCommand.CommandType = CommandType.StoredProcedure;

                var number_list = CreateSqlDataRecordList(numbers);

                // Add the table parameter.
                var p = sqlCommand.Parameters.Add(name, SqlDbType.Structured);
                p.Direction = ParameterDirection.Input;
                p.TypeName = "int_list_type";
                p.Value = number_list;
            }
        }






        // http://code.google.com/p/dapper-dot-net/issues/detail?id=70
        // https://connect.microsoft.com/VisualStudio/feedback/details/381934/sqlparameter-dbtype-dbtype-time-sets-the-parameter-to-sqldbtype-datetime-instead-of-sqldbtype-time




            

        private class DynamicParameterWithIntTVP : DynamicParameters, IDynamicParameters
        {
            private readonly IEnumerable<int> numbers;
            public DynamicParameterWithIntTVP(IEnumerable<int> numbers)
            {
                this.numbers = numbers;
            }

            public new void AddParameters(IDbCommand command, Identity identity)
            {
                base.AddParameters(command, identity);

                var sqlCommand = (SqlCommand)command;
                sqlCommand.CommandType = CommandType.StoredProcedure;

                var number_list = CreateSqlDataRecordList(numbers);

                // Add the table parameter.
                var p = sqlCommand.Parameters.Add("ints", SqlDbType.Structured);
                p.Direction = ParameterDirection.Input;
                p.TypeName = "int_list_type";
                p.Value = number_list;
            }
        }

        



        public class WithInit : ISupportInitialize
        {
            public string Value { get; set; }
            public int Flags { get; set; }

            void ISupportInitialize.BeginInit() => Flags++;

            void ISupportInitialize.EndInit() => Flags += 30;
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
        public void TestSupportForDynamicParametersOutputExpressions_Scalar()
        {
            using (var connection = GetOpenConnection())
            {
                var bob = new Person { Name = "bob", PersonId = 1, Address = new Address { PersonId = 2 } };

                var p = new DynamicParameters(bob);
                p.Output(bob, b => b.PersonId);
                p.Output(bob, b => b.Occupation);
                p.Output(bob, b => b.NumberOfLegs);
                p.Output(bob, b => b.Address.Name);
                p.Output(bob, b => b.Address.PersonId);

                var result = (int)connection.ExecuteScalar(@"
SET @Occupation = 'grillmaster' 
SET @PersonId = @PersonId + 1 
SET @NumberOfLegs = @NumberOfLegs - 1
SET @AddressName = 'bobs burgers'
SET @AddressPersonId = @PersonId
select 42", p);

                Assert.Equal("grillmaster", bob.Occupation);
                Assert.Equal(2, bob.PersonId);
                Assert.Equal(1, bob.NumberOfLegs);
                Assert.Equal("bobs burgers", bob.Address.Name);
                Assert.Equal(2, bob.Address.PersonId);
                Assert.Equal(42, result);
            }
        }








        public class HazX
        {
            public string X { get; set; }
        }











        public class ParameterWithIndexer
        {
            public int A { get; set; }
            public virtual string this[string columnName]
            {
                get { return null; }
                set { }
            }
        }



        public class MultipleParametersWithIndexer : MultipleParametersWithIndexerDeclaringType
        {
            public int A { get; set; }
        }

        public class MultipleParametersWithIndexerDeclaringType
        {
            public object this[object field]
            {
                get { return null; }
                set { }
            }

            public object this[object field, int index]
            {
                get { return null; }
                set { }
            }

            public int B { get; set; }
        }



        public class Dyno
        {
            public dynamic Id { get; set; }
            public string Name { get; set; }

            public object Foo { get; set; }
        }



  







        [Fact]
        public void SO30156367_DynamicParamsWithoutExec()
        {
            var dbParams = new DynamicParameters();
            dbParams.Add("Field1", 1);
            var value = dbParams.Get<int>("Field1");
            Assert.Equal(1, value);
        }

 


        
        private static void Incr(ref int i)
        {
            if (i <= 15) i++;
            else if (i <= 80) i += 5;
            else if (i <= 200) i += 10;
            else if (i <= 1000) i += 50;
            else i += 100;
        }

        [Fact]
        public void Issue601_InternationalParameterNamesWork()
        {
            // regular parameter
            var result = connection.QuerySingle<int>("select @æøå٦", new { æøå٦ = 42 });
            Assert.Equal(42, result);
        }
        
        private static int GetExpectedListExpansionCount(int count, bool enabled)
        {
            if (!enabled) return count;

            if (count <= 5 || count > 2070) return count;

            int padFactor;
            if (count <= 150) padFactor = 10;
            else if (count <= 750) padFactor = 50;
            else if (count <= 2000) padFactor = 100;
            else if (count <= 2070) padFactor = 10;
            else padFactor = 200;

            int blocks = count / padFactor, delta = count % padFactor;
            if (delta != 0) blocks++;
            return blocks * padFactor;
        }
    }
}
