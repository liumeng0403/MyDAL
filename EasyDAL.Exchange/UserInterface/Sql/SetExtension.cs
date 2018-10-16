using MyDAL.Core.Enums;
using MyDAL.UserFacade.Update;
using System;
using System.Linq.Expressions;

namespace MyDAL
{
    public static class SetExtension
    {

        /// <summary>
        /// set 单个字段数据
        /// </summary>
        /// <param name="func">格式: it => it.CreatedOn</param>
        /// <param name="newVal">新值</param>
        public static SetU<M> Set<M, F>(this Updater<M> updater, Expression<Func<M, F>> func, F newVal)
        {
            updater.DC.Action = ActionEnum.Update;
            updater.DC.OP.SetChangeHandle<M, F>(func, newVal, ActionEnum.Update, OptionEnum.Set);
            return new SetU<M>(updater.DC);
        }
        /// <summary>
        /// set 单个字段数据
        /// </summary>
        /// <param name="func">格式: it => it.CreatedOn</param>
        /// <param name="newVal">新值</param>
        public static SetU<M> Set<M, F>(this SetU<M> set, Expression<Func<M, F>> func, F newVal)
        {
            set.DC.Action = ActionEnum.Update;
            set.DC.OP.SetChangeHandle<M, F>(func, newVal, ActionEnum.Update, OptionEnum.Set);
            return set;
        }


        /// <summary>
        /// set 多个字段数据
        /// </summary>
        public static SetU<M> Set<M>(this Updater<M> updater, object mSet)
        {
            updater.DC.Action = ActionEnum.Update;
            updater.DC.OP.SetDynamicHandle<M>(mSet);
            return new SetU<M>(updater.DC);
        }


        /// <summary>
        /// set 单个字段变更
        /// </summary>
        /// <param name="func">格式: it => it.LockedCount</param>
        /// <param name="modifyVal">变更值</param>
        /// <param name="change">+/-/...</param>
        public static SetU<M> Change<M, F>(this Updater<M> updater, Expression<Func<M, F>> func, F modifyVal, ChangeEnum change)
        {
            updater.DC.Action = ActionEnum.Update;
            updater.DC.OP.SetChangeHandle<M, F>(func, modifyVal, ActionEnum.Update, updater.DC.SqlProvider.GetChangeOption(change));
            return new SetU<M>(updater.DC);
        }
        /// <summary>
        /// set 单个字段变更
        /// </summary>
        /// <param name="func">格式: it => it.LockedCount</param>
        /// <param name="modifyVal">变更值</param>
        /// <param name="change">+/-/...</param>
        public static SetU<M> Change<M, F>(this SetU<M> set, Expression<Func<M, F>> func, F modifyVal, ChangeEnum change)
        {
            set.DC.Action = ActionEnum.Update;
            set.DC.OP.SetChangeHandle<M, F>(func, modifyVal, ActionEnum.Update, set.DC.SqlProvider.GetChangeOption(change));
            return set;
        }

    }
}
