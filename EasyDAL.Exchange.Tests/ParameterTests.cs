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

        [Fact]
        public void TestExecuteCommandWithHybridParameters()
        {
            var p = new DynamicParameters(new { a = 1, b = 2 });
            p.Add("c", dbType: DbType.Int32, direction: ParameterDirection.Output);
            connection.Execute("set @c = @a + @b", p);
            Assert.Equal(3, p.Get<int>("@c"));
        }
        
        [FactUnlessCaseSensitiveDatabase]
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

        [Fact]
        public void TestTVPWithAnonymousObject()
        {
            try
            {
                connection.Execute("CREATE TYPE int_list_type AS TABLE (n int NOT NULL PRIMARY KEY)");
                connection.Execute("CREATE PROC get_ints @integers int_list_type READONLY AS select * from @integers");

                var nums = connection.Query<int>("get_ints", new { integers = new IntCustomParam(new int[] { 1, 2, 3 }) }, commandType: CommandType.StoredProcedure).ToList();
                Assert.Equal(1, nums[0]);
                Assert.Equal(2, nums[1]);
                Assert.Equal(3, nums[2]);
                Assert.Equal(3, nums.Count);
            }
            finally
            {
                try
                {
                    connection.Execute("DROP PROC get_ints");
                }
                finally
                {
                    connection.Execute("DROP TYPE int_list_type");
                }
            }
        }

        [Fact]
        public void TestTVPWithAnonymousEmptyObject()
        {
            try
            {
                connection.Execute("CREATE TYPE int_list_type AS TABLE (n int NOT NULL PRIMARY KEY)");
                connection.Execute("CREATE PROC get_ints @integers int_list_type READONLY AS select * from @integers");

                var nums = connection.Query<int>("get_ints", new { integers = new IntCustomParam(new int[] { }) }, commandType: CommandType.StoredProcedure).ToList();
                Assert.Equal(1, nums[0]);
                Assert.Equal(2, nums[1]);
                Assert.Equal(3, nums[2]);
                Assert.Equal(3, nums.Count);
            }
            catch (ArgumentException ex)
            {
                Assert.True(string.Compare(ex.Message, "There are no records in the SqlDataRecord enumeration. To send a table-valued parameter with no rows, use a null reference for the value instead.") == 0);
            }
            finally
            {
                try
                {
                    connection.Execute("DROP PROC get_ints");
                }
                finally
                {
                    connection.Execute("DROP TYPE int_list_type");
                }
            }
        }

        // SQL Server specific test to demonstrate TVP 
        [Fact]
        public void TestTVP()
        {
            try
            {
                connection.Execute("CREATE TYPE int_list_type AS TABLE (n int NOT NULL PRIMARY KEY)");
                connection.Execute("CREATE PROC get_ints @ints int_list_type READONLY AS select * from @ints");

                var nums = connection.Query<int>("get_ints", new IntDynamicParam(new int[] { 1, 2, 3 })).ToList();
                Assert.Equal(1, nums[0]);
                Assert.Equal(2, nums[1]);
                Assert.Equal(3, nums[2]);
                Assert.Equal(3, nums.Count);
            }
            finally
            {
                try
                {
                    connection.Execute("DROP PROC get_ints");
                }
                finally
                {
                    connection.Execute("DROP TYPE int_list_type");
                }
            }
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
        public void TestSqlDataRecordListParametersWithAsTableValuedParameter()
        {
            try
            {
                connection.Execute("CREATE TYPE int_list_type AS TABLE (n int NOT NULL PRIMARY KEY)");
                connection.Execute("CREATE PROC get_ints @integers int_list_type READONLY AS select * from @integers");

                var records = CreateSqlDataRecordList(new int[] { 1, 2, 3 });

                var nums = connection.Query<int>("get_ints", new { integers = records.AsTableValuedParameter() }, commandType: CommandType.StoredProcedure).ToList();
                Assert.Equal(new int[] { 1, 2, 3 }, nums);

                nums = connection.Query<int>("select * from @integers", new { integers = records.AsTableValuedParameter("int_list_type") }).ToList();
                Assert.Equal(new int[] { 1, 2, 3 }, nums);

                try
                {
                    connection.Query<int>("select * from @integers", new { integers = records.AsTableValuedParameter() }).First();
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    ex.Message.Equals("The table type parameter 'ids' must have a valid type name.");
                }
            }
            finally
            {
                try
                {
                    connection.Execute("DROP PROC get_ints");
                }
                finally
                {
                    connection.Execute("DROP TYPE int_list_type");
                }
            }
        }

        [Fact]
        public void TestEmptySqlDataRecordListParametersWithAsTableValuedParameter()
        {
            try
            {
                connection.Execute("CREATE TYPE int_list_type AS TABLE (n int NOT NULL PRIMARY KEY)");
                connection.Execute("CREATE PROC get_ints @integers int_list_type READONLY AS select * from @integers");


                var emptyRecord = CreateSqlDataRecordList(Enumerable.Empty<int>());

                var nums = connection.Query<int>("get_ints", new { integers = emptyRecord.AsTableValuedParameter() }, commandType: CommandType.StoredProcedure).ToList();
                Assert.True(nums.Count == 0);
            }
            finally
            {
                try
                {
                    connection.Execute("DROP PROC get_ints");
                }
                finally
                {
                    connection.Execute("DROP TYPE int_list_type");
                }
            }
        }

        [Fact]
        public void TestSqlDataRecordListParametersWithTypeHandlers()
        {
            try
            {
                connection.Execute("CREATE TYPE int_list_type AS TABLE (n int NOT NULL PRIMARY KEY)");
                connection.Execute("CREATE PROC get_ints @integers int_list_type READONLY AS select * from @integers");

                // Variable type has to be IEnumerable<SqlDataRecord> for TypeHandler to kick in.
                IEnumerable<Microsoft.SqlServer.Server.SqlDataRecord> records = CreateSqlDataRecordList(new int[] { 1, 2, 3 });

                var nums = connection.Query<int>("get_ints", new { integers = records }, commandType: CommandType.StoredProcedure).ToList();
                Assert.Equal(new int[] { 1, 2, 3 }, nums);

                try
                {
                    connection.Query<int>("select * from @integers", new { integers = records }).First();
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    ex.Message.Equals("The table type parameter 'ids' must have a valid type name.");
                }
            }
            finally
            {
                try
                {
                    connection.Execute("DROP PROC get_ints");
                }
                finally
                {
                    connection.Execute("DROP TYPE int_list_type");
                }
            }
        }

#if !NETCOREAPP1_0
        [Fact]
        public void DataTableParameters()
        {
            try { connection.Execute("drop proc #DataTableParameters"); }
            catch { /* don't care */ }
            try { connection.Execute("drop table #DataTableParameters"); }
            catch { /* don't care */ }
            try { connection.Execute("drop type MyTVPType"); }
            catch { /* don't care */ }
            connection.Execute("create type MyTVPType as table (id int)");
            connection.Execute("create proc #DataTableParameters @ids MyTVPType readonly as select count(1) from @ids");

            var table = new DataTable { Columns = { { "id", typeof(int) } }, Rows = { { 1 }, { 2 }, { 3 } } };

            int count = connection.Query<int>("#DataTableParameters", new { ids = table.AsTableValuedParameter() }, commandType: CommandType.StoredProcedure).First();
            Assert.Equal(3, count);

            count = connection.Query<int>("select count(1) from @ids", new { ids = table.AsTableValuedParameter("MyTVPType") }).First();
            Assert.Equal(3, count);

            try
            {
                connection.Query<int>("select count(1) from @ids", new { ids = table.AsTableValuedParameter() }).First();
                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                ex.Message.Equals("The table type parameter 'ids' must have a valid type name.");
            }
        }

        [Fact]
        public void SO29533765_DataTableParametersViaDynamicParameters()
        {
            try { connection.Execute("drop proc #DataTableParameters"); } catch { /* don't care */ }
            try { connection.Execute("drop table #DataTableParameters"); } catch { /* don't care */ }
            try { connection.Execute("drop type MyTVPType"); } catch { /* don't care */ }
            connection.Execute("create type MyTVPType as table (id int)");
            connection.Execute("create proc #DataTableParameters @ids MyTVPType readonly as select count(1) from @ids");

            var table = new DataTable { TableName = "MyTVPType", Columns = { { "id", typeof(int) } }, Rows = { { 1 }, { 2 }, { 3 } } };
            table.SetTypeName(table.TableName); // per SO29533765
            IDictionary<string, object> args = new Dictionary<string, object>
            {
                ["ids"] = table
            };
            int count = connection.Query<int>("#DataTableParameters", args, commandType: CommandType.StoredProcedure).First();
            Assert.Equal(3, count);

            count = connection.Query<int>("select count(1) from @ids", args).First();
            Assert.Equal(3, count);
        }

        [Fact]
        public void SO26468710_InWithTVPs()
        {
            // this is just to make it re-runnable; normally you only do this once
            try { connection.Execute("drop type MyIdList"); }
            catch { /* don't care */ }
            connection.Execute("create type MyIdList as table(id int);");

            var ids = new DataTable
            {
                Columns = { { "id", typeof(int) } },
                Rows = { { 1 }, { 3 }, { 5 } }
            };
            ids.SetTypeName("MyIdList");
            int sum = connection.Query<int>(@"
            declare @tmp table(id int not null);
            insert @tmp (id) values(1), (2), (3), (4), (5), (6), (7);
            select * from @tmp t inner join @ids i on i.id = t.id", new { ids }).Sum();
            Assert.Equal(9, sum);
        }

        [Fact]
        public void DataTableParametersWithExtendedProperty()
        {
            try { connection.Execute("drop proc #DataTableParameters"); }
            catch { /* don't care */ }
            try { connection.Execute("drop table #DataTableParameters"); }
            catch { /* don't care */ }
            try { connection.Execute("drop type MyTVPType"); }
            catch { /* don't care */ }
            connection.Execute("create type MyTVPType as table (id int)");
            connection.Execute("create proc #DataTableParameters @ids MyTVPType readonly as select count(1) from @ids");

            var table = new DataTable { Columns = { { "id", typeof(int) } }, Rows = { { 1 }, { 2 }, { 3 } } };
            table.SetTypeName("MyTVPType"); // <== extended metadata
            int count = connection.Query<int>("#DataTableParameters", new { ids = table }, commandType: CommandType.StoredProcedure).First();
            Assert.Equal(3, count);

            count = connection.Query<int>("select count(1) from @ids", new { ids = table }).First();
            Assert.Equal(3, count);

            try
            {
                connection.Query<int>("select count(1) from @ids", new { ids = table }).First();
                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                ex.Message.Equals("The table type parameter 'ids' must have a valid type name.");
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

        [Fact]
        public void SO29596645_TvpProperty()
        {
            try { connection.Execute("CREATE TYPE SO29596645_ReminderRuleType AS TABLE (id int NOT NULL)"); }
            catch { /* don't care */ }
            connection.Execute(@"create proc #SO29596645_Proc (@Id int, @Rules SO29596645_ReminderRuleType READONLY)
                                as begin select @Id + ISNULL((select sum(id) from @Rules), 0); end");
            var obj = new SO29596645_OrganisationDTO();
            int val = connection.Query<int>("#SO29596645_Proc", obj.Rules, commandType: CommandType.StoredProcedure).Single();

            // 4 + 9 + 7 = 20
            Assert.Equal(20, val);
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
#endif


        
        [Fact]
        public void TestDynamicParamNullSupport()
        {
            var p = new DynamicParameters();

            p.Add("@b", dbType: DbType.Int32, direction: ParameterDirection.Output);
            connection.Execute("select @b = null", p);

            Assert.Null(p.Get<int?>("@b"));
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
        public void TestProcedureWithTimeParameter()
        {
            var p = new DynamicParameters();
            p.Add("a", TimeSpan.FromHours(10), dbType: DbType.Time);

            connection.Execute(@"CREATE PROCEDURE #TestProcWithTimeParameter
    @a TIME
    AS 
    BEGIN
    SELECT @a
    END");
            Assert.Equal(connection.Query<TimeSpan>("#TestProcWithTimeParameter", p, commandType: CommandType.StoredProcedure).First(), new TimeSpan(10, 0, 0));
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
        public void TestSupportForDynamicParametersOutputExpressions()
        {
            var bob = new Person { Name = "bob", PersonId = 1, Address = new Address { PersonId = 2 } };

            var p = new DynamicParameters(bob);
            p.Output(bob, b => b.PersonId);
            p.Output(bob, b => b.Occupation);
            p.Output(bob, b => b.NumberOfLegs);
            p.Output(bob, b => b.Address.Name);
            p.Output(bob, b => b.Address.PersonId);

            connection.Execute(@"
SET @Occupation = 'grillmaster' 
SET @PersonId = @PersonId + 1 
SET @NumberOfLegs = @NumberOfLegs - 1
SET @AddressName = 'bobs burgers'
SET @AddressPersonId = @PersonId", p);

            Assert.Equal("grillmaster", bob.Occupation);
            Assert.Equal(2, bob.PersonId);
            Assert.Equal(1, bob.NumberOfLegs);
            Assert.Equal("bobs burgers", bob.Address.Name);
            Assert.Equal(2, bob.Address.PersonId);
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

        [Fact]
        public void SO25069578_DynamicParams_Procs()
        {
            var parameters = new DynamicParameters();
            parameters.Add("foo", "bar");
            // parameters = new DynamicParameters(parameters);
            try { connection.Execute("drop proc SO25069578"); }
            catch { /* don't care */ }
            connection.Execute("create proc SO25069578 @foo nvarchar(max) as select @foo as [X]");
            var tran = connection.BeginTransaction(); // gist used transaction; behaves the same either way, though
            var row = connection.Query<HazX>("SO25069578", parameters,
                commandType: CommandType.StoredProcedure, transaction: tran).Single();
            tran.Rollback();
            Assert.Equal("bar", row.X);
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



        [Fact]
        public void TestParameterWithIndexer()
        {
            connection.Execute(@"create proc #TestProcWithIndexer 
	@A int
as 
begin
	select @A
end");
            var item = connection.Query<int>("#TestProcWithIndexer", new ParameterWithIndexer(), commandType: CommandType.StoredProcedure).Single();
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

        [Fact]
        public void Issue182_BindDynamicObjectParametersAndColumns()
        {
            connection.Execute("create table #Dyno ([Id] uniqueidentifier primary key, [Name] nvarchar(50) not null, [Foo] bigint not null);");

            var guid = Guid.NewGuid();
            var orig = new Dyno { Name = "T Rex", Id = guid, Foo = 123L };
            var result = connection.Execute("insert into #Dyno ([Id], [Name], [Foo]) values (@Id, @Name, @Foo);", orig);

            var fromDb = connection.Query<Dyno>("select * from #Dyno where Id=@Id", orig).Single();
            Assert.Equal((Guid)fromDb.Id, guid);
            Assert.Equal("T Rex", fromDb.Name);
            Assert.Equal(123L, (long)fromDb.Foo);
        }

        public class Dyno
        {
            public dynamic Id { get; set; }
            public string Name { get; set; }

            public object Foo { get; set; }
        }



  





        [FactUnlessCaseSensitiveDatabase]
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

        [Fact]
        public void RunAllStringSplitTestsDisabled()
        {
            RunAllStringSplitTests(-1, 1500);
        }

        [FactRequiredCompatibilityLevel(FactRequiredCompatibilityLevelAttribute.SqlServer2016)]
        public void RunAllStringSplitTestsEnabled()
        {
            RunAllStringSplitTests(10, 4500);
        }

        private void RunAllStringSplitTests(int stringSplit, int max = 150)
        {
            int oldVal = Settings.InListStringSplitCount;
            try
            {
                Settings.InListStringSplitCount = stringSplit;
                try { connection.Execute("drop table #splits"); } catch { /* don't care */ }
                int count = connection.QuerySingle<int>("create table #splits (i int not null);"
                    + string.Concat(Enumerable.Range(-max, max * 3).Select(i => $"insert #splits (i) values ({i});"))
                    + "select count(1) from #splits");
                Assert.Equal(count, 3 * max);

                for (int i = 0; i < max; Incr(ref i))
                {
                    try
                    {
                        var vals = Enumerable.Range(1, i);
                        var list = connection.Query<int>("select i from #splits where i in @vals", new { vals }).AsList();
                        Assert.Equal(list.Count, i);
                        Assert.Equal(list.Sum(), vals.Sum());
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException($"Error when i={i}: {ex.Message}", ex);
                    }
                }
            }
            finally
            {
                Settings.InListStringSplitCount = oldVal;
            }
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
