using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Configs;
using HPC.DAL.Core.Enums;

namespace HPC.DAL.Core.Common
{
    internal class ValueInfo
    {
        internal object Val { get; set; }
        internal string ValStr { get; set; }

        internal static ValueInfo LikeVI(ValueInfo vi, StringLikeEnum likeType,Context dc)
        {
            if (likeType == StringLikeEnum.Contains)
            {
                return vi;
            }
            else if (likeType == StringLikeEnum.StartsWith)
            {
                return new ValueInfo
                {
                    Val = $"{vi.Val}%",
                    ValStr = string.Empty
                };
            }
            else if (likeType == StringLikeEnum.EndsWith)
            {
                return new ValueInfo
                {
                    Val = $"%{vi.Val}",
                    ValStr = string.Empty
                };
            }
            else
            {
                throw XConfig.EC.Exception(XConfig.EC._026, $"{likeType.ToString()}");
            }
        }
    }
}
