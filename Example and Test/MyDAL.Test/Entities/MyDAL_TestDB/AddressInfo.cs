using System;

namespace MyDAL.Test.Entities.MyDAL_TestDB
{
    [XTable(Name ="AddressInfo")]
    public class AddressInfo
    {
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ContactName { get; set; }
        
        public string ContactPhone { get; set; }
        
        public string DetailAddress { get; set; }

        public bool? IsDefault { get; set; }

        public Guid UserId { get; set; }
    }
}
