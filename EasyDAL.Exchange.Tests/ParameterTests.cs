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



        [Fact]
        public void TestDoubleParam()
        {
            Assert.Equal(0.1d, connection.Query<double>("select @d", new { d = 0.1d }).First());
        }

        [Fact]
        public void TestBoolParam()
        {
            Assert.False(connection.Query<bool>("select @b", new { b = false }).First());
        }

        // http://code.google.com/p/dapper-dot-net/issues/detail?id=70
        // https://connect.microsoft.com/VisualStudio/feedback/details/381934/sqlparameter-dbtype-dbtype-time-sets-the-parameter-to-sqldbtype-datetime-instead-of-sqldbtype-time

        [Fact]
        public void TestTimeSpanParam()
        {
            Assert.Equal(connection.Query<TimeSpan>("select @ts", new { ts = TimeSpan.FromMinutes(42) }).First(), TimeSpan.FromMinutes(42));
        }

        [Fact]
        public void PassInIntArray()
        {
            Assert.Equal(
                new[] { 1, 2, 3 },
                connection.Query<int>("select * from (select 1 as Id union all select 2 union all select 3) as X where Id in @Ids", new { Ids = new int[] { 1, 2, 3 }.AsEnumerable() })
            );
        }

        [Fact]
        public void PassInEmptyIntArray()
        {
            Assert.Equal(
                new int[0],
                connection.Query<int>("select * from (select 1 as Id union all select 2 union all select 3) as X where Id in @Ids", new { Ids = new int[0] })
            );
        }


        
        //[FactUnlessCaseSensitiveDatabase]
        public void TestParameterInclusionNotSensitiveToCurrentCulture()
        {
            // note this might fail if your database server is case-sensitive
            CultureInfo current = ActiveCulture;
            try
            {
                ActiveCulture = new CultureInfo("tr-TR");

                connection.Query<int>("select @pid", new { PId = 1 }).Single();
            }
            finally
            {
                ActiveCulture = current;
            }
        }

        [Fact]
        public void TestMassiveStrings()
        {
            var str = new string('X', 20000);
            Assert.Equal(connection.Query<string>("select @a", new { a = str }).First(), str);
        }


        

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

        

        [Fact]
        public void SupportInit()
        {
            var obj = connection.Query<WithInit>("select 'abc' as Value").Single();
            Assert.Equal("abc", obj.Value);
            Assert.Equal(31, obj.Flags);
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
        public void TestAppendingAList()
        {
            var p = new DynamicParameters();
            var list = new int[] { 1, 2, 3 };
            p.AddDynamicParams(new { list });

            var result = connection.Query<int>("select * from (select 1 A union all select 2 union all select 3) X where A in @list", p).ToList();

            Assert.Equal(1, result[0]);
            Assert.Equal(2, result[1]);
            Assert.Equal(3, result[2]);
        }

        [Fact]
        public void TestAppendingAListAsDictionary()
        {
            var p = new DynamicParameters();
            var list = new int[] { 1, 2, 3 };
            var args = new Dictionary<string, object> { ["ids"] = list };
            p.AddDynamicParams(args);

            var result = connection.Query<int>("select * from (select 1 A union all select 2 union all select 3) X where A in @ids", p).ToList();

            Assert.Equal(1, result[0]);
            Assert.Equal(2, result[1]);
            Assert.Equal(3, result[2]);
        }

        [Fact]
        public void TestAppendingAListByName()
        {
            DynamicParameters p = new DynamicParameters();
            var list = new int[] { 1, 2, 3 };
            p.Add("ids", list);

            var result = connection.Query<int>("select * from (select 1 A union all select 2 union all select 3) X where A in @ids", p).ToList();

            Assert.Equal(1, result[0]);
            Assert.Equal(2, result[1]);
            Assert.Equal(3, result[2]);
        }

        [Fact]
        public void ParameterizedInWithOptimizeHint()
        {
            const string sql = @"
select count(1)
from(
    select 1 as x
    union all select 2
    union all select 5) y
where y.x in @vals
option (optimize for (@vals unKnoWn))";
            int count = connection.Query<int>(sql, new { vals = new[] { 1, 2, 3, 4 } }).Single();
            Assert.Equal(2, count);

            count = connection.Query<int>(sql, new { vals = new[] { 1 } }).Single();
            Assert.Equal(1, count);

            count = connection.Query<int>(sql, new { vals = new int[0] }).Single();
            Assert.Equal(0, count);
        }



        [Fact]
        public void TestUniqueIdentifier()
        {
            var guid = Guid.NewGuid();
            var result = connection.Query<Guid>("declare @foo uniqueidentifier set @foo = @guid select @foo", new { guid }).Single();
            Assert.Equal(guid, result);
        }

        [Fact]
        public void TestNullableUniqueIdentifierNonNull()
        {
            Guid? guid = Guid.NewGuid();
            var result = connection.Query<Guid?>("declare @foo uniqueidentifier set @foo = @guid select @foo", new { guid }).Single();
            Assert.Equal(guid, result);
        }

        [Fact]
        public void TestNullableUniqueIdentifierNull()
        {
            Guid? guid = null;
            var result = connection.Query<Guid?>("declare @foo uniqueidentifier set @foo = @guid select @foo", new { guid }).Single();
            Assert.Equal(guid, result);
        }

        [Fact]
        public void TestSupportForDynamicParameters()
        {
            var p = new DynamicParameters();
            p.Add("name", "bob");
            p.Add("age", dbType: DbType.Int32, direction: ParameterDirection.Output);

            Assert.Equal("bob", connection.Query<string>("set @age = 11 select @name", p).First());
            Assert.Equal(11, p.Get<int>("age"));
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

        [Fact]
        public void TestSupportForDynamicParametersOutputExpressions_Query_Buffered()
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

                var result = connection.Query<int>(@"
SET @Occupation = 'grillmaster' 
SET @PersonId = @PersonId + 1 
SET @NumberOfLegs = @NumberOfLegs - 1
SET @AddressName = 'bobs burgers'
SET @AddressPersonId = @PersonId
select 42", p, buffered: true).Single();

                Assert.Equal("grillmaster", bob.Occupation);
                Assert.Equal(2, bob.PersonId);
                Assert.Equal(1, bob.NumberOfLegs);
                Assert.Equal("bobs burgers", bob.Address.Name);
                Assert.Equal(2, bob.Address.PersonId);
                Assert.Equal(42, result);
            }
        }

        [Fact]
        public void TestSupportForDynamicParametersOutputExpressions_Query_NonBuffered()
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

                var result = connection.Query<int>(@"
SET @Occupation = 'grillmaster' 
SET @PersonId = @PersonId + 1 
SET @NumberOfLegs = @NumberOfLegs - 1
SET @AddressName = 'bobs burgers'
SET @AddressPersonId = @PersonId
select 42", p, buffered: false).Single();

                Assert.Equal("grillmaster", bob.Occupation);
                Assert.Equal(2, bob.PersonId);
                Assert.Equal(1, bob.NumberOfLegs);
                Assert.Equal("bobs burgers", bob.Address.Name);
                Assert.Equal(2, bob.Address.PersonId);
                Assert.Equal(42, result);
            }
        }
        
        [Fact]
        public void TestSupportForExpandoObjectParameters()
        {
            dynamic p = new ExpandoObject();
            p.name = "bob";
            object parameters = p;
            string result = connection.Query<string>("select @name", parameters).First();
            Assert.Equal("bob", result);
        }



        public class HazX
        {
            public string X { get; set; }
        }

        [Fact]
        public void SO25297173_DynamicIn()
        {
            const string query = @"
declare @table table(value int not null);
insert @table values(1);
insert @table values(2);
insert @table values(3);
insert @table values(4);
insert @table values(5);
insert @table values(6);
insert @table values(7);
SELECT value FROM @table WHERE value IN @myIds";
            var queryParams = new Dictionary<string, object>
            {
                ["myIds"] = new[] { 5, 6 }
            };

            var dynamicParams = new DynamicParameters(queryParams);
            List<int> result = connection.Query<int>(query, dynamicParams).ToList();
            Assert.Equal(2, result.Count);
            Assert.Contains(5, result);
            Assert.Contains(6, result);
        }

        [Fact]
        public void Test_AddDynamicParametersRepeatedShouldWork()
        {
            var args = new DynamicParameters();
            args.AddDynamicParams(new { Foo = 123 });
            args.AddDynamicParams(new { Foo = 123 });
            int i = connection.Query<int>("select @Foo", args).Single();
            Assert.Equal(123, i);
        }

        [Fact]
        public void Test_AddDynamicParametersRepeatedIfParamTypeIsDbStiringShouldWork()
        {
            var foo = new DbString() { Value = "123" };

            var args = new DynamicParameters();
            args.AddDynamicParams(new { Foo = foo });
            args.AddDynamicParams(new { Foo = foo });
            int i = connection.Query<int>("select @Foo", args).Single();
            Assert.Equal(123, i);
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

        [Fact]
        public void TestMultipleParametersWithIndexer()
        {
            var order = connection.Query<MultipleParametersWithIndexer>("select 1 A,2 B").First();

            Assert.Equal(1, order.A);
            Assert.Equal(2, order.B);
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



  





        //[FactUnlessCaseSensitiveDatabase]
        public void Issue220_InParameterCanBeSpecifiedInAnyCase()
        {
            // note this might fail if your database server is case-sensitive
            Assert.Equal(
                new[] { 1 },
                connection.Query<int>("select * from (select 1 as Id) as X where Id in @ids", new { Ids = new[] { 1 } })
            );
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
