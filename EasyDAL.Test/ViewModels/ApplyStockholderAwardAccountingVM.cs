using MyDAL.Test.Enums;
using System.ComponentModel.DataAnnotations;

namespace MyDAL.Test.ViewModels
{
    public class ApplyStockholderAwardAccountingVM
    {
        [Display(Name = "年")]
        public int Year { get; set; }

        [Display(Name = "月")]
        public Month Month { get; set; }
    }
}
