using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Core.Models.MysqlFunctionParam.DicParamModel;
using MyDAL.Core.表达式能力;
using MyDAL.DataRainbow.MySQL;
using MyDAL.DataRainbow.XCommon.Bases;

namespace MyDAL.Core.Models.MysqlFunctionParam.DicParamResolve
{
    internal class CountResolve
        : XSQL
    {
        private Context DC;

        internal CountResolve(Context DC)
        {
            this.DC = DC;
        }
        
        internal CountParam Resolve(MethodCallExpression mcExpr)
        {
            var type = mcExpr.Method.DeclaringType;

            DC.Option = OptionEnum.ColumnAs;
            DC.Compare = CompareXEnum.None;
            var cp = new hql_获取列().GetKey(DC, mcExpr, ColFuncEnum.Count, CompareXEnum.None);
            CountParam param = CountDic(DC.TbM1, cp.Prop,ColFuncEnum.Count ,string.Empty);
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
        internal CountParam CountDic(Type mType, string key,ColFuncEnum colFunc, string alias = "")
        {
            CountParam dic = new CountParam();
            dic.SetDicBase(DC);
            dic.TbMType = mType;
            dic.TbAlias = alias;
            dic.TbCol = dic.GetCol(DC, mType, key); // key;
            dic.Param = key;
            dic.ParamRaw = key;
            dic.Columns = new List<DicParam>();
            dic.Columns.Add(new CountParam()
            {
                TbCol = dic.TbCol,
                Option = OptionEnum.Column,
                Func = ColFuncEnum.Count,
                Crud = CrudEnum.Query
            });

            dic.Func = colFunc;

            return dic;
        }

        /// <summary>
        /// 组装 select 片段中的 count()
        /// </summary>
        internal void SelectCountCol(DicParam dic, StringBuilder X,MySql DbSql)
        {
            Function(dic.Func, X); LeftRoundBracket(X);
            if (dic.Crud == CrudEnum.Query)
            {
                DbSql.Column(string.Empty, dic.TbCol, X);
            }
            else 
            {
                throw XConfig.EC.Exception(XConfig.EC._141, $"函数 SelectCountCol -- {dic.Crud} -- 未解析！");
            }
            RightRoundBracket(X);
        }
    }
}