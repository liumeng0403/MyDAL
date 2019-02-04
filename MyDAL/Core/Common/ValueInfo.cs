﻿using MyDAL.Core.Bases;
using MyDAL.Core.Enums;

namespace MyDAL.Core.Common
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
                throw dc.Exception(XConfig.EC._026, $"{likeType.ToString()}");
            }
        }
    }
}
