using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Core.Models.Cache;
using MyDAL.Core.表达式能力;

namespace MyDAL.Core.核心能力
{
    internal class lbds_列表达式
    {
        /// <summary>
        /// 获取列信息
        /// </summary>
        internal ColumnParam hql_获取列(Context DC,Expression bodyL, ColFuncEnum func, CompareXEnum compareX, string format = "")
        {
            if (bodyL.NodeType == ExpressionType.MemberAccess)
            {
                var leftBody = bodyL as MemberExpression;
                var prop = default(PropertyInfo);

                //
                var mType = default(Type);
                var alias = GetAlias(leftBody);
                if (func == ColFuncEnum.CharLength
                    || func == ColFuncEnum.ToString_CS_DateTime_Format)
                {
                    var exp = leftBody.Expression;
                    if (exp is MemberExpression)
                    {
                        var clMemExpr = exp as MemberExpression;
                        mType = clMemExpr.Expression.Type;
                        prop = mType.GetProperty(clMemExpr.Member.Name);
                    }
                    else if (exp is ParameterExpression)
                    {
                        mType = leftBody.Expression.Type;
                        prop = mType.GetProperty(leftBody.Member.Name);
                    }
                    else
                    {
                        throw XConfig.EC.Exception(XConfig.EC._005, bodyL.ToString());
                    }
                }
                else
                {
                    mType = leftBody.Expression.Type;
                    prop = mType.GetProperty(leftBody.Member.Name);
                }

                //
                var type = prop.PropertyType;
                var tbm = default(TableModelCache);
                try
                {
                    tbm = DC.XC.GetTableModel(mType);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("004")
                        && ex.Message.Contains("[XTable]"))
                    {
                        throw XConfig.EC.Exception(XConfig.EC._094, $"表达式 -- {bodyL.ToString()} -- 不能正确解析！");
                    }
                }

                var attr = tbm.PCA
                    .FirstOrDefault(it => prop.Name.Equals(it.PropName, StringComparison.OrdinalIgnoreCase)).ColAttr;
                return new ColumnParam
                {
                    Prop = prop.Name,
                    Key = attr.Name,
                    Alias = alias,
                    ValType = type,
                    TbMType = mType,
                    Format = format
                };
            }
            else if (bodyL.NodeType == ExpressionType.Convert)
            {
                var exp = bodyL as UnaryExpression;
                var opMem = exp.Operand;
                return hql_获取列(DC,opMem, func, compareX);
            }
            else if (bodyL.NodeType == ExpressionType.Call)
            {
                var mcExpr = bodyL as MethodCallExpression;
                if (func == ColFuncEnum.Trim
                    || func == ColFuncEnum.LTrim
                    || func == ColFuncEnum.RTrim)
                {
                    var mem = mcExpr.Object;
                    return hql_获取列(DC,mem, func, compareX);
                }
                else if (compareX == CompareXEnum.In)
                {
                    var mem = mcExpr.Arguments[0];
                    return hql_获取列(DC,mem, func, compareX);
                }
                else if (func == ColFuncEnum.ToString_CS_DateTime_Format)
                {
                    var mem = mcExpr.Object;
                    var val = DC.VH.ValueProcess(mcExpr.Arguments[0], XConfig.CSTC.String);
                    return hql_获取列(DC,mem, func, compareX, val.Val.ToString());
                }
                else if (func == ColFuncEnum.ToString_CS)
                {
                    var mem = mcExpr.Object;
                    return hql_获取列(DC,mem, func, compareX);
                }
                else if(func == ColFuncEnum.Count)
                {
                    var mem = mcExpr.Arguments[0]; 
                    return hql_获取列(DC,mem, func, compareX);
                }
                else if(func == ColFuncEnum.Sum)
                {
                    var mem = mcExpr.Arguments[0];
                    return hql_获取列(DC, mem, func, compareX);
                }
                else
                {
                    throw XConfig.EC.Exception(XConfig.EC._018, $"{bodyL.NodeType}-{func}");
                }
            }
            else
            {
                throw XConfig.EC.Exception(XConfig.EC._017, bodyL.NodeType.ToString());
            }
        }
        
        /// <summary>
        /// 获取别名
        /// </summary>
        private string GetAlias(MemberExpression memExpr)
        {
            var alias = string.Empty;
            if (memExpr.Expression != null)
            {
                var expr = memExpr.Expression;
                if (expr.NodeType == ExpressionType.Parameter)
                {
                    var pExpr = expr as ParameterExpression;
                    alias = pExpr.Name;
                }
                else if (expr.NodeType == ExpressionType.MemberAccess)
                {
                    var maExpr = expr as MemberExpression;
                    if (maExpr.Expression != null
                        && maExpr.Expression.NodeType == ExpressionType.Parameter)
                    {
                        return GetAlias(maExpr);
                    }
                    else if (maExpr.Expression != null
                             && maExpr.Expression.NodeType == ExpressionType.MemberAccess)
                    {
                        var xmaExpr = maExpr.Expression as MemberExpression;
                        return GetAlias(xmaExpr);
                    }

                    alias = maExpr.Member.Name;
                }
                else if (expr.NodeType == ExpressionType.Constant)
                {
                    alias = memExpr.Member.Name;
                }
            }

            return alias;
        }
    }
}