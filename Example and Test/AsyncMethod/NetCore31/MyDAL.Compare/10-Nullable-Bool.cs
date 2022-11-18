using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Compare
{
    public class _10_Nullable_Bool
        : TestBase
    {
        [Fact]
        public void History_01()
        {

            xx = string.Empty;

            /********************************************************************************************************************************************/

            var xx5 = string.Empty;

            var guid5 = Guid.Parse("08d6036c-66c8-7c2c-83b0-725f93ff8137");
            var res5 = MyDAL_TestDB
                .Selecter(out AddressInfo address5, out AddressInfo address55)
                .From(() => address5)
                    .InnerJoin(() => address55)
                        .On(() => address5.Id == address55.Id)
                .Where(() => address5.IsDefault.Value && address5.UserId == guid5)  //  true
                .SelectList<AddressInfo>();

            Assert.True(res5.Count == 1);
            Assert.True(res5.First().IsDefault);

            var res51 = MyDAL_TestDB
                .Selecter(out AddressInfo address51, out AddressInfo address511)
                .From(() => address51)
                    .InnerJoin(() => address511)
                        .On(() => address51.Id == address511.Id)
                .Where(() => address51.IsDefault == true && address51.UserId == guid5)  //  true
                .SelectList<AddressInfo>();
            Assert.True(res51.Count == 1);
            Assert.True(res51.First().IsDefault);

            var guid52 = Guid.Parse("6f390324-2c07-40cf-90ca-0165569461b1");
            var res52 = MyDAL_TestDB
                .Selecter(out AddressInfo address52, out AddressInfo address521)
                .From(() => address52)
                    .InnerJoin(() => address521)
                        .On(() => address52.Id == address521.Id)
                .Where(() => address52.IsDefault == false || address52.Id == guid52)  //  false
                .SelectList<AddressInfo>();
            Assert.True(res52.Count == 3);
            Assert.True(res52.First(it => it.Id != guid52).IsDefault == false);
            Assert.True(res52.First(it => it.Id == guid52).IsDefault);

            

            /********************************************************************************************************************************************/


            xx = string.Empty;

            var res61 = MyDAL_TestDB
                .Selecter<Product>()
                .Where(it => it.VipProduct.Value == false)
                .SelectPaging(1, 10);

            Assert.True(res61.Data.Count == 4);

            

            var res62 = MyDAL_TestDB
                .Selecter<Product>()
                .Where(it => it.VipProduct.Value == true)
                .SelectPaging(1, 10);

            Assert.True(res62.Data.Count == 0);

            

            /********************************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public void Ture_ST()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => true) // true 
                .SelectList();

            Assert.True(res1.Count == 28620);

            

            xx = string.Empty;
        }

        [Fact]
        public void False_ST()
        {
            xx = string.Empty;

            // where 1=1
            var res1 = MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => false) //  false
                .SelectList();

            Assert.True(res1.Count == 0);

            

            xx = string.Empty;
        }

        [Fact]
        public void True_MT()
        {
            xx = string.Empty;

            // where 1=1
            var res1 = MyDAL_TestDB
                .Selecter(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .Where(() => true) // true 
                .SelectList<Agent>();

            Assert.True(res1.Count == 574);

            

            xx = string.Empty;
        }

        [Fact]
        public void False_MT()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .Where(() => false) //  false
                .SelectList<Agent>();

            Assert.True(res1.Count == 0);

            

            xx = string.Empty;
        }

        [Fact]
        public void Not_True_MT()
        {
            xx = string.Empty;

            // where 1=1
            var res1 = MyDAL_TestDB
                .Selecter(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .Where(() => !true)   // false 
                .SelectList<Agent>();

            Assert.True(res1.Count == 0);

            

            xx = string.Empty;
        }

        [Fact]
        public void Not_False_MT()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .Where(() => !false) //  true
                .SelectList<Agent>();

            Assert.True(res1.Count == 574);

            

            xx = string.Empty;
        }

        [Fact]
        public void Bool_Default()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter<AspnetUsers>()
                .Where(it => it.RootUser)  //  true
                .SelectPaging(1, 10);

            Assert.True(res1.Data.Count == 1);

            

            xx = string.Empty;
        }

        [Fact]
        public void Bool_True()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter<AspnetUsers>()
                .Where(it => it.RootUser == true)  //  true
                .SelectPaging(1, 10);

            Assert.True(res1.Data.Count == 1);

            

            xx = string.Empty;
        }

        [Fact]
        public void Bool_False()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter<AspnetUsers>()
                .Where(it => it.RootUser == false)  //  false
                .SelectPaging(1, 10);

            Assert.True(res1.Data.Count == 10);
            Assert.True(res1.TotalCount == 28624);

            

            xx = string.Empty;
        }

        [Fact]
        public void Not_Bool_Default()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter<AspnetUsers>()
                .Where(it => !it.RootUser)  //  false
                .SelectPaging(1, 10);

            Assert.True(res1.Data.Count == 10);
            Assert.True(res1.TotalCount == 28624);

            

            xx = string.Empty;
        }

        [Fact]
        public void Not_Bool_True()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter<AspnetUsers>()
                .Where(it => !(it.RootUser == true))  //  false
                .SelectPaging(1, 10);

            Assert.True(res1.Data.Count == 10);

            

            xx = string.Empty;
        }

        [Fact]
        public void Not_Bool_False()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter<AspnetUsers>()
                .Where(it => !(it.RootUser == false))  //  true
                .SelectPaging(1, 10);

            Assert.True(res1.Data.Count == 1);

            

            xx = string.Empty;
        }

        [Fact]
        public void Nullable_Bool_Default_ST()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter<AddressInfo>()
                .Where(it => it.IsDefault.Value)  //  true
                .SelectList();

            Assert.True(res1.Count == 5);
            Assert.True(res1.First().IsDefault);

            

            xx = string.Empty;
        }

        [Fact]
        public void Nullable_Bool_True_ST()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter<AddressInfo>()
                .Where(it => it.IsDefault.Value == true)  //  true
                .SelectList();

            Assert.True(res1.Count == 5);
            Assert.True(res1.First().IsDefault);

            

            xx = string.Empty;
        }

        [Fact]
        public void Nullable_Bool_False_ST()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter<AddressInfo>()
                .Where(it => it.IsDefault.Value == false)  //  false 
                .SelectList();

            Assert.True(res1.Count == 2);
            Assert.True(res1.First().IsDefault == false);

            

            xx = string.Empty;
        }

        [Fact]
        public void Nullable_Bool_IsNull_ST()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter<Product>()
                .Where(it => it.VipProduct == null)  //  is null  <--  nullable<bool>
                .SelectPaging(1, 10);

            Assert.True(res1.Data.Count == 0);

            

            xx = string.Empty;
        }

        [Fact]
        public void Nullable_Bool_IsNotNull_ST()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter<Product>()
                .Where(it => it.VipProduct != null)  //  is not null  <--  nullable<bool>
                .SelectPaging(1, 10);

            Assert.True(res1.Data.Count == 4);

            

            xx = string.Empty;
        }

        [Fact]
        public void Not_Nullable_Bool_Default_ST()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter<AddressInfo>()
                .Where(it => !it.IsDefault.Value)  //  false
                .SelectList();

            Assert.True(res1.Count == 2);

            

            xx = string.Empty;
        }

        [Fact]
        public void Not_Nullable_Bool_True_ST()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter<AddressInfo>()
                .Where(it => !(it.IsDefault.Value == true))  //  false
                .SelectList();

            Assert.True(res1.Count == 2);

            

            xx = string.Empty;
        }

        [Fact]
        public void Not_Nullable_Bool_False_ST()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter<AddressInfo>()
                .Where(it => !(it.IsDefault.Value == false))  //  true 
                .SelectList();

            Assert.True(res1.Count == 5);

            

            xx = string.Empty;
        }

        [Fact]
        public void Not_Nullable_Bool_IsNull_ST()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter<Product>()
                .Where(it => !(it.VipProduct == null))  //  is not null  <--  nullable<bool>
                .SelectPaging(1, 10);

            Assert.True(res1.Data.Count == 4);

            

            xx = string.Empty;
        }

        [Fact]
        public void Not_Nullable_Bool_IsNotNull_ST()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter<Product>()
                .Where(it => !(it.VipProduct != null))  //  is null  <--  nullable<bool>
                .SelectPaging(1, 10);

            Assert.True(res1.Data.Count == 0);

            

            xx = string.Empty;
        }

        [Fact]
        public void Nullable_Bool_Default_MT()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter(out AddressInfo addr, out AddressInfo addr2)
                .From(() => addr)
                    .InnerJoin(() => addr2)
                        .On(() => addr.Id == addr2.Id)
                .Where(() => addr.IsDefault.Value)  //  true
                .SelectList<AddressInfo>();

            Assert.True(res1.Count == 5);
            Assert.True(res1.First().IsDefault);

            

            xx = string.Empty;
        }

        [Fact]
        public void Nullable_Bool_True_MT()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter(out AddressInfo addr, out AddressInfo addr2)
                .From(() => addr)
                    .InnerJoin(() => addr2)
                        .On(() => addr.Id == addr2.Id)
                .Where(() => addr.IsDefault.Value == true)  //  true
                .SelectList<AddressInfo>();

            Assert.True(res1.Count == 5);
            Assert.True(res1.First().IsDefault);

            

            xx = string.Empty;
        }

        [Fact]
        public void Nullable_Bool_False_MT()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter(out AddressInfo addr, out AddressInfo addr2)
                .From(() => addr)
                    .InnerJoin(() => addr2)
                        .On(() => addr.Id == addr2.Id)
                .Where(() => addr.IsDefault == false)  //  false
                .SelectList<AddressInfo>();

            Assert.True(res1.Count == 2);
            Assert.True(res1.First().IsDefault == false);

            

            xx = string.Empty;
        }

        [Fact]
        public void Nullable_Bool_IsNull_MT()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter(out AddressInfo addr, out AddressInfo addr2)
                .From(() => addr)
                    .InnerJoin(() => addr2)
                        .On(() => addr.Id == addr2.Id)
                .Where(() => addr.IsDefault == null)  //  is null <-- nullable<bool>
                .SelectList<AddressInfo>();

            Assert.True(res1.Count == 0);

            

            xx = string.Empty;
        }

        [Fact]
        public void Nullable_Bool_IsNotNull_MT()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter(out AddressInfo addr, out AddressInfo addr2)
                .From(() => addr)
                    .InnerJoin(() => addr2)
                        .On(() => addr.Id == addr2.Id)
                .Where(() => addr.IsDefault != null)  //  is not null <-- nullable<bool>
                .SelectList<AddressInfo>();

            Assert.True(res1.Count == 7);

            

            xx = string.Empty;
        }

        [Fact]
        public void Nullable_Multi_Default_MT()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter(out AddressInfo addr, out AddressInfo addr2)
                .From(() => addr)
                    .InnerJoin(() => addr2)
                        .On(() => addr.Id == addr2.Id)
                .Where(() => addr.IsDefault == null && addr.IsDefault.Value)  //  is null && true  <-- nullable<bool>
                .SelectList<AddressInfo>();

            Assert.True(res1.Count == 0);

            

            xx = string.Empty;
        }

        [Fact]
        public void Not_Nullable_Multi_Default_MT()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter(out AddressInfo addr, out AddressInfo addr2)
                .From(() => addr)
                    .InnerJoin(() => addr2)
                        .On(() => addr.Id == addr2.Id)
                .Where(() => addr.IsDefault != null && !addr.IsDefault.Value)  //  is not null && false  <-- nullable<bool>
                .SelectList<AddressInfo>();

            Assert.True(res1.Count == 2);

            

            xx = string.Empty;
        }

    }
}
