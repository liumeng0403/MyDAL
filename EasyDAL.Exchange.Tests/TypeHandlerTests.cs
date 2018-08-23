using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.Handler;
using EasyDAL.Exchange.Map;
using EasyDAL.Exchange.MapperX;
using Xunit;

namespace EasyDAL.Exchange.Tests
{
    [Collection(NonParallelDefinition.Name)]
    public class TypeHandlerTests : TestBase
    {
        [Fact]
        public void TestChangingDefaultStringTypeMappingToAnsiString()
        {
            const string sql = "SELECT SQL_VARIANT_PROPERTY(CONVERT(sql_variant, @testParam),'BaseType') AS BaseType";
            var param = new { testParam = "TestString" };

            var result01 = connection.Query<string>(sql, param).FirstOrDefault();
            Assert.Equal("nvarchar", result01);

            SqlMapper.PurgeQueryCache();

            SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);   // Change Default String Handling to AnsiString
            var result02 = connection.Query<string>(sql, param).FirstOrDefault();
            Assert.Equal("varchar", result02);

            SqlMapper.PurgeQueryCache();
            SqlMapper.AddTypeMap(typeof(string), DbType.String);  // Restore Default to Unicode String
        }

 
        [Fact]
        public void TestCustomTypeMap()
        {
            // default mapping
            var item = connection.Query<TypeWithMapping>("Select 'AVal' as A, 'BVal' as B").Single();
            Assert.Equal("AVal", item.A);
            Assert.Equal("BVal", item.B);

            // custom mapping
            var map = new CustomPropertyTypeMap(typeof(TypeWithMapping),
                (type, columnName) => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName));
            SqlMapper.SetTypeMap(typeof(TypeWithMapping), map);

            item = connection.Query<TypeWithMapping>("Select 'AVal' as A, 'BVal' as B").Single();
            Assert.Equal("BVal", item.A);
            Assert.Equal("AVal", item.B);

