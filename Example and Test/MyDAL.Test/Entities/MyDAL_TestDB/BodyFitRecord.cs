
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDAL.Test.Entities.MyDAL_TestDB
{
    [XTable(Name ="BodyFitRecord")]
    public class BodyFitRecord
    {
        public Guid Id { get; set; }
        [XColumn(Name = "CreatedOn_col")]
        public DateTime CreatedOn { get; set; }
        public Guid UserId { get; set; }
        public string BodyMeasureProperty { get; set; }
    }
}
