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







        private class HazBools
        {
            public bool? A { get; set; }
            public bool? B { get; set; }
            public bool? C { get; set; }
        }




        public class Issue149_Person { public string Id { get; set; } }

  
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