            // reset to default
            SqlMapper.SetTypeMap(typeof(TypeWithMapping), null);
            item = connection.Query<TypeWithMapping>("Select 'AVal' as A, 'BVal' as B").Single();
            Assert.Equal("AVal", item.A);
            Assert.Equal("BVal", item.B);
        }

        private static string GetDescriptionFromAttribute(MemberInfo member)
        {
            if (member == null) return null;
            var attrib = (DescriptionAttribute)Attribute.GetCustomAttribute(member, typeof(DescriptionAttribute), false);
            return attrib?.Description;

        }

        public class TypeWithMapping
        {
            [Description("B")]
            public string A { get; set; }

            [Description("A")]
            public string B { get; set; }
        }

        [Fact]
        public void Issue136_ValueTypeHandlers()
        {
            SqlMapper.ResetTypeHandlers();
            SqlMapper.AddTypeHandler(typeof(LocalDate), LocalDateHandler.Default);
            var param = new LocalDateResult
            {
                NotNullable = new LocalDate { Year = 2014, Month = 7, Day = 25 },
                NullableNotNull = new LocalDate { Year = 2014, Month = 7, Day = 26 },
                NullableIsNull = null,
            };

            var result = connection.Query<LocalDateResult>("SELECT @NotNullable AS NotNullable, @NullableNotNull AS NullableNotNull, @NullableIsNull AS NullableIsNull", param).Single();

            SqlMapper.ResetTypeHandlers();
            SqlMapper.AddTypeHandler(typeof(LocalDate?), LocalDateHandler.Default);
            result = connection.Query<LocalDateResult>("SELECT @NotNullable AS NotNullable, @NullableNotNull AS NullableNotNull, @NullableIsNull AS NullableIsNull", param).Single();
        }

        public class LocalDateHandler : TypeHandler<LocalDate>
        {
            private LocalDateHandler() { /* private constructor */ }

            // Make the field type ITypeHandler to ensure it cannot be used with SqlMapper.AddTypeHandler<T>(TypeHandler<T>)
            // by mistake.
            public static readonly ITypeHandler Default = new LocalDateHandler();

            public override LocalDate Parse(object value)
            {
                var date = (DateTime)value;
                return new LocalDate { Year = date.Year, Month = date.Month, Day = date.Day };
            }

            public override void SetValue(IDbDataParameter parameter, LocalDate value)
            {
                parameter.DbType = DbType.DateTime;
                parameter.Value = new DateTime(value.Year, value.Month, value.Day);
            }
        }

        public struct LocalDate
        {
            public int Year { get; set; }
            public int Month { get; set; }
            public int Day { get; set; }
        }

        public class LocalDateResult
        {
            public LocalDate NotNullable { get; set; }
            public LocalDate? NullableNotNull { get; set; }
            public LocalDate? NullableIsNull { get; set; }
        }

        public class LotsOfNumerics
        {
            public enum E_Byte : byte { A = 0, B = 1 }
            public enum E_SByte : sbyte { A = 0, B = 1 }
            public enum E_Short : short { A = 0, B = 1 }
            public enum E_UShort : ushort { A = 0, B = 1 }
            public enum E_Int : int { A = 0, B = 1 }
            public enum E_UInt : uint { A = 0, B = 1 }
            public enum E_Long : long { A = 0, B = 1 }
            public enum E_ULong : ulong { A = 0, B = 1 }

            public E_Byte P_Byte { get; set; }
            public E_SByte P_SByte { get; set; }
            public E_Short P_Short { get; set; }
            public E_UShort P_UShort { get; set; }
            public E_Int P_Int { get; set; }
            public E_UInt P_UInt { get; set; }
            public E_Long P_Long { get; set; }
            public E_ULong P_ULong { get; set; }

            public bool N_Bool { get; set; }
            public byte N_Byte { get; set; }
            public sbyte N_SByte { get; set; }
            public short N_Short { get; set; }
            public ushort N_UShort { get; set; }
            public int N_Int { get; set; }
            public uint N_UInt { get; set; }
            public long N_Long { get; set; }
            public ulong N_ULong { get; set; }

            public float N_Float { get; set; }
            public double N_Double { get; set; }
            public decimal N_Decimal { get; set; }

            public E_Byte? N_P_Byte { get; set; }
            public E_SByte? N_P_SByte { get; set; }
            public E_Short? N_P_Short { get; set; }
            public E_UShort? N_P_UShort { get; set; }
            public E_Int? N_P_Int { get; set; }
            public E_UInt? N_P_UInt { get; set; }
            public E_Long? N_P_Long { get; set; }
            public E_ULong? N_P_ULong { get; set; }

            public bool? N_N_Bool { get; set; }
            public byte? N_N_Byte { get; set; }
            public sbyte? N_N_SByte { get; set; }
            public short? N_N_Short { get; set; }
            public ushort? N_N_UShort { get; set; }
            public int? N_N_Int { get; set; }
            public uint? N_N_UInt { get; set; }
            public long? N_N_Long { get; set; }
            public ulong? N_N_ULong { get; set; }

            public float? N_N_Float { get; set; }
            public double? N_N_Double { get; set; }
            public decimal? N_N_Decimal { get; set; }
        }


        
        private void TestBigIntForEverythingWorksGeneric<T>(T expected, string dbType)
        {
            var query = connection.Query<T>("select cast(1 as " + dbType + ")").Single();
            Assert.Equal(query, expected);

            var scalar = connection.ExecuteScalar<T>("select cast(1 as " + dbType + ")");
            Assert.Equal(scalar, expected);
        }

        [Fact]
        public void TestSubsequentQueriesSuccess()
        {
            var data0 = connection.Query<Fooz0>("select 1 as [Id] where 1 = 0").ToList();
            Assert.Empty(data0);

            var data1 = connection.Query<Fooz1>(new CommandDefinition("select 1 as [Id] where 1 = 0", flags: CommandFlags.Buffered)).ToList();
            Assert.Empty(data1);

            var data2 = connection.Query<Fooz2>(new CommandDefinition("select 1 as [Id] where 1 = 0", flags: CommandFlags.None)).ToList();
            Assert.Empty(data2);

            data0 = connection.Query<Fooz0>("select 1 as [Id] where 1 = 0").ToList();
            Assert.Empty(data0);

            data1 = connection.Query<Fooz1>(new CommandDefinition("select 1 as [Id] where 1 = 0", flags: CommandFlags.Buffered)).ToList();
            Assert.Empty(data1);

            data2 = connection.Query<Fooz2>(new CommandDefinition("select 1 as [Id] where 1 = 0", flags: CommandFlags.None)).ToList();
            Assert.Empty(data2);
        }

        private class Fooz0
        {
            public int Id { get; set; }
        }

        private class Fooz1
        {
            public int Id { get; set; }
        }

        private class Fooz2
        {
            public int Id { get; set; }
        }

        public class RatingValueHandler : TypeHandler<RatingValue>
        {
            private RatingValueHandler()
            {
            }

            public static readonly RatingValueHandler Default = new RatingValueHandler();

            public override RatingValue Parse(object value)
            {
                if (value is int)
                {
                    return new RatingValue() { Value = (int)value };
                }

                throw new FormatException("Invalid conversion to RatingValue");
            }

            public override void SetValue(IDbDataParameter parameter, RatingValue value)
            {
                // ... null, range checks etc ...
                parameter.DbType = System.Data.DbType.Int32;
                parameter.Value = value.Value;
            }
        }

        public class RatingValue
        {
            public int Value { get; set; }
            // ... some other properties etc ...
        }

        public class MyResult
        {
            public string CategoryName { get; set; }
            public RatingValue CategoryRating { get; set; }
        }

        [Fact]
        public void SO24740733_TestCustomValueHandler()
        {
            SqlMapper.AddTypeHandler(RatingValueHandler.Default);
            var foo = connection.Query<MyResult>("SELECT 'Foo' AS CategoryName, 200 AS CategoryRating").Single();

            Assert.Equal("Foo", foo.CategoryName);
            Assert.Equal(200, foo.CategoryRating.Value);
        }

        [Fact]
        public void SO24740733_TestCustomValueSingleColumn()
        {
            SqlMapper.AddTypeHandler(RatingValueHandler.Default);
            var foo = connection.Query<RatingValue>("SELECT 200 AS CategoryRating").Single();

            Assert.Equal(200, foo.Value);
        }

        public class StringListTypeHandler : TypeHandler<List<string>>
        {
            private StringListTypeHandler()
            {
            }

            public static readonly StringListTypeHandler Default = new StringListTypeHandler();
            //Just a simple List<string> type handler implementation
            public override void SetValue(IDbDataParameter parameter, List<string> value)
            {
                parameter.Value = string.Join(",", value);
            }

            public override List<string> Parse(object value)
            {
                return ((value as string) ?? "").Split(',').ToList();
            }
        }

        public class MyObjectWithStringList
        {
            public List<string> Names { get; set; }
        }

        [Fact]
        public void Issue253_TestIEnumerableTypeHandlerParsing()
        {
            SqlMapper.ResetTypeHandlers();
            SqlMapper.AddTypeHandler(StringListTypeHandler.Default);
            var foo = connection.Query<MyObjectWithStringList>("SELECT 'Sam,Kyro' AS Names").Single();
            Assert.Equal(new[] { "Sam", "Kyro" }, foo.Names);
        }



        public class RecordingTypeHandler<T> : TypeHandler<T>
        {
            public override void SetValue(IDbDataParameter parameter, T value)
            {
                SetValueWasCalled = true;
                parameter.Value = value;
            }

            public override T Parse(object value)
            {
                ParseWasCalled = true;
                return (T)value;
            }

            public bool SetValueWasCalled { get; set; }
            public bool ParseWasCalled { get; set; }
        }



     

        private class ResultsChangeType
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
        }

        public class WrongTypes
        {
            public int A { get; set; }
            public double B { get; set; }
            public long C { get; set; }
            public bool D { get; set; }
        }

        [Fact]
        public void TestWrongTypes_WithRightTypes()
        {
            var item = connection.Query<WrongTypes>("select 1 as A, cast(2.0 as float) as B, cast(3 as bigint) as C, cast(1 as bit) as D").Single();
            Assert.Equal(1, item.A);
            Assert.Equal(2.0, item.B);
            Assert.Equal(3L, item.C);
            Assert.True(item.D);
        }

        [Fact]
        public void TestWrongTypes_WithWrongTypes()
        {
            var item = connection.Query<WrongTypes>("select cast(1.0 as float) as A, 2 as B, 3 as C, cast(1 as bigint) as D").Single();
            Assert.Equal(1, item.A);
            Assert.Equal(2.0, item.B);
            Assert.Equal(3L, item.C);
            Assert.True(item.D);
        }

        [Fact]
        public void SO24607639_NullableBools()
        {
            var obj = connection.Query<HazBools>(
                @"declare @vals table (A bit null, B bit null, C bit null);
                insert @vals (A,B,C) values (1,0,null);
                select * from @vals").Single();
            Assert.NotNull(obj);
            Assert.True(obj.A.Value);
            Assert.False(obj.B.Value);
            Assert.Null(obj.C);
        }

        private class HazBools
        {
            public bool? A { get; set; }
            public bool? B { get; set; }
            public bool? C { get; set; }
        }


        [Fact]
        public void Issue149_TypeMismatch_SequentialAccess()
        {
            Guid guid = Guid.Parse("cf0ef7ac-b6fe-4e24-aeda-a2b45bb5654e");
            var ex = Assert.ThrowsAny<Exception>(() => connection.Query<Issue149_Person>("select @guid as Id", new { guid }).First());
            Assert.Equal("Error parsing column 0 (Id=cf0ef7ac-b6fe-4e24-aeda-a2b45bb5654e - Object)", ex.Message);
        }

        public class Issue149_Person { public string Id { get; set; } }

        //[Fact]
        //public void Issue295_NullableDateTime_SqlServer() => Common.TestDateTime(connection);

        [Fact]
        public void SO29343103_UtcDates()
        {
            const string sql = "select @date";
            var date = DateTime.UtcNow;
            var returned = connection.Query<DateTime>(sql, new { date }).Single();
            var delta = returned - date;
            Assert.True(delta.TotalMilliseconds >= -10 && delta.TotalMilliseconds <= 10);
        }

  

        // I would usually expect this to be a struct; using a class
        // so that we can't pass unexpectedly due to forcing an unsafe cast - want
        // to see an InvalidCastException if it is wrong
        private class Blarg
        {
            public Blarg(string value) { Value = value; }
            public string Value { get; }
            public override string ToString()
            {
                return Value;
            }
        }

        private class Issue461_BlargHandler : TypeHandler<Blarg>
        {
            public override void SetValue(IDbDataParameter parameter, Blarg value)
            {
                parameter.Value = ((object)value.Value) ?? DBNull.Value;
            }

            public override Blarg Parse(object value)
            {
                string s = (value == null || value is DBNull) ? null : Convert.ToString(value);
                return new Blarg(s);
            }
        }

        private class Issue461_ParameterlessTypeConstructor
        {
            public int Id { get; set; }

            public string SomeValue { get; set; }
            public Blarg SomeBlargValue { get; set; }
        }

        private class Issue461_ParameterisedTypeConstructor
        {
            public Issue461_ParameterisedTypeConstructor(int id, string someValue, Blarg someBlargValue)
            {
                Id = id;
                SomeValue = someValue;
                SomeBlargValue = someBlargValue;
            }

            public int Id { get; }

            public string SomeValue { get; }
            public Blarg SomeBlargValue { get; }
        }
    }
}
