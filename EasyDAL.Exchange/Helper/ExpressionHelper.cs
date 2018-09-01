
using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EasyDAL.Exchange.Helper
{
    internal class ExpressionHelper : ClassInstance<ExpressionHelper>
    {
        private static ConcurrentDictionary<string, ConcurrentDictionary<Int32, String>> Cache = new ConcurrentDictionary<string, ConcurrentDictionary<Int32, String>>();
        private GenericHelper GH = GenericHelper.Instance;

        // -01-02- 
        private string GetKey(ParameterExpression parameter, Expression body,OptionEnum option)
        {
            var leftBody = body as MemberExpression;
            if (leftBody == null)  // Convert
            {
                var exp = body as UnaryExpression;
                return (exp.Operand as MemberExpression).Member.Name;
            }
            else  // MemberAccess
            {
                var info = default(PropertyInfo);
                if (option == OptionEnum.CharLength)
                {
                    var clMemExpr = leftBody.Expression as MemberExpression;
                    info = parameter.Type.GetProperty(clMemExpr.Member.Name);
                }
                else
                {
                    info = parameter.Type.GetProperty(leftBody.Member.Name);
                }
                var field = Cache
                    .GetOrAdd($"{parameter.GetType().FullName}:{info.Module.GetHashCode()}", moduleKey => new ConcurrentDictionary<Int32, String>())
                    .GetOrAdd(info.MetadataToken, innnerKey =>
                    {
                        if (info.IsDefined(typeof(ColumnAttribute), false))
                        {
                            var attr = (ColumnAttribute)info.GetCustomAttributes(typeof(ColumnAttribute), false)[0];
                            return attr.Name;
                        }
                        return info.Name;
                    });
                return field;
            }
        }
        // -02-03-
        private string GetMemExprVal(Expression memExpre)
        {
            var str = string.Empty;

            //
            var memExpr = memExpre as MemberExpression;
            var outerProp = memExpr.Member as PropertyInfo;
            if (outerProp == null)
            {
                var memMem = memExpr.Member as FieldInfo;
                var memCon = memExpr.Expression as ConstantExpression;
                object memObj = memCon.Value;
                str = memMem.GetValue(memObj).ToString();
            }
            else
            {
                if (memExpr.Expression == null)
                {
                    var type = memExpr.Type as Type;
                    var instance = Activator.CreateInstance(type);
                    str = outerProp.GetValue(instance, null).ToString();
                }
                else
                {
                    MemberExpression innerMember = (MemberExpression)memExpr.Expression;
                    var innerField = innerMember.Member as FieldInfo;
                    if (innerField == null)
                    {
                        var innerFieldX = innerMember.Member as PropertyInfo;
                        ConstantExpression ce = (ConstantExpression)innerMember.Expression;
                        object innerObj = ce.Value;
                        var outerObj = innerFieldX.GetValue(innerObj);
                        var valType = outerProp.PropertyType;
                        str = GH.GetTypeValue(valType, outerProp, outerObj);
                    }
                    else
                    {
                        ConstantExpression ce = (ConstantExpression)innerMember.Expression;
                        object innerObj = ce.Value;
                        var outerObj = innerField.GetValue(innerObj);
                        var valType = outerProp.PropertyType;
                        str = GH.GetTypeValue(valType, outerProp, outerObj);
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(str))
            {
                return str;
            }
            else
            {
                throw new Exception();
            }
        }
        // 01
        private string GetCallVal(Expression expr)
        {
            var val = string.Empty;

            //
            var bodyMC = expr as MethodCallExpression;
            var pExpr = bodyMC.Arguments[0];
            if (pExpr.NodeType == ExpressionType.Constant)
            {
                var type = bodyMC.Type;
                if (type == typeof(DateTime))
                {
                    var obj = Convert.ToDateTime(GetMemExprVal(bodyMC.Object));
                    var method = bodyMC.Method.Name;
                    var args = new List<object>();
                    foreach (var arg in bodyMC.Arguments)
                    {
                        if (arg.NodeType == ExpressionType.Constant)
                        {
                            var carg = arg as ConstantExpression;
                            args.Add(carg.Value);
                        }
                    }
                    val = (type.InvokeMember(method, BindingFlags.Default | BindingFlags.InvokeMethod, null, obj, args.ToArray())).ToString();
                }
                else
                {
                    val = (string)((pExpr as ConstantExpression).Value);
                }
            }
            else if (pExpr.NodeType == ExpressionType.MemberAccess)
            {
                val = GetMemExprVal(pExpr);
            }
            else
            {
                val = string.Empty;
            }

            //
            if (!string.IsNullOrWhiteSpace(val))
            {
                return val;
            }
            else
            {
                throw new Exception();
            }
        }
        // 01
        private string GetBinaryVal(Expression body)
        {
            var val = string.Empty;

            //
            var bodyB = body as BinaryExpression;
            var bRight = bodyB.Right;
            switch (bRight.NodeType)
            {
                case ExpressionType.Constant:
                    val = (bRight as ConstantExpression).Value.ToString();
                    break;
                case ExpressionType.Call:
                    var rightExpr = bRight as MethodCallExpression;
                    var conVal = rightExpr.Arguments[0] as ConstantExpression;
                    val = conVal.Value.ToString();
                    break;
                case ExpressionType.MemberAccess:
                    val = HandMember(bodyB);
                    break;
                case ExpressionType.Convert:
                    val = HandMember(bodyB);
                    break;
                default:
                    throw new Exception();
            }

            //
            return val;
        }
        // 01
        private (string key, string alias) GetMemKeyAlias(Expression expr)
        {
            var memExpr = expr as MemberExpression;
            var mMemExpr = memExpr.Expression as MemberExpression;
            return (memExpr.Member.Name, mMemExpr.Member.Name);
        }

        // 02
        private OptionEnum GetOption(BinaryExpression be)
        {
            var option = OptionEnum.None;
            if (be.NodeType == ExpressionType.Equal)
            {
                option = OptionEnum.Equal;
            }
            else if (be.NodeType == ExpressionType.NotEqual)
            {
                option = OptionEnum.NotEqual;
            }
            else if (be.NodeType == ExpressionType.LessThan)
            {
                option = OptionEnum.LessThan;
            }
            else if (be.NodeType == ExpressionType.LessThanOrEqual)
            {
                option = OptionEnum.LessThanOrEqual;
            }
            else if (be.NodeType == ExpressionType.GreaterThan)
            {
                option = OptionEnum.GreaterThan;
            }
            else if (be.NodeType == ExpressionType.GreaterThanOrEqual)
            {
                option = OptionEnum.GreaterThanOrEqual;
            }

            return option;
        }
        // 02
        private string HandMember(BinaryExpression bodyB)
        {
            var result = default(string);
            if (bodyB.Right.NodeType == ExpressionType.MemberAccess)
            {
                result = GetMemExprVal(bodyB.Right);
            }
            else if (bodyB.Right.NodeType == ExpressionType.Convert)
            {
                var expr = bodyB.Right as UnaryExpression;
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
                    result = GetMemExprVal(expr.Operand);
                }
            }
            else
            {
                throw new Exception();
            }
            return result;
        }

        /// <summary>
        /// 获取表达式信息
        /// </summary>
        public string ExpressionHandle<M, F>(Expression<Func<M, F>> func)
        {
            try
            {
                var parameter = func.Parameters[0];
                var body = func.Body as MemberExpression;
                return GetKey(parameter, body,OptionEnum.None);
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
                var result = default(DicModel);
                var parameter = func.Parameters[0];
                var body = func.Body;
                var key = string.Empty;
                var val = string.Empty;
                var option = OptionEnum.None;
                switch (body.NodeType)
                {
                    case ExpressionType.Equal:
                    case ExpressionType.NotEqual:
                    case ExpressionType.LessThan:
                    case ExpressionType.LessThanOrEqual:
                    case ExpressionType.GreaterThan:
                    case ExpressionType.GreaterThanOrEqual:
                        var binExpr = body as BinaryExpression;
                        var leftBin = binExpr.Left;
                        var rightBin = binExpr.Right;
                        val = GetBinaryVal(body);
                        var leftStr = leftBin.ToString();
                        if (leftStr.Contains(".Length")
                            && leftStr.IndexOf(".")<leftStr.LastIndexOf("."))
                        {
                            option = OptionEnum.CharLength;
                            key = GetKey(parameter, leftBin,option);
                            result = new DicModel
                            {
                                KeyOne = key,
                                Param = key,
                                Value = val,
                                Option = option,
                                FuncSupplement= GetOption(binExpr)
                            };
                        }
                        else
                        {
                            option = GetOption(binExpr);
                            key = GetKey(parameter, leftBin,option);
                            result = new DicModel
                            {
                                KeyOne = key,
                                Param = key,
                                Value = val,
                                Option = option
                            };
                        }
                        break;
                    case ExpressionType.Call:
                        var bodyMC = body as MethodCallExpression;
                        var exprStr = bodyMC.ToString();
                        if (exprStr.Contains(".Contains("))
                        {
                            var mem = bodyMC.Object as MemberExpression;
                            option = OptionEnum.Like;
                            key = GetKey(parameter, mem,option);
                            val = GetCallVal(body);
                            result = new DicModel
                            {
                                KeyOne = key,
                                Param = key,
                                Value = val,
                                Option = option
                            };
                        }
                        break;
                    case ExpressionType.Constant:
                        var bodyC = body as ConstantExpression;
                        if(bodyC.Type==typeof(bool))
                        {
                            result = new DicModel
                            {
                                KeyOne= "OneEqualOne",
                                Param = "OneEqualOne",
                                Value = bodyC.Value.ToString(),
                                ValueType= ValueTypeEnum.Bool,
                                Option = OptionEnum.OneEqualOne
                            };
                        }
                        break;
                    default:
                        throw new Exception();
                }
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
                var result = default(DicModel);
                var body = func.Body;
                switch (body.NodeType)
                {
                    case ExpressionType.Equal:
                    case ExpressionType.LessThan:
                    case ExpressionType.LessThanOrEqual:
                    case ExpressionType.GreaterThan:
                    case ExpressionType.GreaterThanOrEqual:
                        var bExpr = body as BinaryExpression;
                        if (action == ActionEnum.On)
                        {
                            var tuple1 = GetMemKeyAlias(bExpr.Left);
                            var tuple2 = GetMemKeyAlias(bExpr.Right);
                            result = new DicModel
                            {
                                KeyOne = tuple1.key,
                                AliasOne = tuple1.alias,
                                KeyTwo = tuple2.key,
                                AliasTwo = tuple2.alias,
                                Param = string.Empty,
                                Action = action,
                                Option = GetOption(bExpr)
                            };
                        }
                        else if (action == ActionEnum.Where
                            || action == ActionEnum.And)
                        {
                            var tuple = GetMemKeyAlias(bExpr.Left);
                            var val = string.Empty;
                            switch (bExpr.Right.NodeType)
                            {
                                case ExpressionType.Call:
                                    val = GetCallVal(bExpr.Right);
                                    break;
                            }
                            if (string.IsNullOrWhiteSpace(val))
                            {
                                throw new Exception();
                            }
                            result = new DicModel
                            {
                                KeyOne = tuple.key,
                                AliasOne = tuple.alias,
                                Value = val,
                                Param = tuple.key,
                                Action = action,
                                Option = GetOption(bExpr)
                            };
                        }
                        break;
                }

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
