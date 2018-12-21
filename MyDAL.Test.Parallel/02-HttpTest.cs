using MyDAL.Test.Parallels;

namespace MyDAL.Test.QueryParallel
{
    public class HttpTest
    {
        private string token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjllNWQ0MTA0ODY3ZTI5MDhiMGVjMzVmY2IwZThiZWE5IiwidHlwIjoiSldUIn0.eyJuYmYiOjE1NDI3NjkzMTIsImV4cCI6MTU0NTQ0NzcxMiwiaXNzIjoiaHR0cDovL2FwaS5zdC5tYWltYWliYS5zaG9wL2lkZW50aXR5IiwiYXVkIjpbImh0dHA6Ly9hcGkuc3QubWFpbWFpYmEuc2hvcC9pZGVudGl0eS9yZXNvdXJjZXMiLCJPcmRlcmluZ0FwaSJdLCJjbGllbnRfaWQiOiJza3lsYXJrLmNsaWVudCIsInN1YiI6IjA4ZDU5NWU2LTczODYtMDI2OS02MjRiLWY5Y2Q1Y2ZkN2YxYSIsImF1dGhfdGltZSI6MTU0Mjc2OTMxMiwiaWRwIjoibG9jYWwiLCJVc2VySWQiOiIwOGQ1OTVlNi03Mzg2LTAyNjktNjI0Yi1mOWNkNWNmZDdmMWEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMTMxNjIxMDg4NTMiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9tb2JpbGVwaG9uZSI6IjEzMTYyMTA4ODUzIiwicm9sZSI6WyJVc2VyIiwiVmVuZG9yIiwiU2hvcE93bmVyIiwiQ3VzdG9tZXIiLCJDdXN0b21lclNlcnZpY2UiXSwiVmVuZG9ySWQiOiIwOGQ1YzZiYS1jMjhjLTZkMDgtMGVmOS1jNzg5NmMyYTA1NTkiLCJTaG9wT3duZXJJZCI6IjcyYzg1OGRkLTgxMWMtMTFlOC04ZjZmLTUyNTQwMDI0OWUzZCIsIkN1c3RvbWVySWQiOiIwOGQ1YjU3Yy02ZjRjLWU4MzgtMzZiMS01MWU2MDM1MTVlMTQiLCJDdXN0b21lclNlcnZpY2VJZCI6IjA4ZDU5NWU2LTc0NjEtOTY3NC1mZDNhLTg4OTJhZWY1MzM4ZSIsIkludml0ZXJJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIlBJZCI6IiIsInNjb3BlIjpbIk9yZGVyaW5nQXBpIl0sImFtciI6WyJwd2QiXX0.C0yxCWLyahrgeT0in_4AubH2YbFiQt2-J0SNHvBW8SdEtcZSqyV0NjhmBb0MleyKGBoyz2hwWgYo5CryVS6YcL-i-XgXePWvSyZX0woqswt9h_3UPkxpRv31Lnq58ZDTn0GSUSSr1Atoh5m9yt5W7yWZImUw3ee3wkYC4l_uwNpk8nRTBaN2C8IpPIC3TBR3UbuGcSy-OVok2rT1VBe3x2RSw7t5xS5NMrdQd2JDgv60kf2rmlFGeUxQ06-F-Jb_hqCHSIM7uM_6J1Kxhpaoz-4GxtJ54Hj3HmXPEOFX08z9LsChSsaB8sCBbB-w8ujXKvEWBUtCz9rVbFn5GJcI5w";
        private string token2 = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjllNWQ0MTA0ODY3ZTI5MDhiMGVjMzVmY2IwZThiZWE5IiwidHlwIjoiSldUIn0.eyJuYmYiOjE1NDI3Njk1NzgsImV4cCI6MTU0NTQ0Nzk3OCwiaXNzIjoiaHR0cDovL2FwaS5zdC5tYWltYWliYS5zaG9wL2lkZW50aXR5IiwiYXVkIjpbImh0dHA6Ly9hcGkuc3QubWFpbWFpYmEuc2hvcC9pZGVudGl0eS9yZXNvdXJjZXMiLCJDYXJ0QXBpIiwiT3JkZXJpbmdBcGkiXSwiY2xpZW50X2lkIjoic2t5bGFyay5jbGllbnQiLCJzdWIiOiIwOGQ1OTVlNi03Mzg2LTAyNjktNjI0Yi1mOWNkNWNmZDdmMWEiLCJhdXRoX3RpbWUiOjE1NDI3Njk1NzgsImlkcCI6ImxvY2FsIiwiVXNlcklkIjoiMDhkNTk1ZTYtNzM4Ni0wMjY5LTYyNGItZjljZDVjZmQ3ZjFhIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IjEzMTYyMTA4ODUzIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbW9iaWxlcGhvbmUiOiIxMzE2MjEwODg1MyIsInJvbGUiOlsiVXNlciIsIlZlbmRvciIsIlNob3BPd25lciIsIkN1c3RvbWVyIiwiQ3VzdG9tZXJTZXJ2aWNlIl0sIlZlbmRvcklkIjoiMDhkNWM2YmEtYzI4Yy02ZDA4LTBlZjktYzc4OTZjMmEwNTU5IiwiU2hvcE93bmVySWQiOiI3MmM4NThkZC04MTFjLTExZTgtOGY2Zi01MjU0MDAyNDllM2QiLCJDdXN0b21lcklkIjoiMDhkNWI1N2MtNmY0Yy1lODM4LTM2YjEtNTFlNjAzNTE1ZTE0IiwiQ3VzdG9tZXJTZXJ2aWNlSWQiOiIwOGQ1OTVlNi03NDYxLTk2NzQtZmQzYS04ODkyYWVmNTMzOGUiLCJJbnZpdGVySWQiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJQSWQiOiIiLCJzY29wZSI6WyJDYXJ0QXBpIiwiT3JkZXJpbmdBcGkiXSwiYW1yIjpbInB3ZCJdfQ.HSJzgA8DVdRJG-E1P_LaUIhlk707V1YshmISjyCme8nDPpwua1WJUO4cxdYbziqvyhMFn0AXzr0pdYai2Bb3MWIfgEnnymfeZVuGlK9gmFNMHnP-CDpYtownvkQ4k6VffDWYNbx9ghTod5SldyX0kTZVS2GgD8bBPiYOtlin6nFJWxlBg4Nha9kR9mbWewkeK4d7F33foWMPgBHfv-Kfpznc3ZyatmE3S-3868fUgEzisKY4Xe33qg-_aD3H0v2h2dZRDh-Cpx1-FZlj2GNYvePoBf6Ex4HWMMlWih1Gqd4e6LG9VsTJwZnfONzUtVZ0K-OPzwNxLcAVsM9Zclu5dg";

