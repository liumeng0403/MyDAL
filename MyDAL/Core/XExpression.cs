using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Expressions;
using MyDAL.Core.Models.ExpPara;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyDAL.Core.Models.MysqlFunctionParam;
using MyDAL.Core.Models.MysqlFunctionParam.DicParamResolve;
using MyDAL.Core.核心能力;
using MyDAL.Core.表达式能力;
using MyDAL.Tools;

namespace MyDAL.Core
{
    internal sealed class XExpression
    {
        private static CompareXEnum GetCompareType(ExpressionType nodeType, bool isR)
        {
            var option = CompareXEnum.None;
            if (nodeType == ExpressionType.Equal)
            {
                option = !isR ? CompareXEnum.Equal : CompareXEnum.Equal;
            }
            else if (nodeType == ExpressionType.NotEqual)
            {
                option = !isR ? CompareXEnum.NotEqual : CompareXEnum.NotEqual;
            }
            else if (nodeType == ExpressionType.LessThan)
            {
                option = !isR ? CompareXEnum.LessThan : CompareXEnum.GreaterThan;
            }
            else if (nodeType == ExpressionType.LessThanOrEqual)
            {
                option = !isR ? CompareXEnum.LessThanOrEqual : CompareXEnum.GreaterThanOrEqual;
            }
            else if (nodeType == ExpressionType.GreaterThan)
            {
                option = !isR ? CompareXEnum.GreaterThan : CompareXEnum.LessThan;
            }
            else if (nodeType == ExpressionType.GreaterThanOrEqual)
            {
                option = !isR ? CompareXEnum.GreaterThanOrEqual : CompareXEnum.LessThanOrEqual;
            }

            return option;
        }

        private static ActionEnum GetGroupAction(ExpressionType nodeType)
        {
            if (nodeType == ExpressionType.AndAlso)
            {
                return ActionEnum.And;
            }
            else if (nodeType == ExpressionType.OrElse)
            {
                return ActionEnum.Or;
            }

            return ActionEnum.None;
        }

        /********************************************************************************************************************/

        private Context DC { get; set; }

        private XExpression()
        {
        }

        internal XExpression(Context dc)
        {
            DC = dc;
        }

        /********************************************************************************************************************/

        private bool IsBinaryExpr(ExpressionType type)
        {
            if (type == ExpressionType.Equal
                || type == ExpressionType.NotEqual
                || type == ExpressionType.LessThan
                || type == ExpressionType.LessThanOrEqual
                || type == ExpressionType.GreaterThan
                || type == ExpressionType.GreaterThanOrEqual)
            {
                return true;
            }

            return false;
        }

        private bool IsMultiExpr(ExpressionType type)
        {
            if (type == ExpressionType.AndAlso
                || type == ExpressionType.OrElse)
            {
                return true;
            }

            return false;
        }

        private bool IsNullableExpr(Expression body)
        {
            if (body.NodeType == ExpressionType.MemberAccess)
            {
                var exStr = body.ToString();
                if (exStr.EndsWith(".Value")
                    && exStr.LastIndexOf('.') > exStr.IndexOf('.'))
                {
                    return true;
                }
            }

            return false;
        }

        /********************************************************************************************************************/

        private BinExprInfo HandBinExpr(List<string> list, BinaryExpression binExpr)
        {
            var binLeft = binExpr.Left;
            var binRight = binExpr.Right;
            var binNode = binExpr.NodeType;

            //
            var leftStr = binLeft.ToString();
            var rightStr = binRight.ToString();
            if (list.All(it => !leftStr.Contains($"{it}."))
                && list.All(it => !rightStr.Contains($"{it}.")))
            {
                throw XConfig.EC.Exception(XConfig.EC._049, $"查询条件中使用的[[表别名变量]]不在列表[[{string.Join(",", list)}]]中!");
            }

            // 
            if (list.Any(it => leftStr.StartsWith($"{it}.", StringComparison.Ordinal))
                || (list.Any(it => leftStr.Contains($"{it}.")) &&
                    leftStr.StartsWith($"Convert(", StringComparison.Ordinal))
                || (list.Any(it => leftStr.Contains($").{it}.")) &&
                    leftStr.StartsWith($"value(", StringComparison.Ordinal))
                || (list.Any(it => leftStr.Contains($").{it}.")) &&
                    leftStr.StartsWith($"Convert(value(", StringComparison.Ordinal)))
            {
                return new BinExprInfo
                {
                    Left = binLeft,
                    Right = binRight,
                    Node = binNode,
                    Compare = GetCompareType(binNode, false)
                };
            }
            else
            {
                return new BinExprInfo
                {
                    Left = binRight,
                    Right = binLeft,
                    Node = binNode,
                    Compare = GetCompareType(binNode, true)
                };
            }
        }

