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
        internal List<PagingOptionCache> GetPagingOption(Type queryT)
        {
            var key = GetModelKey(queryT.FullName);
            return PagingCache.GetOrAdd(key, k =>
            {
                var opt = new List<PagingOptionCache>();
                var ps = queryT.GetProperties(XConfig.ClassSelfMember);
                foreach (var p in ps)
                {
                    var pc = new PagingOptionCache();
                    if (!(DC.AH.GetAttribute<XQueryAttribute>(queryT, p) is XQueryAttribute qa))
                    {
                        if (DC.Crud == CrudEnum.Join)
                        {
                            throw new Exception($"[[{queryT.Name}.{p.Name}]] 必须用 [XQuery] Attribute 标记出 'Table =' 以表明该字段所属的DB表!!!");
                        }
                        else
                        {
                            pc.Attr = new XQueryAttribute
                            {
                                Table = DC.TbM1,
                                TableAlias = string.Empty
                            };
                            var tbm1 = DC.XC.GetTableModel(DC.TbM1);
                            var col1 = tbm1.PCAs.FirstOrDefault(it =>
                            {
                                return it.ColName.Equals(p.Name, StringComparison.OrdinalIgnoreCase) || it.PropName.Equals(p.Name, StringComparison.OrdinalIgnoreCase);
                            });
                            if (col1 == null)
                            {
                                throw new Exception($"属性 [[{queryT.Name}.{p.Name}]] 必须用 [XQuery] Attribute 中 'Column =' 指明与 DB-Table 对应的列!!!");
                            }
                            pc.Attr.Column = col1.Col.ColumnName;
                            pc.Attr.Compare = CompareEnum.Equal;
                        }
                    }
                    else
                    {
                        pc.Attr = new XQueryAttribute();
                        if (DC.Crud == CrudEnum.Join
                            && qa.Table == null)
                        {
                            throw new Exception($"属性 [[{queryT.Name}.{p.Name}]] 上的 [XQuery] Attribute 中必须指明 'Table =' , 以表明该字段所属的DB表!!!");
                        }
                        else if (DC.Crud != CrudEnum.Join
                                    && qa.Table == null)
                        {
                            pc.Attr.Table = DC.TbM1;
                        }
                        else
                        {
                            pc.Attr.Table = qa.Table;
                        }
                        pc.Attr.TableAlias = qa.TableAlias;
                        var colName = qa.Column.IsNullStr() ? p.Name : qa.Column;
                        var tbm2 = DC.XC.GetTableModel(pc.Attr.Table);
                        var col2 = tbm2.PCAs.FirstOrDefault(it =>
                        {
                            return it.ColName.Equals(colName, StringComparison.OrdinalIgnoreCase) || it.PropName.Equals(colName, StringComparison.OrdinalIgnoreCase);
                        });
                        if (col2 == null)
                        {
                            throw new Exception($"属性 [[{queryT.Name}.{p.Name}]] 必须用 [XQuery] Attribute 中 'Column =' 指明与 DB-Table 对应的列!!!");
                        }
                        pc.Attr.Column = col2.Col.ColumnName;
                        if (qa.Compare == CompareEnum.None)
                        {
                            pc.Attr.Compare = CompareEnum.Equal;
                        }
                        else
                        {
                            pc.Attr.Compare = qa.Compare;
                        }
                    }
                    pc.TbMType = pc.Attr.Table;
                    var tbm3 = DC.XC.GetTableModel(pc.Attr.Table);
                    var col3 = tbm3.PCAs.FirstOrDefault(it =>
                    {
                        return it.ColName.Equals(pc.Attr.Column, StringComparison.OrdinalIgnoreCase) || it.PropName.Equals(pc.Attr.Column, StringComparison.OrdinalIgnoreCase);
                    });
                    if (col3 == null)
                    {
                        throw new Exception($"属性 [[{queryT.Name}.{p.Name}]] 对应的表字段 [[{tbm3.TbName}.{pc.Attr.Column}]] 在实体类 [[{pc.TbMType.Name}]] 中没有找到对应的属性!!!");
                    }
                    pc.TbMProp = col3.Prop;
                    pc.TbName = tbm3.TbName;
                    pc.PgType = queryT;
                    pc.PgProp = p;
                    pc.TbCol = pc.Attr.Column;
                    pc.Compare = pc.Attr.Compare;
                    opt.Add(pc);
                }
                return opt;
            });
        }

    }
}
