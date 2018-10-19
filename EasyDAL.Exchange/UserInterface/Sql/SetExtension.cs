using System;
using System.Linq.Expressions;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.UserFacade.Update;

namespace Yunyong.DataExchange
{
    public static class SetExtension
    {

        /// <summary>
        /// set 单个字段数据
        /// </summary>
        /// <param name="func">格式: it => it.CreatedOn</param>
        /// <param name="newVal">新值</param>
        public static SetU<M> Set<M, F>(this Updater<M> updater, Expression<Func<M, F>> propertyFunc, F newVal)
            where M : class
        {
            updater.DC.Action = ActionEnum.Update;
            updater.DC.OP.SetChangeHandle<M, F>(propertyFunc, newVal, OptionEnum.Set);
            return new SetU<M>(updater.DC);
        }
        /// <summary>
        /// set 单个字段数据
        /// </summary>
        /// <param name="func">格式: it => it.CreatedOn</param>
        /// <param name="newVal">新值</param>
        public static SetU<M> Set<M, F>(this SetU<M> set, Expression<Func<M, F>> propertyFunc, F newVal)
            where M : class
        {
            set.DC.Action = ActionEnum.Update;
            set.DC.OP.SetChangeHandle<M, F>(propertyFunc, newVal, OptionEnum.Set);
            return set;
        }

        /// <summary>
        /// set 多个字段数据
        /// </summary>
        public static SetU<M> Set<M>(this Updater<M> updater, object filedsObject)
        {
            updater.DC.Action = ActionEnum.Update;
            updater.DC.OP.SetDynamicHandle<M>(filedsObject);
            return new SetU<M>(updater.DC);
        }

        /// <summary>
        /// set 单个字段变更
        /// </summary>
        /// <param name="func">格式: it => it.LockedCount</param>
        /// <param name="modifyVal">变更值</param>
        /// <param name="change">+/-/...</param>
        public static SetU<M> Change<M, F>(this Updater<M> updater, Expression<Func<M, F>> propertyFunc, F modifyVal, ChangeEnum change)
            where M : class
        {
            updater.DC.Action = ActionEnum.Update;
            updater.DC.OP.SetChangeHandle<M, F>(propertyFunc, modifyVal, updater.DC.SqlProvider.GetChangeOption(change));
            return new SetU<M>(updater.DC);
        }
        /// <summary>
        /// set 单个字段变更
        /// </summary>
        /// <param name="func">格式: it => it.LockedCount</param>
        /// <param name="modifyVal">变更值</param>
        /// <param name="change">+/-/...</param>
        public static SetU<M> Change<M, F>(this SetU<M> set, Expression<Func<M, F>> propertyFunc, F modifyVal, ChangeEnum change)
            where M : class
        {
            set.DC.Action = ActionEnum.Update;
            set.DC.OP.SetChangeHandle<M, F>(propertyFunc, modifyVal, set.DC.SqlProvider.GetChangeOption(change));
            return set;
        }

    }
}
