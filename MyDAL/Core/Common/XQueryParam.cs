namespace MyDAL.Core.Common
{
    internal class XQueryParam
    {
        internal ColumnParam Cp { get; set; }
        internal string Param { get; set; }
        internal ValueInfo Val { get; set; }
        internal string ColType { get; set; }
        internal CompareEnum Compare { get; set; }
    }
}
