using Yunyong.DataExchange.Core.Enums;

namespace MyDAL.Core.Common
{
    internal class ContainsLikeParam
    {
        internal bool Flag { get; set; }
        internal StringLikeEnum Like { get; set; }
    }
}
