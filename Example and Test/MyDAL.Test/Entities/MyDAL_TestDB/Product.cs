using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDAL.Test.Entities.MyDAL_TestDB
{
    /*
     * CREATE TABLE `product` (
     *   `Id` char(36) NOT NULL,
     *   `CreatedOn` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
     *   `Title` longtext NOT NULL,
     *   `Specification` longtext NOT NULL,
     *   `Description` longtext NOT NULL,
     *   `ProductDetails` longtext NOT NULL,
     *   `UnitPrice` decimal(65,30) NOT NULL,
     *   `VipProduct` bit(1) NULL,
     *   PRIMARY KEY (`Id`)
     * ) ENGINE=InnoDB DEFAULT CHARSET=utf8;
     */
    [XTable(Name ="Product")]
    public class Product 
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        
        public string Title { get; set; }
        
        public string Specification { get; set; }
        
        public string Description { get; set; }
        
        public string ProductDetails { get; set; }
        
        public decimal UnitPrice { get; set; }
        
        public bool? VipProduct { get; set; }
    }
}
