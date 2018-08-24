using EasyDAL.Exchange.Attributes;
using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace EasyDAL.Exchange.Helper
{
    public class ExpressionHelper : ClassInstance<ExpressionHelper>
    {
        private static ConcurrentDictionary<string, ConcurrentDictionary<Int32, String>> Cache = new ConcurrentDictionary<string, ConcurrentDictionary<Int32, String>>();

        private string GetFieldName(ParameterExpression parameter, Expression body)
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
        private string HandMemberVal(BinaryExpression bodyB)
        {
            var result = default(string);
            if (bodyB.Right.NodeType == ExpressionType.MemberAccess)
            {
                var memExpr = bodyB.Right as MemberExpression;
                PropertyInfo outerProp = memExpr.Member as PropertyInfo;
                if (outerProp == null)
                {
                    var memMem = memExpr.Member as FieldInfo;
                    var memCon = memExpr.Expression as ConstantExpression;
                    object memObj = memCon.Value;
                    result = memMem.GetValue(memObj).ToString();
                }
                else
                {
                    if (memExpr.Expression == null)
                    {
                        var type = memExpr.Type as Type;
                        var instance = Activator.CreateInstance(type);
                        result = outerProp.GetValue(instance, null).ToString();
                    }
                    else
                    {
                        MemberExpression innerMember = (MemberExpression)memExpr.Expression;
                        var innerField = (FieldInfo)innerMember.Member;
                        ConstantExpression ce = (ConstantExpression)innerMember.Expression;
                        object innerObj = ce.Value;
                        object outerObj = innerField.GetValue(innerObj);
                        if (outerProp.PropertyType == typeof(DateTime))
                        {
                            result = outerProp.GetValue(outerObj, null).ToString();
                        }
                        else
                        {
                            result = outerProp.GetValue(outerObj, null).ToString();
                        }
                    }
                }
            }
            else if (bodyB.Right.NodeType == ExpressionType.Convert)
            {
                var expr = bodyB.Right as UnaryExpression;
                var exprExpr = expr.Operand as UnaryExpression;
                var memExpr = exprExpr.Operand as MemberExpression;
                var memCon = memExpr.Expression as ConstantExpression;
                var memObj = memCon.Value;
                var memFiled = memExpr.Member as FieldInfo;
                result = memFiled.GetValue(memObj).ToString();
            }
            else
            {
                throw new Exception("请联系 https://www.cnblogs.com/Meng-NET/ 博主!");
            }
            return result;
        }
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
            var fieldName = GetFieldName(parameter, member);

            return fieldName;
        }

        /// <summary>
        /// Get the field name in table according to the name property in ColumnAttibute
        /// </summary>
        /// <param name="func">lambda expression like t=>t.colname==5</param>
        public DicModel<string, string> ExpressionHandle<M>(Expression<Func<M, bool>> func)
        {
            var result = default(DicModel<string, string>);
            var parameter = func.Parameters[0];
            switch (func.Body.NodeType)
            {
                case ExpressionType.Equal:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                    var bodyB = func.Body as BinaryExpression;
                    var leftBody = bodyB.Left;
                    var conVal = default(ConstantExpression);
                    var memVal = default(string);
                    switch (bodyB.Right.NodeType)
                    {
                        case ExpressionType.Constant:
                            conVal = bodyB.Right as ConstantExpression;
                            break;
                        case ExpressionType.Call:
                            var rightExpr = bodyB.Right as MethodCallExpression;
                            conVal = rightExpr.Arguments[0] as ConstantExpression;
                            break;
                        case ExpressionType.MemberAccess:
                            memVal = HandMemberVal(bodyB);
                            break;
                        case ExpressionType.Convert:
                            memVal = HandMemberVal(bodyB);
                            break;
                        default:
                            throw new Exception("请联系 https://www.cnblogs.com/Meng-NET/ 博主!");
                    }
                    result = new DicModel<string, string>
                    {
                        key = GetFieldName(parameter, leftBody),
                        Value = conVal != null ? conVal.Value.ToString() : memVal,
                        Option = GetOption(bodyB),
                        Action = ActionEnum.None
                    };
                    break;
                case ExpressionType.Call:
                    var bodyMC = func.Body as MethodCallExpression;
                    var exprStr = bodyMC.ToString();
                    if (exprStr.Contains(".Contains("))
                    {
                        var mem = bodyMC.Object as MemberExpression;
                        result = new DicModel<string, string>
                        {
                            key = GetFieldName(parameter, mem),
                            Value = (string)((bodyMC.Arguments[0] as ConstantExpression).Value),
                            Option = OptionEnum.Like,
                            Action = ActionEnum.None
                        };
                    }
                    result = null;
                    break;
                default:
                    throw new Exception("请联系 https://www.cnblogs.com/Meng-NET/ 博主!");
            }
            if (result == null)
            {
                throw new Exception($"不支持的表达式:[{func.ToString()}]");
            }
            else
            {
                return result;
            }
        }
    }
}
