using System.Linq;
using System.Data;
using System.Diagnostics;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SqlClient;
using Xunit;
using EasyDAL.Exchange.DynamicParameter;
using EasyDAL.Exchange.Reader;
using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.MapperX;
using EasyDAL.Exchange;
using EasyDAL.Exchange.Tests.Entities;

namespace EasyDAL.Exchange.Tests
{
    public class Tests : TestBase
    {
        private SqlConnection _marsConnection;
        //private SqlConnection MarsConnection => _marsConnection ?? (_marsConnection = GetOpenConnection(true));

        [Fact]
        public async Task TestBasicStringUsageAsync()
        {
            var query = await connection.QueryAsync<string>("select 'abc' as [Value] union all select @txt", new { txt = "def" }).ConfigureAwait(false);
            var arr = query.ToArray();
            Assert.Equal(new[] { "abc", "def" }, arr);
        }

        [Fact]
        public async Task TestBasicStringUsageQueryFirstAsync()
        {
            var str = await connection.QueryFirstAsync<string>(new CommandDefinition("select 'abc' as [Value] union all select @txt", new { txt = "def" })).ConfigureAwait(false);
            Assert.Equal("abc", str);
        }



        [Fact]
        public async Task TestBasicStringUsageQueryFirstOrDefaultAsync()
        {
            var str = await connection.QueryFirstOrDefaultAsync<string>(new CommandDefinition("select null as [Value] union all select @txt", new { txt = "def" })).ConfigureAwait(false);
            Assert.Null(str);
        }



        [Fact]
        public async Task TestBasicStringUsageQuerySingleAsyncDynamic()
        {
            var str = await connection.QuerySingleAsync<string>(new CommandDefinition("select 'abc' as [Value]")).ConfigureAwait(false);
            Assert.Equal("abc", str);
        }


        [Fact]
        public async Task TestBasicStringUsageQuerySingleOrDefaultAsync()
        {
            var str = await connection.QuerySingleOrDefaultAsync<string>(new CommandDefinition("select null as [Value]")).ConfigureAwait(false);
            Assert.Null(str);
        }



        [Fact]
        public async Task TestBasicStringUsageAsyncNonBuffered()
        {
            var query = await connection.QueryAsync<string>(new CommandDefinition("select 'abc' as [Value] union all select @txt", new { txt = "def" }, flags: CommandFlags.None)).ConfigureAwait(false);
            var arr = query.ToArray();
            Assert.Equal(new[] { "abc", "def" }, arr);
        }

        [Fact]
        public void TestLongOperationWithCancellation()
        {
            CancellationTokenSource cancel = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            var task = connection.QueryAsync<int>(new CommandDefinition("waitfor delay '00:00:10';select 1", cancellationToken: cancel.Token));
            try
            {
                if (!task.Wait(TimeSpan.FromSeconds(7)))
                {
                    throw new TimeoutException(); // should have cancelled
                }
            }
            catch (AggregateException agg)
            {
                Assert.True(agg.InnerException is SqlException);
            }
        }

        [Fact]
        public async Task TestBasicStringUsageClosedAsync()
        {
            var query = await connection.QueryAsync<string>("select 'abc' as [Value] union all select @txt", new { txt = "def" }).ConfigureAwait(false);
            var arr = query.ToArray();
            Assert.Equal(new[] { "abc", "def" }, arr);
        }



        [Fact]
        public async Task TestClassWithStringUsageAsync()
        {
            var query = await connection.QueryAsync<BasicType>("select 'abc' as [Value] union all select @txt", new { txt = "def" }).ConfigureAwait(false);
            var arr = query.ToArray();
            Assert.Equal(new[] { "abc", "def" }, arr.Select(x => x.Value));
        }

        [Fact]
        public async Task TestExecuteAsync()
        {
            var val = await connection.ExecuteAsync("declare @foo table(id int not null); insert @foo values(@id);", new { id = 1 }).ConfigureAwait(false);
            Assert.Equal(1, val);
        }

