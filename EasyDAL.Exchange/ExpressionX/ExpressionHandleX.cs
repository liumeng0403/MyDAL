
using EasyDAL.Exchange.Cache;
using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.ExpressionX;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

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

        // -01-02- 
        private (string key, Type type) GetKey(ParameterExpression parameter, Expression bodyL, OptionEnum option)
        {
            var info = default(PropertyInfo);
            if (bodyL.NodeType == ExpressionType.Convert)
            {
                var exp = bodyL as UnaryExpression;
                var opMem = exp.Operand;
                return GetKey(parameter, opMem, option);
            }
            else if (bodyL.NodeType == ExpressionType.MemberAccess)
            {
                var leftBody = bodyL as MemberExpression;
                if (option == OptionEnum.CharLength)
                {
                    var clMemExpr = leftBody.Expression as MemberExpression;
                    info = parameter.Type.GetProperty(clMemExpr.Member.Name);
                }
                else
                {
                    info = parameter.Type.GetProperty(leftBody.Member.Name);
                }
                var type = info.PropertyType;
                var field = StaticCache
                    .EHCache
                    .GetOrAdd($"{parameter.Type.FullName}:{info.Module.GetHashCode()}", moduleKey => new ConcurrentDictionary<Int32, String>())
                    .GetOrAdd(info.MetadataToken, innnerKey =>
                    {
                        if (info.IsDefined(typeof(ColumnAttribute), false))
                        {
                            var attr = (ColumnAttribute)info.GetCustomAttributes(typeof(ColumnAttribute), false)[0];
                            return attr.Name;
                        }
                        return info.Name;
                    });
                return (field, type);
            }
            else if (bodyL.NodeType == ExpressionType.Call)
            {
                var mcExpr = bodyL as MethodCallExpression;
                var mem = mcExpr.Arguments[0];
                if (option == OptionEnum.In)
                {
                    return GetKey(parameter, mem, option);
                }
            }

            return (default(string), default(Type));
        }
        // 01
        private (string key, string alias, Type valType) GetMemKeyAlias(Expression expr)
        {
            var memExpr = expr as MemberExpression;
            var mMemExpr = memExpr.Expression as MemberExpression;
            var key = memExpr.Member.Name;
            var alias = mMemExpr.Member.Name;
            var keyProp = memExpr.Member as PropertyInfo;
            var valType = keyProp.PropertyType;
            return (key, alias, valType);
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
        private (Expression left, Expression right, ExpressionType node , bool isR) HandBinExpr(ParameterExpression parameter, BinaryExpression binExpr)
        {
            var binLeft = binExpr.Left;
            var binRight = binExpr.Right;
            var binNode = binExpr.NodeType;

            if (binLeft.ToString().StartsWith($"{parameter.Name}.", StringComparison.Ordinal))
            {
                return (binLeft, binRight, binNode,false);
            }
            else
            {
                return (binRight, binLeft, binNode,true);
            }
        }
        // 02
        private string HandMember(MemberExpression binRight)
        {
            var result = default(string);
            if (binRight.NodeType == ExpressionType.MemberAccess)
            {
                result = DC.VH. GetMemExprVal(binRight);
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
                    var conVal = rightExpr.Arguments[0] as ConstantExpression;
                    //val = conVal.Value.ToString();
                    val = DC.VH.GetConstantVal(conVal, conVal.Type);
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

        /// <summary>
        /// 获取表达式信息
        /// </summary>
        public string ExpressionHandle<M, F>(Expression<Func<M, F>> func)
        {
            try
            {
                var parameter = func.Parameters[0];
                var body = func.Body as MemberExpression;
                var keyTuple = GetKey(parameter, body, OptionEnum.None);
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
                var parameter = func.Parameters[0];
                var body = func.Body;
                var nodeType = body.NodeType;
                var val = string.Empty;

                //
                if (IsBinaryExpr(nodeType))
                {
                    var binExpr = body as BinaryExpression;
                    var binTuple = HandBinExpr(parameter,binExpr);
                    val = HandBinary(binTuple.right);
                    var leftStr = binTuple.left.ToString();
                    if (leftStr.Contains(".Length")
                        && leftStr.IndexOf(".") < leftStr.LastIndexOf("."))
                    {
                        var keyTuple = GetKey(parameter, binTuple.left, OptionEnum.CharLength);
                        result = DicHandle.BinaryCharLengthHandle(keyTuple.key, val, keyTuple.type, binTuple.node,binTuple.isR);
                    }
                    else
                    {
                        var keyTuple = GetKey(parameter, binTuple.left, DicHandle. GetOption(binTuple.node,binTuple.isR));
                        result = DicHandle. BinaryNormalHandle(keyTuple.key, val, keyTuple.type, binTuple.node,binTuple.isR);
                    }
                }
                else if (nodeType == ExpressionType.Call)
                {
                    var mcExpr = body as MethodCallExpression;
                    var exprStr = mcExpr.ToString();
                    if (exprStr.Contains(".Contains("))
                    {
                        if (mcExpr.Object == null)
                        {
                            var memKey = mcExpr.Arguments[1];
                            var memVal = mcExpr.Arguments[0];
                            if (memVal.NodeType == ExpressionType.MemberAccess)
                            {
                                var keyTuple = GetKey(parameter, memKey, OptionEnum.In);
                                val = HandMember(memVal as MemberExpression);
                                result = DicHandle. CallInHandle(keyTuple.key, val, keyTuple.type);
                            }
                            else if (memVal.NodeType == ExpressionType.NewArrayInit)
                            {
                                var naExpr = memVal as NewArrayExpression;
                                var keyTuple = GetKey(parameter, memKey, OptionEnum.In);
                                var vals = new List<string>();
                                foreach (var exp in naExpr.Expressions)
                                {
                                    vals.Add(DC.VH.GetConstantVal(exp as ConstantExpression, keyTuple.type));
                                }
                                val = string.Join(",", vals);
                                result = DicHandle. CallInHandle(keyTuple.key, val, keyTuple.type);
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
                                    val = HandMember(memO);
                                    var keyTuple = GetKey(parameter, mcExpr, OptionEnum.In);
                                    result = DicHandle. CallInHandle(keyTuple.key, val, keyTuple.type);
                                }
                                else if (memType == typeof(string))
                                {
                                    var keyTuple = GetKey(parameter, memO, OptionEnum.Like);
                                    val = DC.VH.GetCallVal(mcExpr);
                                    result = DicHandle. CallLikeHandle(keyTuple.key, val, keyTuple.type);
                                }
                            }
                            else if (objNodeType == ExpressionType.ListInit)
                            {
                                var liExpr = objExpr as ListInitExpression;
                                var keyTuple = GetKey(parameter, mcExpr, OptionEnum.In);
                                var vals = new List<string>();
                                foreach (var ini in liExpr.Initializers)
                                {
                                    var arg = ini.Arguments[0];
                                    vals.Add(DC.VH.GetConstantVal(ini.Arguments[0] as ConstantExpression, keyTuple.type));
                                }
                                val = string.Join(",", vals);
                                result = DicHandle. CallInHandle(keyTuple.key, val, keyTuple.type);
                            }
                        }
                    }
                }
                else if (nodeType == ExpressionType.Constant)
                {
                    var cExpr = body as ConstantExpression;
                    val = cExpr.Value.ToString();
                    var valType = cExpr.Type;
                    if (cExpr.Type == typeof(bool))
                    {
                        result = DicHandle. ConstantBoolHandle(val, valType);
                    }
                }
                else if (nodeType == ExpressionType.MemberAccess)
                {
                    var memExpr = body as MemberExpression;
                    var memProp = memExpr.Member as PropertyInfo;
                    var valType = memProp.PropertyType;
                    var key = memProp.Name;
                    if (valType == typeof(bool))
                    {
                        result = DicHandle. MemberBoolHandle(key, valType);
                    }
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
                        var tuple1 = GetMemKeyAlias(binExpr.Left);
                        var tuple2 = GetMemKeyAlias(binExpr.Right);
                        result = new DicModel
                        {
                            KeyOne = tuple1.key,
                            AliasOne = tuple1.alias,
                            KeyTwo = tuple2.key,
                            AliasTwo = tuple2.alias,
                            Action = action,
                            Option = DicHandle. GetOption(binExpr.NodeType,false)
                        };
                    }
                    else if (action == ActionEnum.Where
                        || action == ActionEnum.And
                        || action == ActionEnum.Or)
                    {
                        //result = ExpressionHandle(func);
                        var tuple = GetMemKeyAlias(binExpr.Left);
                        var val = string.Empty;
                        switch (binExpr.Right.NodeType)
                        {
                            case ExpressionType.Call:
                                val = DC.VH.GetCallVal((binExpr.Right as MethodCallExpression));
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
                            ValueType = tuple.valType,
                            Param = tuple.key,
                            ParamRaw = tuple.key,
                            Action = action,
                            Option = DicHandle. GetOption(binExpr.NodeType,false)
                        };
                    }
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
