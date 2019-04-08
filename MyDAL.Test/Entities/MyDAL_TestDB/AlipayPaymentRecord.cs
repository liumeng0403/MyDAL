using HPC.DAL;
using System;

namespace MyDAL.Test.Entities.MyDAL_TestDB
{
    /*
     * CREATE TABLE `alipaypaymentrecord` (
     *  `Id` char(36) NOT NULL,
     *  `CreatedOn` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
     *  `PaymentRecordId` char(36) NOT NULL,
     *  `OrderId` char(36) NOT NULL,
     *  `TotalAmount` decimal(65,30) NOT NULL,
     *  `Description` longtext,
     *  `PaymentSN` longtext,
     *  `PayedOn` datetime(6) DEFAULT NULL,
     *  `CanceledOn` datetime(6) DEFAULT NULL,
     *  `PaymentUrl` longtext,
     *  PRIMARY KEY (`Id`)
     *) ENGINE=InnoDB DEFAULT CHARSET=utf8
     */
    [XTable(Name = "AlipayPaymentRecord")]     // XTableAttribute 指明 该 Model 对应 DB 中的 table 名.
    public class AlipayPaymentRecord
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid PaymentRecordId { get; set; }
        public Guid OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Description { get; set; }
        public string PaymentSN { get; set; }
        public DateTime? PayedOn { get; set; }
        public DateTime? CanceledOn { get; set; }
        public string PaymentUrl { get; set; }
    }
}
