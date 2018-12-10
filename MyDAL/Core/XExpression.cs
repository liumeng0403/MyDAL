using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Enums;

namespace Yunyong.DataExchange.Core
{
    internal sealed class XExpression
    {

        private static CompareEnum GetCompareType(ExpressionType nodeType, bool isR)
        {
            var option = CompareEnum.None;
            if (nodeType == ExpressionType.Equal)
            {
                option = !isR ? CompareEnum.Equal : CompareEnum.Equal;
            }
            else if (nodeType == ExpressionType.NotEqual)
            {
                option = !isR ? CompareEnum.NotEqual : CompareEnum.NotEqual;
            }
            else if (nodeType == ExpressionType.LessThan)
            {
                option = !isR ? CompareEnum.LessThan : CompareEnum.GreaterThan;
            }
            else if (nodeType == ExpressionType.LessThanOrEqual)
            {
                option = !isR ? CompareEnum.LessThanOrEqual : CompareEnum.GreaterThanOrEqual;
            }
            else if (nodeType == ExpressionType.GreaterThan)
            {
                option = !isR ? CompareEnum.GreaterThan : CompareEnum.LessThan;
            }
            else if (nodeType == ExpressionType.GreaterThanOrEqual)
            {
                option = !isR ? CompareEnum.GreaterThanOrEqual : CompareEnum.LessThanOrEqual;
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

        private XExpression() { }
        internal XExpression(Context dc)
        {
            DC = dc;
        }

        /********************************************************************************************************************/

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
        private ColumnParam GetKey(Expression bodyL, FuncEnum func, string format = "")
        {
            if (bodyL.NodeType == ExpressionType.Convert)
            {
                var exp = bodyL as UnaryExpression;
                var opMem = exp.Operand;
                return GetKey(opMem, func);
            }
            else if (bodyL.NodeType == ExpressionType.MemberAccess)
            {
                var leftBody = bodyL as MemberExpression;
                var prop = default(PropertyInfo);

                //
                var mType = default(Type);
                var alias = GetAlias(leftBody);
                if (func == FuncEnum.CharLength
                    || func == FuncEnum.DateFormat)
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
                        throw new Exception($"{XConfig.EC._005} -- [[{bodyL.ToString()}]] 不能解析!!!");
                    }
                }
                else
                {
                    mType = leftBody.Expression.Type;
                    prop = mType.GetProperty(leftBody.Member.Name);
                }

                //
                var type = prop.PropertyType;
                var attr = DC.SC.GetXColumnAttribute(prop, DC.SC.GetAttrKey(XConfig.XColumnFullName, prop.Name, mType.FullName));
                var field = string.Empty;
                if (attr != null)
                {
                    field = attr.Name;
                }
                else
                {
                    field = prop.Name;
                }

                //
                return new ColumnParam
                {
                    Prop = prop.Name,
                    Key = field,
                    Alias = alias,
                    ValType = type,
                    ClassFullName = mType.FullName,
                    Format = format
                };
            }
            else if (bodyL.NodeType == ExpressionType.Call)
            {
                var mcExpr = bodyL as MethodCallExpression;
                if (func == FuncEnum.Trim
                    || func == FuncEnum.LTrim
                    || func == FuncEnum.RTrim)
                {
                    var mem = mcExpr.Object;
                    return GetKey(mem, func);
                }
                else if (func == FuncEnum.In)
                {
                    var mem = mcExpr.Arguments[0];
                    return GetKey(mem, func);
                }
                else if (func == FuncEnum.DateFormat)
                {
                    var mem = mcExpr.Object;
                    var val = DC.VH.ValueProcess(mcExpr.Arguments[0], XConfig.TC.String);
                    return GetKey(mem, func, val.val.ToString());
                }
                else
                {
                    throw new Exception($"{XConfig.EC._018} -- [[{bodyL.NodeType}-{func}]] 不能解析!!!");
                }
            }
            else
            {
                throw new Exception($"{XConfig.EC._017} -- [[{bodyL.NodeType}]] 不能解析!!!");
            }
        }

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

        private (Expression left, Expression right, ExpressionType node, bool isR) HandBinExpr(List<string> list, BinaryExpression binExpr)
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
                throw new Exception($"查询条件中使用的[[表别名变量]]不在列表[[{string.Join(",", list)}]]中!");
            }

