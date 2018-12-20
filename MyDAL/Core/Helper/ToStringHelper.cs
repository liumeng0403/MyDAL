using System;
using System.Collections.Generic;
using System.Text;

namespace MyDAL.Core.Helper
{
    internal class ToStringHelper
    {
        internal string DateTime(string format)
        {
            var fcs = format.Trim();
            if ("yyyy-MM-dd".Equals(fcs, StringComparison.OrdinalIgnoreCase))
            {
                return "%Y-%m-%d";
            }
            else if ("yyyy-MM".Equals(fcs, StringComparison.OrdinalIgnoreCase))
            {
                return "%Y-%m";
            }
            else if ("yyyy".Equals(fcs, StringComparison.OrdinalIgnoreCase))
            {
                return "%Y";
            }
            else
            {
                throw new Exception($"{XConfig.EC._001} -- [[{fcs}]] 未能解析!!!");
            }            
        }
    }
}
