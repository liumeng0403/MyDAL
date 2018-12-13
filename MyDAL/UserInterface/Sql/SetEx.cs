using MyDAL.Core.Enums;
using MyDAL.UserFacade.Update;
using System;
using System.Linq.Expressions;

namespace MyDAL
{
    public static class SetEx
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
            updater.SetChangeHandle<M, F>(propertyFunc, newVal, OptionEnum.Set);
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
            set.SetChangeHandle<M, F>(propertyFunc, newVal, OptionEnum.Set);
            return set;
        }

        /// <summary>
        /// set 多个字段数据
        /// </summary>
        public static SetU<M> Set<M>(this Updater<M> updater, dynamic filedsObject)
        {
            updater.DC.Action = ActionEnum.Update;
            updater.SetDynamicHandle<M>(filedsObject as object);
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
            updater.SetChangeHandle<M, F>(propertyFunc, modifyVal, updater.DC.GetChangeOption(change));
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
            set.SetChangeHandle<M, F>(propertyFunc, modifyVal, set.DC.GetChangeOption(change));
            return set;
        }

    }
}