        // ------------------------------------------------------------------------------------------------
        
        private DicParam HandBin(BinaryExpression binExpr, List<string> pres,ColFuncEnum colFunc)
        {
            var bin = HandBinExpr(pres, binExpr);
            if ((bin.Node == ExpressionType.Equal || bin.Node == ExpressionType.NotEqual)
                && bin.Right.NodeType == ExpressionType.Constant
                && (bin.Right as ConstantExpression).Value == null)
            {
                var cp = new lbds_列表达式().hql_获取列(DC,bin.Left, ColFuncEnum.None, CompareXEnum.None);
                if (bin.Node == ExpressionType.Equal)
                {
                    DC.Option = OptionEnum.IsNull;
                }
                else
                {
                    DC.Option = OptionEnum.IsNotNull;
                }

                return DC.DPH.IsNullDic(cp,colFunc);
            }
            else
            {
                var left = default(Expression);
                if (IsNullableExpr(bin.Left))
                {
                    left = (bin.Left as MemberExpression).Expression;
                }
                else
                {
                    left = bin.Left;
                }

                var leftStr = left.ToString();
                var length = DC.CFH.IsLengthFunc(leftStr);
                var tp = length ? new TrimParam { Flag = false } : DC.CFH.IsTrimFunc(leftStr);
                var toString = tp.Flag ? false : DC.CFH.IsToStringFunc(leftStr);
                if (length)
                {
                    var cp = new lbds_列表达式().hql_获取列(DC,left, ColFuncEnum.CharLength, CompareXEnum.None);
                    var val = DC.VH.ValueProcess(bin.Right, cp.ValType);
                    DC.Option = OptionEnum.Function;
                    DC.Compare = bin.Compare;
                    return DC.DPH.CharLengthDic(cp, val, ColFuncEnum.CharLength);
                }
                else if (tp.Flag)
                {
                    var cp = new lbds_列表达式().hql_获取列(DC,left, tp.Trim, CompareXEnum.None);
                    var val = DC.VH.ValueProcess(bin.Right, cp.ValType);
                    DC.Option = OptionEnum.Function;
                    DC.Compare = bin.Compare;
                    return DC.DPH.TrimDic(cp, val,tp.Trim);
                }
                else if (toString)
                {
                    return new CsToStringExpression(DC).WhereFuncToString(left, bin,colFunc);
                }
                else
                {
                    var cp = new lbds_列表达式().hql_获取列(DC,left, ColFuncEnum.None, CompareXEnum.None);
                    var val = DC.VH.ValueProcess(bin.Right, cp.ValType);
                    DC.Option = OptionEnum.Compare;
                    DC.Compare = bin.Compare;
                    return DC.DPH.CompareDic(cp, val,ColFuncEnum.None);
                }
            }
        }

        /********************************************************************************************************************/
        
