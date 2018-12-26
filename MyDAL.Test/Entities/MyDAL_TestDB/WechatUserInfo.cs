using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDAL.Test.Entities.MyDAL_TestDB
{

    /*
     *CREATE TABLE `wechatuserinfo` (
     * `Id` char(36) NOT NULL,
     * `CreatedOn` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
     * `AppId` longtext,
     * `OpenId` longtext,
     * `UserId` char(36) NOT NULL,
     * `Nickname` longtext,
     * `Sex` int(11) NOT NULL,
     * `Province` longtext,
     * `City` longtext,
     * `Country` longtext,
     * `Headimgurl` longtext,
     * `Privilege` longtext,
     * `UnionId` longtext,
     * PRIMARY KEY (`Id`)
     * ) ENGINE=InnoDB DEFAULT CHARSET=utf8
     */
    [Table("WechatUserInfo")]
    public class WechatUserInfo 
    {
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }
        
        public string AppId { get; set; }
        
        public string OpenId { get; set; }
        
        public Guid UserId { get; set; }
        
        public string Nickname { get; set; }
        
        public int Sex { get; set; }
        
        public string Province { get; set; }
        
        public string City { get; set; }
        
        public string Country { get; set; }
        
        public string Headimgurl { get; set; }
        
        public string Privilege { get; set; }
        
        public string UnionId { get; set; }
    }
}
