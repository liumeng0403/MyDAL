using HPC.DAL.Core.Enums;

namespace HPC.DAL.Core.Models.ExpPara
{
    internal class ContainsLikeParam
    {
        internal bool Flag { get; set; }
        internal StringLikeEnum Like { get; set; }
    }
}
