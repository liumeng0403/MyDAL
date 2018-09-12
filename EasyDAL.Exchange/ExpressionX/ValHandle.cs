using Yunyong.DataExchange.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Yunyong.DataExchange.ExpressionX
{
    internal class ValHandle
    {

        private DbContext DC { get; set; }

        private ValHandle() { }

        internal ValHandle(DbContext dc)
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

        private bool IsSysType(Type type)
        {
            if (type == typeof(string)
                || type == typeof(ushort)
                || type == typeof(short)
                || type == typeof(uint)
                || type == typeof(int)
                || type == typeof(ulong)
                || type == typeof(long)
                || type == typeof(Guid))
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
            }

            if (IsSysType(typeT))
            {
                valType = typeT;
            }
            else
            {
                var currAssembly = DC.SC.GetAssembly(typeT.FullName);
                valType = currAssembly.GetType(typeT.FullName);
            }

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
                intVals.Add(DC.GH.GetTypeValue(valType, ds[i]));
            }
            str = string.Join(",", intVals.Select(it => it.ToString()));

            //
            return str;
        }


        /*******************************************************************************************************/

        // -02-03-
        internal string GetMemExprVal(MemberExpression memExpr)
        {
            var str = string.Empty;

            //
            if (memExpr.Expression == null                                                                                    //  null   Property   
                && memExpr.Member.MemberType == MemberTypes.Property)
            {
                var targetProp = memExpr.Member as PropertyInfo;
                var type = memExpr.Type as Type;
                var instance = Activator.CreateInstance(type);
                str = targetProp.GetValue(instance, null).ToString();
            }
            else if (memExpr.Expression.NodeType == ExpressionType.Constant                     //  Constant   Field 
                && memExpr.Member.MemberType == MemberTypes.Field)
            {
                var fInfo = memExpr.Member as FieldInfo;
                var cExpr = memExpr.Expression as ConstantExpression;
                var obj = cExpr.Value;
                var fType = fInfo.FieldType;
                if (IsListT(fType)
                    || fType.IsArray)
                {
                    var type = memExpr.Type as Type;
                    var vals = fInfo.GetValue(obj);
                    str = InValueForListT(type, vals, fType.IsArray);
                }
                else
                {
                    str = fInfo.GetValue(obj).ToString();
                }
            }
            else if (memExpr.Expression.NodeType == ExpressionType.Constant                     //  Constant   Property
                && memExpr.Member.MemberType == MemberTypes.Property)
            {
                var pInfo = memExpr.Member as PropertyInfo;
                var cExpr = memExpr.Expression as ConstantExpression;
                var obj = cExpr.Value;
                var valObj = pInfo.GetValue(obj);
                var valType = pInfo.PropertyType;
                if (IsListT(valType)
                    || valType.IsArray)
                {
                    str = InValueForListT(valType, valObj, valType.IsArray);
                }
                else
                {
                    str = DC.GH.GetTypeValue(valType, pInfo, obj);    // 此项 可能 有问题 
                }
            }
            else if (memExpr.Expression.NodeType == ExpressionType.MemberAccess          //  MemberAccess   Property
                && memExpr.Member.MemberType == MemberTypes.Property)
            {
                var targetProp = memExpr.Member as PropertyInfo;
                MemberExpression innerMember = memExpr.Expression as MemberExpression;
                if (innerMember.Member.MemberType == MemberTypes.Property)
                {
                    var pInfo = innerMember.Member as PropertyInfo;
                    var cExpr = innerMember.Expression as ConstantExpression;
                    var obj = cExpr.Value;
                    var valObj = pInfo.GetValue(obj);
                    var fType = targetProp.PropertyType;
                    if (IsListT(fType)
                        || fType.IsArray)
                    {
                        var type = memExpr.Type as Type;
                        var vals = targetProp.GetValue(valObj);
                        str = InValueForListT(type, vals, fType.IsArray);
                    }
                    else
                    {
                        str = DC.GH.GetTypeValue(fType, targetProp, valObj);
                    }
                }
                else if (innerMember.Member.MemberType == MemberTypes.Field)
                {
                    var fInfo = innerMember.Member as FieldInfo;
                    var cExpr = innerMember.Expression as ConstantExpression;
                    var obj = cExpr.Value;
                    var valObj = fInfo.GetValue(obj);
                    var valType = targetProp.PropertyType;
                    str = DC.GH.GetTypeValue(valType, targetProp, valObj);
                }
            }

            //
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
        internal string GetCallVal(MethodCallExpression mcExpr)
        {
            var val = string.Empty;

            //
            var type = mcExpr.Type;
            var pExpr = mcExpr.Arguments[0];
            if (pExpr.NodeType == ExpressionType.Constant
                && type == typeof(DateTime))
            {
                if (mcExpr.Object == null)
                {
                    var con = pExpr as ConstantExpression;
                    val = GetConstantVal(con, con.Type);
                }
                else if (mcExpr.Object.NodeType == ExpressionType.MemberAccess)
                {
                    var obj = Convert.ToDateTime(GetMemExprVal(mcExpr.Object as MemberExpression));
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
            }
            else if (pExpr.NodeType == ExpressionType.Constant)
            {
                var con = pExpr as ConstantExpression;
                val = GetConstantVal(con, con.Type);
            }
            else if (pExpr.NodeType == ExpressionType.MemberAccess)
            {
                val = GetMemExprVal(pExpr as MemberExpression);
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
        internal string GetConstantVal(ConstantExpression con, Type valType)
        {
            //var con = conExpr as ConstantExpression;

            if (valType.IsEnum)
            {
                return ((int)(con.Value)).ToString();
            }
            else
            {
                return con.Value.ToString();
            }
        }

        /*******************************************************************************************************/

    }
}
