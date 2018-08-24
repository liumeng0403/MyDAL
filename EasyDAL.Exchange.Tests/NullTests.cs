using Xunit;
using System.Linq;
using EasyDAL.Exchange.MapperX;
using EasyDAL.Exchange.AdoNet;

namespace EasyDAL.Exchange.Tests
{
    [Collection(NonParallelDefinition.Name)]
    public class NullTests : TestBase
    {




        private class NullTestClass
        {
            public int Id { get; set; }
            public int A { get; set; }
            public int? B { get; set; }
            public string C { get; set; }
            public AnEnum D { get; set; }
            public AnEnum? E { get; set; }

            public NullTestClass()
            {
                A = 2;
                B = 2;
                C = "def";
                D = AnEnum.B;
                E = AnEnum.B;
            }
        }
    }
}
