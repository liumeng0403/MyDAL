using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Yunyong.DataExchange.Cache;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Enums;

namespace Yunyong.DataExchange.Core
{
    internal class XExpression
    {

        internal static CompareEnum GetCompareType(ExpressionType nodeType, bool isR)
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

        // -01-02- 
        private (string key, string alias, Type valType, string classFullName) GetKey(Expression bodyL, OptionEnum option)
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
                var field = StaticCache
                    .EHCache
                    .GetOrAdd($"{paramType.FullName}:{info.Module.GetHashCode()}", moduleKey => new ConcurrentDictionary<Int32, String>())
                    .GetOrAdd(info.MetadataToken, innnerKey =>
                    {
                        if (info.IsDefined(typeof(XColumnAttribute), false))
                        {
                            var attr = (XColumnAttribute)info.GetCustomAttributes(typeof(XColumnAttribute), false)[0];
                            return attr.Name;
                        }
                        return info.Name;
                    });

                //
                return (field, alias, type, paramType.FullName);
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
            }

            return (default(string), default(string), default(Type), default(string));
        }

        // 01
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
        // 01

        // 01
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
        // 02
        private (object val,string valStr) HandleMember(MemberExpression binRight, string funcStr,Type valType)
        {
            var result = default((object val, string valStr));
            if (binRight.NodeType == ExpressionType.MemberAccess)
            {
                result = DC.VH.MemberValue(binRight, funcStr,valType);
            }
            else
            {
                throw new Exception();
            }
            return result;
        }

        // 01
        private (object val, string valStr) HandleBinary(Expression binRight, string funcStr,Type valType)
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
                    val = DC.VH.MethodCallValue(rightExpr, funcStr,valType);
                    break;
                case ExpressionType.MemberAccess:
                    val = HandleMember(binRight as MemberExpression, funcStr,valType);
                    break;
                case ExpressionType.Convert:
                    val = DC.VH.ConvertValue(binRight as UnaryExpression, funcStr,valType);
                    break;
                default:
                    throw new Exception();
            }

            //
            return val;
        }

        /********************************************************************************************************************/

        private DicModelUI StringLike(MethodCallExpression mcExpr, StringLikeEnum type ,string funcStr)
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
                                val = ($"{DC.VH.MethodCallValue(mcExpr, funcStr, keyTuple.valType).val}%",string.Empty);
                                break;
                            case StringLikeEnum.EndsWith:
                                val = ($"%{DC.VH.MethodCallValue(mcExpr, funcStr, keyTuple.valType).val}",string.Empty);
                                break;
                        }
                        DC.Option = OptionEnum.Like;
                        DC.Compare =  CompareEnum.None;
                        return DC.DH.LikeDic( keyTuple.classFullName, keyTuple.key, keyTuple.alias, val, keyTuple.valType);
                    }
                }
            }

            return null;
        }

        private DicModelUI CollectionIn(Expression expr, MemberExpression memExpr,  string funcStr)
        {
            var keyTuple = GetKey(expr, OptionEnum.In);
            var val = HandleMember(memExpr, funcStr, keyTuple.valType);
            DC.Option = OptionEnum.In;
            DC.Compare = CompareEnum.None;
            return DC.DH.InDic(  keyTuple.classFullName, keyTuple.key, keyTuple.alias, val, keyTuple.valType);
        }

        private DicModelUI NewCollectionIn(ExpressionType nodeType, Expression keyExpr, Expression valExpr ,string funcStr)
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

                var val = (string.Join(",", vals.Select(it=>it.val)),string.Empty);
                DC.Option = OptionEnum.In;
                DC.Compare = CompareEnum.None;
                return DC.DH.InDic(  keyTuple.classFullName, keyTuple.key, keyTuple.alias, val, keyTuple.valType);
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
                }

                var val = (string.Join(",", vals.Select(it=>it.val)),string.Empty);
                DC.Option = OptionEnum.In;
                DC.Compare = CompareEnum.None;
                return DC.DH.InDic( keyTuple.classFullName, keyTuple.key, keyTuple.alias, val, keyTuple.valType);
            }

            return null;
        }

        /********************************************************************************************************************/

        private DicModelUI HandConditionBinary( BinaryExpression binExpr, List<string> pres, string funcStr)
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
                return DC.DH.IsNullDic(tuple.classFullName, tuple.key, tuple.alias, tuple.valType);
            }
            else
            {
                var leftStr = binTuple.left.ToString();
                if (leftStr.Contains(".Length")
                    && leftStr.IndexOf(".") < leftStr.LastIndexOf("."))
                {
                    var keyTuple = GetKey(binTuple.left, OptionEnum.CharLength);
                    var val = HandleBinary(binTuple.right, funcStr,keyTuple.valType);
                    DC.Option = OptionEnum.CharLength;
                    DC.Compare = GetCompareType(binTuple.node, binTuple.isR);
                    var dic = DC.DH.CharLengthDic(keyTuple.key, keyTuple.alias, val, keyTuple.valType );
                    dic.ClassFullName = keyTuple.classFullName;
                    return dic;
                }
                else if (leftStr.Contains(".Trim(")
                    && leftStr.IndexOf(".") < leftStr.LastIndexOf("."))
                {
                    var tuple = GetKey(binTuple.left, OptionEnum.Trim);
                    var val = HandleBinary(binTuple.right, funcStr,tuple.valType);
                    DC.Option = OptionEnum.Trim;
                    DC.Compare = GetCompareType(binTuple.node, binTuple.isR);
                    var dic = DC.DH.TrimDic(tuple.key, tuple.alias, val, tuple.valType);
                    dic.ClassFullName = tuple.classFullName;
                    return dic;
                }
                else if (leftStr.Contains(".TrimStart(")
                    && leftStr.IndexOf(".") < leftStr.LastIndexOf("."))
                {
                    var tuple = GetKey(binTuple.left, OptionEnum.LTrim);
                    var val = HandleBinary(binTuple.right, funcStr,tuple.valType);
                    DC.Option = OptionEnum.LTrim;
                    DC.Compare = GetCompareType(binTuple.node, binTuple.isR);
                    var dic = DC.DH.LTrimDic(tuple.key, tuple.alias, val, tuple.valType);
                    dic.ClassFullName = tuple.classFullName;
                    return dic;
                }
                else if (leftStr.Contains(".TrimEnd(")
                    && leftStr.IndexOf(".") < leftStr.LastIndexOf("."))
                {
                    var tuple = GetKey(binTuple.left, OptionEnum.RTrim);
                    var val = HandleBinary(binTuple.right, funcStr,tuple.valType);
                    DC.Option = OptionEnum.RTrim;
                    DC.Compare = GetCompareType(binTuple.node, binTuple.isR);
                    var dic = DC.DH.RTrimDic(tuple.key, tuple.alias, val, tuple.valType );
                    dic.ClassFullName = tuple.classFullName;
                    return dic;
                }
                else
                {
                    var keyTuple = GetKey(binTuple.left, OptionEnum.Compare);
                    var val = HandleBinary(binTuple.right, funcStr,keyTuple.valType);
                    DC.Option = OptionEnum.Compare;
                    DC.Compare = GetCompareType(binTuple.node, binTuple.isR);
                    return DC.DH.CompareDic( keyTuple.classFullName, keyTuple.key, keyTuple.alias, val, keyTuple.valType );
                }
            }
        }

        private DicModelUI HandConditionCall(MethodCallExpression mcExpr,   string funcStr)
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
                        return NewCollectionIn(memVal.NodeType, memKey, memVal,  funcStr);
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
                            return CollectionIn(mcExpr, memO,  funcStr);
                        }
                        else if (memType == typeof(string))
                        {
                            return StringLike(mcExpr, StringLikeEnum.Contains,  funcStr);
                        }
                    }
                    else if (objNodeType == ExpressionType.ListInit)
                    {
                        return NewCollectionIn(objNodeType, mcExpr, objExpr,  funcStr);
                    }
                }
            }
            else if (exprStr.Contains(".StartsWith("))
            {
                return StringLike(mcExpr, StringLikeEnum.StartsWith,  funcStr);
            }
            else if (exprStr.Contains(".EndsWith("))
            {
                return StringLike(mcExpr, StringLikeEnum.EndsWith,  funcStr);
            }

            return null;
        }

        private DicModelUI HandConditionConstant(ConstantExpression cExpr,Type valType)
        {
            var val = DC.VH.ConstantValue(cExpr, valType);
            if (cExpr.Type == typeof(bool))
            {
                DC.Option = OptionEnum.OneEqualOne;
                DC.Compare = CompareEnum.None;
                return DC.DH.OneEqualOneDic(val, valType);
            }

            return null;
        }

        private DicModelUI HandConditionMemberAccess(MemberExpression memExpr)
        {
            var tuple = GetMemTuple(memExpr);
            if (tuple.valType == typeof(bool))
            {
                DC.Option = OptionEnum.Compare;
                DC.Compare = CompareEnum.Equal;
                return DC.DH.CompareDic(tuple.classFullName, tuple.key, tuple.alias, (true.ToString(),string.Empty), tuple.valType );
            }

            return null;
        }

        private (string key, string alias, Type valType, string classFullName) GetMemTuple(MemberExpression memExpr)
        {
            var tuple = GetKey(memExpr, OptionEnum.None);
            return (tuple.key, tuple.alias, tuple.valType, tuple.classFullName);
        }

        /********************************************************************************************************************/

        private List<DicModelUI> HandSelectMemberInit( MemberInitExpression miExpr)
        {
            var result = new List<DicModelUI>();

            foreach (var mb in miExpr.Bindings)
            {
                var mbEx = mb as MemberAssignment;
                var maMem = mbEx.Expression as MemberExpression;
                var tuple = GetMemTuple(maMem);
                var colAlias = mbEx.Member.Name;
                DC.Option = OptionEnum.None;
                DC.Compare = CompareEnum.None;
                result.Add(DC.DH.SelectMemberInitDic( tuple.classFullName, tuple.key, tuple.alias, colAlias));
            }

            return result;
        }

        /********************************************************************************************************************/

        private DicModelUI HandOnBinary(BinaryExpression binExpr)
        {
            var option = OptionEnum.Compare;
            var tuple1 = GetKey(binExpr.Left, option);
            var tuple2 = GetKey(binExpr.Right, option);
            DC.Option = option;
            DC.Compare = GetCompareType(binExpr.NodeType, false);
            return DC.DH.OnDic(tuple1.classFullName, tuple1.key, tuple1.alias, tuple2.key, tuple2.alias);
            //    new DicModelUI
            //{
            //    ClassFullName = tuple1.classFullName,
            //    ColumnOne = tuple1.key,
            //    TableAliasOne = tuple1.alias,
            //    ColumnTwo = tuple2.key,
            //    TableAliasTwo = tuple2.alias,
            //    Option = option,
            //    Compare = GetCompareType(binExpr.NodeType, false)
            //};
        }

        /********************************************************************************************************************/

        internal List<DicModelUI> FuncMFExpression<M, F>( Expression<Func<M, F>> func)
        {
            try
            {
                var result = new List<DicModelUI>();
                var body = func.Body;
                var nodeType = body.NodeType;
                if (nodeType == ExpressionType.MemberAccess)
                {
                    var memExpr = func.Body as MemberExpression;
                    var keyTuple = GetKey(memExpr, OptionEnum.None);
                    var key = keyTuple.key;

                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        result.Add(DC.DH.ColumnDic(keyTuple.classFullName, key));
                    }
                }
                else if (nodeType == ExpressionType.MemberInit)
                {
                    var miExpr = func.Body as MemberInitExpression;
                    result = HandSelectMemberInit( miExpr);
                }
                else if (nodeType == ExpressionType.Convert)
                {
                    var tuple = GetKey(body, OptionEnum.None);
                    var key = tuple.key;
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        result.Add(DC.DH.ColumnDic(tuple.classFullName, key));
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
        internal List<DicModelUI> FuncMExpression<M>(Expression<Func<M>> func)
        {
            try
            {
                var result = new List<DicModelUI>();
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
                        result.Add(DC.DH.SelectMemberInitDic(tuple.classFullName, tuple.key, tuple.alias, colAlias));
                    }

                }
                else if (nodeType == ExpressionType.MemberAccess)
                {
                    var body = func.Body as MemberExpression;
                    if (body.Expression.NodeType == ExpressionType.Constant)
                    {
                        var alias = body.Member.Name;
                        result.Add(DC.DH.TableDic(body.Type.FullName, alias));
                    }
                    else if (body.Expression.NodeType == ExpressionType.MemberAccess)
                    {
                        var exp2 = body.Expression as MemberExpression;
                        var alias = exp2.Member.Name;
                        var field = body.Member.Name;
                        result.Add(DC.DH.JoinColumnDic(exp2.Type.FullName, field, alias));
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
        internal DicModelUI FuncMBoolExpression<M>( ActionEnum action, Expression<Func<M, bool>> func)
        {
            try
            {
                //
                var result = default(DicModelUI);
                var body = func.Body;
                var nodeType = body.NodeType;

                //
                if (IsBinaryExpr(nodeType))
                {
                    var binExpr = body as BinaryExpression;
                    var pres = new List<string>
                    {
                        func.Parameters[0].Name
                    };
                    result = HandConditionBinary(  binExpr, pres, func.ToString());
                }
                else if (nodeType == ExpressionType.Call)
                {

                    var mcExpr = body as MethodCallExpression;
                    result = HandConditionCall(mcExpr,  func.ToString());
                }
                else if (nodeType == ExpressionType.Constant)
                {
                    var cExpr = body as ConstantExpression;
                    result = HandConditionConstant(cExpr,cExpr.Type);
                }
                else if (nodeType == ExpressionType.MemberAccess)
                {
                    var memExpr = body as MemberExpression;
                    result = HandConditionMemberAccess(  memExpr);
                }
                else
                {
                    throw new Exception();
                }

                //
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
        internal DicModelUI FuncBoolExpression(Expression<Func<bool>> func)
        {
            try
            {
                //
                var result = default(DicModelUI);
                var body = func.Body;
                var nodeType = body.NodeType;

                //
                if (IsBinaryExpr(nodeType))
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
                        var pres = DC.UiConditions.Select(it => it.TableAliasOne).ToList();
                        result = HandConditionBinary( binExpr, pres, func.ToString());
                    }
                }
                else if (nodeType == ExpressionType.Call)
                {
                    var mcExpr = body as MethodCallExpression;
                    result = HandConditionCall(mcExpr,  func.ToString());
                }
                else if (nodeType == ExpressionType.Constant)
                {
                    var cExpr = body as ConstantExpression;
                    result = HandConditionConstant(cExpr,cExpr.Type);
                }
                else if (nodeType == ExpressionType.MemberAccess)
                {
                    var memExpr = body as MemberExpression;
                    result = HandConditionMemberAccess( memExpr);
                }
                else
                {
                    throw new Exception();
                }

                //
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
