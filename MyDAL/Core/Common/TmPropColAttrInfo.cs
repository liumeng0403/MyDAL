using System.Reflection;

namespace MyDAL.Core.Common
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
        /// <summary>
        /// XColumn
        /// </summary>
        internal XColumnAttribute ColAttr { get; set; }
        internal XTableAttribute TbAttr { get; set; }
    }
}