        [Fact]
        public void TestExecuteClosedConnAsyncInner()
        {
            var query = connection.ExecuteAsync("declare @foo table(id int not null); insert @foo values(@id);", new { id = 1 });
            var val = query.Result;
            Assert.Equal(1, val);
        }
        
        [Fact]
        public async Task TestMultiAsync()
        {
            using (GridReader multi = await connection.QueryMultipleAsync("select 1; select 2").ConfigureAwait(false))
            {
                Assert.Equal(1, multi.ReadAsync<int>().Result.Single());
                Assert.Equal(2, multi.ReadAsync<int>().Result.Single());
            }
        }

        [Fact]
        public async Task TestMultiAsyncViaFirstOrDefault()
        {
            using (GridReader multi = await connection.QueryMultipleAsync("select 1; select 2; select 3; select 4; select 5").ConfigureAwait(false))
            {
                Assert.Equal(1, multi.ReadFirstOrDefaultAsync<int>().Result);
                Assert.Equal(2, multi.ReadAsync<int>().Result.Single());
                Assert.Equal(3, multi.ReadFirstOrDefaultAsync<int>().Result);
                Assert.Equal(4, multi.ReadAsync<int>().Result.Single());
                Assert.Equal(5, multi.ReadFirstOrDefaultAsync<int>().Result);
            }
        }

        [Fact]
        public async Task TestMultiClosedConnAsync()
        {
            using (GridReader multi = await connection.QueryMultipleAsync("select 1; select 2").ConfigureAwait(false))
            {
                Assert.Equal(1, multi.ReadAsync<int>().Result.Single());
                Assert.Equal(2, multi.ReadAsync<int>().Result.Single());
            }
        }

        [Fact]
        public async Task TestMultiClosedConnAsyncViaFirstOrDefault()
        {
            using (GridReader multi = await connection.QueryMultipleAsync("select 1; select 2; select 3; select 4; select 5;").ConfigureAwait(false))
            {
                Assert.Equal(1, multi.ReadFirstOrDefaultAsync<int>().Result);
                Assert.Equal(2, multi.ReadAsync<int>().Result.Single());
                Assert.Equal(3, multi.ReadFirstOrDefaultAsync<int>().Result);
                Assert.Equal(4, multi.ReadAsync<int>().Result.Single());
                Assert.Equal(5, multi.ReadFirstOrDefaultAsync<int>().Result);
            }
        }



        [Fact]
        public async Task LiteralReplacementOpen()
        {
            await LiteralReplacement(connection).ConfigureAwait(false);
        }

        [Fact]
        public async Task LiteralReplacementClosed()
        {
            using (var conn = GetClosedConnection()) await LiteralReplacement(conn).ConfigureAwait(false);
        }

        private async Task LiteralReplacement(IDbConnection conn)
        {
            try
            {
                await conn.ExecuteAsync("drop table literal1").ConfigureAwait(false);
            }
            catch { /* don't care */ }
            await conn.ExecuteAsync("create table literal1 (id int not null, foo int not null)").ConfigureAwait(false);
            await conn.ExecuteAsync("insert literal1 (id,foo) values ({=id}, @foo)", new { id = 123, foo = 456 }).ConfigureAwait(false);
            var rows = new[] { new { id = 1, foo = 2 }, new { id = 3, foo = 4 } };
            await conn.ExecuteAsync("insert literal1 (id,foo) values ({=id}, @foo)", rows).ConfigureAwait(false);
            var count = (await conn.QueryAsync<int>("select count(1) from literal1 where id={=foo}", new { foo = 123 }).ConfigureAwait(false)).Single();
            Assert.Equal(1, count);
            int sum = (await conn.QueryAsync<int>("select sum(id) + sum(foo) from literal1").ConfigureAwait(false)).Single();
            Assert.Equal(sum, 123 + 456 + 1 + 2 + 3 + 4);
        }

        [Fact]
        public async Task LiteralReplacementDynamicOpen()
        {
            await LiteralReplacementDynamic(connection).ConfigureAwait(false);
        }

        [Fact]
        public async Task LiteralReplacementDynamicClosed()
        {
            using (var conn = GetClosedConnection()) await LiteralReplacementDynamic(conn).ConfigureAwait(false);
        }

