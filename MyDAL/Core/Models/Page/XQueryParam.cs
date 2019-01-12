using MyDAL.Core.Common;
using MyDAL.Core.Models.ExpPara;

namespace MyDAL.Core.Models.Page
{
    internal class XQueryParam
    {
        internal ColumnParam Cp { get; set; }
        internal ValueInfo Val { get; set; }
        internal CompareEnum Compare { get; set; }
    }
}
