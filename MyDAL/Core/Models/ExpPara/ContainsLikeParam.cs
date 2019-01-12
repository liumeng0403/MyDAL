using MyDAL.Core.Enums;

namespace MyDAL.Core.Models.ExpPara
{
    internal class ContainsLikeParam
    {
        internal bool Flag { get; set; }
        internal StringLikeEnum Like { get; set; }
    }
}
