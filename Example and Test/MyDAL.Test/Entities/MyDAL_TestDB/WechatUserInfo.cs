using System;

namespace MyDAL.Test.Entities.MyDAL_TestDB
{
    [XTable(Name ="WechatUserInfo")]
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