        private DicParam StringLike(MethodCallExpression mcExpr, StringLikeEnum type,ColFuncEnum colFunc)
        {
            if (mcExpr.Object == null)
            {
                return null;
            }
            else
            {
                var objExpr = mcExpr.Object;
                var objNodeType = mcExpr.Object.NodeType;
                if (objNodeType == ExpressionType.MemberAccess)
                {
                    var memO = objExpr as MemberExpression;
                    var memType = objExpr.Type;
                    if (memType == typeof(string))
                    {
                        var cp = new lbds_列表达式().hql_获取列(DC,memO, ColFuncEnum.None, CompareXEnum.None);
                        var valExpr = mcExpr.Arguments[0];
                        var val = DC.VH.ValueProcess(valExpr, cp.ValType);
                        val = ValueInfo.LikeVI(val, type, DC);
                        DC.Option = OptionEnum.Compare;
                        DC.Compare = CompareXEnum.Like;
                        return DC.DPH.LikeDic(cp, val,ColFuncEnum.None);
                    }
                }
            }

            return null;
        }

        private DicParam Equals(MethodCallExpression mcExpr, ExpressionType nodeType, Expression valExpr,ColFuncEnum colFunc)
        {
            if (mcExpr.IsNull())
            {
                return null;
            }
            else
            {
                var objExpr = mcExpr.Object;
                var objNodeType = mcExpr.Object.NodeType;
                if (objNodeType == ExpressionType.MemberAccess)
                {
                    var memO = objExpr as MemberExpression;
                    var cp = new lbds_列表达式().hql_获取列(DC,memO, ColFuncEnum.None, CompareXEnum.None);
                    if (nodeType == ExpressionType.MemberAccess)
                    {
                        var val = DC.VH.ValueProcess(valExpr, cp.ValType);
                        DC.Option = OptionEnum.Compare;
                        DC.Compare = CompareXEnum.Equal; 
                        return DC.DPH.CompareDic(cp, val,ColFuncEnum.None);
                    }
                }
            }

            return null;
        }

        private DicParam CollectionIn(ExpressionType nodeType, Expression keyExpr, Expression valExpr,ColFuncEnum colFunc)
        {
            if (nodeType == ExpressionType.MemberAccess)
            {
                var cp = new lbds_列表达式().hql_获取列(DC,keyExpr, ColFuncEnum.None, CompareXEnum.In);
                var val = DC.VH.ValueProcess(valExpr, cp.ValType);
                DC.Option = OptionEnum.Compare;
                DC.Compare = CompareXEnum.In; 
                return DC.DPH.InDic(cp, val,ColFuncEnum.None);
            }
            else if (nodeType == ExpressionType.NewArrayInit)
            {
                var naExpr = valExpr as NewArrayExpression;
                var cp = new lbds_列表达式().hql_获取列(DC,keyExpr, ColFuncEnum.None, CompareXEnum.In);
                var vals = new List<ValueInfo>();
                foreach (var exp in naExpr.Expressions)
                {
                    vals.Add(DC.VH.ValueProcess(exp, cp.ValType));
                }

                var val = new ValueInfo
                {
                    Val = string.Join(",", vals.Select(it => it.Val)),
                    ValStr = string.Empty
                };
                DC.Option = OptionEnum.Compare;
                DC.Compare = CompareXEnum.In; 
                return DC.DPH.InDic(cp, val,ColFuncEnum.None);
            }
            else if (nodeType == ExpressionType.ListInit)
            {
                var liExpr = valExpr as ListInitExpression;
                var cp = new lbds_列表达式().hql_获取列(DC,keyExpr, ColFuncEnum.None, CompareXEnum.In);
                var vals = new List<ValueInfo>();
                foreach (var ini in liExpr.Initializers)
                {
                    vals.Add(DC.VH.ValueProcess(ini.Arguments[0], cp.ValType));
                }

                var val = new ValueInfo
                {
                    Val = string.Join(",", vals.Select(it => it.Val)),
                    ValStr = string.Empty
                };
                DC.Option = OptionEnum.Compare;
                DC.Compare = CompareXEnum.In; 
                return DC.DPH.InDic(cp, val,ColFuncEnum.None);
            }
            else if (nodeType == ExpressionType.MemberInit)
            {
                var expr = valExpr as MemberInitExpression;
                if (expr.NewExpression.Arguments.Count == 0)
                {
                    throw XConfig.EC.Exception(XConfig.EC._050, $"【{keyExpr}】 中 集合为空!!!");
                }
                else
                {
                    throw XConfig.EC.Exception(XConfig.EC._019, nodeType.ToString());
                }
            }
            else
            {
                throw XConfig.EC.Exception(XConfig.EC._020, nodeType.ToString());
            }
        }

