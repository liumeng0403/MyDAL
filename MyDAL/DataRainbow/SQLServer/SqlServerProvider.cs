using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyDAL.DataRainbow.SQLServer
{
    internal sealed class SqlServerProvider
       : SqlServer, ISqlProvider
    {
        async Task<List<ColumnInfo>> ISqlProvider.GetColumnsInfos(string tableName)
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
            return await DC.DS.ExecuteReaderMultiRowAsync<ColumnInfo>();
        }

        void ISqlProvider.GetSQL()
        {
            throw new NotImplementedException();
        }
    }
}
