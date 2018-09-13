
using EasyDAL.Exchange.Cache;
using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
// ReSharper disable All

namespace EasyDAL.Exchange.ExpressionX
{
    internal class ExpressionHandleX
    {
        private DbContext DC { get; set; }

        private ExpressionHandleX() { }
        internal ExpressionHandleX(DbContext dc)
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
                        && maExpr.Expression.NodeType== ExpressionType.Parameter)
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
        private (string key, string alias, Type valType) GetKey(Expression bodyL, OptionEnum option)
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
                        if (info.IsDefined(typeof(ColumnAttribute), false))
                        {
                            var attr = (ColumnAttribute)info.GetCustomAttributes(typeof(ColumnAttribute), false)[0];
                            return attr.Name;
                        }
                        return info.Name;
                    });

                //
                return (field, alias, type);
            }
            else if (bodyL.NodeType == ExpressionType.Call)
            {
                var mcExpr = bodyL as MethodCallExpression;
                var mem = mcExpr.Arguments[0];
                if (option == OptionEnum.In)
                {
                    return GetKey(mem, option);
                }
            }

            return (default(string), default(string), default(Type));
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

            var leftStr = binLeft.ToString();
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
        private string HandMember(MemberExpression binRight)
        {
            var result = default(string);
            if (binRight.NodeType == ExpressionType.MemberAccess)
            {
                result = DC.VH.GetMemExprVal(binRight);
            }
            //else if (binRight.NodeType == ExpressionType.Convert)
            //{
            //    var expr = binRight as UnaryExpression;
            //    if (expr.Operand.NodeType == ExpressionType.Convert)
            //    {
            //        var exprExpr = expr.Operand as UnaryExpression;
            //        var memExpr = exprExpr.Operand as MemberExpression;
            //        var memCon = memExpr.Expression as ConstantExpression;
            //        var memObj = memCon.Value;
            //        var memFiled = memExpr.Member as FieldInfo;
            //        result = memFiled.GetValue(memObj).ToString();
            //    }
            //    else if (expr.Operand.NodeType == ExpressionType.MemberAccess)
            //    {
            //        result = DC.VH. GetMemExprVal(expr.Operand as MemberExpression);
            //    }
            //}
            else
            {
                throw new Exception();
            }
            return result;
        }
        // 02
        private string HandConvert(Expression binRight)
        {
            var result = default(string);
            //if (binRight.NodeType == ExpressionType.MemberAccess)
            //{
            //    result = DC.VH.GetMemExprVal(binRight as MemberExpression);
            //}
            //else 
            if (binRight.NodeType == ExpressionType.Convert)
            {
                var expr = binRight as UnaryExpression;
                if (expr.Operand.NodeType == ExpressionType.Convert)
                {
                    var exprExpr = expr.Operand as UnaryExpression;
                    var memExpr = exprExpr.Operand as MemberExpression;
                    var memCon = memExpr.Expression as ConstantExpression;
                    var memObj = memCon.Value;
                    var memFiled = memExpr.Member as FieldInfo;
                    result = memFiled.GetValue(memObj).ToString();
                }
                else if (expr.Operand.NodeType == ExpressionType.MemberAccess)
                {
                    result = DC.VH.GetMemExprVal(expr.Operand as MemberExpression);
                }
            }
            else
            {
                throw new Exception();
            }
            return result;
        }
        // 01
        private string HandBinary(Expression binRight)
        {
            var val = string.Empty;

            //
            switch (binRight.NodeType)
            {
                case ExpressionType.Constant:
                    //val = (binRight as ConstantExpression).Value.ToString();
                    var con = binRight as ConstantExpression;
                    val = DC.VH.GetConstantVal(con, con.Type);
                    break;
                case ExpressionType.Call:
                    var rightExpr = binRight as MethodCallExpression;
                    val = DC.VH.GetCallVal(rightExpr);
                    break;
                case ExpressionType.MemberAccess:
                    val = HandMember(binRight as MemberExpression);
                    break;
                case ExpressionType.Convert:
                    val = HandConvert(binRight);
                    break;
                default:
                    throw new Exception();
            }

            //
            return val;
        }

        /********************************************************************************************************************/

        private DicModel HandConditionBinary(BinaryExpression binExpr,List<string> pres)
        {
            var binTuple = HandBinExpr(pres, binExpr);
            var val = HandBinary(binTuple.right);
            var leftStr = binTuple.left.ToString();
            if (leftStr.Contains(".Length")
                && leftStr.IndexOf(".") < leftStr.LastIndexOf("."))
            {
                var keyTuple = GetKey(binTuple.left, OptionEnum.CharLength);
                return DicHandle.BinaryCharLengthHandle(keyTuple.key, keyTuple.alias, val, keyTuple.valType, binTuple.node, binTuple.isR);
            }
            else
            {
                var keyTuple = GetKey(binTuple.left, DicHandle.GetOption(binTuple.node, binTuple.isR));
                return DicHandle.BinaryNormalHandle(keyTuple.key, keyTuple.alias, val, keyTuple.valType, binTuple.node, binTuple.isR);
            }
        }

        private DicModel HandConditionCall(MethodCallExpression mcExpr)
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
                        var keyTuple = GetKey(memKey, OptionEnum.In);
                        var val = HandMember(memVal as MemberExpression);
                        return DicHandle.CallInHandle(keyTuple.key,keyTuple.alias, val, keyTuple.valType);
                    }
                    else if (memVal.NodeType == ExpressionType.NewArrayInit)
                    {
                        var naExpr = memVal as NewArrayExpression;
                        var keyTuple = GetKey(memKey, OptionEnum.In);
                        var vals = new List<string>();
                        foreach (var exp in naExpr.Expressions)
                        {
                            vals.Add(DC.VH.GetConstantVal(exp as ConstantExpression, keyTuple.valType));
                        }
                        var val = string.Join(",", vals);
                        return DicHandle.CallInHandle(keyTuple.key,keyTuple.alias, val, keyTuple.valType);
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
                            var val = HandMember(memO);
                            var keyTuple = GetKey(mcExpr, OptionEnum.In);
                            return DicHandle.CallInHandle(keyTuple.key,keyTuple.alias, val, keyTuple.valType);
                        }
                        else if (memType == typeof(string))
                        {
                            var keyTuple = GetKey(memO, OptionEnum.Like);
                            var val = DC.VH.GetCallVal(mcExpr);
                            return DicHandle.CallLikeHandle(keyTuple.key,keyTuple.alias, val, keyTuple.valType);
                        }
                    }
                    else if (objNodeType == ExpressionType.ListInit)
                    {
                        var liExpr = objExpr as ListInitExpression;
                        var keyTuple = GetKey(mcExpr, OptionEnum.In);
                        var vals = new List<string>();
                        foreach (var ini in liExpr.Initializers)
                        {
                            var arg = ini.Arguments[0];
                            vals.Add(DC.VH.GetConstantVal(ini.Arguments[0] as ConstantExpression, keyTuple.valType));
                        }
                        var val = string.Join(",", vals);
                        return DicHandle.CallInHandle(keyTuple.key,keyTuple.alias, val, keyTuple.valType);
                    }
                }
            }

            return null;
        }

        private DicModel HandConditionConstant(ConstantExpression cExpr)
        {
            var valType = cExpr.Type;
            var val = DC.VH.GetConstantVal(cExpr, valType);
            if (cExpr.Type == typeof(bool))
            {
                return DicHandle.ConstantBoolHandle(val, valType);
            }

            return null;
        }

        private DicModel HandConditionMemberAccess(MemberExpression memExpr)
        {
            var memProp = memExpr.Member as PropertyInfo;
            var valType = memProp.PropertyType;
            var key = memProp.Name;
            var alias = GetAlias(memExpr);
            if (valType == typeof(bool))
            {
                return DicHandle.MemberBoolHandle(key,alias, valType);
            }

            return null;
        }

        /********************************************************************************************************************/

        private DicModel HandOnBinary(BinaryExpression binExpr)
        {
            var option = DicHandle.GetOption(binExpr.NodeType, false);
            var tuple1 = GetKey(binExpr.Left, option);
            var tuple2 = GetKey(binExpr.Right, option);
            return new DicModel
            {
                KeyOne = tuple1.key,
                AliasOne = tuple1.alias,
                KeyTwo = tuple2.key,
                AliasTwo = tuple2.alias,
                Option = option
            };
        }

        /********************************************************************************************************************/

        /// <summary>
        /// 获取表达式信息
        /// </summary>
        public string ExpressionHandle<M, F>(Expression<Func<M, F>> func)
        {
            try
            {
                var body = func.Body as MemberExpression;
                var keyTuple = GetKey(body, OptionEnum.None);
                var key = keyTuple.key;

                if (!string.IsNullOrWhiteSpace(key))
                {
                    return key;
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

        /// <summary>
        /// 获取表达式信息
        /// </summary>
        public DicModel ExpressionHandle<M>(Expression<Func<M, bool>> func)
        {
            try
            {
                //
                var result = default(DicModel);
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
                    result = HandConditionBinary(binExpr, pres);
                }
                else if (nodeType == ExpressionType.Call)
                {

                    var mcExpr = body as MethodCallExpression;
                    result = HandConditionCall(mcExpr);
                }
                else if (nodeType == ExpressionType.Constant)
                {
                    var cExpr = body as ConstantExpression;
                    result = HandConditionConstant(cExpr);
                }
                else if (nodeType == ExpressionType.MemberAccess)
                {
                    var memExpr = body as MemberExpression;
                    result = HandConditionMemberAccess(memExpr);
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

        // join
        public DicModel ExpressionHandle<M>(Expression<Func<M>> func)
        {
            try
            {
                var result = default(DicModel);
                var body = func.Body as MemberExpression;
                var alias = body.Member.Name;
                var table = DC.SqlProvider.GetTableName(body.Type);
                result = new DicModel
                {
                    TableOne = table,
                    AliasOne = alias
                };

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
        public DicModel ExpressionHandle(Expression<Func<bool>> func, ActionEnum action)
        {
            try
            {
                //
                var result = default(DicModel);
                var body = func.Body;
                var nodeType = body.NodeType;

                //
                if (IsBinaryExpr(nodeType))
                {
                    var binExpr = body as BinaryExpression;
                    if (action == ActionEnum.On)
                    {
                        result = HandOnBinary(binExpr);
                        result.Action = action;
                    }
                    else if (action == ActionEnum.Where
                        || action == ActionEnum.And
                        || action == ActionEnum.Or)
                    {
                        var pres = DC.Conditions.Select(it => it.AliasOne).ToList();
                        result = HandConditionBinary(binExpr, pres);
                        result.Action = action;
                    }
                }
                else if (nodeType == ExpressionType.Call)
                {
                    var mcExpr = body as MethodCallExpression;
                    result = HandConditionCall(mcExpr);
                    result.Action = action;
                }
                else if (nodeType == ExpressionType.Constant)
                {
                    var cExpr = body as ConstantExpression;
                    result = HandConditionConstant(cExpr);
                    result.Action = action;
                }
                else if (nodeType == ExpressionType.MemberAccess)
                {
                    var memExpr = body as MemberExpression;
                    result = HandConditionMemberAccess(memExpr);
                    result.Action = action;
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
