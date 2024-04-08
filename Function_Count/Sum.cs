using MyDAL;
using Mysql.Data_Net6.Tables;
using Mysql.DataNet6;

namespace Function_Count;

[TestClass]
public class Sum
    : Net6Base
{
    [TestMethod]
    public void 单表_指定列()
    {
        
        var res1 = DB_MyDAL_DEV.OpenDebug()
            .Selecter<AlipayPaymentRecord>()
            .Where(it => it.CreatedOn > Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30))
            .SelectOne(it => XFunction.SUM(it.TotalAmount));

        Assert.IsTrue(res1 > 10M);
        
                /*
                 * result:
                   1527.2600000000000000000000000M

                 * no debug:
                   select  sum(`TotalAmount`)
                   from `alipaypaymentrecord`
                   where  `CreatedOn`>?CreatedOn_1
                   limit 0,1;

                 * open debug:
                   select  sum(`TotalAmount`)
                   from `alipaypaymentrecord`
                   where  `CreatedOn`>'2018-07-24 13:36:58.000000'
                   limit 0,1;

                 */
        
    }
}