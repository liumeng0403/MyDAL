using EasyDAL.Exchange.Attributes;
using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
//using System.Reflection.Metadata;
using System.Text;

namespace EasyDAL.Exchange.Helper
{
    public class ExpressionHelper : ClassInstance<ExpressionHelper>
    {
        private static ConcurrentDictionary<string, ConcurrentDictionary<Int32, String>> Cache = new ConcurrentDictionary<string, ConcurrentDictionary<Int32, String>>();
        private GenericHelper GH = GenericHelper.Instance;

        // -01-02- 
        private string GetKey(ParameterExpression parameter, Expression body)
        {
            var leftBody = body as MemberExpression;
            if (leftBody == null)  // Convert
            {
                var exp = body as UnaryExpression;
                return (exp.Operand as MemberExpression).Member.Name;
            }
            else  // MemberAccess
            {
                var info = parameter.Type.GetProperty(leftBody.Member.Name);
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
                    var innerField = (FieldInfo)innerMember.Member;
                    ConstantExpression ce = (ConstantExpression)innerMember.Expression;
                    object innerObj = ce.Value;
                    var outerObj = innerField.GetValue(innerObj);
                    var valType = outerProp.PropertyType;
                    str = GH.GetTypeValue(valType, outerProp, outerObj);
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
        private DicModel<string, string> GetCallDicM(ParameterExpression parameter, Expression body)
        {
            var result = default(DicModel<string, string>);
            var bodyMC = body as MethodCallExpression;
            var exprStr = bodyMC.ToString();

            if (exprStr.Contains(".Contains("))
            {
                var mem = bodyMC.Object as MemberExpression;
                var pExpr = bodyMC.Arguments[0];
                var key = GetKey(parameter, mem);
                var val = string.Empty;
                if (pExpr.NodeType == ExpressionType.Constant)
                {
                    val = (string)((pExpr as ConstantExpression).Value);
                }
                else if (pExpr.NodeType == ExpressionType.MemberAccess)
                {
                    val = GetMemExprVal(pExpr);
                }
                else
                {
                    throw new Exception();
                }
                result = new DicModel<string, string>
                {
                    key = key,
                    Value = val,
                    Option = OptionEnum.Like,
                    Action = ActionEnum.None
                };
            }
            else
            {
                throw new Exception();
            }
            return result;
        }
        // 01
        private DicModel<string,string> GetBinaryDicM(ParameterExpression parameter, Expression body)
        {
            var result = default(DicModel<string, string>);
            var bodyB = body as BinaryExpression;
            var leftBody = bodyB.Left;
            var key = GetKey(parameter, leftBody);
            var val = string.Empty;
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
                    throw new Exception("请联系 https://www.cnblogs.com/Meng-NET/ 博主!");
            }
            result = new DicModel<string, string>
            {
                key = key,
                Value = val,
                Option = GetOption(bodyB),
                Action = ActionEnum.None
            };
            return result;
        }
        // 02
        private OptionEnum GetOption(BinaryExpression be)
        {
            var option = OptionEnum.None;
            if (be.NodeType == ExpressionType.Equal)
            {
                option = OptionEnum.Equal;
            }
            if (be.NodeType == ExpressionType.LessThan)
            {
                option = OptionEnum.LessThan;
            }
            if (be.NodeType == ExpressionType.LessThanOrEqual)
            {
                option = OptionEnum.LessThanOrEqual;
            }
            if (be.NodeType == ExpressionType.GreaterThan)
            {
                option = OptionEnum.GreaterThan;
            }
            if (be.NodeType == ExpressionType.GreaterThanOrEqual)
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
                throw new Exception("请联系 https://www.cnblogs.com/Meng-NET/ 博主!");
            }
            return result;
        }

        /// <summary>
        /// Get the field name in table according to the name property in ColumnAttibute
        /// </summary>
        /// <typeparam name="T">Field</typeparam>
        /// <param name="func">lambda expression like t=>t.colname</param>
        public String ExpressionHandle<M, T>(Expression<Func<M, T>> func)
        {
            if (func == null || func.Parameters == null || func.Parameters.Count == 0)
            {
                throw new Exception("Lambda expression is invalid.");
            }

            var parameter = func.Parameters[0];
            if (parameter == null)
            {
                throw new Exception($"Lambda expression[{func.ToString()}] is invalid.");
            }
            var member = func.Body as MemberExpression;
            if (member == null)
            {
                throw new Exception($"Lambda expression[{func.ToString()}] is invalid.");
            }
            var fieldName = GetKey(parameter, member);

            return fieldName;
        }

        /// <summary>
        /// Get the field name in table according to the name property in ColumnAttibute
        /// </summary>
        /// <param name="func">lambda expression like t=>t.colname==5</param>
        public DicModel<string, string> ExpressionHandle<M>(Expression<Func<M, bool>> func)
        {
            try
            {
                var result = default(DicModel<string, string>);
                var parameter = func.Parameters[0];
                var body = func.Body;
                switch (body.NodeType)
                {
                    case ExpressionType.Equal:
                    case ExpressionType.LessThan:
                    case ExpressionType.LessThanOrEqual:
                    case ExpressionType.GreaterThan:
                    case ExpressionType.GreaterThanOrEqual:
                        result = GetBinaryDicM(parameter, body);
                        break;
                    case ExpressionType.Call:
                        result = GetCallDicM(parameter, body);
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
            catch(Exception ex)
            {
                throw new Exception($"不支持的表达式:[{func.ToString()}]");
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
