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

        private string GetFieldName(ParameterExpression parameter, MemberExpression leftBody)
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
        public String GetFieldName<M, T>(Expression<Func<M, T>> func)
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
        public DicModel<string, string, OptionEnum> GetFieldName<M>(Expression<Func<M, bool>> func)
        {
            var parameter = func.Parameters[0];
            switch (func.Body.NodeType)
            {
                case ExpressionType.Equal:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                    var bodyB = func.Body as BinaryExpression;
                    var leftBody = bodyB.Left as MemberExpression;
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
                            var memExpr = bodyB.Right as MemberExpression;
                            PropertyInfo outerProp = memExpr.Member as PropertyInfo;
                            if (outerProp == null)
                            {
                                var memMem = memExpr.Member as FieldInfo;
                                var memCon = memExpr.Expression as ConstantExpression;
                                object memObj = memCon.Value;
                                memVal = memMem.GetValue(memObj).ToString();
                            }
                            else
                            {
                                if (memExpr.Expression == null)
                                {
                                    var type = memExpr.Type as Type;
                                    var instance = Activator.CreateInstance(type);
                                    memVal = outerProp.GetValue(instance,null).ToString();
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
                                        memVal = outerProp.GetValue(outerObj, null).ToString();
                                    }
                                    else
                                    {
                                        memVal = outerProp.GetValue(outerObj, null).ToString();
                                    }
                                }
                            }
                            break;
                            return null;
                    }
                    return new DicModel<string, string, OptionEnum>
                    {
                        key = GetFieldName(parameter, leftBody),
                        Value = conVal != null ? (string)(conVal.Value) : memVal,
                        Other = GetOption(bodyB)
                    };
                    break;
                case ExpressionType.Call:
                    var bodyMC = func.Body as MethodCallExpression;
                    var exprStr = bodyMC.ToString();
                    if (exprStr.Contains(".Contains("))
                    {
                        var mem = bodyMC.Object as MemberExpression;
                        return new DicModel<string, string, OptionEnum>
                        {
                            key = GetFieldName(parameter, mem),
                            Value = (string)((bodyMC.Arguments[0] as ConstantExpression).Value),
                            Other = OptionEnum.Like
                        };
                    }
                    return null;
                    break;
            }
            return null;
        }
    }
}
