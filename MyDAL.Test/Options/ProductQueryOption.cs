using HPC.DAL;

namespace MyDAL.Test.Options
{
    public class ProductQueryOption 
        : PagingOption
    {
        public bool? VipProduct { get; set; }
    }
}
