using Microsoft.AspNetCore.Identity;
using System;

namespace MyDAL.Test.Entities.EasyDal_Exchange
{
    /*
     * CREATE TABLE `aspnetroles` (
     *   `Id` char(36) NOT NULL,
     *   `Name` varchar(256) DEFAULT NULL,
     *   `NormalizedName` varchar(256) DEFAULT NULL,
     *   `ConcurrencyStamp` longtext,
     *   PRIMARY KEY (`Id`),
     *   UNIQUE KEY `RoleNameIndex` (`NormalizedName`) USING BTREE
     * ) ENGINE=InnoDB DEFAULT CHARSET=utf8
     */
    [XTable("AspNetRoles")]
    public class AspnetRoles : IdentityRole<Guid>
    {
    }
}