            // 
            if (list.Any(it => leftStr.StartsWith($"{it}.", StringComparison.Ordinal))
                || (list.Any(it => leftStr.Contains($"{it}.")) && leftStr.StartsWith($"Convert(", StringComparison.Ordinal))
                || (list.Any(it => leftStr.Contains($").{it}.")) && leftStr.StartsWith($"value(", StringComparison.Ordinal))
                || (list.Any(it => leftStr.Contains($").{it}.")) && leftStr.StartsWith($"Convert(value(", StringComparison.Ordinal)))
            {
                return (binLeft, binRight, binNode, false);
            }
            else
            {
                return (binRight, binLeft, binNode, true);
            }
        }

        /********************************************************************************************************************/

        private DicParam StringLike(MethodCallExpression mcExpr, StringLikeEnum type, string funcStr)
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
                        var cp = GetKey(memO, FuncEnum.None);
                        var val = default((object val, string valStr));
                        var valExpr = mcExpr.Arguments[0];
                        switch (type)
                        {
                            case StringLikeEnum.Contains:
                                val = DC.VH.ValueProcess(valExpr, cp.ValType);
                                break;
                            case StringLikeEnum.StartsWith:
                                val = ($"{DC.VH.ValueProcess(valExpr, cp.ValType).val}%", string.Empty);
                                break;
                            case StringLikeEnum.EndsWith:
                                val = ($"%{DC.VH.ValueProcess(valExpr, cp.ValType).val}", string.Empty);
                                break;
                        }
                        DC.Option = OptionEnum.Like;
                        DC.Compare = CompareEnum.None;
                        DC.Func = FuncEnum.None;
                        return DC.DPH.LikeDic(cp, val);
                    }
                }
            }

            return null;
        }
        private DicParam CollectionIn(Expression expr, MemberExpression memExpr, string funcStr)
        {
            var cp = GetKey(expr, FuncEnum.In);
            var val = DC.VH.ValueProcess(memExpr, cp.ValType);
            DC.Option = OptionEnum.Function;
            DC.Func = FuncEnum.In;
            DC.Compare = CompareEnum.None;
            return DC.DPH.InDic(cp, val);
        }
        private DicParam NewCollectionIn(ExpressionType nodeType, Expression keyExpr, Expression valExpr, string funcStr)
        {
            if (nodeType == ExpressionType.NewArrayInit)
            {
                var naExpr = valExpr as NewArrayExpression;
                var cp = GetKey(keyExpr, FuncEnum.In);
                var vals = new List<(object val, string valStr)>();
                foreach (var exp in naExpr.Expressions)
                {
                    vals.Add(DC.VH.ValueProcess(exp, cp.ValType));
                }

                var val = (string.Join(",", vals.Select(it => it.val)), string.Empty);
                DC.Option = OptionEnum.Function;
                DC.Func = FuncEnum.In;
                DC.Compare = CompareEnum.None;
                return DC.DPH.InDic(cp, val);
            }
            else if (nodeType == ExpressionType.ListInit)
            {
                var liExpr = valExpr as ListInitExpression;
                var cp = GetKey(keyExpr, FuncEnum.In);
                var vals = new List<(object val, string valStr)>();
                foreach (var ini in liExpr.Initializers)
                {
                    vals.Add(DC.VH.ValueProcess(ini.Arguments[0], cp.ValType));
                }
                var val = (string.Join(",", vals.Select(it => it.val)), string.Empty);
                DC.Option = OptionEnum.Function;
                DC.Func = FuncEnum.In;
                DC.Compare = CompareEnum.None;
                return DC.DPH.InDic(cp, val);
            }
            else if (nodeType == ExpressionType.MemberInit)
            {
                var expr = valExpr as MemberInitExpression;
                if (expr.NewExpression.Arguments.Count == 0)
                {
                    throw new Exception($"【{keyExpr}】 中 集合为空!!!");
                }
                else
                {
                    throw new Exception($"{XConfig.EC._019} -- [[{nodeType}]] 不能解析!!!");
                }
            }
            else
            {
                throw new Exception($"{XConfig.EC._020} -- [[{nodeType}]] 不能解析!!!");
            }
        }

        /********************************************************************************************************************/

        private DicParam HandConditionBinary(BinaryExpression binExpr, List<string> pres, string funcStr)
        {
            var binTuple = HandBinExpr(pres, binExpr);
            if ((binTuple.node == ExpressionType.Equal || binTuple.node == ExpressionType.NotEqual)
                && binTuple.right.NodeType == ExpressionType.Constant
                && (binTuple.right as ConstantExpression).Value == null)
            {
                var cp = GetKey(binTuple.left, FuncEnum.None);
                if (binTuple.node == ExpressionType.Equal)
                {
                    DC.Option = OptionEnum.IsNull;
                }
                else
                {
                    DC.Option = OptionEnum.IsNotNull;
                }
                return DC.DPH.IsNullDic(cp);
            }
            else
            {
                var leftStr = binTuple.left.ToString();
                if (leftStr.Contains(".Length")
                    && leftStr.IndexOf(".") < leftStr.LastIndexOf("."))
                {
                    var cp = GetKey(binTuple.left, FuncEnum.CharLength);
                    var val = DC.VH.ValueProcess(binTuple.right, cp.ValType);
                    DC.Option = OptionEnum.Function;
                    DC.Func = FuncEnum.CharLength;
                    DC.Compare = GetCompareType(binTuple.node, binTuple.isR);
                    return DC.DPH.CharLengthDic(cp, val);
                }
                else if (leftStr.Contains(".Trim(")
                    && leftStr.IndexOf(".") < leftStr.LastIndexOf("."))
                {
                    var cp = GetKey(binTuple.left, FuncEnum.Trim);
                    var val = DC.VH.ValueProcess(binTuple.right, cp.ValType);
                    DC.Option = OptionEnum.Function;
                    DC.Func = FuncEnum.Trim;
                    DC.Compare = GetCompareType(binTuple.node, binTuple.isR);
                    return DC.DPH.TrimDic(cp, val);
                }
                else if (leftStr.Contains(".TrimStart(")
                    && leftStr.IndexOf(".") < leftStr.LastIndexOf("."))
                {
                    var cp = GetKey(binTuple.left, FuncEnum.LTrim);
                    var val = DC.VH.ValueProcess(binTuple.right, cp.ValType);
                    DC.Option = OptionEnum.Function;
                    DC.Func = FuncEnum.LTrim;
                    DC.Compare = GetCompareType(binTuple.node, binTuple.isR);
                    return DC.DPH.LTrimDic(cp, val);
                }
                else if (leftStr.Contains(".TrimEnd(")
                    && leftStr.IndexOf(".") < leftStr.LastIndexOf("."))
                {
                    var cp = GetKey(binTuple.left, FuncEnum.RTrim);
                    var val = DC.VH.ValueProcess(binTuple.right, cp.ValType);
                    DC.Option = OptionEnum.Function;
                    DC.Func = FuncEnum.RTrim;
                    DC.Compare = GetCompareType(binTuple.node, binTuple.isR);
                    return DC.DPH.RTrimDic(cp, val);
                }
                else if (leftStr.Contains(".ToString(")
                    && leftStr.IndexOf(".") < leftStr.LastIndexOf("."))
                {
                    var cp = GetKey(binTuple.left, FuncEnum.DateFormat);
                    var val = DC.VH.ValueProcess(binTuple.right, cp.ValType, cp.Format);
                    DC.Option = OptionEnum.Function;
                    DC.Func = FuncEnum.DateFormat;
                    DC.Compare = GetCompareType(binTuple.node, binTuple.isR);
                    var format = string.Empty;
                    if ("yyyy-MM-dd".Equals(cp.Format.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        format = "%Y-%m-%d";
                    }
                    else if ("yyyy-MM".Equals(cp.Format.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        format = "%Y-%m";
                    }
                    else if ("yyyy".Equals(cp.Format.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        format = "%Y";
                    }
                    else
                    {
                        throw new Exception($"{XConfig.EC._004} -- [[{funcStr}]] 未能解析!!!");
                    }
                    return DC.DPH.DateFormatDic(cp, val, format);
                }
                else
                {
                    var cp = GetKey(binTuple.left, FuncEnum.None);
                    var val = DC.VH.ValueProcess(binTuple.right, cp.ValType);
                    DC.Option = OptionEnum.Compare;
                    DC.Func = FuncEnum.None;
                    DC.Compare = GetCompareType(binTuple.node, binTuple.isR);
                    return DC.DPH.CompareDic(cp, val);
                }
            }
        }
        private DicParam HandConditionCall(MethodCallExpression mcExpr, string funcStr)
        {
            var exprStr = mcExpr.ToString();
            if (exprStr.Contains(".Contains("))
            {
                if (mcExpr.Object == null)
                {
                    var memKey = mcExpr.Arguments[1];
                    var memVal = mcExpr.Arguments[0];
                    if (memVal.NodeType == ExpressionType.MemberAccess)
                    {
                        return CollectionIn(memKey, memVal as MemberExpression, funcStr);
                    }
                    else if (memVal.NodeType == ExpressionType.NewArrayInit)
                    {
                        return NewCollectionIn(memVal.NodeType, memKey, memVal, funcStr);
                    }
                }
                else
                {
                    var objExpr = mcExpr.Object;
                    var objNodeType = mcExpr.Object.NodeType;
                    if (objNodeType == ExpressionType.MemberAccess)
                    {
                        var memO = objExpr as MemberExpression;
                        var memType = objExpr.Type;
                        if (memType.GetInterfaces() != null
                            && memType.GetInterfaces().Contains(typeof(IList))
                            && !memType.IsArray)
                        {
                            return CollectionIn(mcExpr, memO, funcStr);
                        }
                        else if (memType == typeof(string))
                        {
                            return StringLike(mcExpr, StringLikeEnum.Contains, funcStr);
                        }
                    }
                    else if (objNodeType == ExpressionType.ListInit)
                    {
                        return NewCollectionIn(objNodeType, mcExpr, objExpr, funcStr);
                    }
                    else if (objNodeType == ExpressionType.MemberInit)
                    {
                        return NewCollectionIn(objNodeType, mcExpr, objExpr, funcStr);
                    }
                }
            }
            else if (exprStr.Contains(".StartsWith("))
            {
                return StringLike(mcExpr, StringLikeEnum.StartsWith, funcStr);
            }
            else if (exprStr.Contains(".EndsWith("))
            {
                return StringLike(mcExpr, StringLikeEnum.EndsWith, funcStr);
            }

            return null;
        }
        private DicParam HandConditionConstant(ConstantExpression cExpr, Type valType)
        {
            var val = DC.VH.ValueProcess(cExpr, valType);
            if (cExpr.Type == typeof(bool))
            {
                DC.Option = OptionEnum.OneEqualOne;
                DC.Compare = CompareEnum.None;
                return DC.DPH.OneEqualOneDic(val, valType);
            }

            return null;
        }
        private DicParam HandConditionMemberAccess(MemberExpression memExpr)
        {
            var cp = GetMemTuple(memExpr);
            if (cp.ValType == typeof(bool))
            {
                DC.Option = OptionEnum.Compare;
                DC.Compare = CompareEnum.Equal;
                return DC.DPH.CompareDic(cp, (true.ToString(), string.Empty));
            }

            return null;
        }
        private ColumnParam GetMemTuple(MemberExpression memExpr)
        {
            return GetKey(memExpr, FuncEnum.None);
        }

        /********************************************************************************************************************/

        private List<DicParam> HandSelectMemberInit(MemberInitExpression miExpr)
        {
            var result = new List<DicParam>();

            foreach (var mb in miExpr.Bindings)
            {
                var mbEx = mb as MemberAssignment;
                var maMem = mbEx.Expression as MemberExpression;
                var cp = GetMemTuple(maMem);
                var colAlias = mbEx.Member.Name;
                DC.Option = OptionEnum.None;
                DC.Compare = CompareEnum.None;
                result.Add(DC.DPH.SelectMemberInitDic(cp, colAlias));
            }

            return result;
        }

        /********************************************************************************************************************/

        private DicParam HandOnBinary(BinaryExpression binExpr)
        {
            var cp1 = GetKey(binExpr.Left, FuncEnum.None);
            var cp2 = GetKey(binExpr.Right, FuncEnum.None);
            DC.Option = OptionEnum.Compare;
            DC.Compare = GetCompareType(binExpr.NodeType, false);
            return DC.DPH.OnDic(cp1, cp2);
        }

        /********************************************************************************************************************/

        [Obsolete("作废方法,仅作参考!!!")]
        private static Expression Parser(ParameterExpression parameter, Expression expression)
        {
            if (expression == null) return null;
            switch (expression.NodeType)
            {
                //一元运算符
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.ArrayLength:
                case ExpressionType.Quote:
                case ExpressionType.TypeAs:
                    {
                        var unary = expression as UnaryExpression;
                        var exp = Parser(parameter, unary.Operand);
                        return Expression.MakeUnary(expression.NodeType, exp, unary.Type, unary.Method);
                    }
                //二元运算符
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.Coalesce:
                case ExpressionType.ArrayIndex:
                case ExpressionType.RightShift:
                case ExpressionType.LeftShift:
                case ExpressionType.ExclusiveOr:
                    {
                        var binary = expression as BinaryExpression;
                        var left = Parser(parameter, binary.Left);
                        var right = Parser(parameter, binary.Right);
                        var conversion = Parser(parameter, binary.Conversion);
                        if (binary.NodeType == ExpressionType.Coalesce && binary.Conversion != null)
                        {
                            return Expression.Coalesce(left, right, conversion as LambdaExpression);
                        }
                        else
                        {
                            return Expression.MakeBinary(expression.NodeType, left, right, binary.IsLiftedToNull, binary.Method);
                        }
                    }
                //其他
                case ExpressionType.Call:
                    {
                        var call = expression as MethodCallExpression;
                        List<Expression> arguments = new List<Expression>();
                        foreach (var argument in call.Arguments)
                        {
                            arguments.Add(Parser(parameter, argument));
                        }
                        var instance = Parser(parameter, call.Object);
                        call = Expression.Call(instance, call.Method, arguments);
                        return call;
                    }
                case ExpressionType.Lambda:
                    {
                        var Lambda = expression as LambdaExpression;
                        return Parser(parameter, Lambda.Body);
                    }
                case ExpressionType.MemberAccess:
                    {
                        var memberAccess = expression as MemberExpression;
                        if (memberAccess.Expression == null)
                        {
                            memberAccess = Expression.MakeMemberAccess(null, memberAccess.Member);
                        }
                        else
                        {
                            var exp = Parser(parameter, memberAccess.Expression);
                            var member = exp.Type.GetMember(memberAccess.Member.Name).FirstOrDefault();
                            memberAccess = Expression.MakeMemberAccess(exp, member);
                        }
                        return memberAccess;
                    }
                case ExpressionType.Parameter:
                    return parameter;
                case ExpressionType.Constant:
                    return expression;
                case ExpressionType.TypeIs:
                    {
                        var typeis = expression as TypeBinaryExpression;
                        var exp = Parser(parameter, typeis.Expression);
                        return Expression.TypeIs(exp, typeis.TypeOperand);
                    }
                default:
                    throw new Exception(string.Format("Unhandled expression type: '{0}'", expression.NodeType));
            }
        }
        private DicParam BodyProcess(Expression body, ParameterExpression firstParam, string funcStr)
        {
            //
            var result = default(DicParam);

            //
            var nodeType = body.NodeType;
            if (nodeType == ExpressionType.Call)
            {

                var mcExpr = body as MethodCallExpression;
                result = HandConditionCall(mcExpr, funcStr);
            }
            else if (nodeType == ExpressionType.Constant)
            {
                var cExpr = body as ConstantExpression;
                result = HandConditionConstant(cExpr, cExpr.Type);
            }
            else if (nodeType == ExpressionType.MemberAccess)
            {
                var memExpr = body as MemberExpression;
                result = HandConditionMemberAccess(memExpr);
            }
            else if (IsBinaryExpr(nodeType))
            {
                if (DC.IsSingleTableOption())
                {
                    var binExpr = body as BinaryExpression;
                    var pres = new List<string>
                    {
                        firstParam.Name
                    };
                    result = HandConditionBinary(binExpr, pres, funcStr);
                }
                else if (DC.Crud == CrudTypeEnum.Join)
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
                        var pres = DC.Parameters.Select(it => it.TableAliasOne).ToList();
                        result = HandConditionBinary(binExpr, pres, funcStr);
                    }
                }
            }
            else if (IsMultiExpr(nodeType))
            {
                result = DC.DPH.GroupDic(GetGroupAction(nodeType));
                var binExpr = body as BinaryExpression;
                var left = binExpr.Left;
                result.Group.Add(BodyProcess(left, firstParam, funcStr));
                var right = binExpr.Right;
                result.Group.Add(BodyProcess(right, firstParam, funcStr));
            }
            else
            {
                throw new Exception($"{XConfig.EC._003} -- [[{body.ToString()}]] 不能解析!!!");
            }

            //
            return result;
        }
        private List<DicParam> BodyProcess2(Expression body)
        {
            var result = new List<DicParam>();
            var nodeType = body.NodeType;
            if (nodeType == ExpressionType.MemberAccess)
            {
                var memExpr = body as MemberExpression;
                if (DC.IsSingleTableOption()
                    || DC.Crud == CrudTypeEnum.None)
                {
                    var cp = GetKey(memExpr, FuncEnum.None);
                    if (string.IsNullOrWhiteSpace(cp.Key))
                    {
                        throw new Exception("无法解析 列名 !!!");
                    }
                    result.Add(DC.DPH.ColumnDic(cp));
                }
                else if (DC.Crud == CrudTypeEnum.Join)
                {
                    if (memExpr.Expression.NodeType == ExpressionType.Constant)
                    {
                        var alias = memExpr.Member.Name;
                        result.Add(DC.DPH.TableDic(memExpr.Type.FullName, alias));
                    }
                    else if (memExpr.Expression.NodeType == ExpressionType.MemberAccess)
                    {
                        var leftStr = memExpr.ToString();
                        if (leftStr.Contains(".Length")
                            && leftStr.IndexOf(".") < leftStr.LastIndexOf("."))
                        {
                            var cp = GetKey(memExpr, FuncEnum.CharLength);
                            DC.Func = FuncEnum.CharLength;
                            DC.Compare = CompareEnum.None;
                            result.Add(DC.DPH.CharLengthDic(cp, (null, string.Empty)));
                        }
                        else
                        {
                            var exp2 = memExpr.Expression as MemberExpression;
                            var alias = exp2.Member.Name;
                            var field = memExpr.Member.Name;
                            result.Add(DC.DPH.JoinColumnDic(exp2.Type.FullName, field, alias, field));
                        }
                    }
                }
            }
            else if (nodeType == ExpressionType.Call)
            {
                var leftStr = body.ToString();
                if (leftStr.Contains(".ToString(")
                    && leftStr.IndexOf(".") < leftStr.LastIndexOf("."))
                {
                    var cp = GetKey(body, FuncEnum.DateFormat);
                    DC.Option = OptionEnum.ColumnAs;
                    DC.Func = FuncEnum.DateFormat;
                    DC.Compare = CompareEnum.None;
                    var format = string.Empty;
                    if ("yyyy-MM-dd".Equals(cp.Format.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        format = "%Y-%m-%d";
                    }
                    else if ("yyyy-MM".Equals(cp.Format.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        format = "%Y-%m";
                    }
                    else if ("yyyy".Equals(cp.Format.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        format = "%Y";
                    }
                    else
                    {
                        throw new Exception($"{XConfig.EC._001} -- [[{body}]] 未能解析!!!");
                    }
                    result.Add(DC.DPH.DateFormatDic(cp, (null, string.Empty), format));
                }
            }
            else if (nodeType == ExpressionType.Convert)
            {
                var cp = GetKey(body, FuncEnum.None);
                if (string.IsNullOrWhiteSpace(cp.Key))
                {
                    throw new Exception("无法解析 列名2 !!!");
                }
                result.Add(DC.DPH.ColumnDic(cp));
            }
            else if (nodeType == ExpressionType.MemberInit)
            {
                var miExpr = body as MemberInitExpression;
                result = HandSelectMemberInit(miExpr);
            }
            else if (nodeType == ExpressionType.New)
            {
                var nExpr = body as NewExpression;
                var args = nExpr.Arguments;
                var mems = nExpr.Members;
                for (var i = 0; i < args.Count; i++)
                {
                    var cp = GetMemTuple(args[i] as MemberExpression);
                    var colAlias = mems[i].Name;
                    DC.Option = OptionEnum.None;
                    DC.Compare = CompareEnum.None;
                    result.Add(DC.DPH.SelectMemberInitDic(cp, colAlias));
                }

            }
            else
            {
                throw new Exception($"{XConfig.EC._002} -- [[{body.ToString()}]] 不能解析!!!");
            }
            return result;
        }

        /********************************************************************************************************************/

        internal List<DicParam> FuncMFExpression<M, F>(Expression<Func<M, F>> propertyFunc)
            where M : class
        {
            return BodyProcess2(propertyFunc.Body);
        }
        internal List<DicParam> FuncTExpression<T>(Expression<Func<T>> mOrPropFunc)
        {
            return BodyProcess2(mOrPropFunc.Body);
        }
        internal DicParam FuncMBoolExpression<M>(Expression<Func<M, bool>> func)
            where M : class
        {
            return BodyProcess(func.Body, func.Parameters[0], func.ToString());
        }
        internal DicParam FuncBoolExpression(Expression<Func<bool>> func)
        {
            return BodyProcess(func.Body, null, func.ToString());
        }

    }
}