        private DicParam BoolDefaultCondition(ColumnParam cp,ColFuncEnum colFunc)
        {
            if (cp.ValType == XConfig.CSTC.Bool
                || cp.ValType == XConfig.CSTC.BoolNull)
            {
                DC.Option = OptionEnum.Compare;
                DC.Compare = CompareXEnum.Equal;
                return DC.DPH.CompareDic(cp, new ValueInfo
                {
                    Val = true.ToString(),
                    ValStr = string.Empty
                },colFunc);
            }

            return null;
        }

        /********************************************************************************************************************/

        private DicParam NotHandle(Expression body, ParameterExpression firstParam,ColFuncEnum colFunc)
        {
            var ue = body as UnaryExpression;
            var result = BodyProcess(ue.Operand, firstParam,colFunc);
            if (result.Compare == CompareXEnum.Like)
            {
                result.Compare = CompareXEnum.NotLike;
            }
            else if (result.Compare == CompareXEnum.In)
            {
                result.Compare = CompareXEnum.NotIn;
            }
            else if (result.Compare == CompareXEnum.Equal)
            {
                result.Compare = CompareXEnum.NotEqual;
            }
            else if (result.Compare == CompareXEnum.NotEqual)
            {
                result.Compare = CompareXEnum.Equal;
            }
            else if (result.Compare == CompareXEnum.LessThan)
            {
                result.Compare = CompareXEnum.GreaterThanOrEqual;
            }
            else if (result.Compare == CompareXEnum.LessThanOrEqual)
            {
                result.Compare = CompareXEnum.GreaterThan;
            }
            else if (result.Compare == CompareXEnum.GreaterThan)
            {
                result.Compare = CompareXEnum.LessThanOrEqual;
            }
            else if (result.Compare == CompareXEnum.GreaterThanOrEqual)
            {
                result.Compare = CompareXEnum.LessThan;
            }
            else if (result.Option == OptionEnum.IsNull)
            {
                result.Option = OptionEnum.IsNotNull;
            }
            else if (result.Option == OptionEnum.IsNotNull)
            {
                result.Option = OptionEnum.IsNull;
            }
            else
            {
                throw XConfig.EC.Exception(XConfig.EC._027, body.ToString());
            }

            return result;
        }

        private DicParam CallHandle(MethodCallExpression mcExpr,ColFuncEnum colFunc)
        {
            MydalFunction mf
                = DC.CFH.IsMydalMysqlFunction(mcExpr);
            ContainsLikeParam clp
                = mf.Flag ? new ContainsLikeParam { Flag = false } : DC.CFH.IsContainsLikeFunc(mcExpr);
            ContainsInParam cip
                = clp.Flag || mf.Flag ? new ContainsInParam { Flag = false } : DC.CFH.IsContainsInFunc(mcExpr);
            ToStringParam tsp
                = cip.Flag || clp.Flag || mf.Flag ? new ToStringParam { Flag = false } : DC.CFH.IsToStringFunc(mcExpr);
            EqualsParam ep
                = tsp.Flag || cip.Flag || clp.Flag || mf.Flag
                    ? new EqualsParam { Flag = false }
                    : DC.CFH.IsEqualsFunc(mcExpr);

            
            if (mf.Flag)  // mydal 原生函数
            {
                return ProcessMysqlFunction(mcExpr);
            }
            else if (clp.Flag)
            {
                if (clp.Like == StringLikeEnum.Contains
                    || clp.Like == StringLikeEnum.StartsWith
                    || clp.Like == StringLikeEnum.EndsWith)
                {
                    return StringLike(mcExpr, clp.Like,colFunc);
                }
            }
            else if (cip.Flag)
            {
                if (cip.Type == ExpressionType.MemberAccess
                    || cip.Type == ExpressionType.NewArrayInit
                    || cip.Type == ExpressionType.ListInit
                    || cip.Type == ExpressionType.MemberInit)
                {
                    return CollectionIn(cip.Type, cip.Key, cip.Val,colFunc);
                }
            }
            else if (tsp.Flag)
            {
                return new CsToStringExpression(DC).SelectFuncToString(mcExpr,colFunc);
            }
            else if (ep.Flag)
            {
                return Equals(mcExpr, ep.Type, ep.Val,colFunc);
            }

            // todo 请参考可用的函数 文章
            throw XConfig.EC.Exception(XConfig.EC._046, $"出现异常 -- [[{mcExpr.ToString()}]] 不能解析!!!");
        }

