using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Yunyong.DataExchange.Cache;
using Yunyong.DataExchange.Common;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Enums;

namespace Yunyong.DataExchange.ExpressionX
{
    internal class ExpressionHandleX
    {
        private Context DC { get; set; }

        private ExpressionHandleX() { }
        internal ExpressionHandleX(Context dc)
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
                        if (info.IsDefined(typeof(TableColumnAttribute), false))
                        {
                            var attr = (TableColumnAttribute)info.GetCustomAttributes(typeof(TableColumnAttribute), false)[0];
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
        private string HandMember(MemberExpression binRight)
        {
            var result = default(string);
            if (binRight.NodeType == ExpressionType.MemberAccess)
            {
                result = DC.VH.GetMemExprVal(binRight);
            }
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

        private DicModel StringLike(MethodCallExpression mcExpr, StringLikeEnum type)
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
                        var val = string.Empty;
                        switch (type)
                        {
                            case StringLikeEnum.Contains:
                                val = DC.VH.GetCallVal(mcExpr);
                                break;
                            case StringLikeEnum.StartsWith:
                                val = $"{DC.VH.GetCallVal(mcExpr)}%";
                                break;
                            case StringLikeEnum.EndsWith:
                                val = $"%{DC.VH.GetCallVal(mcExpr)}";
                                break;
                        }
                        return DicHandle.CallLikeHandle(keyTuple.key, keyTuple.alias, val, keyTuple.valType);
                    }
                }
            }

