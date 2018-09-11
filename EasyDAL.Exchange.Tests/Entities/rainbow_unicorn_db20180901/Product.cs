using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EasyDAL.Exchange.Tests.Entities.rainbow_unicorn_db20180901
{
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