        private bool IsLookTask = true;

        private void HttpAsyncTest1()
        {
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
            parallel.IsLookTask = IsLookTask;
            parallel.IsSubTask = true;
            parallel.IsHttpFunc = true;
            parallel.Token = token2;
            parallel.URL = "http://api.st.maimaiba.shop/Ordering/api/CustomerOrder/AddOrders";
            parallel.RequestMethod = "POST";
            parallel.JsonContent = json;
            parallel.ApiDebug();
        }
        public void HttpAsyncTest2()
        {
            var json = @"
{
	'Quantity': 1,

    'skuId': '08d5a65b-cdea-d47f-89b5-e59293e8a605',
	'SKU': {
                'ProductId': '00000000-0000-0000-0000-000000000000',
		'SkuName': '粉色 约105*88*70cm',
		'Inventory': 100,
		'AlertInventory': 0,
		'SellPrice': 187,
		'OrigPrice': 0,
		'SellCount': 0,
		'SellerCode': 'AM-400087',
		'AttrsString': '08d59ee8-5182-5496-c293-2dafcc814259;08d59f52-86bc-1dd6-b075-d2cb2cc3afbd',
		'Image1': '00000000-0000-0000-0000-000000000000',
		'VersionStamp': '5b13cd86-538f-4440-8f80-939487b34b1f',
		'Commission': 8.55,
		'SkuAttrValues': [{
			'ProductId': '08d5a65b-cdea-c47b-14ad-b87fb80ffbba',
			'SkuId': '08d5a65b-cdea-d47f-89b5-e59293e8a605',
			'IsSkuValue': true,
			'AttrId': '08d59ee8-5182-5496-c293-2dafcc814259',
			'ValueType': 'CustOptionValue',
			'AttriInputType': 'Option',
			'AttrValueId': '08d5a65b-cdea-cbee-10e8-034b2db36341',
			'CategGroupIdSnapshot': '5be1e1d6-f691-4fdf-42c8-08d5905ae0a4',
			'CategGroupNameSnapshot': '规格属性',
			'AttrNameSnapshot': '颜色',
			'AttrValueStrSnapshot': '粉色',
			'IsPictureValue': false,
			'PrivateAttrInputType': 'Input',
			'Id': '08d5a65b-cdeb-2d58-25c4-d86085c275d0',
			'IsEnable': true

        }, {
			'ProductId': '08d5a65b-cdea-c47b-14ad-b87fb80ffbba',
			'SkuId': '08d5a65b-cdea-d47f-89b5-e59293e8a605',
			'IsSkuValue': true,
			'AttrId': '08d59f52-86bc-1dd6-b075-d2cb2cc3afbd',
			'ValueType': 'CustOptionValue',
			'AttriInputType': 'Option',
			'AttrValueId': '08d5a65b-cdea-d076-b8c8-8a51d2fc76b0',
			'CategGroupIdSnapshot': '5be1e1d6-f691-4fdf-42c8-08d5905ae0a4',
			'CategGroupNameSnapshot': '规格属性',
			'AttrNameSnapshot': '规格',
			'AttrValueStrSnapshot': '约105*88*70cm',
			'IsPictureValue': false,
			'PrivateAttrInputType': 'Input',
			'Id': '08d5a65b-cdeb-7ad0-211f-ba95fcb91ffd',
			'IsEnable': true
		}],
		'Weight': 0,
		'Id': '08d5a65b-cdea-d47f-89b5-e59293e8a605',
		'IsEnable': false,
		'Product': {
			'VendorId': '08d5947b-8947-6f0e-d27f-8699b3208748',
			'MainImageGuidsList': ['08d5a65b-92ba-7133-1076-5ba790ff1a6b', '08d5a65b-92b9-d317-39b5-0951fbd2ebdc', '08d5a65b-92c1-55ae-681e-cc9f39242543', '08d5a65b-92c1-166c-05b7-08767b024f5f', '08d5a65b-92c1-8f76-c149-bef3597dbd43'],
			'Id': '08d5a65b-cdea-c47b-14ad-b87fb80ffbba',
			'ProdcutName': '六甲村 雅致型授乳巾',
			'RootCategoryName': '47cc4a03-b21d-47b6-90f7-f748ed69aa23',
			'CommissionRate': 0,
			'VendorNameRedu': '爱买全球科技（南通）有限公司',
			'IsVipPackage': false
		}
	}
}
                                    ";

            var parallel = new XParallelTest();
            parallel.IsLookTask = IsLookTask;
            parallel.IsSubTask = true;
            parallel.IsHttpFunc = true;
            parallel.Token = token2;
            parallel.URL = "http://api.st.maimaiba.shop/cart/api/CustomerCart/AddToBasket";
            parallel.RequestMethod = "POST";
            parallel.JsonContent = json;
            parallel.ApiDebug();
        }
        private void HttpAsyncTest3()
        {
            var json = @"
{
	'Quantity': 2,

    'skuId': '08d5d673-60e3-fd87-ea86-6a1efc37c521',
	'SKU': {
                'ProductId': '00000000-0000-0000-0000-000000000000',
		'SkuName': '白色系 1.7L    F-EKS17A',
		'Inventory': 100,
		'AlertInventory': 0,
		'SellPrice': 880,
		'OrigPrice': 0,
		'SellCount': 0,
		'SellerCode': 'JJ-1000002',
		'AttrsString': '08d5946d-3bd7-1ffe-ef19-64861a88e218;08d59484-d80e-ae03-d338-9d5384af3cf9;08d5948a-3aeb-51b2-11af-1b2fab4f0e7c',
		'Image1': '00000000-0000-0000-0000-000000000000',
		'VersionStamp': 'a660284f-8f0a-4263-ba0c-8d4514e12482',
		'Commission': 287.5,
		'SkuAttrValues': [{
			'ProductId': '08d5d673-60e3-f3de-fecf-11430cfda7aa',
			'SkuId': '08d5d673-60e3-fd87-ea86-6a1efc37c521',
			'IsSkuValue': true,
			'AttrId': '08d5946d-3bd7-1ffe-ef19-64861a88e218',
			'ValueType': 'SystValue',
			'AttriInputType': 'Option',
			'AttrValueId': '08d5946d-3bd7-613c-c190-702017088067',
			'CategGroupIdSnapshot': '5be1e1d6-f691-4fdf-42c8-08d5905ae0a4',
			'CategGroupNameSnapshot': '规格属性',
			'AttrNameSnapshot': '颜色',
			'AttrValueStrSnapshot': '白色系',
			'IsPictureValue': false,
			'PrivateAttrInputType': 'Input',
			'Id': '08d5d673-60e4-3880-8795-7d31179105ea',
			'IsEnable': true

        }, {
			'ProductId': '08d5d673-60e3-f3de-fecf-11430cfda7aa',
			'SkuId': '08d5d673-60e3-fd87-ea86-6a1efc37c521',
			'IsSkuValue': true,
			'AttrId': '08d59484-d80e-ae03-d338-9d5384af3cf9',
			'ValueType': 'CustOptionValue',
			'AttriInputType': 'Option',
			'AttrValueId': '08d5d673-60e3-f909-e1fd-9a22a1116ad5',
			'CategGroupIdSnapshot': '5be1e1d6-f691-4fdf-42c8-08d5905ae0a4',
			'CategGroupNameSnapshot': '规格属性',
			'AttrNameSnapshot': '容量',
			'AttrValueStrSnapshot': '1.7L  ',
			'IsPictureValue': false,
			'PrivateAttrInputType': 'Input',
			'Id': '08d5d673-60e4-6db8-4461-25f9846c0673',
			'IsEnable': true
		}, {
			'ProductId': '08d5d673-60e3-f3de-fecf-11430cfda7aa',
			'SkuId': '08d5d673-60e3-fd87-ea86-6a1efc37c521',
			'IsSkuValue': true,
			'AttrId': '08d5948a-3aeb-51b2-11af-1b2fab4f0e7c',
			'ValueType': 'CustOptionValue',
			'AttriInputType': 'Option',
			'AttrValueId': '08d5d673-60e3-fb15-a2e8-d0b01d43b9b4',
			'CategGroupIdSnapshot': '5be1e1d6-f691-4fdf-42c8-08d5905ae0a4',
			'CategGroupNameSnapshot': '规格属性',
			'AttrNameSnapshot': '型号',
			'AttrValueStrSnapshot': ' F-EKS17A',
			'IsPictureValue': false,
			'PrivateAttrInputType': 'Input',
			'Id': '08d5d673-60e4-a1be-583c-900942b9c96d',
			'IsEnable': true
		}],
		'Weight': 0,
		'Id': '08d5d673-60e3-fd87-ea86-6a1efc37c521',
		'IsEnable': false,
		'Product': {
			'VendorId': '08d5d294-6c3a-0268-1578-0b8f9972ad50',
			'MainImageGuidsList': ['08d5d673-243b-ab47-6f5a-c29f79493328', '08d5d673-243b-ef6f-3d0c-856cb49fb61b', '08d5d673-2439-0e6c-e52d-2b2cdfcadcae', '08d5d673-314e-b46f-f835-54d420949328', '08d5d673-3150-311e-d464-ba03b437b359'],
			'Id': '08d5d673-60e3-f3de-fecf-11430cfda7aa',
			'ProdcutName': '\'艾尔芬电热水壶    F-EKS17A \'',
			'RootCategoryName': '981d76dd-add0-456f-ac6d-a42787f433ff',
			'CommissionRate': 0,
			'VendorNameRedu': '广东伽伽那家居用品有限公司',
			'IsVipPackage': false
		}
	}
}
                                    ";

            var parallel = new XParallelTest();
            parallel.IsLookTask = IsLookTask;
            parallel.IsSubTask = true;
            parallel.IsHttpFunc = true;
            parallel.Token = token2;
            parallel.URL = "http://api.st.maimaiba.shop/cart/api/CustomerCart/AddToBasket";
            parallel.RequestMethod = "POST";
            parallel.JsonContent = json;
            parallel.ApiDebug();
        }
        private None test(None none)
        {
            HttpAsyncTest3();
            HttpAsyncTest2();
            HttpAsyncTest1();
            return none;
        }

        public void HttpApiTest()
        {
            var parallel = new XParallelTest();
            parallel.IsLookTask = IsLookTask;
            parallel.IsSubTask = false;
            parallel.Request = new None();
            parallel.Response = new None();
            parallel.TargetFunc = new HttpTest().test;
            //parallel.ApiDebug();
            //parallel.Parallel_100_10000();
            //parallel.Parallel_90_10000();
            //parallel.Parallel_80_10000();
            //parallel.Parallel_70_10000();
            //parallel.Parallel_60_10000();
            //parallel.Parallel_50_10000();
            //parallel.Parallel_40_10000();
            //parallel.Parallel_30_10000();
            parallel.Parallel_20_10000();
            //parallel.Parallel_10_10000();
            //parallel.Parallel_5_10000();
            //parallel.Parallel_1_10000();
        }
    }
}
