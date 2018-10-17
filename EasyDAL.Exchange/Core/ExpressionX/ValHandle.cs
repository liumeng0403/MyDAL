using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Extensions;

namespace Yunyong.DataExchange.Core.ExpressionX
{
    internal class ValHandle
    {

        private Context DC { get; set; }

        private ValHandle() { }

        internal ValHandle(Context dc)
        {
            DC = dc;
        }

        /*******************************************************************************************************/

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
        private string InValueForListT(Type type, object vals, bool isArray)
        {
            var str = string.Empty;

            //
            var valType = default(Type);
            var typeT = default(Type);
            if (isArray)
            {
                typeT = type.GetElementType();
            }
            else
            {
                typeT = type.GetGenericArguments()[0];
                if (typeT.IsGenericType
                    && typeT.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    typeT = typeT.GetGenericArguments()[0];
                }
            }

            valType = typeT;

            //
            var ds = vals as dynamic;
            var intVals = new List<object>();
            var num = default(int);
            if (isArray)
            {
                num = ds.Length;
            }
            else
            {
                num = ds.Count;
            }
            for (var i = 0; i < num; i++)
            {
                intVals.Add(DC.GH.GetTypeValue(ds[i]));
            }
            str = string.Join(",", intVals.Select(it => it.ToString()));

            //
            return str;
        }

        private object GetMemObj(Expression exprX, MemberInfo memX)
        {
            if (memX.MemberType == MemberTypes.Field)
            {
                var fInfo = memX as FieldInfo;
                var obj = default(object);
                if (exprX.NodeType == ExpressionType.Constant)
                {
                    var cExpr = exprX as ConstantExpression;
                    obj = cExpr.Value;
                }
                else if (exprX.NodeType == ExpressionType.MemberAccess)
                {
                    var expr = exprX as MemberExpression;
                    obj = GetMemObj(expr.Expression, expr.Member);
                }
                return fInfo.GetValue(obj);
            }
            else if (memX.MemberType == MemberTypes.Property)
            {
                var fInfo = memX as PropertyInfo;
                var obj = default(object);
                if (exprX.NodeType == ExpressionType.Constant)
                {
                    var cExpr = exprX as ConstantExpression;
                    obj = cExpr.Value;
                }
                else if (exprX.NodeType == ExpressionType.MemberAccess)
                {
                    var expr = exprX as MemberExpression;
                    obj = GetMemObj(expr.Expression, expr.Member);
                }
                return fInfo.GetValue(obj);
            }

            return null;
        }

        private string DateTimeProcess(object val, Type valType)
        {
            var valStr = string.Empty;
            if (valType == XConfig.DateTime)
            {
                valStr = val.ToDateTimeStr();
            }
            return valStr;
        }

        /*******************************************************************************************************/
        
        internal (object val, string valStr) MemberValue(MemberExpression memExpr, string funcStr, Type valTypex)
        {
            var objx = default((object val, string valStr));
            var fName = string.Empty;

            //
            if (memExpr.Expression == null                                                                                    //  null   Property   
                && memExpr.Member.MemberType == MemberTypes.Property)
            {
                var targetProp = memExpr.Member as PropertyInfo;
                var type = memExpr.Type as Type;
                var instance = Activator.CreateInstance(type);
                fName = targetProp.Name;
                var val = DC.GH.GetTypeValue(targetProp, instance);
                var valStr = DateTimeProcess(val, valTypex);
                objx = (val, valStr);
            }
            else if (memExpr.Expression.NodeType == ExpressionType.Constant                     //  Constant   Field 
                && memExpr.Member.MemberType == MemberTypes.Field)
            {
                var fInfo = memExpr.Member as FieldInfo;
                var obj = GetMemObj(memExpr.Expression, memExpr.Member);
                var fType = fInfo.FieldType;
                if (IsListT(fType)
                    || fType.IsArray)
                {
                    var type = memExpr.Type as Type;
                    fName = fInfo.Name;
                    var val = InValueForListT(type, obj, fType.IsArray);
                    var valStr = DateTimeProcess(val, valTypex);
                    objx = (val, valStr);
                }
                else
                {
                    fName = fInfo.Name;
                    var val = obj;
                    var valStr = DateTimeProcess(val, valTypex);
                    objx = (val, valStr);
                }
            }
            else if (memExpr.Expression.NodeType == ExpressionType.Constant                     //  Constant   Property
                && memExpr.Member.MemberType == MemberTypes.Property)
            {
                var pInfo = memExpr.Member as PropertyInfo;
                var obj = GetMemObj(memExpr.Expression, memExpr.Member);
                var valType = pInfo.PropertyType;
                if (IsListT(valType)
                    || valType.IsArray)
                {
                    fName = pInfo.Name;
                    var val = InValueForListT(valType, obj, valType.IsArray);
                    var valStr = DateTimeProcess(val, valTypex);
                    objx = (val, valStr);
                }
                else
                {
                    fName = pInfo.Name;
                    var val = obj;
                    var valStr = DateTimeProcess(val, valTypex);
                    objx = (val, valStr);
                }
            }
            else if (memExpr.Expression.NodeType == ExpressionType.MemberAccess          //  MemberAccess   Property
                && memExpr.Member.MemberType == MemberTypes.Property)
            {
                var targetProp = memExpr.Member as PropertyInfo;
                MemberExpression innerMember = memExpr.Expression as MemberExpression;
                if (innerMember.Member.MemberType == MemberTypes.Property)
                {
                    var valObj = GetMemObj(innerMember.Expression, innerMember.Member);
                    var fType = targetProp.PropertyType;
                    if (IsListT(fType)
                        || fType.IsArray)
                    {
                        var type = memExpr.Type as Type;
                        var vals = targetProp.GetValue(valObj);
                        fName = targetProp.Name;
                        var val = InValueForListT(type, vals, fType.IsArray);
                        var valStr = DateTimeProcess(val, valTypex);
                        objx = (val, valStr);
                    }
                    else
                    {
                        fName = targetProp.Name;
                        var val = DC.GH.GetTypeValue(targetProp, valObj);
                        var valStr = DateTimeProcess(val, valTypex);
                        objx = (val, valStr);
                    }
                }
                else if (innerMember.Member.MemberType == MemberTypes.Field)
                {
                    var expr = innerMember.Expression;
                    var mem = innerMember.Member;
                    var objX = GetMemObj(expr, mem);

                    fName = targetProp.Name;
                    var val = DC.GH.GetTypeValue(targetProp, objX);
                    var valStr = DateTimeProcess(val, valTypex);
                    objx = (val, valStr);
                }
            }

            if (objx.val == null)
            {
                throw new Exception($"条件筛选表达式【{funcStr}】中,条件值【{fName}】不能为 Null !");
            }

            return objx;
        }

