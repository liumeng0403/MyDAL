using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDAL.Test.Entities.MyDAL_TestDB
{
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