        private async Task LiteralReplacementDynamic(IDbConnection conn)
        {
            var args = new DynamicParameters();
            args.Add("id", 123);
            try { await conn.ExecuteAsync("drop table literal2").ConfigureAwait(false); }
            catch { /* don't care */ }
            await conn.ExecuteAsync("create table literal2 (id int not null)").ConfigureAwait(false);
            await conn.ExecuteAsync("insert literal2 (id) values ({=id})", args).ConfigureAwait(false);

            args = new DynamicParameters();
            args.Add("foo", 123);
            var count = (await conn.QueryAsync<int>("select count(1) from literal2 where id={=foo}", args).ConfigureAwait(false)).Single();
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task LiteralInAsync()
        {
            await connection.ExecuteAsync("create table #literalin(id int not null);").ConfigureAwait(false);
            await connection.ExecuteAsync("insert #literalin (id) values (@id)", new[] {
                new { id = 1 },
                new { id = 2 },
                new { id = 3 },
            }).ConfigureAwait(false);
            var count = (await connection.QueryAsync<int>("select count(1) from #literalin where id in {=ids}",
                new { ids = new[] { 1, 3, 4 } }).ConfigureAwait(false)).Single();
            Assert.Equal(2, count);
        }



        //[FactLongRunning]
        //public void RunSequentialVersusParallelSync()
        //{
        //    var ids = Enumerable.Range(1, 20000).Select(id => new { id }).ToArray();
        //    MarsConnection.Execute(new CommandDefinition("select @id", ids.Take(5), flags: CommandFlags.None));

        //    var watch = Stopwatch.StartNew();
        //    MarsConnection.Execute(new CommandDefinition("select @id", ids, flags: CommandFlags.None));
        //    watch.Stop();
        //    Console.WriteLine("No pipeline: {0}ms", watch.ElapsedMilliseconds);

        //    watch = Stopwatch.StartNew();
        //    MarsConnection.Execute(new CommandDefinition("select @id", ids, flags: CommandFlags.Pipelined));
        //    watch.Stop();
        //    Console.WriteLine("Pipeline: {0}ms", watch.ElapsedMilliseconds);
        //}

        [Collection(NonParallelDefinition.Name)]
        public class AsyncQueryCacheTests : TestBase
        {
            private SqlConnection _marsConnection;
            //private SqlConnection MarsConnection => _marsConnection ?? (_marsConnection = GetOpenConnection(true));


        }

        private class BasicType
        {
            public string Value { get; set; }
        }
        
        [Fact]
        public async Task Issue22_ExecuteScalarAsync()
        {
            int i = await connection.ExecuteScalarAsync<int>("select 123").ConfigureAwait(false);
            Assert.Equal(123, i);

            i = await connection.ExecuteScalarAsync<int>("select cast(123 as bigint)").ConfigureAwait(false);
            Assert.Equal(123, i);

            long j = await connection.ExecuteScalarAsync<long>("select 123").ConfigureAwait(false);
            Assert.Equal(123L, j);

            j = await connection.ExecuteScalarAsync<long>("select cast(123 as bigint)").ConfigureAwait(false);
            Assert.Equal(123L, j);

            int? k = await connection.ExecuteScalarAsync<int?>("select @i", new { i = default(int?) }).ConfigureAwait(false);
            Assert.Null(k);
        }

        /***************************************************************************************************************/

        [Fact]
        public async Task CreateAsyncTest()
        {
            var conn = GetOpenConnection();
            var m = new BodyFitRecord
            {
                Id = Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"),
                CreatedOn = DateTime.Now,
                UserId = Guid.NewGuid(),
                BodyMeasureProperty = "{xxx:yyy,mmm:nnn}"
            };
            var res = await conn.Creater().CreateAsync(m);

            var xx = "";
        }


        [Fact]
        public async Task UpdateAsyncTest()
        {
            var conn = GetOpenConnection();
            // 
            //
            var m = new BodyFitRecord
            {
                Id = Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"),
                CreatedOn = DateTime.Now,
                UserId = Guid.NewGuid(),
                BodyMeasureProperty = "{xxx:yyy,mmm:nnn,zzz:aaa}"
            };

            var res = await conn
                .Updater<BodyFitRecord>()
                .Set(it => it.CreatedOn)
                .Set(it => it.BodyMeasureProperty)
                .Where(it=>it.Id)
                .UpdateAsync(m);                

            var xx = "";
        }


        /****************************************************************************/


        [Fact]
        public async Task Issue346_QueryAsyncConvert()
        {
            int i = (await connection.QueryAsync<int>("Select Cast(123 as bigint)").ConfigureAwait(false)).First();
            Assert.Equal(123, i);
        }

        [Fact]
        public async Task TestSupportForDynamicParametersOutputExpressionsAsync()
        {
            {
                var bob = new Person { Name = "bob", PersonId = 1, Address = new Address { PersonId = 2 } };

                var p = new DynamicParameters(bob);
                p.Output(bob, b => b.PersonId);
                p.Output(bob, b => b.Occupation);
                p.Output(bob, b => b.NumberOfLegs);
                p.Output(bob, b => b.Address.Name);
                p.Output(bob, b => b.Address.PersonId);

                await connection.ExecuteAsync(@"
SET @Occupation = 'grillmaster' 
SET @PersonId = @PersonId + 1 
SET @NumberOfLegs = @NumberOfLegs - 1
SET @AddressName = 'bobs burgers'
SET @AddressPersonId = @PersonId", p).ConfigureAwait(false);

                Assert.Equal("grillmaster", bob.Occupation);
                Assert.Equal(2, bob.PersonId);
                Assert.Equal(1, bob.NumberOfLegs);
                Assert.Equal("bobs burgers", bob.Address.Name);
                Assert.Equal(2, bob.Address.PersonId);
            }
        }

        [Fact]
        public async Task TestSupportForDynamicParametersOutputExpressions_ScalarAsync()
        {
            var bob = new Person { Name = "bob", PersonId = 1, Address = new Address { PersonId = 2 } };

            var p = new DynamicParameters(bob);
            p.Output(bob, b => b.PersonId);
            p.Output(bob, b => b.Occupation);
            p.Output(bob, b => b.NumberOfLegs);
            p.Output(bob, b => b.Address.Name);
            p.Output(bob, b => b.Address.PersonId);

            var result = (int)(await connection.ExecuteScalarAsync(@"
SET @Occupation = 'grillmaster' 
SET @PersonId = @PersonId + 1 
SET @NumberOfLegs = @NumberOfLegs - 1
SET @AddressName = 'bobs burgers'
SET @AddressPersonId = @PersonId
select 42", p).ConfigureAwait(false));

            Assert.Equal("grillmaster", bob.Occupation);
            Assert.Equal(2, bob.PersonId);
            Assert.Equal(1, bob.NumberOfLegs);
            Assert.Equal("bobs burgers", bob.Address.Name);
            Assert.Equal(2, bob.Address.PersonId);
            Assert.Equal(42, result);
        }

        [Fact]
        public async Task TestSupportForDynamicParametersOutputExpressions_Query_Default()
        {
            var bob = new Person { Name = "bob", PersonId = 1, Address = new Address { PersonId = 2 } };

            var p = new DynamicParameters(bob);
            p.Output(bob, b => b.PersonId);
            p.Output(bob, b => b.Occupation);
            p.Output(bob, b => b.NumberOfLegs);
            p.Output(bob, b => b.Address.Name);
            p.Output(bob, b => b.Address.PersonId);

            var result = (await connection.QueryAsync<int>(@"
SET @Occupation = 'grillmaster' 
SET @PersonId = @PersonId + 1 
SET @NumberOfLegs = @NumberOfLegs - 1
SET @AddressName = 'bobs burgers'
SET @AddressPersonId = @PersonId
select 42", p).ConfigureAwait(false)).Single();

            Assert.Equal("grillmaster", bob.Occupation);
            Assert.Equal(2, bob.PersonId);
            Assert.Equal(1, bob.NumberOfLegs);
            Assert.Equal("bobs burgers", bob.Address.Name);
            Assert.Equal(2, bob.Address.PersonId);
            Assert.Equal(42, result);
        }

        [Fact]
        public async Task TestSupportForDynamicParametersOutputExpressions_Query_BufferedAsync()
        {
            var bob = new Person { Name = "bob", PersonId = 1, Address = new Address { PersonId = 2 } };

            var p = new DynamicParameters(bob);
            p.Output(bob, b => b.PersonId);
            p.Output(bob, b => b.Occupation);
            p.Output(bob, b => b.NumberOfLegs);
            p.Output(bob, b => b.Address.Name);
            p.Output(bob, b => b.Address.PersonId);

            var result = (await connection.QueryAsync<int>(new CommandDefinition(@"
SET @Occupation = 'grillmaster' 
SET @PersonId = @PersonId + 1 
SET @NumberOfLegs = @NumberOfLegs - 1
SET @AddressName = 'bobs burgers'
SET @AddressPersonId = @PersonId
select 42", p, flags: CommandFlags.Buffered)).ConfigureAwait(false)).Single();

            Assert.Equal("grillmaster", bob.Occupation);
            Assert.Equal(2, bob.PersonId);
            Assert.Equal(1, bob.NumberOfLegs);
            Assert.Equal("bobs burgers", bob.Address.Name);
            Assert.Equal(2, bob.Address.PersonId);
            Assert.Equal(42, result);
        }

        [Fact]
        public async Task TestSupportForDynamicParametersOutputExpressions_Query_NonBufferedAsync()
        {
            var bob = new Person { Name = "bob", PersonId = 1, Address = new Address { PersonId = 2 } };

            var p = new DynamicParameters(bob);
            p.Output(bob, b => b.PersonId);
            p.Output(bob, b => b.Occupation);
            p.Output(bob, b => b.NumberOfLegs);
            p.Output(bob, b => b.Address.Name);
            p.Output(bob, b => b.Address.PersonId);

            var result = (await connection.QueryAsync<int>(new CommandDefinition(@"
SET @Occupation = 'grillmaster' 
SET @PersonId = @PersonId + 1 
SET @NumberOfLegs = @NumberOfLegs - 1
SET @AddressName = 'bobs burgers'
SET @AddressPersonId = @PersonId
select 42", p, flags: CommandFlags.None)).ConfigureAwait(false)).Single();

            Assert.Equal("grillmaster", bob.Occupation);
            Assert.Equal(2, bob.PersonId);
            Assert.Equal(1, bob.NumberOfLegs);
            Assert.Equal("bobs burgers", bob.Address.Name);
            Assert.Equal(2, bob.Address.PersonId);
            Assert.Equal(42, result);
        }

        [Fact]
        public async Task TestSupportForDynamicParametersOutputExpressions_QueryMultipleAsync()
        {
            var bob = new Person { Name = "bob", PersonId = 1, Address = new Address { PersonId = 2 } };

            var p = new DynamicParameters(bob);
            p.Output(bob, b => b.PersonId);
            p.Output(bob, b => b.Occupation);
            p.Output(bob, b => b.NumberOfLegs);
            p.Output(bob, b => b.Address.Name);
            p.Output(bob, b => b.Address.PersonId);

            int x, y;
            using (var multi = await connection.QueryMultipleAsync(@"
SET @Occupation = 'grillmaster' 
SET @PersonId = @PersonId + 1 
SET @NumberOfLegs = @NumberOfLegs - 1
SET @AddressName = 'bobs burgers'
select 42
select 17
SET @AddressPersonId = @PersonId", p).ConfigureAwait(false))
            {
                x = multi.ReadAsync<int>().Result.Single();
                y = multi.ReadAsync<int>().Result.Single();
            }

            Assert.Equal("grillmaster", bob.Occupation);
            Assert.Equal(2, bob.PersonId);
            Assert.Equal(1, bob.NumberOfLegs);
            Assert.Equal("bobs burgers", bob.Address.Name);
            Assert.Equal(2, bob.Address.PersonId);
            Assert.Equal(42, x);
            Assert.Equal(17, y);
        }

        [Fact]
        public async Task TestSubsequentQueriesSuccessAsync()
        {
            var data0 = (await connection.QueryAsync<AsyncFoo0>("select 1 as [Id] where 1 = 0").ConfigureAwait(false)).ToList();
            Assert.Empty(data0);

            var data1 = (await connection.QueryAsync<AsyncFoo1>(new CommandDefinition("select 1 as [Id] where 1 = 0", flags: CommandFlags.Buffered)).ConfigureAwait(false)).ToList();
            Assert.Empty(data1);

            var data2 = (await connection.QueryAsync<AsyncFoo2>(new CommandDefinition("select 1 as [Id] where 1 = 0", flags: CommandFlags.None)).ConfigureAwait(false)).ToList();
            Assert.Empty(data2);

            data0 = (await connection.QueryAsync<AsyncFoo0>("select 1 as [Id] where 1 = 0").ConfigureAwait(false)).ToList();
            Assert.Empty(data0);

            data1 = (await connection.QueryAsync<AsyncFoo1>(new CommandDefinition("select 1 as [Id] where 1 = 0", flags: CommandFlags.Buffered)).ConfigureAwait(false)).ToList();
            Assert.Empty(data1);

            data2 = (await connection.QueryAsync<AsyncFoo2>(new CommandDefinition("select 1 as [Id] where 1 = 0", flags: CommandFlags.None)).ConfigureAwait(false)).ToList();
            Assert.Empty(data2);
        }

        private class AsyncFoo0 { public int Id { get; set; } }

        private class AsyncFoo1 { public int Id { get; set; } }

        private class AsyncFoo2 { public int Id { get; set; } }

        [Fact]
        public async Task TestSchemaChangedViaFirstOrDefaultAsync()
        {
            await connection.ExecuteAsync("create table #dog(Age int, Name nvarchar(max)) insert #dog values(1, 'Alf')").ConfigureAwait(false);
            try
            {
                var d = await connection.QueryFirstOrDefaultAsync<Dog>("select * from #dog").ConfigureAwait(false);
                Assert.Equal("Alf", d.Name);
                Assert.Equal(1, d.Age);
                connection.Execute("alter table #dog drop column Name");
                d = await connection.QueryFirstOrDefaultAsync<Dog>("select * from #dog").ConfigureAwait(false);
                Assert.Null(d.Name);
                Assert.Equal(1, d.Age);
            }
            finally
            {
                await connection.ExecuteAsync("drop table #dog").ConfigureAwait(false);
            }
        }



        [Fact]
        public async Task Issue157_ClosedReaderAsync()
        {
            var args = new { x = 42 };
            const string sql = "select 123 as [A], 'abc' as [B] where @x=42";
            var row = (await connection.QueryAsync<SomeType>(new CommandDefinition(
                sql, args, flags: CommandFlags.None)).ConfigureAwait(false)).Single();
            Assert.NotNull(row);
            Assert.Equal(123, row.A);
            Assert.Equal("abc", row.B);

            args = new { x = 5 };
            Assert.False((await connection.QueryAsync<SomeType>(new CommandDefinition(sql, args, flags: CommandFlags.None)).ConfigureAwait(false)).Any());
        }

        [Fact]
        public async Task TestAtEscaping()
        {
            var id = (await connection.QueryAsync<int>(@"
                declare @@Name int
                select @@Name = @Id+1
                select @@Name
                ", new Product { Id = 1 }).ConfigureAwait(false)).Single();
            Assert.Equal(2, id);
        }


        [Fact]
        public async Task Issue563_QueryAsyncShouldThrowException()
        {
            try
            {
                var data = (await connection.QueryAsync<int>("select 1 union all select 2; RAISERROR('after select', 16, 1);").ConfigureAwait(false)).ToList();
                Assert.True(false, "Expected Exception");
            }
            catch (SqlException ex) when (ex.Message == "after select") { /* swallow only this */ }
        }
    }
}
