using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Core.Extensions;

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
        private (string key, string alias, Type valType, string classFullName, string format) GetKey(Expression bodyL, OptionEnum option, string format = "")
        {
            if (bodyL.NodeType == ExpressionType.Convert)
            {
                var exp = bodyL as UnaryExpression;
                var opMem = exp.Operand;
                return GetKey(opMem, option);
            }
            else if (bodyL.NodeType == ExpressionType.MemberAccess)
            {
                var leftBody = bodyL as MemberExpression;
                var info = default(PropertyInfo);

                //
                var paramType = default(Type);
                var alias = GetAlias(leftBody);
                if (option == OptionEnum.CharLength)
                {
                    var clMemExpr = leftBody.Expression as MemberExpression;
                    paramType = clMemExpr.Expression.Type;
                    info = paramType.GetProperty(clMemExpr.Member.Name);
                }
                else
                {
                    paramType = leftBody.Expression.Type;
                    info = paramType.GetProperty(leftBody.Member.Name);
                }

                //
                var type = info.PropertyType;
                var attr = DC.SC.GetXColumnAttribute(info, DC.SC.GetAttrKey(XConfig.XColumnFullName, info.Name, paramType.FullName));
                var field = string.Empty;
                if (attr != null)
                {
                    field = attr.Name;
                }
                else
                {
                    field = info.Name;
                }

                //
                return (field, alias, type, paramType.FullName, format);
            }
            else if (bodyL.NodeType == ExpressionType.Call)
            {
                var mcExpr = bodyL as MethodCallExpression;
                if (option == OptionEnum.Trim
                    || option == OptionEnum.LTrim
                    || option == OptionEnum.RTrim)
                {
                    var mem = mcExpr.Object;
                    return GetKey(mem, option);
                }
                else if (option == OptionEnum.In)
                {
                    var mem = mcExpr.Arguments[0];
                    return GetKey(mem, option);
                }
                else if (option == OptionEnum.DateFormat)
                {
                    var mem = mcExpr.Object;
                    var con = mcExpr.Arguments[0] as ConstantExpression;
                    return GetKey(mem, option, con.Value.ToString());
                }
            }

            return (default(string), default(string), default(Type), default(string), default(string));
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
        private (object val, string valStr) HandleMember(MemberExpression binRight, string funcStr, Type valType)
        {
            var result = default((object val, string valStr));
            if (binRight.NodeType == ExpressionType.MemberAccess)
            {
                result = DC.VH.MemberValue(binRight, funcStr, valType);
            }
            else
            {
                throw new Exception();
            }
            return result;
        }
        private (object val, string valStr) HandleBinary(Expression binRight, string funcStr, Type valType)
        {
            var val = default((object val, string valStr));

            //
            switch (binRight.NodeType)
            {
                case ExpressionType.Constant:
                    var con = binRight as ConstantExpression;
                    val = DC.VH.ConstantValue(con, valType);
                    break;
                case ExpressionType.Call:
                    var rightExpr = binRight as MethodCallExpression;
                    val = DC.VH.MethodCallValue(rightExpr, funcStr, valType);
                    break;
                case ExpressionType.MemberAccess:
                    val = HandleMember(binRight as MemberExpression, funcStr, valType);
                    break;
                case ExpressionType.Convert:
                    val = DC.VH.ConvertValue(binRight as UnaryExpression, funcStr, valType);
                    break;
                default:
                    throw new Exception();
            }

            //
            return val;
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
                        var keyTuple = GetKey(memO, OptionEnum.Like);
                        var val = default((object val, string valStr));
                        switch (type)
                        {
                            case StringLikeEnum.Contains:
                                val = DC.VH.MethodCallValue(mcExpr, funcStr, keyTuple.valType);
                                break;
                            case StringLikeEnum.StartsWith:
                                val = ($"{DC.VH.MethodCallValue(mcExpr, funcStr, keyTuple.valType).val}%", string.Empty);
                                break;
                            case StringLikeEnum.EndsWith:
                                val = ($"%{DC.VH.MethodCallValue(mcExpr, funcStr, keyTuple.valType).val}", string.Empty);
                                break;
                        }
                        DC.Option = OptionEnum.Like;
                        DC.Compare = CompareEnum.None;
                        return DC.DPH.LikeDic(keyTuple.classFullName, keyTuple.key, keyTuple.alias, val, keyTuple.valType);
                    }
                }
            }

            return null;
        }
        private DicParam CollectionIn(Expression expr, MemberExpression memExpr, string funcStr)
        {
            var keyTuple = GetKey(expr, OptionEnum.In);
            var val = HandleMember(memExpr, funcStr, keyTuple.valType);
            DC.Option = OptionEnum.In;
            DC.Compare = CompareEnum.None;
            return DC.DPH.InDic(keyTuple.classFullName, keyTuple.key, keyTuple.alias, val, keyTuple.valType);
        }
        private DicParam NewCollectionIn(ExpressionType nodeType, Expression keyExpr, Expression valExpr, string funcStr)
        {
            if (nodeType == ExpressionType.NewArrayInit)
            {
                var naExpr = valExpr as NewArrayExpression;
                var keyTuple = GetKey(keyExpr, OptionEnum.In);
                var vals = new List<(object val, string valStr)>();
                foreach (var exp in naExpr.Expressions)
                {
                    if (exp.NodeType == ExpressionType.Constant)
                    {
                        vals.Add(DC.VH.ConstantValue(exp as ConstantExpression, keyTuple.valType));
                    }
                    else if (exp.NodeType == ExpressionType.Convert)
                    {
                        vals.Add(DC.VH.ConvertValue(exp as UnaryExpression, funcStr, keyTuple.valType));
                    }
                }

                var val = (string.Join(",", vals.Select(it => it.val)), string.Empty);
                DC.Option = OptionEnum.In;
                DC.Compare = CompareEnum.None;
                return DC.DPH.InDic(keyTuple.classFullName, keyTuple.key, keyTuple.alias, val, keyTuple.valType);
            }
            else if (nodeType == ExpressionType.ListInit)
            {
                var liExpr = valExpr as ListInitExpression;
                var keyTuple = GetKey(keyExpr, OptionEnum.In);
                var vals = new List<(object val, string valStr)>();
                foreach (var ini in liExpr.Initializers)
                {
                    var arg = ini.Arguments[0];
                    if (arg.NodeType == ExpressionType.Constant)
                    {
                        vals.Add(DC.VH.ConstantValue(arg as ConstantExpression, keyTuple.valType));
                    }
                    else if (arg.NodeType == ExpressionType.Convert)
                    {
                        vals.Add(DC.VH.ConvertValue(arg as UnaryExpression, funcStr, keyTuple.valType));
                    }
                    else if (arg.NodeType == ExpressionType.MemberAccess)
                    {
                        vals.Add(DC.VH.MemberValue(arg as MemberExpression, funcStr, keyTuple.valType));
                    }
                    else
                    {
                        throw new Exception($"NewCollectionIn ListInit {arg.NodeType} 未能解析!!!");
                    }
                }

                var val = (string.Join(",", vals.Select(it => it.val)), string.Empty);
                DC.Option = OptionEnum.In;
                DC.Compare = CompareEnum.None;
                return DC.DPH.InDic(keyTuple.classFullName, keyTuple.key, keyTuple.alias, val, keyTuple.valType);
            }
            else if (nodeType == ExpressionType.MemberInit)
            {
                var expr = valExpr as MemberInitExpression;
                if (expr.NewExpression.Arguments.Count == 0)
                {
                    throw new Exception($"【{keyExpr}】 中 集合为空!!!");
                }
            }

            return null;
        }

        /********************************************************************************************************************/

        private DicParam HandConditionBinary(BinaryExpression binExpr, List<string> pres, string funcStr)
        {
            var binTuple = HandBinExpr(pres, binExpr);
            if ((binTuple.node == ExpressionType.Equal || binTuple.node == ExpressionType.NotEqual)
                && binTuple.right.NodeType == ExpressionType.Constant
                && (binTuple.right as ConstantExpression).Value == null)
            {
                var optionx = OptionEnum.None;
                var tuple = GetKey(binTuple.left, optionx);
                if (binTuple.node == ExpressionType.Equal)
                {
                    optionx = OptionEnum.IsNull;
                }
                else
                {
                    optionx = OptionEnum.IsNotNull;
                }
                DC.Option = optionx;
                return DC.DPH.IsNullDic(tuple.classFullName, tuple.key, tuple.alias, tuple.valType);
            }
            else
            {
                var leftStr = binTuple.left.ToString();
                if (leftStr.Contains(".Length")
                    && leftStr.IndexOf(".") < leftStr.LastIndexOf("."))
                {
                    var keyTuple = GetKey(binTuple.left, OptionEnum.CharLength);
                    var val = HandleBinary(binTuple.right, funcStr, keyTuple.valType);
                    DC.Option = OptionEnum.CharLength;
                    DC.Compare = GetCompareType(binTuple.node, binTuple.isR);
                    return DC.DPH.CharLengthDic(keyTuple.classFullName, keyTuple.key, keyTuple.alias, val, keyTuple.valType);
                    //dic.ClassFullName = ;
                    //return dic;
                }
                else if (leftStr.Contains(".Trim(")
                    && leftStr.IndexOf(".") < leftStr.LastIndexOf("."))
                {
                    var tuple = GetKey(binTuple.left, OptionEnum.Trim);
                    var val = HandleBinary(binTuple.right, funcStr, tuple.valType);
                    DC.Option = OptionEnum.Trim;
                    DC.Compare = GetCompareType(binTuple.node, binTuple.isR);
                    var dic = DC.DPH.TrimDic(tuple.key, tuple.alias, val, tuple.valType);
                    dic.ClassFullName = tuple.classFullName;
                    return dic;
                }
                else if (leftStr.Contains(".TrimStart(")
                    && leftStr.IndexOf(".") < leftStr.LastIndexOf("."))
                {
                    var tuple = GetKey(binTuple.left, OptionEnum.LTrim);
                    var val = HandleBinary(binTuple.right, funcStr, tuple.valType);
                    DC.Option = OptionEnum.LTrim;
                    DC.Compare = GetCompareType(binTuple.node, binTuple.isR);
                    var dic = DC.DPH.LTrimDic(tuple.key, tuple.alias, val, tuple.valType);
                    dic.ClassFullName = tuple.classFullName;
                    return dic;
                }
                else if (leftStr.Contains(".TrimEnd(")
                    && leftStr.IndexOf(".") < leftStr.LastIndexOf("."))
                {
                    var tuple = GetKey(binTuple.left, OptionEnum.RTrim);
                    var val = HandleBinary(binTuple.right, funcStr, tuple.valType);
                    DC.Option = OptionEnum.RTrim;
                    DC.Compare = GetCompareType(binTuple.node, binTuple.isR);
                    var dic = DC.DPH.RTrimDic(tuple.key, tuple.alias, val, tuple.valType);
                    dic.ClassFullName = tuple.classFullName;
                    return dic;
                }
                else if (leftStr.Contains(".ToString(")
                    && leftStr.IndexOf(".") < leftStr.LastIndexOf("."))
                {
                    var tuple = GetKey(binTuple.left, OptionEnum.DateFormat);
                    var val = HandleBinary(binTuple.right, funcStr, tuple.valType);
                    DC.Option = OptionEnum.DateFormat;
                    DC.Compare = GetCompareType(binTuple.node, binTuple.isR);
                    var format = string.Empty;
                    if ("yyyy-MM-dd".Equals(tuple.format.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        format = "%Y-%m-%d";
                    }
                    else if("yyyy-MM".Equals(tuple.format.Trim(),StringComparison.OrdinalIgnoreCase))
                    {
                        format = "%Y-%m";
                    }
                    else if("yyyy".Equals(tuple.format.Trim(),StringComparison.OrdinalIgnoreCase))
                    {
                        format = "%Y";
                    }
                    else
                    {
                        throw new Exception($"{XConfig._001} -- [[{funcStr}]] 未能解析!!!");
                    }
                    var dic = DC.DPH.DateFormatDic(tuple.key, tuple.alias, val, tuple.valType, format);
                    dic.ClassFullName = tuple.classFullName;
                    return dic;
                }
                else
                {
                    var keyTuple = GetKey(binTuple.left, OptionEnum.Compare);
                    var val = HandleBinary(binTuple.right, funcStr, keyTuple.valType);
                    DC.Option = OptionEnum.Compare;
                    DC.Compare = GetCompareType(binTuple.node, binTuple.isR);
                    return DC.DPH.CompareDic(keyTuple.classFullName, keyTuple.key, keyTuple.alias, val, keyTuple.valType);
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
            var val = DC.VH.ConstantValue(cExpr, valType);
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
            var tuple = GetMemTuple(memExpr);
            if (tuple.valType == typeof(bool))
            {
                DC.Option = OptionEnum.Compare;
                DC.Compare = CompareEnum.Equal;
                return DC.DPH.CompareDic(tuple.classFullName, tuple.key, tuple.alias, (true.ToString(), string.Empty), tuple.valType);
            }

            return null;
        }
        private (string key, string alias, Type valType, string classFullName) GetMemTuple(MemberExpression memExpr)
        {
            var tuple = GetKey(memExpr, OptionEnum.None);
            return (tuple.key, tuple.alias, tuple.valType, tuple.classFullName);
        }

        /********************************************************************************************************************/

        private List<DicParam> HandSelectMemberInit(MemberInitExpression miExpr)
        {
            var result = new List<DicParam>();

            foreach (var mb in miExpr.Bindings)
            {
                var mbEx = mb as MemberAssignment;
                var maMem = mbEx.Expression as MemberExpression;
                var tuple = GetMemTuple(maMem);
                var colAlias = mbEx.Member.Name;
                DC.Option = OptionEnum.None;
                DC.Compare = CompareEnum.None;
                result.Add(DC.DPH.SelectMemberInitDic(tuple.classFullName, tuple.key, tuple.alias, colAlias));
            }

            return result;
        }

        /********************************************************************************************************************/

        private DicParam HandOnBinary(BinaryExpression binExpr)
        {
            var option = OptionEnum.Compare;
            var tuple1 = GetKey(binExpr.Left, option);
            var tuple2 = GetKey(binExpr.Right, option);
            DC.Option = option;
            DC.Compare = GetCompareType(binExpr.NodeType, false);
            return DC.DPH.OnDic(tuple1.classFullName, tuple1.key, tuple1.alias, tuple2.key, tuple2.alias);
        }

        /********************************************************************************************************************/

        internal List<DicParam> FuncMFExpression<M, F>(Expression<Func<M, F>> func)
            where M : class
        {
            try
            {
                var result = new List<DicParam>();
                var body = func.Body;
                var nodeType = body.NodeType;
                if (nodeType == ExpressionType.MemberAccess)
                {
                    var memExpr = func.Body as MemberExpression;
                    var keyTuple = GetKey(memExpr, OptionEnum.None);
                    var key = keyTuple.key;

                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        result.Add(DC.DPH.ColumnDic(keyTuple.classFullName, key));
                    }
                }
                else if (nodeType == ExpressionType.MemberInit)
                {
                    var miExpr = func.Body as MemberInitExpression;
                    result = HandSelectMemberInit(miExpr);
                }
                else if (nodeType == ExpressionType.Convert)
                {
                    var tuple = GetKey(body, OptionEnum.None);
                    var key = tuple.key;
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        result.Add(DC.DPH.ColumnDic(tuple.classFullName, key));
                    }
                }

                if (result != null
                    && result.Count > 0)
                {
                    return result;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrWhiteSpace(ex.Message))
                {
                    throw ex;
                }
                else
                {
                    throw new Exception($"不支持的表达式:[{func.ToString()}]");
                }
            }
        }
        internal List<DicParam> FuncTExpression<T>(Expression<Func<T>> func)
        {
            try
            {
                var result = new List<DicParam>();
                var nodeType = func.Body.NodeType;
                if (nodeType == ExpressionType.New)
                {
                    var nExpr = func.Body as NewExpression;
                    var args = nExpr.Arguments;
                    var mems = nExpr.Members;
                    for (var i = 0; i < args.Count; i++)
                    {
                        var tuple = GetMemTuple(args[i] as MemberExpression);
                        var colAlias = mems[i].Name;
                        DC.Option = OptionEnum.None;
                        DC.Compare = CompareEnum.None;
                        result.Add(DC.DPH.SelectMemberInitDic(tuple.classFullName, tuple.key, tuple.alias, colAlias));
                    }

                }
                else if (nodeType == ExpressionType.MemberAccess)
                {
                    var body = func.Body as MemberExpression;
                    if (body.Expression.NodeType == ExpressionType.Constant)
                    {
                        var alias = body.Member.Name;
                        result.Add(DC.DPH.TableDic(body.Type.FullName, alias));
                    }
                    else if (body.Expression.NodeType == ExpressionType.MemberAccess)
                    {
                        var leftStr = body.ToString();
                        if (leftStr.Contains(".Length")
                            && leftStr.IndexOf(".") < leftStr.LastIndexOf("."))
                        {
                            var keyTuple = GetKey(body, OptionEnum.CharLength);
                            DC.Func = FuncEnum.CharLength;
                            DC.Compare = CompareEnum.None;
                            result.Add(DC.DPH.CharLengthDic(keyTuple.classFullName, keyTuple.key, keyTuple.alias, (null, string.Empty), keyTuple.valType));
                        }
                        else
                        {
                            var exp2 = body.Expression as MemberExpression;
                            var alias = exp2.Member.Name;
                            var field = body.Member.Name;
                            result.Add(DC.DPH.JoinColumnDic(exp2.Type.FullName, field, alias));
                        }
                    }
                }
                else if (nodeType == ExpressionType.MemberInit)
                {
                    var miExpr = func.Body as MemberInitExpression;
                    result = HandSelectMemberInit(miExpr);
                }

                //
                if (result != null
                    && result.Count > 0)
                {
                    return result;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrWhiteSpace(ex.Message))
                {
                    throw ex;
                }
                else
                {
                    throw new Exception($"不支持的表达式:[{func.ToString()}]");
                }
            }
        }
        internal DicParam FuncMBoolExpression<M>(Expression<Func<M, bool>> func)
            where M : class
        {
            try
            {
                var body = func.Body;
                var nodeType = body.NodeType;
                var result = BodyProcess(nodeType, body, func.Parameters[0], func.ToString());
                if (result != null)
                {
                    return result;
                }
                else
                {
                    throw new Exception("");
                }
            }
            catch (Exception ex)
            {
                if (!ex.Message.IsNullStr())
                {
                    throw ex;
                }
                else
                {
                    throw new Exception($"不支持的表达式:[{func.ToString()}]");
                }
            }
        }
        internal DicParam FuncBoolExpression(Expression<Func<bool>> func)
        {
            try
            {
                var body = func.Body;
                var nodeType = body.NodeType;
                var result = BodyProcess(nodeType, body, null, func.ToString());
                if (result != null)
                {
                    return result;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrWhiteSpace(ex.Message))
                {
                    throw ex;
                }
                else
                {
                    throw new Exception($"不支持的表达式:[{func.ToString()}]");
                }
            }
        }

        /********************************************************************************************************************/

        private DicParam BodyProcess(ExpressionType nodeType, Expression body, ParameterExpression firstParam, string funcStr)
        {
            //
            var result = default(DicParam);

            //
            if (IsMultiExpr(nodeType))
            {
                result = DC.DPH.GroupDic(GetGroupAction(nodeType));
                var binExpr = body as BinaryExpression;
                var left = binExpr.Left;
                result.Group.Add(BodyProcess(left.NodeType, left, firstParam, funcStr));
                var right = binExpr.Right;
                result.Group.Add(BodyProcess(right.NodeType, right, firstParam, funcStr));
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
            else if (nodeType == ExpressionType.Call)
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

            //
            return result;
        }
        [Obsolete("仅作参考使用!!!")]
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
    }
}
