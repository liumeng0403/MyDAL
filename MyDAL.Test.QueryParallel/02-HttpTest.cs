using Yunyong.DataExchange;

namespace MyDAL.Test.QueryParallel
{
    public class HttpTest
    {
        public void HttpAsyncTest()
        {
            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjllNWQ0MTA0ODY3ZTI5MDhiMGVjMzVmY2IwZThiZWE5IiwidHlwIjoiSldUIn0.eyJuYmYiOjE1NDI3MDIwODYsImV4cCI6MTU0NTM4MDQ4NiwiaXNzIjoiaHR0cDovL2FwaS5zdC5tYWltYWliYS5zaG9wL2lkZW50aXR5IiwiYXVkIjpbImh0dHA6Ly9hcGkuc3QubWFpbWFpYmEuc2hvcC9pZGVudGl0eS9yZXNvdXJjZXMiLCJPcmRlcmluZ0FwaSJdLCJjbGllbnRfaWQiOiJza3lsYXJrLmNsaWVudCIsInN1YiI6IjA4ZDU5NWU2LTczODYtMDI2OS02MjRiLWY5Y2Q1Y2ZkN2YxYSIsImF1dGhfdGltZSI6MTU0MjcwMjA4NiwiaWRwIjoibG9jYWwiLCJVc2VySWQiOiIwOGQ1OTVlNi03Mzg2LTAyNjktNjI0Yi1mOWNkNWNmZDdmMWEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMTMxNjIxMDg4NTMiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9tb2JpbGVwaG9uZSI6IjEzMTYyMTA4ODUzIiwicm9sZSI6WyJVc2VyIiwiVmVuZG9yIiwiU2hvcE93bmVyIiwiQ3VzdG9tZXIiLCJDdXN0b21lclNlcnZpY2UiXSwiVmVuZG9ySWQiOiIwOGQ1YzZiYS1jMjhjLTZkMDgtMGVmOS1jNzg5NmMyYTA1NTkiLCJTaG9wT3duZXJJZCI6IjcyYzg1OGRkLTgxMWMtMTFlOC04ZjZmLTUyNTQwMDI0OWUzZCIsIkN1c3RvbWVySWQiOiIwOGQ1YjU3Yy02ZjRjLWU4MzgtMzZiMS01MWU2MDM1MTVlMTQiLCJDdXN0b21lclNlcnZpY2VJZCI6IjA4ZDU5NWU2LTc0NjEtOTY3NC1mZDNhLTg4OTJhZWY1MzM4ZSIsIkludml0ZXJJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIlBJZCI6IiIsInNjb3BlIjpbIk9yZGVyaW5nQXBpIl0sImFtciI6WyJwd2QiXX0.QFzyzcD5dx6YtVsesG8keKeubQ-Sbl6H2xazMrhRRNY1nOJh4XvdZThQ_Pw60FJvGr-n-zKs-QTVgmSU8B3WcT7NZyYTzV5rnnMxK4ZB7GOdIX9kBXFUtJTNz-DAg-WXgo1FJZ9Rs3WBRjSklC-C_SrAqVwcFI2IOh1ForJ6PooEgJmHNfPVTO2nwwVKfSQSzG8WhuWVvp_GIOI7A_4Apu2GSVvRHOxGmOq0troIEtYxfrBIqnwiYFFL97quUM4UlMBghLNO3xezZtQtUQ3y7uubFh-jN7yVSl9lR9HJkgZVRCcGmWyTtOFN1REjBqDtI3w0E2iaulfcXY_AGGFJAg";

            var json = @"
{
	'CreateCustomerOrderItems': [{

        'Count': '2',
		'UnitPrice': 880.0000000000000000000000000,
		'SkuId': '08d5d673-60e3-fd87-ea86-6a1efc37c521',
		'VendorId': '08d5d294-6c3a-0268-1578-0b8f9972ad50',
		'VendorName': '广东伽伽那家居用品有限公司',
		'Image1': '08d5d673-243b-ab47-6f5a-c29f79493328',
		'ProductId': '08d5d673-60e3-f3de-fecf-11430cfda7aa',
		'SkuName': '白色系 1.7L    F-EKS17A',
		'RootCategoryName': '981d76dd-add0-456f-ac6d-a42787f433ff',
		'IsVipPackage': false

    }, {
		'Count': '1',
		'UnitPrice': 187.00000000000000000000000000,
		'SkuId': '08d5a65b-cdea-d47f-89b5-e59293e8a605',
		'VendorId': '08d5947b-8947-6f0e-d27f-8699b3208748',
		'VendorName': '爱买全球科技（南通）有限公司',
		'Image1': '08d5a65b-92ba-7133-1076-5ba790ff1a6b',
		'ProductId': '08d5a65b-cdea-c47b-14ad-b87fb80ffbba',
		'SkuName': '粉色 约105*88*70cm',
		'RootCategoryName': '47cc4a03-b21d-47b6-90f7-f748ed69aa23',
		'IsVipPackage': false
	}],
	'CreateCustomerOrderInvoiceVM': {
		'TitleType': 'Personal',
		'TitleTypeName': '个人'
	},
	'IsUseMCoin': false,
	'IsFromShoppingCart': true,
	'MaxMCoinPrice': 1947.0,
	'MCoinPayPrice': 0,
	'paySumPricePre': '1947.0',
	'ExpressCharge': '0.00',
	'IsSelfPickUp': false,
	'TownId': '60780c75-6316-4e08-970c-08d55e6655b3',
	'AddressId': '60780c75-6316-4e08-970c-08d55e6655b3',
	'TownAddress': '北京市 市辖区 东城区 东华门街道办事处',
	'DetailAddress': '??????',
	'ContactName': '?????',
	'ContactPhone': '13888888888'
}
                                    ";

            var parallel = new XParallelTest();
            parallel.IsLookTask = true;
            parallel.IsHttpFunc = true;
            parallel.Token = token;
            parallel.URL = "http://localhost:5130/Ordering/api/CustomerOrder/AddOrders";
            parallel.RequestMethod = "POST";
            parallel.JsonContent = json;
            //parallel.ApiDebug();
            //parallel.Parallel_100_10000();
            //parallel.Parallel_90_10000();
            //parallel.Parallel_80_10000();
            //parallel.Parallel_70_10000();
            //parallel.Parallel_60_10000();
            //parallel.Parallel_50_10000();
            //parallel.Parallel_40_10000();
            //parallel.Parallel_30_10000();
            //parallel.Parallel_20_10000();
            parallel.Parallel_10_10000();
            //parallel.Parallel_5_10000();
            //parallel.Parallel_1_10000();
        }
    }
}