        /// <summary>
        ///  
        /// </summary>
        private DicParam ProcessMysqlFunction(MethodCallExpression mcExpr)
        {
            string funcName = mcExpr.Method.Name;
            switch (funcName)
            {
                case "COUNT":
                    return new CountResolve(DC).Resolve(mcExpr);
                case "SUM":
                    return new SumResolve(DC).Resolve(mcExpr);
                    
            }
            throw XConfig.EC.Exception(XConfig.EC._046, $"出现异常 -- [[{mcExpr.ToString()}]] 不能解析!!!");
        }

        private DicParam ConstantHandle(ConstantExpression cExpr, Type valType,ColFuncEnum colFunc)
        {
            var val = DC.VH.ValueProcess(cExpr, valType);
            if (cExpr.Type == typeof(bool))
            {
                DC.Option = OptionEnum.OneEqualOne;
                DC.Compare = CompareXEnum.None;
                return DC.DPH.OneEqualOneDic(val, valType,colFunc);
            }

            return null;
        }

        internal DicParam MemberAccessHandle(MemberExpression memExpr,ColFuncEnum colFunc)
        {
            // 原
            // query where
            // join where

            if (DC.IsSingleTableOption()
                || DC.Crud == CrudEnum.None)
            {
                var cp = new lbds_列表达式().hql_获取列(DC,memExpr, ColFuncEnum.None, CompareXEnum.None);
                if (string.IsNullOrWhiteSpace(cp.Key))
                {
                    throw XConfig.EC.Exception(XConfig.EC._047, "无法解析 列名 !!!");
                }

                if (DC.Action == ActionEnum.Select)
                {
                    return DC.DPH.SelectColumnDic(new List<DicParam>
                    {
                        DC.DPH.ColumnDic(cp,colFunc)
                    },colFunc);
                }
                else if (DC.Action == ActionEnum.Where)
                {
                    var dp = BoolDefaultCondition(cp,colFunc);
                    if (dp != null)
                    {
                        return dp;
                    }
                    else
                    {
                        throw XConfig.EC.Exception(XConfig.EC._040, memExpr.ToString());
                    }
                }
                else
                {
                    return DC.DPH.ColumnDic(cp,colFunc);
                }
            }
            else if (DC.Crud == CrudEnum.Join)
            {
                if (DC.Action == ActionEnum.Where)
                {
                    var cp = new lbds_列表达式().hql_获取列(DC,memExpr, ColFuncEnum.None, CompareXEnum.None);
                    var dp = BoolDefaultCondition(cp,colFunc);
                    if (dp != null)
                    {
                        return dp;
                    }
                    else
                    {
                        throw XConfig.EC.Exception(XConfig.EC._039, memExpr.ToString());
                    }
                }
                else
                {
                    if (memExpr.Expression.NodeType == ExpressionType.Constant)
                    {
                        var alias = memExpr.Member.Name;
                        return DC.DPH.TableDic(memExpr.Type, alias);
                    }
                    else if (memExpr.Expression.NodeType == ExpressionType.MemberAccess)
                    {
                        var leftStr = memExpr.ToString();
                        if (DC.CFH.IsLengthFunc(leftStr))
                        {
                            var cp = new lbds_列表达式().hql_获取列(DC,memExpr, ColFuncEnum.CharLength, CompareXEnum.None);
                            DC.Compare = CompareXEnum.None;
                            return DC.DPH.CharLengthDic(cp, null,ColFuncEnum.CharLength);
                        }
                        else
                        {
                            var exp2 = memExpr.Expression as MemberExpression;
                            var alias = exp2.Member.Name;
                            var field = memExpr.Member.Name;
                            DC.Option = OptionEnum.Column;
                            if (DC.Action == ActionEnum.Select)
                            {
                                return DC.DPH.SelectColumnDic(new List<DicParam>
                                {
                                    DC.DPH.JoinColumnDic(exp2.Type, field, alias, field)
                                },colFunc);
                            }
                            else
                            {
                                return DC.DPH.JoinColumnDic(exp2.Type, field, alias, field);
                            }
                        }
                    }
                    else
                    {
                        throw XConfig.EC.Exception(XConfig.EC._021,
                            $"{memExpr.Expression.NodeType} - {memExpr.ToString()}");
                    }
                }
            }
            else
            {
                throw XConfig.EC.Exception(XConfig.EC._048, $"未知的操作 -- [[{DC.Crud}]] !!!");
            }
        }

