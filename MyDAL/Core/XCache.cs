using MyDAL.AdoNet;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Core.Helper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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
        private static ConcurrentDictionary<string, List<PagingOptionCache>> PagingCache { get; } = new ConcurrentDictionary<string, List<PagingOptionCache>>();

        /*****************************************************************************************************************************************************/

        private Context DC { get; set; }
        internal XCache(Context dc)
        {
            DC = dc;
        }

        /*****************************************************************************************************************************************************/

        internal string GetAssemblyKey(string mFullNameOrNamespace)
        {
            return $"{mFullNameOrNamespace}:{DC.Conn.Database}";
        }
        internal string GetModelKey(string mFullName)
        {
            return $"{mFullName}:{DC.Conn.Database}";
        }
        internal string GetHandleKey(int sqlHash, int colHash, string mFullName)
        {
            return $"{sqlHash}:{colHash}:{GetModelKey(mFullName)}";
        }

        /*****************************************************************************************************************************************************/

        internal Assembly GetAssembly(string key)
        {
            if (!AssemblyCache.TryGetValue(key, out var ass))
            {
                ass = new GenericHelper(DC).LoadAssembly(key.Split(':')[1]);
                AssemblyCache[key] = ass;
            }
            return ass;
        }

        internal static ConcurrentDictionary<Type, RowMap> TypeMaps { get; } = new ConcurrentDictionary<Type, RowMap>();

        internal Func<IDataReader, M> GetHandle<M>(string sql, IDataReader reader)
        {
            var key = GetHandleKey(sql.GetHashCode(), GetColumnHash(reader), typeof(M).FullName);
            if (!ModelRowCache.TryGetValue(key, out var row))
            {
                ModelRowCache[key] = row = IL<M>.Row(reader);
            }
            return ((Row<M>)row).Handle;
        }
        internal Func<IDataReader, object> GetHandle(string sql, IDataReader reader, Type mType)
        {
            var key = GetHandleKey(sql.GetHashCode(), GetColumnHash(reader), mType.FullName);
            if (!ModelRowCache.TryGetValue(key, out var row))
            {
                ModelRowCache[key] = row = IL.Row(reader, mType);
            }
            return ((Row)row).Handle;
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
                 tm.TbCols = DC.SqlProvider.GetColumnsInfos(ta.Name).GetAwaiter().GetResult();
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
        internal List<PagingOptionCache> GetPagingOption(Type qt)
        {
            var key = GetModelKey(qt.FullName);
            if (!PagingCache.TryGetValue(key, out var op))
            {
                var list = new List<PagingOptionCache>();
                var ps = qt.GetProperties(XConfig.ClassSelfMember);
                foreach (var p in ps)
                {
                    var oc = new PagingOptionCache();
                    var xa = DC.AH.GetAttribute<XQueryAttribute>(qt, p) as XQueryAttribute;
                    if (DC.Crud == CrudEnum.Join
                        && xa == null)
                    {
                        throw new Exception($"[[{qt.Name}.{p.Name}]] 必须用 [XQuery] Attribute 标记出 'Table =' 以表明该字段所属的DB表!!!");
                    }
                    if (DC.Crud == CrudEnum.Join
                        && xa?.Table == null)
                    {
                        throw new Exception($"[[{qt.Name}.{p.Name}]] 上的 [XQuery] Attribute 中必须指明 'Table =' , 以表明该字段所属的DB表!!!");
                    }
                    oc.TbType = xa?.Table;
                    var tbName = string.Empty;
                    if (xa?.Table != null)
                    {
                        var tbm = DC.XC.GetTableModel(xa?.Table);
                        tbName = tbm.TbName;
                    }
                    oc.TbName = tbName;
                    oc.PropName = p.Name;
                    if ((xa?.Column.IsNullStr()).ToBool())
                    {
                        xa.Column = p.Name;
                    }
                    oc.ColName = xa.Column; // 检查 这个 col 是 db col 还是 m prop ?
                }
            }
            return op;
        }

    }
}
