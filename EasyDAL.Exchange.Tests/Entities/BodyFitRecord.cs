using EasyDAL.Exchange.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDAL.Exchange.Tests.Entities
{
    [Table(Name = "BodyFitRecord")]
    public class BodyFitRecord
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid UserId { get; set; }
        public string BodyMeasureProperty { get; set; }
    }
}
