using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Models.MysqlFunctionParam.DicParamModel;
using MyDAL.Core.核心能力;
using MyDAL.Core.表达式能力;
using MyDAL.DataRainbow.MySQL;
using MyDAL.DataRainbow.XCommon.Bases;

namespace MyDAL.Core.Models.MysqlFunctionParam.DicParamResolve
{
    internal class SumResolve
        : XSQL
    {
        private Context DC;

        internal SumResolve(Context DC)
        {
            this.DC = DC;
        }
        
        internal SumParam Resolve(MethodCallExpression mcExpr)
        {
            DC.Option = OptionEnum.ColumnAs;
            DC.Compare = CompareXEnum.None;
            var cp = new lbds_列表达式().hql_获取列(DC, mcExpr, ColFuncEnum.Sum, CompareXEnum.None);
            SumParam param = SumDic(DC.TbM1, cp.Prop ,string.Empty);
            param.FuncName = "SUM";
            return param;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mType">表实体类型</param>
        /// <param name="key">字段属性名</param>
        /// <param name="alias">表别名</param>
        /// <returns></returns>
        private SumParam SumDic(Type mType, string propName, string alias = "")
        {
            SumParam dic = new SumParam();
            dic.SetDicBase(DC);
            dic.TbMType = mType;
            dic.TbAlias = alias;
            dic.TbCol = GetCol(DC, mType, propName); // key;
            dic.Param = propName;
            dic.ParamRaw = propName;
            dic.Columns = new List<DicParam>();
            dic.Columns.Add(new SumParam()
            {
                TbCol = dic.TbCol,
                Option = OptionEnum.Column,
                Func = ColFuncEnum.Sum,
                Crud = CrudEnum.Query
            });

            dic.Func = ColFuncEnum.Sum;

            return dic;
        }
        
        /// <summary>
        /// 组装 select 片段中的 sum()
        /// </summary>
        internal void SelectSumCol(DicParam dic, StringBuilder X,MySql DbSql)
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