using HPC.DAL;
using System;

namespace MyDAL.Test.Entities.MyDAL_TestDB
{
    /*
     * CREATE TABLE `addressinfo` (
     * `Id` char(36) NOT NULL,
     * `CreatedOn` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
     * `ContactName` longtext,
     * `ContactPhone` longtext,
     * `DetailAddress` longtext,
     * `IsDefault` bit(1) NOT NULL,
     * `UserId` char(36) NOT NULL,
     * PRIMARY KEY (`Id`)
     * ) ENGINE=InnoDB DEFAULT CHARSET=utf8
     */
    [XTable(Name ="AddressInfo")]
    public class AddressInfo
    {
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ContactName { get; set; }
        
        public string ContactPhone { get; set; }
        
        public string DetailAddress { get; set; }

        public bool IsDefault { get; set; }

        public Guid UserId { get; set; }
    }
}
