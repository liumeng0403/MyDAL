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
    public class ExpressionHelper: ClassInstance<ExpressionHelper>
    {
        private static ConcurrentDictionary<string, ConcurrentDictionary<Int32, String>> Cache =new ConcurrentDictionary<string, ConcurrentDictionary<Int32, String>>();

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
        private OptionEnum GetOption(BinaryExpression exprB)
        {
            var option = OptionEnum.None;
            if (exprB.NodeType == ExpressionType.Equal)
            {
                option = OptionEnum.Equal;
            }
            return option;
        }

        /// <summary>
        /// Get the field name in table according to the name property in ColumnAttibute
        /// </summary>
        /// <typeparam name="T">Field</typeparam>
        /// <param name="func">lambda expression like t=>t.colname</param>
        public String GetFieldName<M,T>(Expression<Func<M, T>> func)
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
            if (func == null || func.Parameters == null || func.Parameters.Count == 0)
            {
                throw new Exception("Lambda expression is invalid.");
            }

            var parameter = func.Parameters[0];
            if (parameter == null)
            {
                throw new Exception($"Lambda expression[{func.ToString()}] is invalid.");
            }

            var exprB = func.Body as BinaryExpression;  // NodeType : Equal , LessThanOrEqual 
            if (exprB != null)
            {
                var leftBody = exprB.Left as MemberExpression;
                var value = exprB.Right as ConstantExpression;  // NodeType : Constant 
                if (value == null)
                {
                    var rightExpr = exprB.Right as MethodCallExpression;    // NodeType : Call 
                    if(rightExpr==null)  // NodeType : MemberAccess 
                    {
                        var memExpr = exprB.Right as MemberExpression;
                        
                        PropertyInfo outerProp = (PropertyInfo)memExpr.Member;
                        MemberExpression innerMember = (MemberExpression)memExpr.Expression;
                        FieldInfo innerField = (FieldInfo)innerMember.Member;
                        ConstantExpression ce = (ConstantExpression)innerMember.Expression;
                        object innerObj = ce.Value;
                        object outerObj = innerField.GetValue(innerObj);
                        string valuexxx = (string)outerProp.GetValue(outerObj, null);



                    }
                    value = rightExpr.Arguments[0] as ConstantExpression;
                }

                if (leftBody == null)
                {
                    throw new Exception($"Lambda expression[{func.ToString()}] is invalid.");
                }
                return new DicModel<string, string, OptionEnum>
                {
                    key = GetFieldName(parameter, leftBody),
                    Value = (string)(value.Value),
                    Other = GetOption(exprB)
                };
            }
            else
            {
                var expr = func.Body as MethodCallExpression;   // NodeType : Call 
                var exprStr = expr.ToString();
                if (exprStr.Contains(".Contains("))
                {
                    var mem = expr.Object as MemberExpression;
                    return new DicModel<string, string, OptionEnum>
                    {
                        key = GetFieldName(parameter, mem),
                        Value = (string)((expr.Arguments[0] as ConstantExpression).Value),
                        Other = OptionEnum.Like
                    };
                }
                return null;
            }
        }
    }
}
