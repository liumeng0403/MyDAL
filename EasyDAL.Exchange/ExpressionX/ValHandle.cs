using EasyDAL.Exchange.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace EasyDAL.Exchange.ExpressionX
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
                var currAssembly = DC.SC.GetAssembly(typeT.FullName);
                valType = currAssembly.GetType(typeT.FullName);
            }

            //
            var ds = vals as dynamic;
            var intVals = new List<object>();
            for (var i = 0; i < ds.Count; i++)
            {
                intVals.Add(DC.GH.GetTypeValue(valType, ds[i]));
            }
            str = string.Join(",", intVals.Select(it => it.ToString()));

            //
            return str;
        }
        // 03 04
        private string InValueForArray(Type type, object vals)
        {
            var str = string.Empty;

            //
            var valType = default(Type);
            var typeT = type.GetElementType();
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
                var currAssembly = DC.SC.GetAssembly(typeT.FullName);
                valType = currAssembly.GetType(typeT.FullName);
            }

            //
            var ds = vals as dynamic;
            var intVals = new List<object>();
            for (var i = 0; i < ds.Length; i++)
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
            var targetProp = memExpr.Member as PropertyInfo;
            if (targetProp == null)
            {
                var targetField = memExpr.Member as FieldInfo;
                var memCon = memExpr.Expression as ConstantExpression;
                object memObj = memCon.Value;
                var fType = targetField.FieldType;
                if (IsListT(fType)
                    || fType.IsArray)
                {
                    var type = memExpr.Type as Type;
                    var vals = targetField.GetValue(memObj);
                    if (fType.IsArray)
                    {
                        str = InValueForArray(type, vals);
                    }
                    else
                    {
                        str = InValueForListT(type, vals);
                    }
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
                        if (IsListT(valType)
                            || valType.IsArray)
                        {
                            if (valType.IsArray)
                            {
                                str = InValueForArray(valType, vals);
                            }
                            else
                            {
                                str = InValueForListT(valType, vals);
                            }
                        }
                        else
                        {
                            str = DC.GH.GetTypeValue(valType, memP, innerObj);    // 此项 可能 有问题 
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
                            if (IsListT(fType)
                                || fType.IsArray)
                            {
                                var type = memExpr.Type as Type;
                                var vals = targetProp.GetValue(outerObj);
                                if (fType.IsArray)
                                {
                                    str = InValueForArray(type, vals);
                                }
                                else
                                {
                                    str = InValueForListT(type, vals);
                                }
                            }
                            else
                            {
                                str = DC.GH.GetTypeValue(fType, targetProp, outerObj);
                            }
                        }
                        else
                        {
                            var ce = innerMember.Expression as ConstantExpression;
                            object innerObj = ce.Value;
                            var outerObj = innerField.GetValue(innerObj);
                            var valType = targetProp.PropertyType;
                            str = DC.GH.GetTypeValue(valType, targetProp, outerObj);
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
        internal string GetCallVal(MethodCallExpression mcExpr)
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
                else
                {
                    val = (string)((pExpr as ConstantExpression).Value);
                }
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
