using MyDAL.AdoNet;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Helper;
using MyDAL.Core.Models.Cache;
using MyDAL.ModelTools;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace MyDAL.Core
{
    internal class XCache
    {
        private int GetColumnHash(IDataReader reader)
        {
            unchecked
            {
                int max = reader.FieldCount;
                int hash = max;
                for (int i = 0; i < max; i++)
                {
                    var col = reader.GetName(i);
                    hash = (-79 * ((hash * 31) + (col?.GetHashCode() ?? 0))) + (reader.GetFieldType(i)?.GetHashCode() ?? 0);
                }
                return hash;
            }
        }

        private static ConcurrentDictionary<string, Assembly> AssemblyCache { get; } = new ConcurrentDictionary<string, Assembly>();
        private static ConcurrentDictionary<string, IRow> ModelRowCache { get; } = new ConcurrentDictionary<string, IRow>();
        private static ConcurrentDictionary<string, TableModelCache> TableCache { get; } = new ConcurrentDictionary<string, TableModelCache>();

        /*****************************************************************************************************************************************************/

        private Context DC { get; set; }
        internal XCache(Context dc)
        {
            DC = dc;
        }

        /*****************************************************************************************************************************************************/

        internal string GetModelKey(string mFullName)
        {
            return $"{mFullName}:{DC.Conn.Database}:{DC.DB.ToString()}";
        }
        internal string GetHandleKey(int sqlHash, int colHash, string mFullName)
        {
            return $"{DC.Conn.GetType().FullName}:{sqlHash}:{colHash}:{GetModelKey(mFullName)}";
        }

        /*****************************************************************************************************************************************************/

        internal static ConcurrentDictionary<Type, RowMap> TypeMaps { get; } = new ConcurrentDictionary<Type, RowMap>();

        internal Func<IDataReader, M> GetHandle<M>(string sql, IDataReader reader)
        {
            var key = GetHandleKey(sql.GetHashCode(), GetColumnHash(reader), typeof(M).FullName);
            return ((Row<M>)ModelRowCache.GetOrAdd(key, k => IL<M>.Row(reader))).Handle;
        }
        internal Func<IDataReader, object> GetHandle(string sql, IDataReader reader, Type mType)
        {
            var key = GetHandleKey(sql.GetHashCode(), GetColumnHash(reader), mType.FullName);
            return ((Row)ModelRowCache.GetOrAdd(key, k => IL.Row(reader, mType))).Handle;
        }

        internal TableModelCache GetTableModel(Type t)
        {
            var key = GetModelKey(t.FullName);
            return TableCache.GetOrAdd(key, k =>
             {
                 var tm = new TableModelCache();
                 tm.TbMType = t;
                 var ta = DC.AH.GetAttribute<XTableAttribute>(t) as XTableAttribute;
                 if (ta == null)
                 {
                     throw new Exception($"类 [[{t.FullName}]] 必须是与 DB Table 对应的实体类,并且要由 [XTable] 标记指定类对应的表名!!!");
                 }
                 tm.TbMProps = DC.GH.GetPropertyInfos(t);
                 tm.TbCols = DC.SqlProvider.GetColumnsInfos(ta.Name);
                 if (tm.TbCols == null
                    || tm.TbCols.Count <= 0)
                 {
                     throw new Exception($"表 [[{DC.Conn.Database}.{ta.Name}]] 中不存在任何列!!!");
                 }
                 tm.TbMAttr = new XTableAttribute { Name = tm.TbCols.First().TableName };
                 var list = new List<TmPropColAttrInfo>();
                 foreach (var p in tm.TbMProps)
                 {
                     var pca = new TmPropColAttrInfo();
                     pca.Prop = p;
                     var pa = DC.AH.GetAttribute<XColumnAttribute>(t, p) as XColumnAttribute;
                     if (pa == null
                        || pa.Name.IsNullStr())
                     {
                         pca.Col = tm.TbCols.FirstOrDefault(it => it.ColumnName.Equals(p.Name, StringComparison.OrdinalIgnoreCase));
                         if (pca.Col == null)
                         {
                             throw new Exception($"属性 [[{t.Name}.{p.Name}]] 在表 [[{DC.Conn.Database}.{tm.TbName}]] 中无对应的列!!!");
                         }
                     }
                     else
                     {
                         pca.Col = tm.TbCols.FirstOrDefault(it => it.ColumnName.Equals(pa.Name, StringComparison.OrdinalIgnoreCase));
                         if (pca.Col == null)
                         {
                             throw new Exception($"属性 [[{t.Name}.{p.Name}]] 上 [XColumn] 标注的字段名 [[{pa.Name}]] 有误!!!");
                         }
                     }
                     pca.Attr = new XColumnAttribute { Name = pca.Col.ColumnName };
                     list.Add(pca);
                 }
                 tm.PCAs = list;
                 return tm;
             });
        }
        internal TableModelCache GetTableModel(string key)
        {
            return DC.AR.Invoke(key, p => TableCache[p]);
        }

    }
}
