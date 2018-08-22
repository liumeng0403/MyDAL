using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EasyDAL.Exchange.Tests
{
    public class DataReaderTests : TestBase
    {
        private abstract class Discriminated_BaseType
        {
            public abstract int Type { get; }
        }

        private class Discriminated_Foo : Discriminated_BaseType
        {
            public string Name { get; set; }
            public override int Type => 1;
        }

        private class Discriminated_Bar : Discriminated_BaseType
        {
            public float Value { get; set; }
            public override int Type => 2;
        }

        private abstract class DiscriminatedWithMultiMapping_BaseType : Discriminated_BaseType
        {
            public abstract HazNameId HazNameIdObject { get; set; }
        }

        private class DiscriminatedWithMultiMapping_Foo : DiscriminatedWithMultiMapping_BaseType
        {
            public override HazNameId HazNameIdObject { get; set; }
            public string Name { get; set; }
            public override int Type => 1;
        }

        private class DiscriminatedWithMultiMapping_Bar : DiscriminatedWithMultiMapping_BaseType
        {
            public override HazNameId HazNameIdObject { get; set; }
            public float Value { get; set; }
            public override int Type => 2;
        }
    }
}