        private DicParam NewHandle(Expression body,ColFuncEnum colFunc)
        {
            var list = new List<DicParam>();
            var nExpr = body as NewExpression;
            var args = nExpr.Arguments;
            var mems = nExpr.Members;
            for (var i = 0; i < args.Count; i++)
            {
                var cp = new lbds_列表达式().hql_获取列(DC,args[i], ColFuncEnum.None, CompareXEnum.None);
                var colAlias = mems[i].Name;
                DC.Option = OptionEnum.None;
                DC.Compare = CompareXEnum.None;
                list.Add(DC.DPH.SelectMemberInitDic(cp, colAlias));
            }

            return DC.DPH.SelectColumnDic(list,ColFuncEnum.None);
        }

        private DicParam BinaryHandle(Expression body, ParameterExpression firstParam,ColFuncEnum colFunc)
        {
            var result = default(DicParam);
            if (DC.IsSingleTableOption())
            {
                var binExpr = body as BinaryExpression;
                var pres = new List<string>
                {
                    firstParam.Name
                };
                result = HandBin(binExpr, pres,colFunc);
            }
            else if (DC.Crud == CrudEnum.Join)
            {
                var binExpr = body as BinaryExpression;
                if (DC.Action == ActionEnum.On)
                {
                    result = HandOnBinary(binExpr);
                }
                else if (DC.Action == ActionEnum.Where
                         || DC.Action == ActionEnum.And
                         || DC.Action == ActionEnum.Or)
                {
                    var pres = DC.Parameters.Select(it => it.TbAlias).ToList();
                    result = HandBin(binExpr, pres,colFunc);
                }
                else
                {
                    throw XConfig.EC.Exception(XConfig.EC._029, DC.Action.ToString());
                }
            }
            else
            {
                throw XConfig.EC.Exception(XConfig.EC._030, DC.Crud.ToString());
            }

            return result;
        }

        private DicParam MultiHandle(Expression body, ParameterExpression firstParam, ExpressionType nodeType,ColFuncEnum colFunc)
        {
            var result = DC.DPH.GroupDic(GetGroupAction(nodeType));
            var binExpr = body as BinaryExpression;
            var left = binExpr.Left;
            result.Group.Add(BodyProcess(left, firstParam,colFunc));
            var right = binExpr.Right;
            result.Group.Add(BodyProcess(right, firstParam,colFunc));
            return result;
        }

        /********************************************************************************************************************/

