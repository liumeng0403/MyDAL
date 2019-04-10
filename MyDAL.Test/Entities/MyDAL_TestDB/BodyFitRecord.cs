
using HPC.DAL;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDAL.Test.Entities.MyDAL_TestDB
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
