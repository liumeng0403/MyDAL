using HPC.DAL.AdoNet;
using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Common;
using HPC.DAL.Core.Enums;
using HPC.DAL.DataRainbow.XCommon;
using HPC.DAL.DataRainbow.XCommon.Interfaces;
using System.Collections.Generic;

namespace HPC.DAL.DataRainbow.SQLServer
{
    internal sealed class SqlServerProvider
       : SqlContext, ISqlProvider
    {
        private SqlServerProvider() { }
        internal SqlServerProvider(Context dc)
        {
            DC = dc;
            DC.SqlProvider = this;
            DbSql = new SqlServer();
        }

        /****************************************************************************************************************/

        List<ColumnInfo> ISqlProvider.GetColumnsInfos(string tableName)
        {
            DC.SQL.Clear();
            DC.SQL.Add($@"
                                                SELECT   obj.name AS TableName,
                                                        col.name AS ColumnName ,
                                                        t.name AS DataType  ,
                                                        ISNULL(comm.text, '') AS ColumnDefault ,
                                                        CASE WHEN col.isnullable = 1 THEN 'Yes'
                                                                ELSE 'No'
                                                        END AS IsNullable,
                                                        ISNULL(ep.[value], '') AS ColumnComment,
                                                        CASE 
					                                                WHEN EXISTS ( 
												                                                SELECT   1
												                                                FROM     dbo.sysindexes si
																                                                INNER JOIN dbo.sysindexkeys sik 
																				                                                ON si.id = sik.id
																							                                                AND si.indid = sik.indid
																                                                INNER JOIN dbo.syscolumns sc 
																				                                                ON sc.id = sik.id
																							                                                AND sc.colid = sik.colid
																                                                INNER JOIN dbo.sysobjects so 
																				                                                ON so.name = si.name
																							                                                AND so.xtype = 'PK'
												                                                WHERE    sc.id = col.id
															                                                AND sc.colid = col.colid 
											                                                ) 
					                                                THEN 'PK'
					                                                ELSE 'Other'
                                                        END AS KeyType
                                                FROM    dbo.syscolumns col
                                                        LEFT  JOIN dbo.systypes t ON col.xtype = t.xusertype
                                                        inner JOIN dbo.sysobjects obj ON col.id = obj.id
                                                                                            AND obj.xtype = 'U'
                                                                                            AND obj.status >= 0
                                                        LEFT  JOIN dbo.syscomments comm ON col.cdefault = comm.id
                                                        LEFT  JOIN sys.extended_properties ep ON col.id = ep.major_id
                                                                                                        AND col.colid = ep.minor_id
                                                                                                        AND ep.name = 'MS_Description'
                                                        LEFT  JOIN sys.extended_properties epTwo ON obj.id = epTwo.major_id
                                                                                                            AND epTwo.minor_id = 0
                                                                                                            AND epTwo.name = 'MS_Description'
                                                WHERE   obj.name = '{tableName.Trim().ToUpper()}'
                                                                or obj.name ='{tableName.Trim().ToLower()}'
                                                                or obj.name ='{tableName.Trim()}'
                                                                or obj.name ='{tableName}'
                                                ORDER BY col.colorder 
                                                ;
                                    ");
            return new DataSourceSync(DC).ExecuteReaderMultiRowForCols<ColumnInfo>();
        }

        void ISqlProvider.GetSQL()
        {
            DC.SQL.Clear();
            switch (DC.Method)
            {
                case UiMethodEnum.Create:
                case UiMethodEnum.CreateBatch:
                    InsertInto(X); Table(); InsertColumn(); Values(X); InsertValue(); End();
                    break;
                case UiMethodEnum.Delete:
                    Delete(X); From(X); Table(); Where(); End();
                    break;
                case UiMethodEnum.Update:
                    Update(X); Table(); Set(X); UpdateColumn(); Where(); End();
                    break;
                case UiMethodEnum.Top:
                case UiMethodEnum.QueryOne:
                case UiMethodEnum.QueryList:
                    Select(X); DistinctX(); DbSql.Top(DC, X); SelectColumn(); From(X); Table(); Where(); OrderBy(); End();
                    break;
                case UiMethodEnum.QueryPaging:
                    Select(X); Count(); From(X); Table(); Where(); CountMulti(); End();
                    Select(X); DistinctX(); SelectColumn(); From(X); Table(); Where(); OrderBy(); DbSql.Pager(DC, X); End();
                    break;
                case UiMethodEnum.IsExist:
                case UiMethodEnum.Count:
                    Select(X); Count(); From(X); Table(); Where(); CountMulti(); End();
                    break;
                case UiMethodEnum.Sum:
                    Select(X); Sum(); From(X); Table(); Where(); End();
                    break;
            }
        }
    }
}
