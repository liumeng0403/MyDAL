using System;
using System.Data;
using System.Linq;
using EasyDAL.Exchange.Attributes;
using Xunit;
using EasyDAL.Exchange.AdoNet;

namespace EasyDAL.Exchange.Tests
{
    public class ConstructorTests : TestBase
    {
 







        private class _ExplicitConstructors
        {
            public int Field { get; set; }
            public int Field_1 { get; set; }

            private readonly bool WentThroughProperConstructor;

            public _ExplicitConstructors() { /* yep */ }

            [ExplicitConstructor]
            public _ExplicitConstructors(string foo, int bar)
            {
                WentThroughProperConstructor = true;
            }

            public bool GetWentThroughProperConstructor()
            {
                return WentThroughProperConstructor;
            }
        }

        public static class AbstractInheritance
        {
            public abstract class Order
            {
                internal int Internal { get; set; }
                protected int Protected { get; set; }
                public int Public { get; set; }

                public int ProtectedVal => Protected;
            }

            public class ConcreteOrder : Order
            {
                public int Concrete { get; set; }
            }
        }

        private class MultipleConstructors
        {
            public MultipleConstructors()
            {
            }

            public MultipleConstructors(int a, string b)
            {
                A = a + 1;
                B = b + "!";
            }

            public int A { get; set; }
            public string B { get; set; }
        }

        private class ConstructorsWithAccessModifiers
        {
            private ConstructorsWithAccessModifiers()
            {
            }

            public ConstructorsWithAccessModifiers(int a, string b)
            {
                A = a + 1;
                B = b + "!";
            }

            public int A { get; set; }
            public string B { get; set; }
        }

        private class NoDefaultConstructor
        {
            public NoDefaultConstructor(int a1, int? b1, float f1, string s1, Guid G1)
            {
                A = a1;
                B = b1;
                F = f1;
                S = s1;
                G = G1;
            }

            public int A { get; set; }
            public int? B { get; set; }
            public float F { get; set; }
            public string S { get; set; }
            public Guid G { get; set; }
        }

        private class NoDefaultConstructorWithChar
        {
            public NoDefaultConstructorWithChar(char c1, char? c2, char? c3)
            {
                Char1 = c1;
                Char2 = c2;
                Char3 = c3;
            }

            public char Char1 { get; set; }
            public char? Char2 { get; set; }
            public char? Char3 { get; set; }
        }

        private class NoDefaultConstructorWithEnum
        {
            public NoDefaultConstructorWithEnum(ShortEnum e1, ShortEnum? n1, ShortEnum? n2)
            {
                E = e1;
                NE1 = n1;
                NE2 = n2;
            }

            public ShortEnum E { get; set; }
            public ShortEnum? NE1 { get; set; }
            public ShortEnum? NE2 { get; set; }
        }

        private class WithPrivateConstructor
        {
            public int Foo { get; set; }
            private WithPrivateConstructor()
            {
            }
        }


    }
}
