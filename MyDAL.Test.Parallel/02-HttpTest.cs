using MyDAL.Test.Parallels;

namespace MyDAL.Test.QueryParallel
{
    public class HttpTest
    {
        private void HttpAsyncTest1()
        {
            var IsLookTask = true;
            var token = "dGl0elIjpbIkVS6YcL-i-XgXePWvSyZX0woqGcSy-j3HmXPEOFX08z9LsChSsaB8sCBbB-w8ujXKvEWBUtCz9rVbFn5GJcI5w";
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

            var parallel = new XParallelTest
            {
                IsLookTask = IsLookTask,
                IsSubTask = true,
                IsHttpFunc = true,
                Token = token,
                URL = "http://api.st.maimaiba.shop/Ordering/api/CustomerOrder/AddOrders",
                RequestMethod = "POST",
                JsonContent = json
            };
            parallel.ApiDebug();
        }
        public void HttpAsyncTest2()
        {
            var IsLookTask = true;
            var token = "dGl0elIjpbIkVS6YcL-i-XgXePWvSyZX0woqGcSy-j3HmXPEOFX08z9LsChSsaB8sCBbB-w8ujXKvEWBUtCz9rVbFn5GJcI5w";
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
			'MainImageGuidsList': ['08d5a65b-92c1-166c-05b7-08767b024f5f', '08d5a65b-92c1-8f76-c149-bef3597dbd43'],
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

            var parallel = new XParallelTest
            {
                IsLookTask = IsLookTask,
                IsSubTask = true,
                IsHttpFunc = true,
                Token = token,
                URL = "http://api.st.maimaiba.shop/cart/api/CustomerCart/AddToBasket",
                RequestMethod = "POST",
                JsonContent = json
            };
            parallel.ApiDebug();
        }
        private void HttpAsyncTest3()
        {
            var IsLookTask = true;
            var token = "dGl0elIjpbIkVS6YcL-i-XgXePWvSyZX0woqGcSy-j3HmXPEOFX08z9LsChSsaB8sCBbB-w8ujXKvEWBUtCz9rVbFn5GJcI5w";
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
			'MainImageGuidsList': ['08d5d673-314e-b46f-f835-54d420949328', '08d5d673-3150-311e-d464-ba03b437b359'],
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

            var parallel = new XParallelTest
            {
                IsLookTask = IsLookTask,
                IsSubTask = true,
                IsHttpFunc = true,
                Token = token,
                URL = "http://api.st.maimaiba.shop/cart/api/CustomerCart/AddToBasket",
                RequestMethod = "POST",
                JsonContent = json
            };
            parallel.ApiDebug();
        }
        private None Test(None none)
        {
            HttpAsyncTest3();
            HttpAsyncTest2();
            HttpAsyncTest1();
            return none;
        }

        public void HttpApiTest()
        {
            var IsLookTask = true;
            var parallel = new XParallelTest
            {
                IsLookTask = IsLookTask,
                IsSubTask = false,
                Request = new None(),
                Response = new None(),
                TargetFunc = new HttpTest().Test
            };
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
