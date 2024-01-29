using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Core.Models.MysqlFunctionParam.DicParamModel;
using MyDAL.Core.表达式能力;

namespace MyDAL.Core.Models.MysqlFunctionParam.DicParamResolve
{
    internal class CountResolve
    {
        internal CountParam Resolve(Context DC,MethodCallExpression mcExpr)
        {
            var type = mcExpr.Method.DeclaringType;
            
            DC.Option = OptionEnum.ColumnAs;
            DC.Compare = CompareXEnum.None;
            var cp = new hql_获取列().GetKey(DC,mcExpr, FuncEnum.Count, CompareXEnum.None);
            DC.Func = FuncEnum.Count;
            CountParam param = CountDic(DC, DC.TbM1, cp.Prop,string.Empty);
            param.FuncName = "COUNT";
            return param;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DC">上下文</param>
        /// <param name="mType">表实体类型</param>
        /// <param name="key">字段属性名</param>
        /// <param name="alias">表别名</param>
        /// <returns></returns>
        internal CountParam CountDic(Context DC,Type mType, string key, string alias = "")
        {
            CountParam dic = new CountParam();
            dic.SetDicBase(DC);
            dic.TbMType = mType;
            dic.TbAlias = alias;
            dic.TbCol = dic.GetCol(DC,mType, key);  // key;
            dic.Param = key;
            dic.ParamRaw = key;
            dic.Columns = new List<DicParam>();
            dic.Columns.Add(new CountParam()
            {
                TbCol = dic.TbCol,
                Option = OptionEnum.Column,
                Func = FuncEnum.Count
            });

            return dic;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dic"></param>
        internal void SelectCountCol(DicParam dic)
        {
        // todo 
            
        }
    }
}