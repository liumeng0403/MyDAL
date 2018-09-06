using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EasyDAL.Exchange.UserFacade.Update
{
    public class Setter<M>: Operator
    {
        internal Setter(DbContext dc)
        {
            DC = dc;
        }

        /// <summary>
        /// set 单个字段数据
        /// </summary>
        /// <param name="func">格式: it => it.CreatedOn</param>
        /// <param name="newVal">新值</param>
        public Setter<M> Set<F>(Expression<Func<M, F>> func, F newVal)
        {
            SetChangeHandle<M, F>(func, newVal, ActionEnum.Update, OptionEnum.Set);
            return this;
        }
        /// <summary>
        /// set 多个字段数据
        /// </summary>
        public Setter<M> Set(object mSet)
        {
            DynamicSetHandle<M>(mSet);
            return this;
        }

        /// <summary>
        /// set 单个字段变更
        /// </summary>
        /// <param name="func">格式: it => it.LockedCount</param>
        /// <param name="modifyVal">变更值</param>
        /// <param name="change">+/-/...</param>
        public Setter<M> Change<F>(Expression<Func<M, F>> func, F modifyVal, ChangeEnum change)
        {
            SetChangeHandle<M, F>(func, modifyVal, ActionEnum.Update, DC.SqlProvider.GetChangeOption(change));
            return this;
        }

        /// <summary>
        /// 过滤条件起点
        /// </summary>
        /// <param name="func">格式: it => it.AgentId == id2</param>
        public UpdateFilter<M> Where(Expression<Func<M, bool>> func)
        {
            WhereHandle(func,CrudTypeEnum.Update);
            return new UpdateFilter<M>(DC);
        }


    }
}
