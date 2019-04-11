using System.Reflection;

namespace HPC.DAL.Core.Common
{
    // mydal:2019-04-11:final
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
        internal XColumnAttribute ColAttr { get; set; }
        internal XTableAttribute TbAttr { get; set; }
    }
}
