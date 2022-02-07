using Microsoft.AspNetCore.Identity;
using MyDAL.Test.Enums;
using System;

namespace MyDAL.Test.Entities.MyDAL_TestDB
{
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
