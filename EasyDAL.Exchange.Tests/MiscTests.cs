using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Xunit;
using EasyDAL.Exchange.DataBase;


using System.IO;
using System.Threading;
using System.Threading.Tasks;
using EasyDAL.Exchange.AdoNet;




namespace EasyDAL.Exchange.Tests
{
    public class MiscTests : TestBase
    {
 



        private struct Car
        {
            public enum TrapEnum : int
            {
                A = 1,
                B = 2
            }
#pragma warning disable 0649
            public string Name;
#pragma warning restore 0649
            public int Age { get; set; }
            public TrapEnum Trap { get; set; }
        }

        private struct CarWithAllProps
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public Car.TrapEnum Trap { get; set; }
        }





  
        





        private class Student
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }





        private class TestObj
        {
            public int _internal;
            internal int Internal
            {
                set { _internal = value; }
            }

            public int _priv;
            private int Priv
            {
                set { _priv = value; }
            }

            private int PrivGet => _priv;
        }






        


        private class WithBigInt
        {
            public long Value { get; set; }
        }



        private class TestFieldCaseAndPrivatesEntity
        {
#pragma warning disable IDE1006 // Naming Styles
            public int a { get; set; }
            private int b { get; set; }
            public int GetB() { return b; }
            public int c = 0;
#pragma warning disable RCS1169 // Mark field as read-only.
            private int d = 0;
#pragma warning restore RCS1169 // Mark field as read-only.
            public int GetD() { return d; }
            public int e { get; set; }
            private string f
            {
                get { return e.ToString(); }
                set { e = int.Parse(value); }
            }
#pragma warning restore IDE1006 // Naming Styles
        }

        private class InheritanceTest1
        {
            public string Base1 { get; set; }
            public string Base2 { get; private set; }
        }

        private class InheritanceTest2 : InheritanceTest1
        {
            public string Derived1 { get; set; }
            public string Derived2 { get; private set; }
        }




        
        [Fact]
        public void TestDefaultDbStringDbType()
        {
            var origDefaultStringDbType = DbString.IsAnsiDefault;
            try
            {
                DbString.IsAnsiDefault = true;
                var a = new DbString { Value = "abcde" };
                var b = new DbString { Value = "abcde", IsAnsi = false };
                Assert.True(a.IsAnsi);
                Assert.False(b.IsAnsi);
            }
            finally
            {
                DbString.IsAnsiDefault = origDefaultStringDbType;
            }
        }


        private class PrivateDan
        {
            public int Shadow { get; set; }
            private string ShadowInDB
            {
                set { Shadow = value == "one" ? 1 : 0; }
            }
        }

  

  

        private class WithBizarreData
        {
            public GenericUriParser Foo { get; set; }
            public int Bar { get; set; }
        }

        private class WithCharValue
        {
            public char Value { get; set; }
            public char? ValueNullable { get; set; }
        }





        private struct CanHazInt
        {
            public int Value { get; set; }
        }





        public class WithInt16Values
        {
            public short NonNullableInt16 { get; set; }
            public short? NullableInt16 { get; set; }
            public ShortEnum NonNullableInt16Enum { get; set; }
            public ShortEnum? NullableInt16Enum { get; set; }
        }

        public class WithInt32Values
        {
            public int NonNullableInt32 { get; set; }
            public int? NullableInt32 { get; set; }
            public IntEnum NonNullableInt32Enum { get; set; }
            public IntEnum? NullableInt32Enum { get; set; }
        }

        public enum IntEnum : int
        {
            Zero = 0, One = 1, Two = 2, Three = 3, Four = 4, Five = 5, Six = 6
        }



        public class Issue40_User
        {
            public Issue40_User()
            {
                Email = Password = string.Empty;
            }

            public int UserID { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public bool Active { get; set; }
        }







        






        private class HasInt32
        {
            public int Value { get; set; }
        }






        [Fact]
        public void Issue22_ExecuteScalar()
        {
            int i = connection.ExecuteScalar<int>("select 123");
            Assert.Equal(123, i);

            i = connection.ExecuteScalar<int>("select cast(123 as bigint)");
            Assert.Equal(123, i);

            long j = connection.ExecuteScalar<long>("select 123");
            Assert.Equal(123L, j);

            j = connection.ExecuteScalar<long>("select cast(123 as bigint)");
            Assert.Equal(123L, j);

            int? k = connection.ExecuteScalar<int?>("select @i", new { i = default(int?) });
            Assert.Null(k);
        }


        public class Issue142_Status
        {
            public StatusType Status { get; set; }
        }

        public class Issue142_StatusType
        {
            public Status Status { get; set; }
        }

        public enum StatusType : byte
        {
            NotStarted = 1, Started = 2, Finished = 3
        }

        public enum Status : byte
        {
            NotStarted = 1, Started = 2, Finished = 3
        }
        


 

        


        public class TPTable
        {
            public int Pid { get; set; }
            public int Value { get; set; }
        }

        //[Fact]
        //public void GetOnlyProperties()
        //{
        //    var obj = connection.QuerySingle<HazGetOnly>("select 42 as [Id], 'def' as [Name];");
        //    Assert.Equal(42, obj.Id);
        //    Assert.Equal("def", obj.Name);
        //}

        private class HazGetOnly
        {
            public int Id { get; }
            public string Name { get; } = "abc";
        }
    }
}
