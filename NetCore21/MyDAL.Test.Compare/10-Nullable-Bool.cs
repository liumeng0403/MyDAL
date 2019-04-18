using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Compare
{
    public class _10_Nullable_Bool
        : TestBase
    {
        [Fact]
        public async Task History_01()
        {

            xx = string.Empty;

            /********************************************************************************************************************************************/

            var xx5 = string.Empty;

            var guid5 = Guid.Parse("08d6036c-66c8-7c2c-83b0-725f93ff8137");
            var res5 = await Conn
                .Queryer(out AddressInfo address5, out AddressInfo address55)
                .From(() => address5)
                    .InnerJoin(() => address55)
                        .On(() => address5.Id == address55.Id)
                .Where(() => address5.IsDefault.Value && address5.UserId == guid5)  //  true
                .QueryListAsync<AddressInfo>();

            Assert.True(res5.Count == 1);
            Assert.True(res5.First().IsDefault);

            var res51 = await Conn
                .Queryer(out AddressInfo address51, out AddressInfo address511)
                .From(() => address51)
                    .InnerJoin(() => address511)
                        .On(() => address51.Id == address511.Id)
                .Where(() => address51.IsDefault == true && address51.UserId == guid5)  //  true
                .QueryListAsync<AddressInfo>();
            Assert.True(res51.Count == 1);
            Assert.True(res51.First().IsDefault);

            var guid52 = Guid.Parse("6f390324-2c07-40cf-90ca-0165569461b1");
            var res52 = await Conn
                .Queryer(out AddressInfo address52, out AddressInfo address521)
                .From(() => address52)
                    .InnerJoin(() => address521)
                        .On(() => address52.Id == address521.Id)
                .Where(() => address52.IsDefault == false || address52.Id == guid52)  //  false
                .QueryListAsync<AddressInfo>();
            Assert.True(res52.Count == 3);
            Assert.True(res52.First(it => it.Id != guid52).IsDefault == false);
            Assert.True(res52.First(it => it.Id == guid52).IsDefault);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************************/


            xx = string.Empty;

            var res61 = await Conn
                .Queryer<Product>()
                .Where(it => it.VipProduct.Value == false)
                .QueryPagingAsync(1, 10);

            Assert.True(res61.Data.Count == 4);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var res62 = await Conn
                .Queryer<Product>()
                .Where(it => it.VipProduct.Value == true)
                .QueryPagingAsync(1, 10);

            Assert.True(res62.Data.Count == 0);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task Ture_ST()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => true) // true 
                .QueryListAsync();

            Assert.True(res1.Count == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task False_ST()
        {
            xx = string.Empty;

            // where 1=1
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => false) //  false
                .QueryListAsync();

            Assert.True(res1.Count == 0);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task True_MT()
        {
            xx = string.Empty;

            // where 1=1
            var res1 = await Conn
                .Queryer(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .Where(() => true) // true 
                .QueryListAsync<Agent>();

            Assert.True(res1.Count == 574);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task False_MT()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .Where(() => false) //  false
                .QueryListAsync<Agent>();

            Assert.True(res1.Count == 0);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Not_True_MT()
        {
            xx = string.Empty;

            // where 1=1
            var res1 = await Conn
                .Queryer(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .Where(() => !true)   // false 
                .QueryListAsync<Agent>();

            Assert.True(res1.Count == 0);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Not_False_MT()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .Where(() => !false) //  true
                .QueryListAsync<Agent>();

            Assert.True(res1.Count == 574);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Bool_Default()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<AspnetUsers>()
                .Where(it => it.RootUser)  //  true
                .QueryPagingAsync(1, 10);

            Assert.True(res1.Data.Count == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Bool_True()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<AspnetUsers>()
                .Where(it => it.RootUser == true)  //  true
                .QueryPagingAsync(1, 10);

            Assert.True(res1.Data.Count == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Bool_False()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<AspnetUsers>()
                .Where(it => it.RootUser == false)  //  false
                .QueryPagingAsync(1, 10);

            Assert.True(res1.Data.Count == 10);
            Assert.True(res1.TotalCount == 28624);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Not_Bool_Default()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<AspnetUsers>()
                .Where(it => !it.RootUser)  //  false
                .QueryPagingAsync(1, 10);

            Assert.True(res1.Data.Count == 10);
            Assert.True(res1.TotalCount == 28624);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Not_Bool_True()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<AspnetUsers>()
                .Where(it => !(it.RootUser == true))  //  false
                .QueryPagingAsync(1, 10);

            Assert.True(res1.Data.Count == 10);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Not_Bool_False()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<AspnetUsers>()
                .Where(it => !(it.RootUser == false))  //  true
                .QueryPagingAsync(1, 10);

            Assert.True(res1.Data.Count == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Nullable_Bool_Default_ST()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<AddressInfo>()
                .Where(it => it.IsDefault.Value)  //  true
                .QueryListAsync();

            Assert.True(res1.Count == 5);
            Assert.True(res1.First().IsDefault);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Nullable_Bool_True_ST()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<AddressInfo>()
                .Where(it => it.IsDefault.Value == true)  //  true
                .QueryListAsync();

            Assert.True(res1.Count == 5);
            Assert.True(res1.First().IsDefault);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Nullable_Bool_False_ST()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<AddressInfo>()
                .Where(it => it.IsDefault.Value == false)  //  false 
                .QueryListAsync();

            Assert.True(res1.Count == 2);
            Assert.True(res1.First().IsDefault == false);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Nullable_Bool_IsNull_ST()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Product>()
                .Where(it => it.VipProduct == null)  //  is null  <--  nullable<bool>
                .QueryPagingAsync(1, 10);

            Assert.True(res1.Data.Count == 0);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Nullable_Bool_IsNotNull_ST()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Product>()
                .Where(it => it.VipProduct != null)  //  is not null  <--  nullable<bool>
                .QueryPagingAsync(1, 10);

            Assert.True(res1.Data.Count == 4);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Not_Nullable_Bool_Default_ST()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<AddressInfo>()
                .Where(it => !it.IsDefault.Value)  //  false
                .QueryListAsync();

            Assert.True(res1.Count == 2);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Not_Nullable_Bool_True_ST()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<AddressInfo>()
                .Where(it => !(it.IsDefault.Value == true))  //  false
                .QueryListAsync();

            Assert.True(res1.Count == 2);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Not_Nullable_Bool_False_ST()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<AddressInfo>()
                .Where(it => !(it.IsDefault.Value == false))  //  true 
                .QueryListAsync();

            Assert.True(res1.Count == 5);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Not_Nullable_Bool_IsNull_ST()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Product>()
                .Where(it => !(it.VipProduct == null))  //  is not null  <--  nullable<bool>
                .QueryPagingAsync(1, 10);

            Assert.True(res1.Data.Count == 4);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Not_Nullable_Bool_IsNotNull_ST()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Product>()
                .Where(it => !(it.VipProduct != null))  //  is null  <--  nullable<bool>
                .QueryPagingAsync(1, 10);

            Assert.True(res1.Data.Count == 0);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Nullable_Bool_Default_MT()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer(out AddressInfo addr, out AddressInfo addr2)
                .From(() => addr)
                    .InnerJoin(() => addr2)
                        .On(() => addr.Id == addr2.Id)
                .Where(() => addr.IsDefault.Value)  //  true
                .QueryListAsync<AddressInfo>();

            Assert.True(res1.Count == 5);
            Assert.True(res1.First().IsDefault);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Nullable_Bool_True_MT()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer(out AddressInfo addr, out AddressInfo addr2)
                .From(() => addr)
                    .InnerJoin(() => addr2)
                        .On(() => addr.Id == addr2.Id)
                .Where(() => addr.IsDefault.Value == true)  //  true
                .QueryListAsync<AddressInfo>();

            Assert.True(res1.Count == 5);
            Assert.True(res1.First().IsDefault);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Nullable_Bool_False_MT()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer(out AddressInfo addr, out AddressInfo addr2)
                .From(() => addr)
                    .InnerJoin(() => addr2)
                        .On(() => addr.Id == addr2.Id)
                .Where(() => addr.IsDefault == false)  //  false
                .QueryListAsync<AddressInfo>();

            Assert.True(res1.Count == 2);
            Assert.True(res1.First().IsDefault == false);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Nullable_Bool_IsNull_MT()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer(out AddressInfo addr, out AddressInfo addr2)
                .From(() => addr)
                    .InnerJoin(() => addr2)
                        .On(() => addr.Id == addr2.Id)
                .Where(() => addr.IsDefault == null)  //  is null <-- nullable<bool>
                .QueryListAsync<AddressInfo>();

            Assert.True(res1.Count == 0);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Nullable_Bool_IsNotNull_MT()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer(out AddressInfo addr, out AddressInfo addr2)
                .From(() => addr)
                    .InnerJoin(() => addr2)
                        .On(() => addr.Id == addr2.Id)
                .Where(() => addr.IsDefault != null)  //  is not null <-- nullable<bool>
                .QueryListAsync<AddressInfo>();

            Assert.True(res1.Count == 7);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Nullable_Multi_Default_MT()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer(out AddressInfo addr, out AddressInfo addr2)
                .From(() => addr)
                    .InnerJoin(() => addr2)
                        .On(() => addr.Id == addr2.Id)
                .Where(() => addr.IsDefault == null && addr.IsDefault.Value)  //  is null && true  <-- nullable<bool>
                .QueryListAsync<AddressInfo>();

            Assert.True(res1.Count == 0);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Not_Nullable_Multi_Default_MT()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer(out AddressInfo addr, out AddressInfo addr2)
                .From(() => addr)
                    .InnerJoin(() => addr2)
                        .On(() => addr.Id == addr2.Id)
                .Where(() => addr.IsDefault != null && !addr.IsDefault.Value)  //  is not null && false  <-- nullable<bool>
                .QueryListAsync<AddressInfo>();

            Assert.True(res1.Count == 2);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

    }
}
