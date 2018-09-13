using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EasyDAL.Exchange.Tests.Entities.EasyDal_Exchange
{
    /*
     * CREATE TABLE `product` (
     * `Id` char(36) NOT NULL,
     * `CreatedOn` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
     * `Title` longtext NOT NULL,
     * `Specification` longtext NOT NULL,
     * `Description` longtext NOT NULL,
     * `IsPublished` bit(1) NOT NULL,
     * `SnapshotId` char(36) NOT NULL,
     * PRIMARY KEY (`Id`)
     * ) ENGINE=InnoDB DEFAULT CHARSET=utf8
     */
    [Display(Name = "商品信息")]
    [Table("Product")]
    public class Product 
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        
        [Display(Name = "商品标题")]
        [Required]
        public string Title { get; set; }
        
        [Required]
        [Display(Name = "商品规格")]
        public string Specification { get; set; }
        
        [Required]
        [Display(Name = "商品描述")]
        public string Description { get; set; }
        
        [Required]
        [Display(Name = "商品详情页")]
        public string ProductDetails { get; set; }
        
        public decimal UnitPrice { get; set; }
        
        public bool VipProduct { get; set; }
    }
}
