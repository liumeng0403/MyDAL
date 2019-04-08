using System.Reflection;

namespace HPC.DAL.Core.Common
{
    internal class TmPropColAttrInfo
    {
        internal string PropName
        {
            get
            {
                return Prop.Name;
            }
        }
        internal PropertyInfo Prop { get; set; }
        internal string ColName
        {
            get
            {
                return Col.ColumnName;
            }
        }
        internal ColumnInfo Col { get; set; }
        internal XColumnAttribute Attr { get; set; }
    }
}
