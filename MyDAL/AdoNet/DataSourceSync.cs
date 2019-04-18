using MyDAL.AdoNet.Bases;
using MyDAL.Core;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace MyDAL.AdoNet
{
    internal sealed class DataSourceSync
        : DataSource
    {
        internal DataSourceSync()
            : base()
        { }
        internal DataSourceSync(Context dc)
            : base(dc)
        { }

        /*********************************************************************************************************************************************/

        internal void Open(IDbConnection cnn)
        {
            if (cnn is DbConnection dbConn)
            {
                dbConn.Open();
            }
            else
            {
                throw new InvalidOperationException("请使用一个继承自 DbConnection 对象的实例!!!");
            }
        }
        private DbDataReader ExecuteReaderWithRetry(DbCommand cmd, CommandBehavior behavior)
        {
            try
            {
                return cmd.ExecuteReader(behavior);
            }
            catch
            {
                try
                {
                    return cmd.ExecuteReader(behavior);
                }
                catch
                {
                    return null;
                }
            }
        }
        private void ProcessDateTimeYearColumn<F>(List<F> result, DicParam dic)
        {
            while (Reader.Read())
            {
                result.Add(
                    DC.GH.ConvertT<F>(
                        new DateTime(
                            Reader.GetInt32(
                                Reader.GetOrdinal(
                                    dic.Option == OptionEnum.Column
                                        ? dic.TbCol
                                        : dic.Option == OptionEnum.ColumnAs
                                            ? dic.TbColAlias
                                            : throw DC.Exception(XConfig.EC._038, dic.Option.ToString()))), 1, 1).ToString(dic.Format)));
            }
        }
        private List<F> ReadColumn<F>()
        {
            var result = new List<F>();
            if (IsDateTimeYearColumn(out var dic))
            {
                ProcessDateTimeYearColumn(result, dic);
            }
            else if (DC.Crud == CrudEnum.SQL)
            {
                while (Reader.Read())
                {
                    result.Add(DC.GH.ConvertT<F>(Reader.GetValue(0)));
                }
            }
            else
            {
                var col = DC.Parameters.FirstOrDefault(it => it.Action == ActionEnum.Select && it.Columns != null);
                dic = col?.Columns.FirstOrDefault(it => it.Option == OptionEnum.Column);
                if (dic == null)
                {
                    throw new Exception("[[ReadColumn<F>()]] - 多表连接 - 单列 - 查询 - 异常 !!!");
                }
                var tbm = DC.XC.GetTableModel(dic.TbMType);
                var func = DC.XC.GetHandle(SqlOne, Reader, tbm.TbMType);
                var prop = tbm.TbMProps.FirstOrDefault(it => it.Name.Equals(dic.TbMProp, StringComparison.Ordinal));
                while (Reader.Read())
                {
                    var obj = func(Reader);
                    result.Add(DC.GH.ConvertT<F>(prop.GetValue(obj)));
                }
            }
            return result;
        }
        private List<F> ReadColumn<M, F>(Func<M, F> propertyFunc)
            where M : class
        {
            var result = new List<F>();
            if (IsDateTimeYearColumn(out var dic))
            {
                ProcessDateTimeYearColumn(result, dic);
            }
            else
            {
                var func = DC.XC.GetHandle<M>(SqlOne, Reader);
                while (Reader.Read())
                {
                    result.Add(propertyFunc(func(Reader)));
                }
            }
            return result;
        }
        private List<M> ReadRow<M>()
        {
            var result = new List<M>();
            if (Reader.FieldCount == 0)
            {
                return new List<M>();
            }
            var func = DC.XC.GetHandle<M>(SqlCount == 1 ? SqlOne : SqlTwo, Reader);
            while (Reader.Read())
            {
                result.Add(func(Reader));
            }
            return result;
        }

        /*********************************************************************************************************************************************/

        internal PagingResult<T> ExecuteReaderPaging<M, T>(bool single, Func<M, T> mapFunc)
            where M : class
        {
            var result = new PagingResult<T>();
            result.PageIndex = DC.PageIndex.Value;
            result.PageSize = DC.PageSize.Value;
            result.Data = new List<T>();
            bool needClose = Conn.State == ConnectionState.Closed;
            try
            {
                if (needClose) { Open(Conn); }
                var ci = new CommandInfo(SqlOne, Parameter);
                using (var cmdC = SettingCommand(ci, Conn, ci.Parameter.ParamReader))
                {
                    var obj = cmdC.ExecuteScalar();
                    result.TotalCount = DC.GH.ConvertT<long>(obj);
                }
                ci = new CommandInfo(SqlTwo, Parameter);
                using (var cmdD = SettingCommand(ci, Conn, ci.Parameter.ParamReader))
                {
                    using (Reader = ExecuteReaderWithRetry(cmdD, XConfig.MultiRow))
                    {
                        if (single)
                        {
                            if (mapFunc == null)
                            {
                                result.Data = ReadColumn<T>();
                            }
                            else
                            {
                                result.Data = ReadColumn(mapFunc);
                            }
                        }
                        else
                        {
                            result.Data = ReadRow<T>();
                        }
                    }
                }
            }
            finally
            {
                if (needClose) { Conn.Close(); }
            }
            return result;
        }
        internal List<M> ExecuteReaderMultiRow<M>()
        {
            var result = new List<M>();
            var ci = new CommandInfo(SqlOne, Parameter);
            bool needClose = Conn.State == ConnectionState.Closed;
            try
            {
                if (needClose) { Open(Conn); }
                using (var cmd = SettingCommand(ci, Conn, ci.Parameter.ParamReader))
                {
                    using (Reader = ExecuteReaderWithRetry(cmd, XConfig.MultiRow))
                    {
                        result = ReadRow<M>();
                    }
                }
            }
            finally
            {
                if (needClose) { Conn.Close(); }
            }
            return result;
        }
        internal List<F> ExecuteReaderSingleColumn<M, F>(Func<M, F> propertyFunc)
            where M : class
        {
            var result = new List<F>();
            var comm = new CommandInfo(SqlOne, Parameter);
            var needClose = Conn.State == ConnectionState.Closed;
            try
            {
                if (needClose) { Open(Conn); }
                using (var cmd = SettingCommand(comm, Conn, comm.Parameter.ParamReader))
                {
                    using (Reader = ExecuteReaderWithRetry(cmd, XConfig.MultiRow))
                    {
                        result = ReadColumn(propertyFunc);
                    }
                }
            }
            finally
            {
                if (needClose) { Conn.Close(); }
            }
            return result;
        }
        internal List<F> ExecuteReaderSingleColumn<F>()
        {
            var result = new List<F>();
            var comm = new CommandInfo(SqlOne, Parameter);
            var needClose = Conn.State == ConnectionState.Closed;
            try
            {
                if (needClose) { Open(Conn); }
                using (var cmd = SettingCommand(comm, Conn, comm.Parameter.ParamReader))
                {
                    using (Reader = ExecuteReaderWithRetry(cmd, XConfig.MultiRow))
                    {
                        result = ReadColumn<F>();
                    }
                }
            }
            finally
            {
                if (needClose) { Conn.Close(); }
            }
            return result;
        }
        internal int ExecuteNonQuery()
        {
            var result = default(int);
            var comm = new CommandInfo(SqlOne, Parameter);
            var needClose = Conn.State == ConnectionState.Closed;
            try
            {
                if (needClose) { Open(Conn); }
                using (var cmd = SettingCommand(comm, Conn, comm.Parameter.ParamReader))
                {
                    result = cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (needClose) { Conn.Close(); }
            }
            return result;
        }
        internal T ExecuteScalar<T>()
            where T : struct
        {
            var result = default(T);
            var comm = new CommandInfo(SqlOne, Parameter);
            var needClose = Conn.State == ConnectionState.Closed;
            try
            {
                if (needClose) { Open(Conn); }
                using (var cmd = SettingCommand(comm, Conn, comm.Parameter.ParamReader))
                {
                    var obj = cmd.ExecuteScalar();
                    result = DC.GH.ConvertT<T>(obj);
                }
            }
            finally
            {
                if (needClose) { Conn.Close(); }
            }
            return result;
        }

    }
}
