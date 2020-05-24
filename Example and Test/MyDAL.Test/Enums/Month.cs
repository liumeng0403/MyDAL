using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyDAL.Test.Enums
{
    public enum Month
    {
        [Display(Name = "一月")] January = 1,
        [Display(Name = "二月")] February = 2,
        [Display(Name = "三月")] March = 3,
        [Display(Name = "四月")] April = 4,
        [Display(Name = "五月")] May = 5,
        [Display(Name = "六月")] June = 6,
        [Display(Name = "七月")] July = 7,
        [Display(Name = "八月")] August = 8,
        [Display(Name = "九月")] September = 9,
        [Display(Name = "十月")] October = 10, // 0x0000000A
        [Display(Name = "十一月")] November = 11, // 0x0000000B
        [Display(Name = "十二月")] December = 12, // 0x0000000C
    }
}
