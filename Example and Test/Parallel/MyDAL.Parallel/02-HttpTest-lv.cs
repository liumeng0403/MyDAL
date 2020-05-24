using MyDAL.Test.Parallels;

namespace MyDAL.Parallel
{
    public class _02_HttpTest_lv
    {

        private void HttpAsyncTest1()
        {
            var IsLookTask = true;       
            var json = @"
{'reqDtos':{'tempOrderId':'04e63a8afcd04aa6b05b335663a27f20','adultNum':1,'childNum':0,'babyNum':0,'elderNum':0,'departureDate':'2019 - 11 - 22','passengerDTOs':[{'passengerType':1,'namech':'测试下单','nameen':'','gender':1,'phone':'13066913601','documenttype':'1','documentno':'110102198901019551','birthday':'1993 - 08 - 31'}],'ticketPassengerDTO':{'passengerType':1,'namech':'测试下单','nameen':'','gender':1,'phone':'13066913601','documenttype':'1','documentno':'110102198901019551','birthday':'1993 - 08 - 31'},'orderContactsDTO':{'name':'测试下单','phone':'13482036580','email':'ww@126.com'},'bookPriceTotal':'1199','totalPrice':1199,'promotionFlag':false,'deviceFingerprint':'FFI9gWlE8 - EJhU5Fq_cjZ_hPPRjGhja_','remark':'','orderInvoiceDTO':{},'orderInsuranceDTOS':[],'orderBoardPositionDTOS':[],'typeOrigin':1,'utm':'','isSupportBaitiao':0},'lvsign':'3F647B30638D2A5BC2108153A9368BD311DAD2C1','lvsessionid':'d6c53697 - e123 - 4fcc - bc5f - 99c25b3e35d7_18411598'}
                                    ";

            var parallel = new XParallelTest
            {
                IsLookTask = IsLookTask,
                IsSubTask = true,
                IsHttpFunc = true,
                URL = "http://vacationstest.lvmama.com/booking/w/order/submitorder",
                RequestMethod = "POST",
                JsonContent = json
            };
            parallel.ApiDebug();
        }

        private None Test(None none)
        {
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
                TargetFunc = () => new _02_HttpTest_lv().Test
            };
            parallel.ApiDebug();
            //parallel.Parallel_100_10000();
            //parallel.Parallel_90_10000();
            //parallel.Parallel_80_10000();
            //parallel.Parallel_70_10000();
            //parallel.Parallel_60_10000();
            //parallel.Parallel_50_10000();
            //parallel.Parallel_40_10000();
            //parallel.Parallel_30_10000();
            //parallel.Parallel_20_10000();
            //parallel.Parallel_10_10000();
            //parallel.Parallel_5_10000();
            //parallel.Parallel_1_10000();
        }

    }
}
