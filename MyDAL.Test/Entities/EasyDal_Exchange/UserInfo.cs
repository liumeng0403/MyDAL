using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyDAL.Test.Entities.EasyDal_Exchange
{
    /*
     * CREATE TABLE `userinfo` (
     * `Id` char(36) NOT NULL,
     * `CreatedOn` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
     * `Name` longtext,
     * `Phone` longtext,
     * `RootUser` bit(1) NOT NULL,
     * `InvitedCount` int(11) NOT NULL,
     * `PathId` longtext,
     * `SuperiorId` char(36) NOT NULL,
     * `ArrangePathId` longtext,
     * `ArrangeSuperiorId` char(36) NOT NULL,
     * `IsVIP` bit(1) NOT NULL,
     * `IsActived` bit(1) NOT NULL,
     * PRIMARY KEY (`Id`)
     * ) ENGINE=InnoDB DEFAULT CHARSET=utf8
     */
    [Table("UserInfo")]
    public class UserInfo
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        
        public string PathId { get; set; }

        public string Phone { get; set; }

        public string Name { get; set; }

        public Guid SuperiorId { get; set; }

        public string ArrangePathId { get; set; }

        public Guid ArrangeSuperiorId { get; set; }

        public bool RootUser { get; set; } = false;
        
        public int InvitedCount { get; set; }
        
        public bool IsVIP { get; set; }
        
        public bool IsActived { get; set; }
    }
}
