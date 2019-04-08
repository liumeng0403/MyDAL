using HPC.DAL;
using Microsoft.AspNetCore.Identity;
using System;

namespace MyDAL.Test.Entities.MyDAL_TestDB
{
    /*
     * CREATE TABLE `aspnetuserroles` (
     *   `UserId` char(36) NOT NULL,
     *   `RoleId` char(36) NOT NULL,
     *   PRIMARY KEY (`UserId`,`RoleId`),
     *   KEY `IX_AspNetUserRoles_RoleId` (`RoleId`) USING BTREE,
     *   CONSTRAINT `aspnetuserroles_ibfk_1` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
     *   CONSTRAINT `aspnetuserroles_ibfk_2` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
     * ) ENGINE=InnoDB DEFAULT CHARSET=utf8
     */
    [XTable(Name = "AspNetUserRoles")]
    public class AspnetUserRoles : IdentityUserRole<Guid>
    {
    }
}
