using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyDAL.Test.Entities.MyDAL_TestDB
{
    [XTable(Name ="UserInfo")]
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
