
using EasyDAL.Exchange.Cache;
using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EasyDAL.Exchange.Helper
{
    internal class ExpressionHelper : ClassInstance<ExpressionHelper>
    {

        private GenericHelper GH = GenericHelper.Instance;
        private StaticCache SC = StaticCache.Instance;

        /********************************************************************************************************************/

        // -01-02- 
        private (string key, Type type) GetKey(ParameterExpression parameter, Expression body, OptionEnum option)
        {
            var leftBody = body as MemberExpression;
            var info = default(PropertyInfo);
            if (body.NodeType == ExpressionType.Convert)
            {
                var exp = body as UnaryExpression;
                var opMem = exp.Operand;
                return GetKey(parameter, opMem, option);
            }
            else if (body.NodeType == ExpressionType.MemberAccess)
            {
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
                return (field, type);
            }
            else if (body.NodeType == ExpressionType.Call)
            {
                var mcExpr = body as MethodCallExpression;
                var mem = mcExpr.Arguments[0];
                if (option == OptionEnum.In)
                {
                    return GetKey(parameter, mem, option);
                }
            }

            return (default(string), default(Type));
        }
        // -02-03-
        private string GetMemExprVal(Expression memExpre)
        {
            var str = string.Empty;

            //
            var memExpr = memExpre as MemberExpression;
            var targetProp = memExpr.Member as PropertyInfo;
            if (targetProp == null)
            {
                var targetField = memExpr.Member as FieldInfo;
                var memCon = memExpr.Expression as ConstantExpression;
                object memObj = memCon.Value;
                var fType = targetField.FieldType;
                if (IsListT(fType))
                {
                    var type = memExpr.Type as Type;
                    var vals = targetField.GetValue(memObj);
                    str = InValueForListT(type, vals);
                }
                else
                {
                    str = targetField.GetValue(memObj).ToString();
                }
            }
            else
            {
                if (memExpr.Expression == null)
                {
                    var type = memExpr.Type as Type;
                    var instance = Activator.CreateInstance(type);
                    str = targetProp.GetValue(instance, null).ToString();
                }
                else
                {
                    MemberExpression innerMember = memExpr.Expression as MemberExpression;
                    if (innerMember == null)
                    {
                        var ce = memExpr.Expression as ConstantExpression;
                        object innerObj = ce.Value;
                        var memP = memExpr.Member as PropertyInfo;
                        var vals = memP.GetValue(innerObj);
                        var valType = memP.PropertyType;
                        if (IsListT(valType))
                        {
                            str = InValueForListT(valType, vals);
                        }
                        else
                        {
                            str = GH.GetTypeValue(valType, memP, innerObj);    // 此项 可能 有问题 
                        }
                    }
                    else
                    {
                        var innerField = innerMember.Member as FieldInfo;
                        if (innerField == null)
                        {

                            var innerFieldX = innerMember.Member as PropertyInfo;
                            var ce = innerMember.Expression as ConstantExpression;
                            object innerObj = ce.Value;
                            var outerObj = innerFieldX.GetValue(innerObj);
                            var fType = targetProp.PropertyType;
                            if (IsListT(fType))
                            {
                                var type = memExpr.Type as Type;
                                var vals = targetProp.GetValue(outerObj);
                                str = InValueForListT(type, vals);
                            }
                            else
                            {
                                str = GH.GetTypeValue(fType, targetProp, outerObj);
                            }
                        }
                        else
                        {
                            var ce = innerMember.Expression as ConstantExpression;
                            object innerObj = ce.Value;
                            var outerObj = innerField.GetValue(innerObj);
                            var valType = targetProp.PropertyType;
                            str = GH.GetTypeValue(valType, targetProp, outerObj);
                        }
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
        private string GetCallVal(MethodCallExpression mcExpr)
        {
            var val = string.Empty;

            //
            //var bodyMC = expr as MethodCallExpression;
            var pExpr = mcExpr.Arguments[0];
            if (pExpr.NodeType == ExpressionType.Constant)
            {
                var type = mcExpr.Type;
                if (type == typeof(DateTime))
                {
                    var obj = Convert.ToDateTime(GetMemExprVal(mcExpr.Object));
                    var method = mcExpr.Method.Name;
                    var args = new List<object>();
                    foreach (var arg in mcExpr.Arguments)
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
        private string GetBinaryVal(Expression binRight)
        {
            var val = string.Empty;

            //
            //var bodyB = body as BinaryExpression;
            //var binRight = binExpr.Right;
            switch (binRight.NodeType)
            {
                case ExpressionType.Constant:
                    val = (binRight as ConstantExpression).Value.ToString();
                    break;
                case ExpressionType.Call:
                    var rightExpr = binRight as MethodCallExpression;
                    var conVal = rightExpr.Arguments[0] as ConstantExpression;
                    val = conVal.Value.ToString();
                    break;
                case ExpressionType.MemberAccess:
                    val = HandMember(binRight);
                    break;
                case ExpressionType.Convert:
                    val = HandMember(binRight);
                    break;
                default:
                    throw new Exception();
            }

            //
            return val;
        }
        // 01
        private string GetConVal(Expression conExpr, Type valType)
        {
            var con = conExpr as ConstantExpression;

            if (valType.IsEnum)
            {
                return ((int)(con.Value)).ToString();
            }
            else
            {
                return con.Value.ToString();
            }
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
        private DicModel BinaryCharLengthHandle(string key, string value, Type valType, ExpressionType nodeType)
        {
            return new DicModel
            {
                KeyOne = key,
                Param = key,
                ParamRaw = key,
                Value = value,
                ValueType = valType,
                Option = OptionEnum.CharLength,
                FuncSupplement = GetOption(nodeType)
            };
        }
        // 01
        private DicModel BinaryNormalHandle(string key, string value, Type valType, ExpressionType nodeType)
        {
            return new DicModel
            {
                KeyOne = key,
                Param = key,
                ParamRaw = key,
                Value = value,
                ValueType = valType,
                Option = GetOption(nodeType)
            };
        }
        // 01
        private DicModel CallInHandle(string key, string value, Type valType)
        {
            if (valType.IsEnum)
            {
                valType = typeof(int);
            }
            return new DicModel
            {
                KeyOne = key,
                Param = key,
                ParamRaw = key,
                Value = value,
                ValueType = valType,
                Option = OptionEnum.In
            };
        }
        // 01
        private DicModel CallLikeHandle(string key, string value, Type valType)
        {
            return new DicModel
            {
                KeyOne = key,
                Param = key,
                ParamRaw = key,
                Value = value,
                ValueType = valType,
                Option = OptionEnum.Like
            };
        }
        // 01
        private DicModel ConstantBoolHandle(string value, Type valType)
        {
            return new DicModel
            {
                KeyOne = "OneEqualOne",
                Param = "OneEqualOne",
                ParamRaw = "OneEqualOne",
                Value = value,
                ValueType = valType,
                Option = OptionEnum.OneEqualOne
            };
        }
        // 01
        private DicModel MemberBoolHandle(string key, Type valType)
        {
            return new DicModel
            {
                KeyOne = key,
                Param = key,
                ParamRaw = key,
                Value = true.ToString(),
                ValueType = valType,
                Option = OptionEnum.Equal
            };
        }
        // 01
        private (Expression left, Expression right, ExpressionType node) HandBinExpr(BinaryExpression binExpr)
        {
            var binLeft = binExpr.Left;
            var binRight = binExpr.Right;
            var binNode = binExpr.NodeType;
            return (binLeft, binRight, binNode);
        }
        // 02
        private OptionEnum GetOption(ExpressionType nodeType)
        {
            var option = OptionEnum.None;
            if (nodeType == ExpressionType.Equal)
            {
                option = OptionEnum.Equal;
            }
            else if (nodeType == ExpressionType.NotEqual)
            {
                option = OptionEnum.NotEqual;
            }
            else if (nodeType == ExpressionType.LessThan)
            {
                option = OptionEnum.LessThan;
            }
            else if (nodeType == ExpressionType.LessThanOrEqual)
            {
                option = OptionEnum.LessThanOrEqual;
            }
            else if (nodeType == ExpressionType.GreaterThan)
            {
                option = OptionEnum.GreaterThan;
            }
            else if (nodeType == ExpressionType.GreaterThanOrEqual)
            {
                option = OptionEnum.GreaterThanOrEqual;
            }

            return option;
        }
        // 02
        private string HandMember(Expression binRight)
        {
            var result = default(string);
            if (binRight.NodeType == ExpressionType.MemberAccess)
            {
                result = GetMemExprVal(binRight);
            }
            else if (binRight.NodeType == ExpressionType.Convert)
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
                    result = GetMemExprVal(expr.Operand);
                }
            }
            else
            {
                throw new Exception();
            }
            return result;
        }

        // 03 04
        private bool IsListT(Type type)
        {
            if (type.IsGenericType
                && "System.Collections.Generic".Equals(type.Namespace, StringComparison.OrdinalIgnoreCase)
                && "List`1".Equals(type.Name, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
        // 03 04
        private string InValueForListT(Type type, object vals)
        {
            var str = string.Empty;

            //
            var valType = default(Type);
            var typeT = type.GetGenericArguments()[0];
            if (typeT == typeof(string)
                || typeT == typeof(ushort)
                || typeT == typeof(short)
                || typeT == typeof(uint)
                || typeT == typeof(int)
                || typeT == typeof(ulong)
                || typeT == typeof(long))
            {
                valType = typeT;
            }
            else
            {
                var currAssembly = SC.GetAssembly(typeT.FullName);
                valType = currAssembly.GetType(typeT.FullName);
            }

            //
            var ds = vals as dynamic;
            var intVals = new List<object>();
            for (var i = 0; i < ds.Count; i++)
            {
                intVals.Add(GH.GetTypeValue(valType, ds[i]));
            }
            str = string.Join(",", intVals.Select(it => it.ToString()));

            //
            return str;
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
                    var binTuple = HandBinExpr(binExpr);
                    val = GetBinaryVal(binTuple.right);
                    var leftStr = binTuple.left.ToString();
                    if (leftStr.Contains(".Length")
                        && leftStr.IndexOf(".") < leftStr.LastIndexOf("."))
                    {
                        var keyTuple = GetKey(parameter, binTuple.left, OptionEnum.CharLength);
                        result = BinaryCharLengthHandle(keyTuple.key, val, keyTuple.type, binTuple.node);
                    }
                    else
                    {
                        var keyTuple = GetKey(parameter, binTuple.left, GetOption(binTuple.node));
                        result = BinaryNormalHandle(keyTuple.key, val, keyTuple.type, binTuple.node);
                    }
                }
                else if (nodeType == ExpressionType.Call)
                {
                    var mcExpr = body as MethodCallExpression;
                    var exprStr = mcExpr.ToString();
                    var memType = mcExpr.Object.Type;
                    if (exprStr.Contains(".Contains("))
                    {
                        var objNodeType = mcExpr.Object.NodeType;
                        if (objNodeType == ExpressionType.MemberAccess)
                        {
                            var memO = mcExpr.Object as MemberExpression;
                            if (memType.GetInterfaces() != null
                                && memType.GetInterfaces().Contains(typeof(IList))
                                && !memType.IsArray)
                            {
                                val = HandMember(mcExpr.Object);
                                var keyTuple = GetKey(parameter, mcExpr, OptionEnum.In);
                                result = CallInHandle(keyTuple.key, val, keyTuple.type);
                            }
                            else if (memType == typeof(string))
                            {
                                var keyTuple = GetKey(parameter, memO, OptionEnum.Like);
                                val = GetCallVal(mcExpr);
                                result = CallLikeHandle(keyTuple.key, val, keyTuple.type);
                            }
                        }
                        else if (objNodeType == ExpressionType.ListInit)
                        {
                            var liExpr = mcExpr.Object as ListInitExpression;
                            var keyTuple = GetKey(parameter, mcExpr, OptionEnum.In);
                            var vals = new List<string>();
                            foreach (var ini in liExpr.Initializers)
                            {
                                var arg = ini.Arguments[0];
                                vals.Add(GetConVal(ini.Arguments[0], keyTuple.type));
                            }
                            val = string.Join(",", vals);
                            result = CallInHandle(keyTuple.key, val, keyTuple.type);
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
                        result = ConstantBoolHandle(val, valType);
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
                        result = MemberBoolHandle(key, valType);
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
                            Option = GetOption(binExpr.NodeType)
                        };
                    }
                    else if (action == ActionEnum.Where
                        || action == ActionEnum.And
                        || action == ActionEnum.Or)
                    {
                        var tuple = GetMemKeyAlias(binExpr.Left);
                        var val = string.Empty;
                        switch (binExpr.Right.NodeType)
                        {
                            case ExpressionType.Call:
                                val = GetCallVal((binExpr.Right as MethodCallExpression));
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
                            Option = GetOption(binExpr.NodeType)
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
