using Microsoft.AspNetCore.Identity;
using System;

namespace MyDAL.Test.Entities.MyDAL_TestDB
{
    [XTable(Name = "AspNetRoles")]
    public class AspnetRoles : IdentityRole<Guid>
    {
    }
}
