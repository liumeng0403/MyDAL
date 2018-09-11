
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyDAL.Exchange.Tests.Entities
{

    /*
     * CREATE TABLE `bodyfitrecord` (
     * `Id` char(36) NOT NULL,
     * `CreatedOn` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
     * `UserId` char(36) NOT NULL,
     * `BodyMeasureProperty` longtext,
     * PRIMARY KEY (`Id`)
     * ) ENGINE=InnoDB DEFAULT CHARSET=utf8
     */

    [Table("BodyFitRecord")]
    public class BodyFitRecord
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid UserId { get; set; }
        public string BodyMeasureProperty { get; set; }
    }
}
