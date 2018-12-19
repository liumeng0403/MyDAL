using MyDAL.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyDAL.Core.Common
{
    internal class ContainsLikeParam
    {
        internal bool Flag { get; set; }
        internal StringLikeEnum Like { get; set; }
    }
}
