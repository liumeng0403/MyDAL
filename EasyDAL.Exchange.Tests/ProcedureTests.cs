using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using EasyDAL.Exchange.DataBase;
using EasyDAL.Exchange.DynamicParameter;
using Xunit;
using EasyDAL.Exchange.AdoNet;

namespace EasyDAL.Exchange.Tests
{
    public class ProcedureTests : TestBase

    { 




        private class PracticeRebateOrders
        {
            public string fTaxInvoiceNumber;
#if !NETCOREAPP1_0
            [System.Xml.Serialization.XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
#endif
            public string TaxInvoiceNumber
            {
                get { return fTaxInvoiceNumber; }
                set { fTaxInvoiceNumber = value; }
            }
        }
        
        private class Issue327_Person
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private class Issue327_Magic
        {
            public string Creature { get; set; }
            public string SpiritAnimal { get; set; }
            public string Location { get; set; }
        }



        // https://stackoverflow.com/q/8593871
        [Fact]
        public void TestListOfAnsiStrings()
        {
            var results = connection.Query<string>("select * from (select 'a' str union select 'b' union select 'c') X where str in @strings",
                new
                {
                    strings = new[] {
                    new DbString { IsAnsi = true, Value = "a" },
                    new DbString { IsAnsi = true, Value = "b" }
                }
                }).ToList();

            Assert.Equal(2, results.Count);
            results.Sort();
            Assert.Equal("a", results[0]);
            Assert.Equal("b", results[1]);
        }





  
    }
}