            return null;
        }

        private DicModel CollectionIn(Expression expr, MemberExpression memExpr)
        {
            var keyTuple = GetKey(expr, OptionEnum.In);
            var val = HandMember(memExpr);
            return DicHandle.CallInHandle(keyTuple.key, keyTuple.alias, val, keyTuple.valType);
        }

        private DicModel NewCollectionIn(ExpressionType nodeType, Expression keyExpr, Expression valExpr)
        {
            if (nodeType == ExpressionType.NewArrayInit)
            {
                var naExpr = valExpr as NewArrayExpression;
                var keyTuple = GetKey(keyExpr, OptionEnum.In);
                var vals = new List<string>();
                foreach (var exp in naExpr.Expressions)
                {
                    vals.Add(DC.VH.GetConstantVal(exp as ConstantExpression, keyTuple.valType));
                }

                var val = string.Join(",", vals);
                return DicHandle.CallInHandle(keyTuple.key, keyTuple.alias, val, keyTuple.valType);
            }
            else if (nodeType == ExpressionType.ListInit)
            {
                var liExpr = valExpr as ListInitExpression;
                var keyTuple = GetKey(keyExpr, OptionEnum.In);
                var vals = new List<string>();
                foreach (var ini in liExpr.Initializers)
                {
                    var arg = ini.Arguments[0];
                    vals.Add(DC.VH.GetConstantVal(ini.Arguments[0] as ConstantExpression, keyTuple.valType));
                }

                var val = string.Join(",", vals);
                return DicHandle.CallInHandle(keyTuple.key, keyTuple.alias, val, keyTuple.valType);
            }

            return null;
        }

        /********************************************************************************************************************/

        private DicModel HandConditionBinary(BinaryExpression binExpr, List<string> pres)
        {
            var binTuple = HandBinExpr(pres, binExpr);
            if ((binTuple.node == ExpressionType.Equal || binTuple.node == ExpressionType.NotEqual)
                && binTuple.right.NodeType == ExpressionType.Constant
                && (binTuple.right as ConstantExpression).Value == null)
            {
                var optionx = OptionEnum.None;
                var tuple = GetKey(binTuple.left, optionx);
                if(binTuple.node== ExpressionType.Equal)
                {
                    optionx = OptionEnum.IsNull;
                }
                else
                {
                    optionx = OptionEnum.IsNotNull;
                }
                return new DicModel
                {
                    ColumnOne = tuple.key,
                    TableAliasOne = tuple.alias,
                    Param = tuple.key,
                    ParamRaw = tuple.key,
                    CsValue = null,
                    ValueType = tuple.valType,
                    Option = optionx,
                    Compare = CompareEnum.None
                };
            }
            else
            {
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
                    var keyTuple = GetKey(binTuple.left, OptionEnum.Compare /*DicHandle.GetOption(binTuple.node, binTuple.isR)*/);
                    return DicHandle.BinaryNormalHandle(keyTuple.key, keyTuple.alias, val, keyTuple.valType, binTuple.node, binTuple.isR);
                }
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
                        return CollectionIn(memKey, memVal as MemberExpression);
                    }
                    else if (memVal.NodeType == ExpressionType.NewArrayInit)
                    {
                        return NewCollectionIn(memVal.NodeType, memKey, memVal);
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
                            return CollectionIn(mcExpr, memO);
                        }
                        else if (memType == typeof(string))
                        {
                            return StringLike(mcExpr, StringLikeEnum.Contains);
                        }
                    }
                    else if (objNodeType == ExpressionType.ListInit)
                    {
                        return NewCollectionIn(objNodeType, mcExpr, objExpr);
                    }
                }
            }
            else if (exprStr.Contains(".StartsWith("))
            {
                return StringLike(mcExpr, StringLikeEnum.StartsWith);
            }
            else if (exprStr.Contains(".EndsWith("))
            {
                return StringLike(mcExpr, StringLikeEnum.EndsWith);
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
            var tuple = GetMemTuple(memExpr);
            if (tuple.valType == typeof(bool))
            {
                return DicHandle.MemberBoolHandle(tuple.key, tuple.alias, tuple.valType);
            }

            return null;
        }

        private (string key, string alias, Type valType) GetMemTuple(MemberExpression memExpr)
        {
            //var memProp = memExpr.Member as PropertyInfo;
            //var valType = memProp.PropertyType;
            //var key = memProp.Name;
            //var alias = GetAlias(memExpr);
            var tuple = GetKey(memExpr, OptionEnum.None);
            return (tuple.key, tuple.alias, tuple.valType);
        }

        /********************************************************************************************************************/

        private List<DicModel> HandSelectMemberInit(MemberInitExpression miExpr)
        {
            var result = new List<DicModel>();

            foreach (var mb in miExpr.Bindings)
            {
                var mbEx = mb as MemberAssignment;
                var maMem = mbEx.Expression as MemberExpression;
                var tuple = GetMemTuple(maMem);
                var colAlias = mbEx.Member.Name;
                result.Add(new DicModel
                {
                    TableAliasOne = tuple.alias,
                    ColumnOne = tuple.key,
                    ColumnAlias = colAlias
                });
            }

            return result;
        }

        /********************************************************************************************************************/

        private DicModel HandOnBinary(BinaryExpression binExpr)
        {
            var option = OptionEnum.Compare;
            var tuple1 = GetKey(binExpr.Left, option);
            var tuple2 = GetKey(binExpr.Right, option);
            return new DicModel
            {
                ColumnOne = tuple1.key,
                TableAliasOne = tuple1.alias,
                KeyTwo = tuple2.key,
                AliasTwo = tuple2.alias,
                Option = option,
                Compare = DicHandle.GetOption(binExpr.NodeType, false)
            };
        }

        /********************************************************************************************************************/

        /// <summary>
        /// 获取表达式信息
        /// </summary>
        public List<DicModel> ExpressionHandle<M, F>(Expression<Func<M, F>> func)
        {
            try
            {
                var result = new List<DicModel>();
                var body = func.Body;
                var nodeType = body.NodeType;
                if (nodeType == ExpressionType.MemberAccess)
                {
                    var memExpr = func.Body as MemberExpression;
                    var keyTuple = GetKey(memExpr, OptionEnum.None);
                    var key = keyTuple.key;

                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        result.Add(new DicModel
                        {
                            ColumnOne = key
                        });
                    }
                }
                else if (nodeType == ExpressionType.MemberInit)
                {
                    var miExpr = func.Body as MemberInitExpression;
                    result = HandSelectMemberInit(miExpr);
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
        public List<DicModel> ExpressionHandle<M>(Expression<Func<M>> func)
        {
            try
            {
                var result = new List<DicModel>();
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
                        result.Add(new DicModel
                        {
                            TableAliasOne = tuple.alias,
                            ColumnOne = tuple.key,
                            ColumnAlias = colAlias
                        });
                    }

                }
                else if (nodeType == ExpressionType.MemberAccess)
                {
                    var body = func.Body as MemberExpression;
                    var alias = body.Member.Name;
                    var table = DC.SC.GetModelTableName(DC.SC.GetKey(body.Type.FullName, DC.Conn.Database));  // DC.SqlProvider.GetTableName(body.Type);
                    result.Add(new DicModel
                    {
                        TableOne = table,
                        ClassFullName = body.Type.FullName,
                        TableAliasOne = alias
                    });
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
                        var pres = DC.Conditions.Select(it => it.TableAliasOne).ToList();
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
