using Microsoft.AspNetCore.Identity;
using MyDAL.Test.Enums;
using System;

namespace MyDAL.Test.Entities.MyDAL_TestDB
{
    /*
     * CREATE TABLE `aspnetusers` (
     *   `Id` char(36) NOT NULL,
     *   `UserName` varchar(256) DEFAULT NULL,
     *   `NormalizedUserName` varchar(256) DEFAULT NULL,
     *   `Email` varchar(256) DEFAULT NULL,
     *   `NormalizedEmail` varchar(256) DEFAULT NULL,
     *   `EmailConfirmed` bit(1) NOT NULL,
     *   `PasswordHash` longtext,
     *   `SecurityStamp` longtext,
     *   `ConcurrencyStamp` longtext,
     *   `PhoneNumber` longtext,
     *   `PhoneNumberConfirmed` bit(1) NOT NULL,
     *   `TwoFactorEnabled` bit(1) NOT NULL,
     *   `LockoutEnd` datetime(6) DEFAULT NULL,
     *   `LockoutEnabled` bit(1) NOT NULL,
     *   `AccessFailedCount` int(11) NOT NULL,
     *   `AgentId` char(36) DEFAULT NULL,
     *   `CustomerServiceId` char(36) DEFAULT NULL,
     *   `NickName` longtext,
     *   `InviterId` char(36) NOT NULL,
     *   `InvitedCount` int(11) NOT NULL,
     *   `PathId` longtext,
     *   `RootUser` bit(1) NOT NULL,
     *   `AgentLevel` int(11) DEFAULT NULL,
     *   `CrmUserId` longtext,
     *   `WxOpenId` longtext,
     *   `IsRealNameAuthed` bit(1) NOT NULL,
     *   `IsImport` bit(1) NOT NULL,
     *   PRIMARY KEY (`Id`),
     *   UNIQUE KEY `UserNameIndex` (`NormalizedUserName`) USING BTREE,
     *   KEY `EmailIndex` (`NormalizedEmail`) USING BTREE
     * ) ENGINE=InnoDB DEFAULT CHARSET=utf8
     */
    [XTable(Name = "AspNetUsers")]
    public class AspnetUsers : IdentityUser<Guid>
    {
        public Guid? AgentId { get; set; }
        public Guid? CustomerServiceId { get; set; }
        public string NickName { get; set; }
        public Guid InviterId { get; set; }
        public int InvitedCount { get; set; }
        public string PathId { get; set; }
        public bool RootUser { get; set; } = false;
        public AgentLevel? AgentLevel { get; set; }
        public string CrmUserId { get; set; }
        public bool IsRealNameAuthed { get; set; } = false;
        public bool IsImport { get; set; }
    }
}
