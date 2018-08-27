//using EasyDAL.Exchange.Common;
//using EasyDAL.Exchange.Core;
//using EasyDAL.Exchange.Core.Update;
//using EasyDAL.Exchange.Enums;
//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using System.Text;

//namespace EasyDAL.Exchange.Extensions
//{
//    public static class UpdateOperationExtension
//    {

//        public static Setter<M> Set<M,F>(this UpdateOperation<M> update, Expression<Func<M, F>> func, F newVal)
//        {
//            var key = update.DC.EH.ExpressionHandle(func);
//            //Fields.Add(key);
//            update.DC.Conditions.Add(new DicModel<string, string>
//            {
//                key = key,
//                Value = update.DC.GH.GetTypeValue(newVal.GetType(), newVal),
//                Option = OptionEnum.Set,
//                Action = ActionEnum.Set
//            });
//            return this;
//        }

//        public static Setter<M> Change<M,F>(this UpdateOperation<M> update, Expression<Func<M, F>> func, F modifyVal, ChangeEnum change)
//        {
//            var key = update.DC.EH.ExpressionHandle(func);
//            //Fields.Add(key);
//            update.DC.Conditions.Add(new DicModel<string, string>
//            {
//                key = key,
//                Value = update.DC.GH.GetTypeValue(modifyVal.GetType(), modifyVal),
//                Option = update.DC.OP.GetChangeOption(change),
//                Action = ActionEnum.Change
//            });
//            return this;
//        }

//    }
//}
