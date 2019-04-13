using HPC.DAL.Core.Enums;
using HPC.DAL.UserFacade.Update;
using System;
using System.Linq.Expressions;

namespace HPC.DAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static class SetEx
    {

        /// <summary>
        /// 请参阅: <see langword=".UpdateAsync() 之 .Set() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static SetU<M> Set<M, F>(this Updater<M> updater, Expression<Func<M, F>> propertyFunc, F newVal)
            where M : class
        {
            updater.DC.Action = ActionEnum.Update;
            updater.SetChangeHandle<M, F>(propertyFunc, newVal, OptionEnum.Set);
            return new SetU<M>(updater.DC);
        }
        /// <summary>
        /// 请参阅: <see langword=".UpdateAsync() 之 .Set() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static SetU<M> Set<M, F>(this SetU<M> set, Expression<Func<M, F>> propertyFunc, F newVal)
            where M : class
        {
            set.DC.Action = ActionEnum.Update;
            set.SetChangeHandle<M, F>(propertyFunc, newVal, OptionEnum.Set);
            return set;
        }

        /// <summary>
        /// 请参阅: <see langword=".UpdateAsync() 之 .Set() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static SetU<M> Set<M>(this Updater<M> updater, dynamic filedsObject)
            where M : class
        {
            updater.DC.Action = ActionEnum.Update;
            updater.SetDynamicHandle<M>(filedsObject as object);
            return new SetU<M>(updater.DC);
        }

        /// <summary>
        /// 请参阅: <see langword=".UpdateAsync() 之 .Set() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static SetU<M> Change<M, F>(this Updater<M> updater, Expression<Func<M, F>> propertyFunc, F modifyVal, ChangeEnum change)
            where M : class
        {
            updater.DC.Action = ActionEnum.Update;
            updater.SetChangeHandle<M, F>(propertyFunc, modifyVal, updater.DC.GetChangeOption(change));
            return new SetU<M>(updater.DC);
        }
        /// <summary>
        /// 请参阅: <see langword=".UpdateAsync() 之 .Set() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static SetU<M> Change<M, F>(this SetU<M> set, Expression<Func<M, F>> propertyFunc, F modifyVal, ChangeEnum change)
            where M : class
        {
            set.DC.Action = ActionEnum.Update;
            set.SetChangeHandle<M, F>(propertyFunc, modifyVal, set.DC.GetChangeOption(change));
            return set;
        }

    }
}
