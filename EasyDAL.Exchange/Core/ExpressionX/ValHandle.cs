using MyDAL.Core.Common;
using MyDAL.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MyDAL.Core.ExpressionX
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
                if (fType.IsList()
                    || fType.IsArray)
                {
                    fName = fInfo.Name;
                    var val = InValue(fType, obj).val;
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
                if (valType.IsList()
                    || valType.IsArray)
                {
                    fName = pInfo.Name;
                    var val = InValue(valType, obj).val;
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
                var innerMember = memExpr.Expression as MemberExpression;

                var valObj = GetMemObj(innerMember.Expression, innerMember.Member);
                fName = targetProp.Name;

                //
                var fType = targetProp.PropertyType;
                if (fType.IsList()
                    || fType.IsArray)
                {
                    var vals = targetProp.GetValue(valObj);
                    var val = InValue(fType, vals).val;
                    var valStr = DateTimeProcess(val, valTypex);
                    objx = (val, valStr);
                }
                else
                {
                    var val = DC.GH.GetTypeValue(targetProp, valObj);
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
                    var obj = Convert.ToDateTime(MemberValue(mcExpr.Object as MemberExpression, funcStr, valType).val);
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
                val = MemberValue(pExpr as MemberExpression, funcStr, valType);
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
                result = MemberValue(expr.Operand as MemberExpression, funcStr, valType);
            }
            else if (expr.Operand.NodeType == ExpressionType.Constant)
            {
                result = ConstantValue(expr.Operand as ConstantExpression, valType);
            }
            return result;
        }

        internal (object val, string valStr) PropertyValue(PropertyInfo prop, object obj)
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

        internal (string val,Type valType) InValue(Type type, object vals)
        {
            var valType = default(Type);
            var typeT = default(Type);
            if (type.IsArray)
            {
                typeT = type.GetElementType();
            }
            else
            {
                typeT = type.GetGenericArguments()[0];
            }
            if (typeT.IsNullable())
            {
                typeT = Nullable.GetUnderlyingType(typeT);
            }
            valType = typeT;

            //
            var ds = vals as dynamic;
            var intVals = new List<object>();
            var num = default(int);
            if (type.IsArray)
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
            var val = string.Join(",", intVals.Select(it => it.ToString()));

            //
            return (val,valType);
        }

    }
}