        private List<DicParam> HandSelectMemberInit(MemberInitExpression miExpr)
        {
            var result = new List<DicParam>();

            foreach (var mb in miExpr.Bindings)
            {
                var mbEx = mb as MemberAssignment;
                //var maMem = mbEx.Expression as MemberExpression;
                var expStr = mbEx.Expression.ToString();
                var cp = default(ColumnParam);
                if (DC.CFH.IsToStringFunc(expStr))
                {
                    //cp=
                }
                else
                {
                    cp = new lbds_列表达式().hql_获取列(DC,mbEx.Expression, ColFuncEnum.None, CompareXEnum.None);
                }

                var colAlias = mbEx.Member.Name;
                result.Add(DC.DPH.SelectMemberInitDic(cp, colAlias));
            }

            return result;
        }

        /********************************************************************************************************************/

        private DicParam HandOnBinary(BinaryExpression binExpr)
        {
            var cp1 = new lbds_列表达式().hql_获取列(DC,binExpr.Left, ColFuncEnum.None, CompareXEnum.None);
            var cp2 = new lbds_列表达式().hql_获取列(DC,binExpr.Right, ColFuncEnum.None, CompareXEnum.None);
            DC.Option = OptionEnum.Compare;
            DC.Compare = GetCompareType(binExpr.NodeType, false);
            return DC.DPH.OnDic(cp1, cp2);
        }

        /********************************************************************************************************************/

        

        

        private DicParam BodyProcess(Expression body, ParameterExpression firstParam,ColFuncEnum colFunc)
        {
            //
            var nodeType = body.NodeType;
            if (nodeType == ExpressionType.Not)
            {
                return NotHandle(body, firstParam,colFunc);
            }
            else if (nodeType == ExpressionType.Call)
            {
                return CallHandle(body as MethodCallExpression,colFunc);
            }
            else if (nodeType == ExpressionType.Constant)
            {
                var cExpr = body as ConstantExpression;
                return ConstantHandle(cExpr, cExpr.Type,colFunc);
            }
            else if (nodeType == ExpressionType.MemberAccess)
            {
                if (IsNullableExpr(body))
                {
                    return MemberAccessHandle((body as MemberExpression).Expression as MemberExpression,colFunc);
                }
                else
                {
                    return MemberAccessHandle(body as MemberExpression,colFunc);
                }
            }
            else if (nodeType == ExpressionType.Convert)
            {
                var cp = new lbds_列表达式().hql_获取列(DC,body, ColFuncEnum.None, CompareXEnum.None);
                if (string.IsNullOrWhiteSpace(cp.Key))
                {
                    throw XConfig.EC.Exception(XConfig.EC._052, "无法解析 列名2 !!!");
                }

                return DC.DPH.ColumnDic(cp,colFunc);
            }
            else if (nodeType == ExpressionType.MemberInit)
            {
                var miExpr = body as MemberInitExpression;
                return DC.DPH.SelectColumnDic(HandSelectMemberInit(miExpr),ColFuncEnum.None);
            }
            else if (nodeType == ExpressionType.New)
            {
                return NewHandle(body,colFunc);
            }
            else if (IsBinaryExpr(nodeType))
            {
                return BinaryHandle(body, firstParam,colFunc);
            }
            else if (IsMultiExpr(nodeType))
            {
                return MultiHandle(body, firstParam, nodeType,colFunc);
            }
            else
            {
                throw XConfig.EC.Exception(XConfig.EC._003, body.ToString());
            }
        }

        /********************************************************************************************************************/

        internal DicParam FuncMFExpression<M, F>(Expression<Func<M, F>> func,ColFuncEnum colFunc)
            where M : class
        {
            return BodyProcess(func.Body, null,colFunc);
        }

        internal DicParam FuncTExpression<T>(Expression<Func<T>> func,ColFuncEnum colFunc)
        {
            return BodyProcess(func.Body, null,colFunc);
        }

        internal DicParam FuncMBoolExpression<M>(Expression<Func<M, bool>> func,ColFuncEnum colFunc)
            where M : class
        {
            return BodyProcess(func.Body, func.Parameters[0],colFunc);
        }

        internal DicParam FuncBoolExpression(Expression<Func<bool>> func,ColFuncEnum colFunc)
        {
            return BodyProcess(func.Body, null,colFunc);
        }
    }
}