        internal (object val, string valStr) MethodCallValue(MethodCallExpression mcExpr, string funcStr, Type valType)
        {
            var val = default((object val, string valStr));

            //
            var type = mcExpr.Type;
            var pExpr = mcExpr.Arguments[0];
            if (pExpr.NodeType == ExpressionType.Constant
                && type == typeof(DateTime))
            {
                if (mcExpr.Object == null)
                {
                    var con = pExpr as ConstantExpression;
                    val = ConstantValue(con, valType);
                }
                else if (mcExpr.Object.NodeType == ExpressionType.MemberAccess)
                {
                    var obj = Convert.ToDateTime(MemberValue(mcExpr.Object as MemberExpression, funcStr,valType).val);
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
                    var objx = type.InvokeMember(method, BindingFlags.Default | BindingFlags.InvokeMethod, null, obj, args.ToArray());
                    var valStr = DateTimeProcess(objx, valType);
                    val = (objx, valStr);
                }
            }
            else if (pExpr.NodeType == ExpressionType.Constant)
            {
                var con = pExpr as ConstantExpression;
                val = ConstantValue(con, valType);
            }
            else if (pExpr.NodeType == ExpressionType.MemberAccess)
            {
                val = MemberValue(pExpr as MemberExpression, funcStr,valType);
            }

            if (val.val != null)
            {
                return val;
            }
            else
            {
                throw new Exception();
            }
        }

        internal (object val, string valStr) ConstantValue(ConstantExpression con, Type valType)
        {
            var val = con.Value;
            var valStr = DateTimeProcess(val, valType);
            return (val, valStr);
        }
        
        internal (object val, string valStr) ConvertValue(UnaryExpression expr, string funcStr, Type valType)
        {
            var result = default((object, string));
            if (expr.Operand.NodeType == ExpressionType.Convert)
            {
                //
                var exprExpr = expr.Operand as UnaryExpression;
                var memExpr = exprExpr.Operand as MemberExpression;
                var memCon = memExpr.Expression as ConstantExpression;
                var memObj = memCon.Value;
                var memFiled = memExpr.Member as FieldInfo;

                //
                var val = memFiled.GetValue(memObj);
                var valStr = DateTimeProcess(val, valType);
                result = (val, valStr);
            }
            else if (expr.Operand.NodeType == ExpressionType.MemberAccess)
            {
                result = MemberValue(expr.Operand as MemberExpression, funcStr,valType);
            }
            else if (expr.Operand.NodeType == ExpressionType.Constant)
            {
                result = ConstantValue(expr.Operand as ConstantExpression, valType);
            }
            return result;
        }

        internal (object val, string valStr) PropertyValue(PropertyInfo prop ,object obj)
        {
            var valType = prop.PropertyType;
            var val = DC.GH.GetTypeValue(prop, obj);
            var valStr = DateTimeProcess(val, valType);
            return (val, valStr);
        }

        internal (object val, string valStr) ExpandoObjectValue(object obj)
        {
            var valType = obj.GetType();
            var val = DC.GH.GetTypeValue(obj);
            var valStr = DateTimeProcess(val, valType);
            return (val, valStr);
        }

    }
}
