using System;
using System.Threading.Tasks;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.UserFacade.Transaction
{
    /// <summary>
    /// </summary>
    public sealed class Transactioner 
        : Operator, IBusinessUnitOption
    {

        internal Transactioner(Context dc)
            : base(dc)
        { }

        /// <summary>
        /// 业务单元 -- 已处理 using(connection) / using(transaction) / try(exception)
        /// </summary>
        /// <returns>异常信息</returns>
        public async Task<string> BusinessUnitOption(Func<Task<string>> businessFunc)
        {
            var errorMsg = string.Empty;
            using (DC.Conn)
            {
                using (DC.Tran = DC.Conn.BeginTransaction())
                {
                    try
                    {
                        errorMsg = await businessFunc();
                        DC.Tran.Commit();
                        return errorMsg;
                    }
                    catch (AggregateException ae)
                    {
                        DC.Tran.Rollback();
                        var msg = string.Empty;
                        ae.Flatten().Handle(e =>
                        {
                            msg += e.Message;
                            return true;
                        });
                        errorMsg += msg;
                        return errorMsg;
                    }
                    catch (Exception e)
                    {
                        DC.Tran.Rollback();
                        errorMsg += e.Message;
                        return errorMsg;
                    }
                }
            }
        }

        /// <summary>
        /// 业务单元 -- 已处理 using(connection) / using(transaction) / try(exception)
        /// </summary>
        /// <typeparam name="M">期望得到的数据</typeparam>
        /// <returns>errMsg - 异常信息; data - 期望得到的数据;</returns>
        public async Task<(string errMsg, M data)> BusinessUnitOption<M>(Func<Task<M>> businessFunc)
        {
            var errMsg = string.Empty;
            using (DC.Conn)
            {
                using (DC.Tran = DC.Conn.BeginTransaction())
                {
                    try
                    {
                        var result = await businessFunc();
                        DC.Tran.Commit();
                        return (errMsg, result);
                    }
                    catch (AggregateException ae)
                    {
                        DC.Tran.Rollback();
                        var msg = string.Empty;
                        ae.Flatten().Handle(e =>
                        {
                            msg += e.Message;
                            return true;
                        });
                        errMsg = msg;
                        return (errMsg, default(M));
                    }
                    catch (Exception e)
                    {
                        DC.Tran.Rollback();
                        errMsg = e.Message;
                        return (errMsg, default(M));
                    }
                }
            }
        }

        /// <summary>
        /// 业务单元 -- 已处理 using(connection) / using(transaction) / try(exception)
        /// </summary>
        /// <typeparam name="M">期望得到的数据</typeparam>
        /// <returns>errMsg - 异常信息或自定义错误提示; data - 期望得到的数据;</returns>
        public async Task<(string errMsg, M data)> BusinessUnitOption<M>(Func<Task<(string errMsg, M data)>> businessFunc)
        {
            using (DC.Conn)
            {
                using (DC.Tran = DC.Conn.BeginTransaction())
                {
                    try
                    {
                        var tuple = await businessFunc();
                        DC.Tran.Commit();
                        return tuple;
                    }
                    catch (AggregateException ae)
                    {
                        DC.Tran.Rollback();
                        var msg = string.Empty;
                        ae.Flatten().Handle(e =>
                        {
                            msg += e.Message;
                            return true;
                        });
                        return (msg, default(M));
                    }
                    catch (Exception e)
                    {
                        DC.Tran.Rollback();
                        return (e.Message, default(M));
                    }
                }
            }
        }

    }
}
