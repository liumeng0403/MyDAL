using MyDAL.AdoNet;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.DataRainbow.XCommon;
using MyDAL.DataRainbow.XCommon.Interfaces;
using System.Collections.Generic;

namespace MyDAL.DataRainbow.MySQL
{
    internal sealed class MySqlProvider
        : SqlContext, ISqlProvider
    {
        private MySqlProvider() { }
        internal MySqlProvider(Context dc)
        {
            DC = dc;
            DC.SqlProvider = this;
            DbSql = new MySql();
        }

        /****************************************************************************************************************/

        List<ColumnInfo> ISqlProvider.GetColumnsInfos(string tableName)
        {
            DC.SQL.Clear();
            DC.SQL.Add($@"
                                            SELECT distinct
                                                TABLE_NAME as TableName,
                                                column_name as ColumnName,
                                                DATA_TYPE as DataType,
                                                column_default as ColumnDefault,
                                                is_nullable AS IsNullable,
                                                column_comment as ColumnComment,
                                                column_key as KeyType
                                            FROM
                                                information_schema.COLUMNS
                                            WHERE  ( 
                                                                table_schema='{DC.XConn.Conn.Database.Trim().ToUpper()}' 
                                                                or table_schema='{DC.XConn.Conn.Database.Trim().ToLower()}' 
                                                                or table_schema='{DC.XConn.Conn.Database.Trim()}' 
                                                                or table_schema='{DC.XConn.Conn.Database}' 
                                                            )
                                                            and  ( 
                                                                        TABLE_NAME = '{tableName.Trim().ToUpper()}' 
                                                                        or TABLE_NAME = '{tableName.Trim().ToLower()}' 
                                                                        or TABLE_NAME = '{tableName.Trim()}' 
                                                                        or TABLE_NAME = '{tableName}' 
                                                                      )
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
                    Select(X); DistinctX(); SelectColumn(); From(X); Table(); Where(); OrderBy(); DbSql.Top(DC, X); End();
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
