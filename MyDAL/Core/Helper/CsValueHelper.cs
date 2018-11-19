using MyDAL.Core.Bases;
using MyDAL.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MyDAL.Core.Helper
{
    internal class CsValueHelper
    {

        private Context DC { get; set; }
        private CsValueHelper() { }
        internal CsValueHelper(Context dc)
        {
            DC = dc;
        }

        /*******************************************************************************************************/

        [Obsolete("作废方法,仅供参考!!!")]
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
        [Obsolete("作废方法,仅作参考!!!")]
        private (object val, string valStr) MemberValue(MemberExpression memExpr, Type valTypex)
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
                var valStr = ValueProcess(val, valTypex, string.Empty);
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
                    var valStr = ValueProcess(val, valTypex, string.Empty);
                    objx = (val, valStr);
                }
                else
                {
                    fName = fInfo.Name;
                    var val = obj;
                    var valStr = ValueProcess(val, valTypex, string.Empty);
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
                    var valStr = ValueProcess(val, valTypex, string.Empty);
                    objx = (val, valStr);
                }
                else
                {
                    fName = pInfo.Name;
                    var val = obj;
                    var valStr = ValueProcess(val, valTypex, string.Empty);
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
                    var valStr = ValueProcess(val, valTypex, string.Empty);
                    objx = (val, valStr);
                }
                else
                {
                    var val = DC.GH.GetTypeValue(targetProp, valObj);
                    var valStr = ValueProcess(val, valTypex, string.Empty);
                    objx = (val, valStr);
                }
            }
            else if (memExpr.Expression.NodeType == ExpressionType.MemberAccess          //  MemberAccess   Field
                && memExpr.Member.MemberType == MemberTypes.Field)
            {
                var targetProp = memExpr.Member as FieldInfo;
                var innerMember = memExpr.Expression as MemberExpression;

                var valObj = GetMemObj(innerMember.Expression, innerMember.Member);
                fName = targetProp.Name;

                //
                var fType = targetProp.FieldType;
                if (fType.IsList()
                    || fType.IsArray)
                {
                    var vals = targetProp.GetValue(valObj);
                    var val = InValue(fType, vals).val;
                    var valStr = ValueProcess(val, valTypex, string.Empty);
                    objx = (val, valStr);
                }
                else
                {
                    var val = DC.GH.GetTypeValue(targetProp, valObj);
                    var valStr = ValueProcess(val, valTypex, string.Empty);
                    objx = (val, valStr);
                }
            }
            else
            {
                throw new Exception($"【{memExpr}】无法解析!!!");
            }

            if (objx.val == null)
            {
                throw new Exception($"条件筛选表达式【】中,条件值【{fName}】不能为 Null !");
            }

            return objx;
        }
        private string InValue(object vals, bool isArray)
        {
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
            return string.Join(",", intVals.Select(it => it.ToString()));
        }
        private string DateTimeProcess(object val,string format)
        {
            return val.ToDateTimeStr(format);
        }
        private string ValueProcess(object val, Type valType,string format)
        {
            if (valType == XConfig.TC.DateTime)
            {
                return DateTimeProcess(val,format);
            }
            else if (valType.IsNullable())
            {
                return ValueProcess(val, Nullable.GetUnderlyingType(valType),format);
            }
            return string.Empty;
        }
        
        /*******************************************************************************************************/

        internal (object val, string valStr) ValueProcess(Expression expr, Type valType,string format="")
        {
            var val = Expression.Lambda(expr).Compile().DynamicInvoke();
            if(expr!=null
                && val==null)
            {
                throw new Exception($"[[{expr.ToString()}]] 中,传入的 SQL 筛选条件为 Null !!!");
            }
            var type = val.GetType();
            if (type.IsArray)
            {
                val = InValue(val, true);
            }
            else if (type.IsList())
            {
                val = InValue(val, false);
            }
            return (val, ValueProcess(val, valType,format));
        }
        internal (object val, string valstr) PropertyValue(PropertyInfo prop, object obj)
        {
            var valtype = prop.PropertyType;
            var val = DC.GH.GetTypeValue(prop, obj);
            var valstr = ValueProcess(val, valtype,string.Empty);
            return (val, valstr);
        }
        internal (object val, string valStr) ExpandoObjectValue(object obj)
        {
            var valType = obj.GetType();
            var val = DC.GH.GetTypeValue(obj);
            var valStr = ValueProcess(val, valType,string.Empty);
            return (val, valStr);
        }
        internal (string val, Type valType) InValue(Type type, object vals)
        {
            var isArray = false;
            var valType = default(Type);
            var typeT = default(Type);
            if (type.IsArray)
            {
                typeT = type.GetElementType();
                isArray = true;
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
            return (InValue(vals, isArray), valType);
        }

    }
}
