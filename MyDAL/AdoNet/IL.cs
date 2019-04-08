using HPC.DAL.Core;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace HPC.DAL.AdoNet
{
    internal class CommonIL
    {
        internal static RowMap GetTypeMap(Type mType)
        {
            if (!XCache.TypeMaps.TryGetValue(mType, out var map))
            {
                map = new RowMap(mType);
                XCache.TypeMaps[mType] = map;
            }
            return map;
        }
        internal static void EmitInt32(ILGenerator il, int value)
        {
            switch (value)
            {
                case -1: il.Emit(OpCodes.Ldc_I4_M1); break;
                case 0: il.Emit(OpCodes.Ldc_I4_0); break;
                case 1: il.Emit(OpCodes.Ldc_I4_1); break;
                case 2: il.Emit(OpCodes.Ldc_I4_2); break;
                case 3: il.Emit(OpCodes.Ldc_I4_3); break;
                case 4: il.Emit(OpCodes.Ldc_I4_4); break;
                case 5: il.Emit(OpCodes.Ldc_I4_5); break;
                case 6: il.Emit(OpCodes.Ldc_I4_6); break;
                case 7: il.Emit(OpCodes.Ldc_I4_7); break;
                case 8: il.Emit(OpCodes.Ldc_I4_8); break;
                default:
                    if (value >= -128 && value <= 127)
                    {
                        il.Emit(OpCodes.Ldc_I4_S, (sbyte)value);
                    }
                    else
                    {
                        il.Emit(OpCodes.Ldc_I4, value);
                    }
                    break;
            }
        }
        internal static void StoreLocal(ILGenerator il, int index)
        {
            if (index < 0 || index >= short.MaxValue) throw new ArgumentNullException(nameof(index));
            switch (index)
            {
                case 0: il.Emit(OpCodes.Stloc_0); break;
                case 1: il.Emit(OpCodes.Stloc_1); break;
                case 2: il.Emit(OpCodes.Stloc_2); break;
                case 3: il.Emit(OpCodes.Stloc_3); break;
                default:
                    if (index <= 255)
                    {
                        il.Emit(OpCodes.Stloc_S, (byte)index);
                    }
                    else
                    {
                        il.Emit(OpCodes.Stloc, (short)index);
                    }
                    break;
            }
        }
        internal static MethodInfo ResolveOperator(MethodInfo[] methods, Type from, Type to, string name)
        {
            for (int i = 0; i < methods.Length; i++)
            {
                if (methods[i].Name != name
                    || methods[i].ReturnType != to)
                {
                    continue;
                }
                var args = methods[i].GetParameters();
                if (args.Length != 1
                    || args[0].ParameterType != from)
                {
                    continue;
                }
                return methods[i];
            }
            return null;
        }
        internal static MethodInfo GetOperator(Type from, Type to)
        {
            if (to == null)
            {
                return null;
            }
            MethodInfo[] fromMethods, toMethods;
            return ResolveOperator(fromMethods = from.GetMethods(BindingFlags.Static | BindingFlags.Public), from, to, "op_Implicit")
                ?? ResolveOperator(toMethods = to.GetMethods(BindingFlags.Static | BindingFlags.Public), from, to, "op_Implicit")
                ?? ResolveOperator(fromMethods, from, to, "op_Explicit")
                ?? ResolveOperator(toMethods, from, to, "op_Explicit");
        }
        internal static void FlexibleConvertBoxedFromHeadOfStack(ILGenerator il, Type from, Type to, Type via)
        {
            MethodInfo op;
            if (from == (via ?? to))
            {
                il.Emit(OpCodes.Unbox_Any, to); // stack is now [target][target][typed-value]
            }
            else if ((op = GetOperator(from, to)) != null)
            {
                // this is handy for things like decimal <===> double
                il.Emit(OpCodes.Unbox_Any, from); // stack is now [target][target][data-typed-value]
                il.Emit(OpCodes.Call, op); // stack is now [target][target][typed-value]
            }
            else
            {
                bool handled = false;
                OpCode opCode = default(OpCode);
                switch (Type.GetTypeCode(from))
                {
                    case TypeCode.Boolean:
                    case TypeCode.Byte:
                    case TypeCode.SByte:
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                    case TypeCode.Single:
                    case TypeCode.Double:
                        handled = true;
                        switch (Type.GetTypeCode(via ?? to))
                        {
                            case TypeCode.Byte:
                                opCode = OpCodes.Conv_Ovf_I1_Un; break;
                            case TypeCode.SByte:
                                opCode = OpCodes.Conv_Ovf_I1; break;
                            case TypeCode.UInt16:
                                opCode = OpCodes.Conv_Ovf_I2_Un; break;
                            case TypeCode.Int16:
                                opCode = OpCodes.Conv_Ovf_I2; break;
                            case TypeCode.UInt32:
                                opCode = OpCodes.Conv_Ovf_I4_Un; break;
                            case TypeCode.Boolean: // boolean is basically an int, at least at this level
                            case TypeCode.Int32:
                                opCode = OpCodes.Conv_Ovf_I4; break;
                            case TypeCode.UInt64:
                                opCode = OpCodes.Conv_Ovf_I8_Un; break;
                            case TypeCode.Int64:
                                opCode = OpCodes.Conv_Ovf_I8; break;
                            case TypeCode.Single:
                                opCode = OpCodes.Conv_R4; break;
                            case TypeCode.Double:
                                opCode = OpCodes.Conv_R8; break;
                            default:
                                handled = false;
                                break;
                        }
                        break;
                }
                if (handled)
                {
                    il.Emit(OpCodes.Unbox_Any, from); // stack is now [target][target][col-typed-value]
                    il.Emit(opCode); // stack is now [target][target][typed-value]
                    if (to == typeof(bool))
                    { // compare to zero; I checked "csc" - this is the trick it uses; nice
                        il.Emit(OpCodes.Ldc_I4_0);
                        il.Emit(OpCodes.Ceq);
                        il.Emit(OpCodes.Ldc_I4_0);
                        il.Emit(OpCodes.Ceq);
                    }
                }
                else
                {
                    il.Emit(OpCodes.Ldtoken, via ?? to); // stack is now [target][target][value][member-type-token]
                    il.EmitCall(OpCodes.Call, typeof(Type).GetMethod(nameof(Type.GetTypeFromHandle)), null); // stack is now [target][target][value][member-type]
                    il.EmitCall(OpCodes.Call, typeof(Convert).GetMethod(nameof(Convert.ChangeType), new Type[] { typeof(object), typeof(Type) }), null); // stack is now [target][target][boxed-member-type-value]
                    il.Emit(OpCodes.Unbox_Any, to); // stack is now [target][target][typed-value]
                }
            }
        }
        internal static void LoadLocal(ILGenerator il, int index)
        {
            if (index < 0
                || index >= short.MaxValue)
            {
                throw new ArgumentNullException(nameof(index));
            }
            switch (index)
            {
                case 0:
                    il.Emit(OpCodes.Ldloc_0);
                    break;
                case 1:
                    il.Emit(OpCodes.Ldloc_1);
                    break;
                case 2:
                    il.Emit(OpCodes.Ldloc_2);
                    break;
                case 3:
                    il.Emit(OpCodes.Ldloc_3);
                    break;
                default:
                    if (index <= 255)
                    {
                        il.Emit(OpCodes.Ldloc_S, (byte)index);
                    }
                    else
                    {
                        il.Emit(OpCodes.Ldloc, (short)index);
                    }
                    break;
            }
        }
        internal static char ReadChar(object value)
        {
            if (value == null
                || value is DBNull)
            {
                throw new ArgumentNullException(nameof(value));
            }
            var s = value as string;
            if (s == null
                || s.Length != 1)
            {
                throw new ArgumentException("A single-character was expected", nameof(value));
            }
            return s[0];
        }
        internal static char? ReadNullableChar(object value)
        {
            if (value == null
                || value is DBNull)
            {
                return null;
            }
            var s = value as string;
            if (s == null
                || s.Length != 1)
            {
                throw new ArgumentException("A single-character was expected", nameof(value));
            }
            return s[0];
        }

        public static void ThrowDataException(Exception ex, int index, IDataReader reader, object value)
        {
            Exception toThrow;
            try
            {
                string name = "(n/a)", formattedValue = "(n/a)";
                if (reader != null && index >= 0 && index < reader.FieldCount)
                {
                    name = reader.GetName(index);
                    try
                    {
                        if (value == null || value is DBNull)
                        {
                            formattedValue = "<null>";
                        }
                        else
                        {
                            formattedValue = Convert.ToString(value) + " - " + Type.GetTypeCode(value.GetType());
                        }
                    }
                    catch (Exception valEx)
                    {
                        formattedValue = valEx.Message;
                    }
                }
                toThrow = new DataException($"Error parsing column {index} ({name}={formattedValue})", ex);
            }
            catch
            { // throw the **original** exception, wrapped as DataException
                toThrow = new DataException(ex.Message, ex);
            }
            throw toThrow;
        }
    }

    internal class IL<M>
    {
        private static Func<IDataReader, M> SetFunc(IDataReader reader)
        {
            // 
            var mType = typeof(M);
            var dm = new DynamicMethod("MyDAL" + Guid.NewGuid().ToString(), mType, new[] { typeof(IDataReader) }, mType, true);
            var il = dm.GetILGenerator();
            il.DeclareLocal(typeof(int));    // 定义 loc0  int
            il.DeclareLocal(mType);    //  定义 loc1 M
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Stloc_0);   //  赋值 loc0 = 0

            //
            var length = reader.FieldCount;
            var names = Enumerable.Range(0, length).Select(i => reader.GetName(i)).ToArray();
            var typeMap = CommonIL.GetTypeMap(mType);
            int index = 0;
            var ctor = typeMap.DefaultConstructor();

            //
            il.Emit(OpCodes.Newobj, ctor);
            il.Emit(OpCodes.Stloc_1);   //  赋值 loc1 = new M();
            il.BeginExceptionBlock();    //  try begin 
            il.Emit(OpCodes.Ldloc_1);    //   加载 loc1 

            //
            var members = names.Select(n => typeMap.GetMember(n)).ToList();
            //bool first = true;
            var allDone = il.DefineLabel();
            int enumDeclareLocal = -1;
            int valueCopyLocal = il.DeclareLocal(typeof(object)).LocalIndex;    // 定义 loc_object 变量, 然后返回此本地变量的index值, 其实就是截止目前, 定义了本地变量的个数            
            foreach (var item in members)
            {
                if (item != null)
                {
                    il.Emit(OpCodes.Dup);
                    Label isDbNullLabel = il.DefineLabel();
                    Label finishLabel = il.DefineLabel();

                    il.Emit(OpCodes.Ldarg_0);
                    CommonIL.EmitInt32(il, index);
                    il.Emit(OpCodes.Dup);
                    il.Emit(OpCodes.Stloc_0);
                    il.Emit(OpCodes.Callvirt, XConfig.GetItem);   //  获取reader读取的值, reader[index]
                    il.Emit(OpCodes.Dup);
                    CommonIL.StoreLocal(il, valueCopyLocal);   //  将 reader[index]的值, 存放到本地变量 loc_object 中
                    Type colType = reader.GetFieldType(index);    // reader[index] 的列的类型  source
                    Type memberType = item.MemberType();   //  M[item] 的类型   

                    if (memberType == XConfig.TC.Char || memberType == XConfig.TC.CharNull)
                    {
                        il.EmitCall(
                            OpCodes.Call,
                            typeof(CommonIL).GetMethod(
                                memberType == XConfig.TC.Char
                                ? nameof(CommonIL.ReadChar)
                                : nameof(CommonIL.ReadNullableChar), BindingFlags.Static | BindingFlags.NonPublic),
                            null);
                    }
                    else
                    {
                        il.Emit(OpCodes.Dup);
                        il.Emit(OpCodes.Isinst, typeof(DBNull));    //  判断是否为DBNull类型, 如果是, 则跳转到 标签isDbNullLabel 
                        il.Emit(OpCodes.Brtrue_S, isDbNullLabel);

                        var nullUnderlyingType = Nullable.GetUnderlyingType(memberType);
                        var unboxType = nullUnderlyingType?.IsEnum == true ? nullUnderlyingType : memberType;

                        if (unboxType.IsEnum)
                        {
                            Type numericType = Enum.GetUnderlyingType(unboxType);
                            if (colType == typeof(string))
                            {
                                if (enumDeclareLocal == -1)
                                {
                                    enumDeclareLocal = il.DeclareLocal(typeof(string)).LocalIndex;
                                }
                                il.Emit(OpCodes.Castclass, typeof(string)); // stack is now [target][target][string]
                                CommonIL.StoreLocal(il, enumDeclareLocal); // stack is now [target][target]
                                il.Emit(OpCodes.Ldtoken, unboxType); // stack is now [target][target][enum-type-token]
                                il.EmitCall(OpCodes.Call, typeof(Type).GetMethod(nameof(Type.GetTypeFromHandle)), null);// stack is now [target][target][enum-type]
                                CommonIL.LoadLocal(il, enumDeclareLocal); // stack is now [target][target][enum-type][string]
                                il.Emit(OpCodes.Ldc_I4_1); // stack is now [target][target][enum-type][string][true]
                                il.EmitCall(OpCodes.Call, XConfig.EnumParse, null); // stack is now [target][target][enum-as-object]
                                il.Emit(OpCodes.Unbox_Any, unboxType); // stack is now [target][target][typed-value]
                            }
                            else
                            {
                                CommonIL.FlexibleConvertBoxedFromHeadOfStack(il, colType, unboxType, numericType);
                            }

                            if (nullUnderlyingType != null)
                            {
                                il.Emit(OpCodes.Newobj, memberType.GetConstructor(new[] { nullUnderlyingType })); // stack is now [target][target][typed-value]
                            }
                        }
                        else if (memberType.FullName == XConfig.TC.LinqBinary)
                        {
                            il.Emit(OpCodes.Unbox_Any, typeof(byte[])); // stack is now [target][target][byte-array]
                            il.Emit(OpCodes.Newobj, memberType.GetConstructor(new Type[] { typeof(byte[]) }));// stack is now [target][target][binary]
                        }
                        else
                        {
                            TypeCode dataTypeCode = Type.GetTypeCode(colType), unboxTypeCode = Type.GetTypeCode(unboxType);
                            if (colType == unboxType || dataTypeCode == unboxTypeCode || dataTypeCode == Type.GetTypeCode(nullUnderlyingType))
                            {
                                il.Emit(OpCodes.Unbox_Any, unboxType); // stack is now [target][target][typed-value]
                            }
                            else
                            {
                                // not a direct match; need to tweak the unbox
                                CommonIL.FlexibleConvertBoxedFromHeadOfStack(il, colType, nullUnderlyingType ?? unboxType, null);
                                if (nullUnderlyingType != null)
                                {
                                    il.Emit(OpCodes.Newobj, unboxType.GetConstructor(new[] { nullUnderlyingType })); // stack is now [target][target][typed-value]
                                }
                            }
                        }
                    }
                    // Store the value in the property/field
                    if (item.Property != null)
                    {
                        il.Emit(OpCodes.Callvirt, RowMap.GetPropertySetter(item.Property, mType));
                    }
                    else
                    {
                        il.Emit(OpCodes.Stfld, item.Field); // stack is now [target]
                    }

                    il.Emit(OpCodes.Br_S, finishLabel); // stack is now [target]
                    il.MarkLabel(isDbNullLabel); // incoming stack: [target][target][value]
                    il.Emit(OpCodes.Pop); // stack is now [target][target]
                    il.Emit(OpCodes.Pop); // stack is now [target]
                    il.MarkLabel(finishLabel);
                }
                //first = false;
                index++;
            }

            il.Emit(OpCodes.Stloc_1); // stack is empty
            il.MarkLabel(allDone);
            il.BeginCatchBlock(typeof(Exception)); // stack is Exception
            il.Emit(OpCodes.Ldloc_0); // stack is Exception, index
            il.Emit(OpCodes.Ldarg_0); // stack is Exception, index, reader
            CommonIL.LoadLocal(il, valueCopyLocal); // stack is Exception, index, reader, value
            il.EmitCall(OpCodes.Call, typeof(CommonIL).GetMethod(nameof(CommonIL.ThrowDataException)), null);
            il.EndExceptionBlock();

            il.Emit(OpCodes.Ldloc_1); // stack is [rval]
            il.Emit(OpCodes.Ret);

            var funcType = Expression.GetFuncType(typeof(IDataReader), mType);
            return (Func<IDataReader, M>)dm.CreateDelegate(funcType);
        }

        internal static Row<M> Row(IDataReader reader)
        {
            return new Row<M>
            {
                Handle = SetFunc(reader)
            };
        }
    }

    internal class IL
    {
        private static Func<IDataReader, object> SetFunc(IDataReader reader, Type mType)
        {
            // 
            var dm = new DynamicMethod("MyDAL" + Guid.NewGuid().ToString(), mType, new[] { typeof(IDataReader) }, mType, true);
            var il = dm.GetILGenerator();
            il.DeclareLocal(typeof(int));    // 定义 loc0  int
            il.DeclareLocal(mType);    //  定义 loc1 M
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Stloc_0);   //  赋值 loc0 = 0

            //
            var length = reader.FieldCount;
            var names = Enumerable.Range(0, length).Select(i => reader.GetName(i)).ToArray();
            var typeMap = CommonIL.GetTypeMap(mType);
            int index = 0;
            var ctor = typeMap.DefaultConstructor();

            //
            il.Emit(OpCodes.Newobj, ctor);
            il.Emit(OpCodes.Stloc_1);   //  赋值 loc1 = new M();
            il.BeginExceptionBlock();    //  try begin 
            il.Emit(OpCodes.Ldloc_1);    //   加载 loc1 

            //
            var members = names.Select(n => typeMap.GetMember(n)).ToList();
            //bool first = true;
            var allDone = il.DefineLabel();
            int enumDeclareLocal = -1;
            int valueCopyLocal = il.DeclareLocal(typeof(object)).LocalIndex;    // 定义 loc_object 变量, 然后返回此本地变量的index值, 其实就是截止目前, 定义了本地变量的个数            
            foreach (var item in members)
            {
                if (item != null)
                {
                    il.Emit(OpCodes.Dup);
                    Label isDbNullLabel = il.DefineLabel();
                    Label finishLabel = il.DefineLabel();

                    il.Emit(OpCodes.Ldarg_0);
                    CommonIL.EmitInt32(il, index);
                    il.Emit(OpCodes.Dup);
                    il.Emit(OpCodes.Stloc_0);
                    il.Emit(OpCodes.Callvirt, XConfig.GetItem);   //  获取reader读取的值, reader[index]
                    il.Emit(OpCodes.Dup);
                    CommonIL.StoreLocal(il, valueCopyLocal);   //  将 reader[index]的值, 存放到本地变量 loc_object 中
                    Type colType = reader.GetFieldType(index);    // reader[index] 的列的类型  source
                    Type memberType = item.MemberType();   //  M[item] 的类型   

                    if (memberType == XConfig.TC.Char || memberType == XConfig.TC.CharNull)
                    {
                        il.EmitCall(
                            OpCodes.Call,
                            typeof(CommonIL).GetMethod(
                                memberType == typeof(char)
                                ? nameof(CommonIL.ReadChar)
                                : nameof(CommonIL.ReadNullableChar), BindingFlags.Static | BindingFlags.NonPublic),
                            null);
                    }
                    else
                    {
                        il.Emit(OpCodes.Dup);
                        il.Emit(OpCodes.Isinst, typeof(DBNull));    //  判断是否为DBNull类型, 如果是, 则跳转到 标签isDbNullLabel 
                        il.Emit(OpCodes.Brtrue_S, isDbNullLabel);

                        var nullUnderlyingType = Nullable.GetUnderlyingType(memberType);
                        var unboxType = nullUnderlyingType?.IsEnum == true ? nullUnderlyingType : memberType;

                        if (unboxType.IsEnum)
                        {
                            Type numericType = Enum.GetUnderlyingType(unboxType);
                            if (colType == typeof(string))
                            {
                                if (enumDeclareLocal == -1)
                                {
                                    enumDeclareLocal = il.DeclareLocal(typeof(string)).LocalIndex;
                                }
                                il.Emit(OpCodes.Castclass, typeof(string)); // stack is now [target][target][string]
                                CommonIL.StoreLocal(il, enumDeclareLocal); // stack is now [target][target]
                                il.Emit(OpCodes.Ldtoken, unboxType); // stack is now [target][target][enum-type-token]
                                il.EmitCall(OpCodes.Call, typeof(Type).GetMethod(nameof(Type.GetTypeFromHandle)), null);// stack is now [target][target][enum-type]
                                CommonIL.LoadLocal(il, enumDeclareLocal); // stack is now [target][target][enum-type][string]
                                il.Emit(OpCodes.Ldc_I4_1); // stack is now [target][target][enum-type][string][true]
                                il.EmitCall(OpCodes.Call, XConfig.EnumParse, null); // stack is now [target][target][enum-as-object]
                                il.Emit(OpCodes.Unbox_Any, unboxType); // stack is now [target][target][typed-value]
                            }
                            else
                            {
                                CommonIL.FlexibleConvertBoxedFromHeadOfStack(il, colType, unboxType, numericType);
                            }

                            if (nullUnderlyingType != null)
                            {
                                il.Emit(OpCodes.Newobj, memberType.GetConstructor(new[] { nullUnderlyingType })); // stack is now [target][target][typed-value]
                            }
                        }
                        else if (memberType.FullName == XConfig.TC.LinqBinary)
                        {
                            il.Emit(OpCodes.Unbox_Any, typeof(byte[])); // stack is now [target][target][byte-array]
                            il.Emit(OpCodes.Newobj, memberType.GetConstructor(new Type[] { typeof(byte[]) }));// stack is now [target][target][binary]
                        }
                        else
                        {
                            TypeCode dataTypeCode = Type.GetTypeCode(colType), unboxTypeCode = Type.GetTypeCode(unboxType);
                            if (colType == unboxType || dataTypeCode == unboxTypeCode || dataTypeCode == Type.GetTypeCode(nullUnderlyingType))
                            {
                                il.Emit(OpCodes.Unbox_Any, unboxType); // stack is now [target][target][typed-value]
                            }
                            else
                            {
                                // not a direct match; need to tweak the unbox
                                CommonIL.FlexibleConvertBoxedFromHeadOfStack(il, colType, nullUnderlyingType ?? unboxType, null);
                                if (nullUnderlyingType != null)
                                {
                                    il.Emit(OpCodes.Newobj, unboxType.GetConstructor(new[] { nullUnderlyingType })); // stack is now [target][target][typed-value]
                                }
                            }
                        }
                    }
                    // Store the value in the property/field
                    if (item.Property != null)
                    {
                        il.Emit(OpCodes.Callvirt, RowMap.GetPropertySetter(item.Property, mType));
                    }
                    else
                    {
                        il.Emit(OpCodes.Stfld, item.Field); // stack is now [target]
                    }

                    il.Emit(OpCodes.Br_S, finishLabel); // stack is now [target]
                    il.MarkLabel(isDbNullLabel); // incoming stack: [target][target][value]
                    il.Emit(OpCodes.Pop); // stack is now [target][target]
                    il.Emit(OpCodes.Pop); // stack is now [target]
                    il.MarkLabel(finishLabel);
                }
                //first = false;
                index++;
            }

            il.Emit(OpCodes.Stloc_1); // stack is empty
            il.MarkLabel(allDone);
            il.BeginCatchBlock(typeof(Exception)); // stack is Exception
            il.Emit(OpCodes.Ldloc_0); // stack is Exception, index
            il.Emit(OpCodes.Ldarg_0); // stack is Exception, index, reader
            CommonIL.LoadLocal(il, valueCopyLocal); // stack is Exception, index, reader, value
            il.EmitCall(OpCodes.Call, typeof(CommonIL).GetMethod(nameof(CommonIL.ThrowDataException)), null);
            il.EndExceptionBlock();

            il.Emit(OpCodes.Ldloc_1); // stack is [rval]
            il.Emit(OpCodes.Ret);

            var funcType = Expression.GetFuncType(typeof(IDataReader), mType);
            return (Func<IDataReader, object>)dm.CreateDelegate(funcType);
        }

        internal static Row Row(IDataReader reader, Type mType)
        {
            return new Row
            {
                Handle = SetFunc(reader, mType)
            };
        }
    }
}
