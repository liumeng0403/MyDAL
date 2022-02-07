using Microsoft.AspNetCore.Identity;
using System;

namespace MyDAL.Test.Entities.MyDAL_TestDB
{
    [XTable(Name = "AspNetUserRoles")]
    public class AspnetUserRoles : IdentityUserRole<Guid>
    {
    }
}
