using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MyDAL.Core
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
        private DicParam CollectionIn(ExpressionType nodeType, Expression keyExpr, Expression valExpr, string funcStr)
        {
            if (nodeType == ExpressionType.MemberAccess)
            {
                var cp = GetKey(keyExpr, FuncEnum.In);
                var val = DC.VH.ValueProcess(valExpr, cp.ValType);
                DC.Option = OptionEnum.Function;
                DC.Func = FuncEnum.In;
                DC.Compare = CompareEnum.None;
                return DC.DPH.InDic(cp, val);
            }
            else if (nodeType == ExpressionType.NewArrayInit)
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
                var length = DC.CFH.IsLengthFunc(leftStr);
                var tp = length ? new TrimParam { Flag = false } : DC.CFH.IsTrimFunc(leftStr);
                var toString = tp.Flag ? false : DC.CFH.IsToStringFunc(leftStr);
                if (length)
                {
                    var cp = GetKey(binTuple.left, FuncEnum.CharLength);
                    var val = DC.VH.ValueProcess(binTuple.right, cp.ValType);
                    DC.Option = OptionEnum.Function;
                    DC.Func = FuncEnum.CharLength;
                    DC.Compare = GetCompareType(binTuple.node, binTuple.isR);
                    return DC.DPH.CharLengthDic(cp, val);
                }
                else if (tp.Flag)
                {
                    var cp = GetKey(binTuple.left, tp.Trim);
                    var val = DC.VH.ValueProcess(binTuple.right, cp.ValType);
                    DC.Option = OptionEnum.Function;
                    DC.Func = tp.Trim;
                    DC.Compare = GetCompareType(binTuple.node, binTuple.isR);
                    return DC.DPH.TrimDic(cp, val);
                }
                else if (toString)
                {
                    var cp = GetKey(binTuple.left, FuncEnum.DateFormat);
                    var val = DC.VH.ValueProcess(binTuple.right, cp.ValType, cp.Format);
                    DC.Option = OptionEnum.Function;
                    DC.Func = FuncEnum.DateFormat;
                    DC.Compare = GetCompareType(binTuple.node, binTuple.isR);
                    var format = DC.TSH.DateTime(cp.Format);
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
            var clp = DC.CFH.IsContainsLikeFunc(mcExpr);
            var cip = clp.Flag ? new ContainsInParam { Flag = false } : DC.CFH.IsContainsInFunc(mcExpr);
            if (clp.Flag)
            {
                if (clp.Like == StringLikeEnum.Contains
                    || clp.Like == StringLikeEnum.StartsWith
                    || clp.Like == StringLikeEnum.EndsWith)
                {
                    return StringLike(mcExpr, clp.Like, funcStr);
                }
                else
                {
                    throw new Exception($"出现异常 -- [[{mcExpr.ToString()}]] 不能解析!!!");
                }
            }
            else if (cip.Flag)
            {
                if (cip.Type == ExpressionType.MemberAccess
                    || cip.Type == ExpressionType.NewArrayInit
                    || cip.Type == ExpressionType.ListInit
                    || cip.Type == ExpressionType.MemberInit)
                {
                    return CollectionIn(cip.Type, cip.Key, cip.Val, funcStr);
                }
                else
                {
                    throw new Exception($"出现异常 -- [[{mcExpr.ToString()}]] 不能解析!!!");
                }
            }
            else
            {
                throw new Exception($"出现异常 -- [[{mcExpr.ToString()}]] 不能解析!!!");
            }
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
            var cp = GetKey(memExpr, FuncEnum.None); //GetMemTuple(memExpr);
            if (cp.ValType == typeof(bool))
            {
                DC.Option = OptionEnum.Compare;
                DC.Compare = CompareEnum.Equal;
                return DC.DPH.CompareDic(cp, (true.ToString(), string.Empty));
            }

            return null;
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
                    cp = GetKey(mbEx.Expression, FuncEnum.None); //GetMemTuple(maMem);
                }
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
                        if (DC.CFH.IsLengthFunc(leftStr))
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
                if (DC.CFH.IsToStringFunc(leftStr))
                {
                    var cp = GetKey(body, FuncEnum.DateFormat);
                    DC.Option = OptionEnum.ColumnAs;
                    DC.Func = FuncEnum.DateFormat;
                    DC.Compare = CompareEnum.None;
                    var format = DC.TSH.DateTime(cp.Format);
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
                    var cp = GetKey(args[i], FuncEnum.None);//GetMemTuple(args[i] as MemberExpression);
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
