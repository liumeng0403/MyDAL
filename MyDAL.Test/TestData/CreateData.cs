using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MyDAL.Test.TestData
{
    public class CreateData
    {
        public async Task<List<AddressInfo>> PreCreateBatch(IDbConnection Conn)
        {
            var res1 = await Conn
                .Deleter<AddressInfo>()
                .Where(a => true)
                .DeleteAsync();

            var list = new List<AddressInfo>();
            for (var i = 0; i < 10; i++)
            {
                if (i % 2 == 0)
                {
                    list.Add(new AddressInfo
                    {
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTime.Now,
                        ContactName = "Name_" + i.ToString(),
                        ContactPhone = "1800000000" + i.ToString(),
                        DetailAddress = "Address_" + i.ToString(),
                        IsDefault = true,   // f:bool c:bit(1)
                        UserId = Guid.NewGuid()
                    });
                }
                else
                {
                    list.Add(new AddressInfo
                    {
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTime.Now,
                        ContactName = "Name_" + i.ToString(),
                        ContactPhone = "1800000000" + i.ToString(),
                        DetailAddress = "Address_" + i.ToString(),
                        IsDefault = false,   // f:bool c:bit(1)
                        UserId = Guid.NewGuid()
                    });
                }
            }
            return list;
        }
    }
}
