
using EasyDAL.Exchange.Cache;
using EasyDAL.Exchange.DataBase;
using EasyDAL.Exchange.DynamicParameter;
using EasyDAL.Exchange.Extensions;
using EasyDAL.Exchange.Handler;
using EasyDAL.Exchange.Map;
using EasyDAL.Exchange.MapperX;
using EasyDAL.Exchange.Parameter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.Xml.Linq;


namespace EasyDAL.Exchange.AdoNet
{
    /// <summary>
    /// Dapper, a light weight object mapper for ADO.NET
    /// </summary>
    internal static partial class SqlMapper
    {

        /// <summary>
        /// Gets type-map for the given type
        /// </summary>
        /// <returns>Type map instance, default is to create new instance of DefaultTypeMap</returns>
        public static Func<Type, ITypeMap> TypeMapProvider { get; } = (Type type) => new DefaultTypeMap(type);

        internal static int GetColumnHash(IDataReader reader, int startBound = 0, int length = -1)
        {
            unchecked
            {
                int max = length < 0 ? reader.FieldCount : startBound + length;
                int hash = (-37 * startBound) + max;
                for (int i = startBound; i < max; i++)
                {
                    object tmp = reader.GetName(i);
                    hash = (-79 * ((hash * 31) + (tmp?.GetHashCode() ?? 0))) + (reader.GetFieldType(i)?.GetHashCode() ?? 0);
                }
                return hash;
            }
        }

        private static Dictionary<Type, DbType> typeMap { get; set; }

        static SqlMapper()
        {
            typeMap = new Dictionary<Type, DbType>
            {
                [typeof(byte)] = DbType.Byte,
                [typeof(sbyte)] = DbType.SByte,
                [typeof(short)] = DbType.Int16,
                [typeof(ushort)] = DbType.UInt16,
                [typeof(int)] = DbType.Int32,
                [typeof(uint)] = DbType.UInt32,
                [typeof(long)] = DbType.Int64,
                [typeof(ulong)] = DbType.UInt64,
                [typeof(float)] = DbType.Single,
                [typeof(double)] = DbType.Double,
                [typeof(decimal)] = DbType.Decimal,
                [typeof(bool)] = DbType.Boolean,
                [typeof(string)] = DbType.String,
                [typeof(char)] = DbType.StringFixedLength,
                [typeof(Guid)] = DbType.Guid,
                [typeof(DateTime)] = DbType.DateTime,
                [typeof(DateTimeOffset)] = DbType.DateTimeOffset,
                [typeof(TimeSpan)] = DbType.Time,
                [typeof(byte[])] = DbType.Binary,
                [typeof(byte?)] = DbType.Byte,
                [typeof(sbyte?)] = DbType.SByte,
                [typeof(short?)] = DbType.Int16,
                [typeof(ushort?)] = DbType.UInt16,
                [typeof(int?)] = DbType.Int32,
                [typeof(uint?)] = DbType.UInt32,
                [typeof(long?)] = DbType.Int64,
                [typeof(ulong?)] = DbType.UInt64,
                [typeof(float?)] = DbType.Single,
                [typeof(double?)] = DbType.Double,
                [typeof(decimal?)] = DbType.Decimal,
                [typeof(bool?)] = DbType.Boolean,
                [typeof(char?)] = DbType.StringFixedLength,
                [typeof(Guid?)] = DbType.Guid,
                [typeof(DateTime?)] = DbType.DateTime,
                [typeof(DateTimeOffset?)] = DbType.DateTimeOffset,
                [typeof(TimeSpan?)] = DbType.Time,
                [typeof(object)] = DbType.Object
            };
            ResetTypeHandlers(false);
        }

        private static void ResetTypeHandlers(bool clone)
        {
            typeHandlers = new Dictionary<Type, ITypeHandler>();
            //AddTypeHandlerImpl(typeof(DataTable), new DataTableHandler(), clone);
            //try
            //{
            //    AddSqlDataRecordsTypeHandler(clone);
            //}
            //catch { /* https://github.com/StackExchange/dapper-dot-net/issues/424 */ }
            AddTypeHandlerImpl(typeof(XmlDocument), new XmlDocumentHandler(), clone);
            AddTypeHandlerImpl(typeof(XDocument), new XDocumentHandler(), clone);
            AddTypeHandlerImpl(typeof(XElement), new XElementHandler(), clone);
        }

        internal static bool HasTypeHandler(Type type) => typeHandlers.ContainsKey(type);

        /// <summary>
        /// Configure the specified type to be processed by a custom handler.
        /// </summary>
        /// <param name="type">The type to handle.</param>
        /// <param name="handler">The handler to process the <paramref name="type"/>.</param>
        /// <param name="clone">Whether to clone the current type handler map.</param>
        public static void AddTypeHandlerImpl(Type type, ITypeHandler handler, bool clone)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            Type secondary = null;
            if (type.IsValueTypeX())
            {
                var underlying = Nullable.GetUnderlyingType(type);
                if (underlying == null)
                {
                    secondary = typeof(Nullable<>).MakeGenericType(type); // the Nullable<T>
                    // type is already the T
                }
                else
                {
                    secondary = type; // the Nullable<T>
                    type = underlying; // the T
                }
            }

            var snapshot = typeHandlers;
            if (snapshot.TryGetValue(type, out ITypeHandler oldValue) && handler == oldValue) return; // nothing to do

            var newCopy = clone ? new Dictionary<Type, ITypeHandler>(snapshot) : snapshot;

#pragma warning disable 618
            typeof(TypeHandlerCache<>).MakeGenericType(type).GetMethod(nameof(TypeHandlerCache<int>.SetHandler), BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, new object[] { handler });
            if (secondary != null)
            {
                typeof(TypeHandlerCache<>).MakeGenericType(secondary).GetMethod(nameof(TypeHandlerCache<int>.SetHandler), BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, new object[] { handler });
            }
#pragma warning restore 618
            if (handler == null)
            {
                newCopy.Remove(type);
                if (secondary != null) newCopy.Remove(secondary);
            }
            else
            {
                newCopy[type] = handler;
                if (secondary != null) newCopy[secondary] = handler;
            }
            typeHandlers = newCopy;
        }

        private static Dictionary<Type, ITypeHandler> typeHandlers { get; set; }

        /// <summary>
        /// OBSOLETE: For internal usage only. Lookup the DbType and handler for a given Type and member
        /// </summary>
        /// <param name="type">The type to lookup.</param>
        /// <param name="name">The name (for error messages).</param>
        /// <param name="demand">Whether to demand a value (throw if missing).</param>
        /// <param name="handler">The handler for <paramref name="type"/>.</param>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static DbType LookupDbType(Type type, string name, bool demand, out ITypeHandler handler)
        {
            handler = null;
            var nullUnderlyingType = Nullable.GetUnderlyingType(type);
            if (nullUnderlyingType != null)
            {
                type = nullUnderlyingType;
            }
            if (type.IsEnumX() && !typeMap.ContainsKey(type))
            {
                type = Enum.GetUnderlyingType(type);
            }
            if (typeMap.TryGetValue(type, out DbType dbType))
            {
                return dbType;
            }
            if (type.FullName == Settings.LinqBinary)
            {
                return DbType.Binary;
            }
            if (typeHandlers.TryGetValue(type, out handler))
            {
                return DbType.Object;
            }
            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                return DynamicParameters.EnumerableMultiParameter;
            }

#if !NETSTANDARD1_3 && !NETSTANDARD2_0
            switch (type.FullName)
            {
                case "Microsoft.SqlServer.Types.SqlGeography":
                    AddTypeHandler(type, handler = new UdtTypeHandler("geography"));
                    return DbType.Object;
                case "Microsoft.SqlServer.Types.SqlGeometry":
                    AddTypeHandler(type, handler = new UdtTypeHandler("geometry"));
                    return DbType.Object;
                case "Microsoft.SqlServer.Types.SqlHierarchyId":
                    AddTypeHandler(type, handler = new UdtTypeHandler("hierarchyid"));
                    return DbType.Object;
            }
#endif
            if (demand)
                throw new NotSupportedException($"The member {name} of type {type.FullName} cannot be used as a parameter value");
            return DbType.Object;
        }

        private static int[] ErrTwoRows { get; } = new int[2];
        private static int[] ErrZeroRows { get; } = new int[0];
        internal static void ThrowMultipleRows(Row row)
        {
            switch (row)
            {  // get the standard exception from the runtime
                case Row.Single: ErrTwoRows.Single(); break;
                case Row.SingleOrDefault: ErrTwoRows.SingleOrDefault(); break;
                default: throw new InvalidOperationException();
            }
        }

        internal static void ThrowZeroRows(Row row)
        {
            switch (row)
            { // get the standard exception from the runtime
                case Row.First: ErrZeroRows.First(); break;
                case Row.Single: ErrZeroRows.Single(); break;
                default: throw new InvalidOperationException();
            }
        }

        private static CommandBehavior GetBehavior(bool close, CommandBehavior @default)
        {
            return (close ? (@default | CommandBehavior.CloseConnection) : @default) & Settings.AllowedCommandBehaviors;
        }

        internal static CacheInfo GetCacheInfo(Identity identity)
        {
            var info = new CacheInfo();
            Action<IDbCommand, DynamicParameters> reader = (cmd, paras) => paras.AddParameters(cmd, identity);
            info.ParamReader = reader;

            return info;
        }

        private static Func<IDataReader, object> GetHandlerDeserializer(ITypeHandler handler, Type type, int startBound)
        {
            return reader => handler.Parse(type, reader.GetValue(startBound));
        }

        private static Exception MultiMapException(IDataRecord reader)
        {
            bool hasFields = false;
            try { hasFields = reader != null && reader.FieldCount != 0; }
            catch { /* don't throw when trying to throw */ }
            if (hasFields)
                return new ArgumentException("When using the multi-mapping APIs ensure you set the splitOn param if you have keys other than Id", "splitOn");
            else
                return new InvalidOperationException("No columns were selected");
        }

        internal static Func<IDataReader, object> GetDapperRowDeserializer(IDataRecord reader, int startBound, int length, bool returnNullIfFirstMissing)
        {
            var fieldCount = reader.FieldCount;
            if (length == -1)
            {
                length = fieldCount - startBound;
            }

            if (fieldCount <= startBound)
            {
                throw MultiMapException(reader);
            }

            var effectiveFieldCount = Math.Min(fieldCount - startBound, length);

            DapperTable table = null;

            return
                r =>
                {
                    if (table == null)
                    {
                        string[] names = new string[effectiveFieldCount];
                        for (int i = 0; i < effectiveFieldCount; i++)
                        {
                            names[i] = r.GetName(i + startBound);
                        }
                        table = new DapperTable(names);
                    }

                    var values = new object[effectiveFieldCount];

                    if (returnNullIfFirstMissing)
                    {
                        values[0] = r.GetValue(startBound);
                        if (values[0] is DBNull)
                        {
                            return null;
                        }
                    }

                    if (startBound == 0)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            object val = r.GetValue(i);
                            values[i] = val is DBNull ? null : val;
                        }
                    }
                    else
                    {
                        var begin = returnNullIfFirstMissing ? 1 : 0;
                        for (var iter = begin; iter < effectiveFieldCount; ++iter)
                        {
                            object obj = r.GetValue(iter + startBound);
                            values[iter] = obj is DBNull ? null : obj;
                        }
                    }
                    return new DapperRow(table, values);
                };
        }
        /// <summary>
        /// Internal use only.
        /// </summary>
        /// <param name="value">The object to convert to a character.</param>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static char ReadChar(object value)
        {
            if (value == null || value is DBNull) throw new ArgumentNullException(nameof(value));
            var s = value as string;
            if (s == null || s.Length != 1) throw new ArgumentException("A single-character was expected", nameof(value));
            return s[0];
        }

        /// <summary>
        /// Internal use only.
        /// </summary>
        /// <param name="value">The object to convert to a character.</param>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static char? ReadNullableChar(object value)
        {
            if (value == null || value is DBNull) return null;
            var s = value as string;
            if (s == null || s.Length != 1) throw new ArgumentException("A single-character was expected", nameof(value));
            return s[0];
        }

        /// <summary>
        /// OBSOLETE: For internal usage only. Sanitizes the paramter value with proper type casting.
        /// </summary>
        /// <param name="value">The value to sanitize.</param>
        public static object SanitizeParameterValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }

            return value;
        }
        
        private static bool IsValueTuple(Type type) => type?.IsValueTypeX() == true && type.FullName.StartsWith("System.ValueTuple`", StringComparison.Ordinal);

        private static List<IMemberMap> GetValueTupleMembers(Type type, string[] names)
        {
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            var result = new List<IMemberMap>(names.Length);
            for (int i = 0; i < names.Length; i++)
            {
                FieldInfo field = null;
                string name = "Item" + (i + 1).ToString(CultureInfo.InvariantCulture);
                foreach (var test in fields)
                {
                    if (test.Name == name)
                    {
                        field = test;
                        break;
                    }
                }
                result.Add(field == null ? null : new SimpleMemberMap(string.IsNullOrWhiteSpace(names[i]) ? name : names[i], field));
            }
            return result;
        }

        private static readonly MethodInfo StringReplace = typeof(string).GetPublicInstanceMethodX(nameof(string.Replace), new Type[] { typeof(string), typeof(string) }),
            InvariantCulture = typeof(CultureInfo).GetProperty(nameof(CultureInfo.InvariantCulture), BindingFlags.Public | BindingFlags.Static).GetGetMethod();

        private static Func<IDataReader, object> GetStructDeserializer(Type type, Type effectiveType, int index)
        {
            // no point using special per-type handling here; it boils down to the same, plus not all are supported anyway (see: SqlDataReader.GetChar - not supported!)
#pragma warning disable 618
            if (type == typeof(char))
            { // this *does* need special handling, though
                return r => ReadChar(r.GetValue(index));
            }
            if (type == typeof(char?))
            {
                return r => ReadNullableChar(r.GetValue(index));
            }
            if (type.FullName == Settings.LinqBinary)
            {
                return r => Activator.CreateInstance(type, r.GetValue(index));
            }
#pragma warning restore 618

            if (effectiveType.IsEnumX())
            {   // assume the value is returned as the correct type (int/byte/etc), but box back to the typed enum
                return r =>
                {
                    var val = r.GetValue(index);
                    if (val is float || val is double || val is decimal)
                    {
                        val = Convert.ChangeType(val, Enum.GetUnderlyingType(effectiveType), CultureInfo.InvariantCulture);
                    }
                    return val is DBNull ? null : Enum.ToObject(effectiveType, val);
                };
            }
            if (typeHandlers.TryGetValue(type, out ITypeHandler handler))
            {
                return r =>
                {
                    var val = r.GetValue(index);
                    return val is DBNull ? null : handler.Parse(type, val);
                };
            }
            return r =>
            {
                var val = r.GetValue(index);
                return val is DBNull ? null : val;
            };
        }

        private static T Parse<T>(object value)
        {
            if (value == null || value is DBNull) return default(T);
            if (value is T) return (T)value;
            var type = typeof(T);
            type = Nullable.GetUnderlyingType(type) ?? type;
            if (type.IsEnumX())
            {
                if (value is float || value is double || value is decimal)
                {
                    value = Convert.ChangeType(value, Enum.GetUnderlyingType(type), CultureInfo.InvariantCulture);
                }
                return (T)Enum.ToObject(type, value);
            }
            if (typeHandlers.TryGetValue(type, out ITypeHandler handler))
            {
                return (T)handler.Parse(type, value);
            }
            return (T)Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Gets type-map for the given <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type to get a map for.</param>
        /// <returns>Type map implementation, DefaultTypeMap instance if no override present</returns>
        public static ITypeMap GetTypeMap(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            var map = (ITypeMap)_typeMaps[type];
            if (map == null)
            {
                lock (_typeMaps)
                {   // double-checked; store this to avoid reflection next time we see this type
                    // since multiple queries commonly use the same domain-entity/DTO/view-model type
                    map = (ITypeMap)_typeMaps[type];

                    if (map == null)
                    {
                        map = TypeMapProvider(type);
                        _typeMaps[type] = map;
                    }
                }
            }
            return map;
        }

        // use Hashtable to get free lockless reading
        private static Hashtable _typeMaps { get; } = new Hashtable();

        /// <summary>
        /// Internal use only
        /// </summary>
        /// <param name="type"></param>
        /// <param name="reader"></param>
        /// <param name="startBound"></param>
        /// <param name="length"></param>
        /// <param name="returnNullIfFirstMissing"></param>
        /// <returns></returns>
        public static Func<IDataReader, object> GetTypeDeserializer(
            Type type, IDataReader reader, int startBound = 0, int length = -1, bool returnNullIfFirstMissing = false
        )
        {
            return TypeDeserializerCache.GetReader(type, reader, startBound, length, returnNullIfFirstMissing);
        }

        private static LocalBuilder GetTempLocal(ILGenerator il, ref Dictionary<Type, LocalBuilder> locals, Type type, bool initAndLoad)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            locals = locals ?? new Dictionary<Type, LocalBuilder>();
            if (!locals.TryGetValue(type, out LocalBuilder found))
            {
                found = il.DeclareLocal(type);
                locals.Add(type, found);
            }
            if (initAndLoad)
            {
                il.Emit(OpCodes.Ldloca, (short)found.LocalIndex);
                il.Emit(OpCodes.Initobj, type);
                il.Emit(OpCodes.Ldloca, (short)found.LocalIndex);
                il.Emit(OpCodes.Ldobj, type);
            }
            return found;
        }

        internal static Func<IDataReader, object> GetTypeDeserializerImpl(
            Type type, IDataReader reader, int startBound = 0, int length = -1, bool returnNullIfFirstMissing = false
        )
        {
            var returnType = type.IsValueTypeX() ? typeof(object) : type;
            var dm = new DynamicMethod("Deserialize" + Guid.NewGuid().ToString(), returnType, new[] { typeof(IDataReader) }, type, true);
            var il = dm.GetILGenerator();
            il.DeclareLocal(typeof(int));
            il.DeclareLocal(type);
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Stloc_0);

            if (length == -1)
            {
                length = reader.FieldCount - startBound;
            }

            if (reader.FieldCount <= startBound)
            {
                throw MultiMapException(reader);
            }

            var names = Enumerable.Range(startBound, length).Select(i => reader.GetName(i)).ToArray();

            ITypeMap typeMap = GetTypeMap(type);

            int index = startBound;
            ConstructorInfo specializedConstructor = null;

            bool supportInitialize = false;
            Dictionary<Type, LocalBuilder> structLocals = null;
            if (type.IsValueTypeX())
            {
                il.Emit(OpCodes.Ldloca_S, (byte)1);
                il.Emit(OpCodes.Initobj, type);
            }
            else
            {
                var types = new Type[length];
                for (int i = startBound; i < startBound + length; i++)
                {
                    types[i - startBound] = reader.GetFieldType(i);
                }

                var ctor = typeMap.FindConstructor(names, types);
                if (ctor == null)
                {
                    string proposedTypes = "(" + string.Join(", ", types.Select((t, i) => t.FullName + " " + names[i]).ToArray()) + ")";
                    throw new InvalidOperationException($"A parameterless default constructor or one matching signature {proposedTypes} is required for {type.FullName} materialization");
                }

                if (ctor.GetParameters().Length == 0)
                {
                    il.Emit(OpCodes.Newobj, ctor);
                    il.Emit(OpCodes.Stloc_1);
                    supportInitialize = typeof(ISupportInitialize).IsAssignableFrom(type);
                    if (supportInitialize)
                    {
                        il.Emit(OpCodes.Ldloc_1);
                        il.EmitCall(OpCodes.Callvirt, typeof(ISupportInitialize).GetMethod(nameof(ISupportInitialize.BeginInit)), null);
                    }
                }
                else
                {
                    specializedConstructor = ctor;
                }

            }

            il.BeginExceptionBlock();
            if (type.IsValueTypeX())
            {
                il.Emit(OpCodes.Ldloca_S, (byte)1);// [target]
            }
            else if (specializedConstructor == null)
            {
                il.Emit(OpCodes.Ldloc_1);// [target]
            }

            var members = IsValueTuple(type) ? GetValueTupleMembers(type, names) : ((specializedConstructor != null
                ? names.Select(n => typeMap.GetConstructorParameter(specializedConstructor, n))
                : names.Select(n => typeMap.GetMember(n))).ToList());

            // stack is now [target]

            bool first = true;
            var allDone = il.DefineLabel();
            int enumDeclareLocal = -1, valueCopyLocal = il.DeclareLocal(typeof(object)).LocalIndex;
            bool applyNullSetting = Settings.ApplyNullValues;
            foreach (var item in members)
            {
                if (item != null)
                {
                    if (specializedConstructor == null)
                        il.Emit(OpCodes.Dup); // stack is now [target][target]
                    Label isDbNullLabel = il.DefineLabel();
                    Label finishLabel = il.DefineLabel();

                    il.Emit(OpCodes.Ldarg_0); // stack is now [target][target][reader]
                    EmitInt32(il, index); // stack is now [target][target][reader][index]
                    il.Emit(OpCodes.Dup);// stack is now [target][target][reader][index][index]
                    il.Emit(OpCodes.Stloc_0);// stack is now [target][target][reader][index]
                    il.Emit(OpCodes.Callvirt, Settings.GetItem); // stack is now [target][target][value-as-object]
                    il.Emit(OpCodes.Dup); // stack is now [target][target][value-as-object][value-as-object]
                    StoreLocal(il, valueCopyLocal);
                    Type colType = reader.GetFieldType(index);
                    Type memberType = item.MemberType;

                    if (memberType == typeof(char) || memberType == typeof(char?))
                    {
                        il.EmitCall(OpCodes.Call, typeof(SqlMapper).GetMethod(
                            memberType == typeof(char) ? nameof(SqlMapper.ReadChar) : nameof(SqlMapper.ReadNullableChar), BindingFlags.Static | BindingFlags.Public), null); // stack is now [target][target][typed-value]
                    }
                    else
                    {
                        il.Emit(OpCodes.Dup); // stack is now [target][target][value][value]
                        il.Emit(OpCodes.Isinst, typeof(DBNull)); // stack is now [target][target][value-as-object][DBNull or null]
                        il.Emit(OpCodes.Brtrue_S, isDbNullLabel); // stack is now [target][target][value-as-object]

                        // unbox nullable enums as the primitive, i.e. byte etc

                        var nullUnderlyingType = Nullable.GetUnderlyingType(memberType);
                        var unboxType = nullUnderlyingType?.IsEnumX() == true ? nullUnderlyingType : memberType;

                        if (unboxType.IsEnumX())
                        {
                            Type numericType = Enum.GetUnderlyingType(unboxType);
                            if (colType == typeof(string))
                            {
                                if (enumDeclareLocal == -1)
                                {
                                    enumDeclareLocal = il.DeclareLocal(typeof(string)).LocalIndex;
                                }
                                il.Emit(OpCodes.Castclass, typeof(string)); // stack is now [target][target][string]
                                StoreLocal(il, enumDeclareLocal); // stack is now [target][target]
                                il.Emit(OpCodes.Ldtoken, unboxType); // stack is now [target][target][enum-type-token]
                                il.EmitCall(OpCodes.Call, typeof(Type).GetMethod(nameof(Type.GetTypeFromHandle)), null);// stack is now [target][target][enum-type]
                                LoadLocal(il, enumDeclareLocal); // stack is now [target][target][enum-type][string]
                                il.Emit(OpCodes.Ldc_I4_1); // stack is now [target][target][enum-type][string][true]
                                il.EmitCall(OpCodes.Call, Settings.EnumParse, null); // stack is now [target][target][enum-as-object]
                                il.Emit(OpCodes.Unbox_Any, unboxType); // stack is now [target][target][typed-value]
                            }
                            else
                            {
                                FlexibleConvertBoxedFromHeadOfStack(il, colType, unboxType, numericType);
                            }

                            if (nullUnderlyingType != null)
                            {
                                il.Emit(OpCodes.Newobj, memberType.GetConstructor(new[] { nullUnderlyingType })); // stack is now [target][target][typed-value]
                            }
                        }
                        else if (memberType.FullName == Settings.LinqBinary)
                        {
                            il.Emit(OpCodes.Unbox_Any, typeof(byte[])); // stack is now [target][target][byte-array]
                            il.Emit(OpCodes.Newobj, memberType.GetConstructor(new Type[] { typeof(byte[]) }));// stack is now [target][target][binary]
                        }
                        else
                        {
                            TypeCode dataTypeCode = TypeExtensionsX.GetTypeCodeX(colType), unboxTypeCode = TypeExtensionsX.GetTypeCodeX(unboxType);
                            bool hasTypeHandler;
                            if ((hasTypeHandler = typeHandlers.ContainsKey(unboxType)) || colType == unboxType || dataTypeCode == unboxTypeCode || dataTypeCode == TypeExtensionsX.GetTypeCodeX(nullUnderlyingType))
                            {
                                if (hasTypeHandler)
                                {
#pragma warning disable 618
                                    il.EmitCall(OpCodes.Call, typeof(TypeHandlerCache<>).MakeGenericType(unboxType).GetMethod(nameof(TypeHandlerCache<int>.Parse)), null); // stack is now [target][target][typed-value]
#pragma warning restore 618
                                }
                                else
                                {
                                    il.Emit(OpCodes.Unbox_Any, unboxType); // stack is now [target][target][typed-value]
                                }
                            }
                            else
                            {
                                // not a direct match; need to tweak the unbox
                                FlexibleConvertBoxedFromHeadOfStack(il, colType, nullUnderlyingType ?? unboxType, null);
                                if (nullUnderlyingType != null)
                                {
                                    il.Emit(OpCodes.Newobj, unboxType.GetConstructor(new[] { nullUnderlyingType })); // stack is now [target][target][typed-value]
                                }
                            }
                        }
                    }
                    if (specializedConstructor == null)
                    {
                        // Store the value in the property/field
                        if (item.Property != null)
                        {
                            il.Emit(type.IsValueTypeX() ? OpCodes.Call : OpCodes.Callvirt, DefaultTypeMap.GetPropertySetter(item.Property, type));
                        }
                        else
                        {
                            il.Emit(OpCodes.Stfld, item.Field); // stack is now [target]
                        }
                    }

                    il.Emit(OpCodes.Br_S, finishLabel); // stack is now [target]

                    il.MarkLabel(isDbNullLabel); // incoming stack: [target][target][value]
                    if (specializedConstructor != null)
                    {
                        il.Emit(OpCodes.Pop);
                        if (item.MemberType.IsValueTypeX())
                        {
                            int localIndex = il.DeclareLocal(item.MemberType).LocalIndex;
                            LoadLocalAddress(il, localIndex);
                            il.Emit(OpCodes.Initobj, item.MemberType);
                            LoadLocal(il, localIndex);
                        }
                        else
                        {
                            il.Emit(OpCodes.Ldnull);
                        }
                    }
                    else if (applyNullSetting && (!memberType.IsValueTypeX() || Nullable.GetUnderlyingType(memberType) != null))
                    {
                        il.Emit(OpCodes.Pop); // stack is now [target][target]
                        // can load a null with this value
                        if (memberType.IsValueTypeX())
                        { // must be Nullable<T> for some T
                            GetTempLocal(il, ref structLocals, memberType, true); // stack is now [target][target][null]
                        }
                        else
                        { // regular reference-type
                            il.Emit(OpCodes.Ldnull); // stack is now [target][target][null]
                        }

                        // Store the value in the property/field
                        if (item.Property != null)
                        {
                            il.Emit(type.IsValueTypeX() ? OpCodes.Call : OpCodes.Callvirt, DefaultTypeMap.GetPropertySetter(item.Property, type));
                            // stack is now [target]
                        }
                        else
                        {
                            il.Emit(OpCodes.Stfld, item.Field); // stack is now [target]
                        }
                    }
                    else
                    {
                        il.Emit(OpCodes.Pop); // stack is now [target][target]
                        il.Emit(OpCodes.Pop); // stack is now [target]
                    }

                    if (first && returnNullIfFirstMissing)
                    {
                        il.Emit(OpCodes.Pop);
                        il.Emit(OpCodes.Ldnull); // stack is now [null]
                        il.Emit(OpCodes.Stloc_1);
                        il.Emit(OpCodes.Br, allDone);
                    }

                    il.MarkLabel(finishLabel);
                }
                first = false;
                index++;
            }
            if (type.IsValueTypeX())
            {
                il.Emit(OpCodes.Pop);
            }
            else
            {
                if (specializedConstructor != null)
                {
                    il.Emit(OpCodes.Newobj, specializedConstructor);
                }
                il.Emit(OpCodes.Stloc_1); // stack is empty
                if (supportInitialize)
                {
                    il.Emit(OpCodes.Ldloc_1);
                    il.EmitCall(OpCodes.Callvirt, typeof(ISupportInitialize).GetMethod(nameof(ISupportInitialize.EndInit)), null);
                }
            }
            il.MarkLabel(allDone);
            il.BeginCatchBlock(typeof(Exception)); // stack is Exception
            il.Emit(OpCodes.Ldloc_0); // stack is Exception, index
            il.Emit(OpCodes.Ldarg_0); // stack is Exception, index, reader
            LoadLocal(il, valueCopyLocal); // stack is Exception, index, reader, value
            il.EmitCall(OpCodes.Call, typeof(SqlMapper).GetMethod(nameof(SqlMapper.ThrowDataException)), null);
            il.EndExceptionBlock();

            il.Emit(OpCodes.Ldloc_1); // stack is [rval]
            if (type.IsValueTypeX())
            {
                il.Emit(OpCodes.Box, type);
            }
            il.Emit(OpCodes.Ret);

            var funcType = System.Linq.Expressions.Expression.GetFuncType(typeof(IDataReader), returnType);
            return (Func<IDataReader, object>)dm.CreateDelegate(funcType);
        }

        private static void FlexibleConvertBoxedFromHeadOfStack(ILGenerator il, Type from, Type to, Type via)
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
                switch (TypeExtensionsX.GetTypeCodeX(from))
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
                        switch (TypeExtensionsX.GetTypeCodeX(via ?? to))
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

        private static MethodInfo GetOperator(Type from, Type to)
        {
            if (to == null) return null;
            MethodInfo[] fromMethods, toMethods;
            return ResolveOperator(fromMethods = from.GetMethods(BindingFlags.Static | BindingFlags.Public), from, to, "op_Implicit")
                ?? ResolveOperator(toMethods = to.GetMethods(BindingFlags.Static | BindingFlags.Public), from, to, "op_Implicit")
                ?? ResolveOperator(fromMethods, from, to, "op_Explicit")
                ?? ResolveOperator(toMethods, from, to, "op_Explicit");
        }

        private static MethodInfo ResolveOperator(MethodInfo[] methods, Type from, Type to, string name)
        {
            for (int i = 0; i < methods.Length; i++)
            {
                if (methods[i].Name != name || methods[i].ReturnType != to) continue;
                var args = methods[i].GetParameters();
                if (args.Length != 1 || args[0].ParameterType != from) continue;
                return methods[i];
            }
            return null;
        }

        private static void LoadLocal(ILGenerator il, int index)
        {
            if (index < 0 || index >= short.MaxValue) throw new ArgumentNullException(nameof(index));
            switch (index)
            {
                case 0: il.Emit(OpCodes.Ldloc_0); break;
                case 1: il.Emit(OpCodes.Ldloc_1); break;
                case 2: il.Emit(OpCodes.Ldloc_2); break;
                case 3: il.Emit(OpCodes.Ldloc_3); break;
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

        private static void LoadLocalAddress(ILGenerator il, int index)
        {
            if (index < 0 || index >= short.MaxValue) throw new ArgumentNullException(nameof(index));

            if (index <= 255)
            {
                il.Emit(OpCodes.Ldloca_S, (byte)index);
            }
            else
            {
                il.Emit(OpCodes.Ldloca, (short)index);
            }
        }

        /// <summary>
        /// Throws a data exception, only used internally
        /// </summary>
        /// <param name="ex">The exception to throw.</param>
        /// <param name="index">The index the exception occured at.</param>
        /// <param name="reader">The reader the exception occured in.</param>
        /// <param name="value">The value that caused the exception.</param>
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
                            formattedValue = Convert.ToString(value) + " - " + TypeExtensionsX.GetTypeCodeX(value.GetType());
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

        private static void EmitInt32(ILGenerator il, int value)
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

        /// <summary>
        /// Key used to indicate the type name associated with a DataTable.
        /// </summary>
        private static string DataTableTypeNameKey { get; } = "dapper:TypeName";

        /// <summary>
        /// Fetch the type name associated with a <see cref="DataTable"/>.
        /// </summary>
        /// <param name="table">The <see cref="DataTable"/> that has a type name associated with it.</param>
        public static string GetTypeName(this DataTable table) =>
            table?.ExtendedProperties[DataTableTypeNameKey] as string;

        // one per thread
        [ThreadStatic]
        private static StringBuilder perThreadStringBuilderCache;
        internal static StringBuilder GetStringBuilder()
        {
            var tmp = perThreadStringBuilderCache;
            if (tmp != null)
            {
                perThreadStringBuilderCache = null;
                tmp.Length = 0;
                return tmp;
            }
            return new StringBuilder();
        }

        internal static string __ToStringRecycle(this StringBuilder obj)
        {
            if (obj == null) return "";
            var s = obj.ToString();
            perThreadStringBuilderCache = perThreadStringBuilderCache ?? obj;
            return s;
        }

        internal static Func<IDataReader, object> GetDeserializer(Type type, IDataReader reader, int startBound, int length, bool returnNullIfFirstMissing)
        {
            // dynamic is passed in as Object ... by c# design
            if (type == typeof(object) || type == typeof(DapperRow))
            {
                return GetDapperRowDeserializer(reader, startBound, length, returnNullIfFirstMissing);
            }
            Type underlyingType = null;
            if (!(typeMap.ContainsKey(type) || type.IsEnumX() || type.FullName == Settings.LinqBinary
                || (type.IsValueTypeX() && (underlyingType = Nullable.GetUnderlyingType(type)) != null && underlyingType.IsEnumX())))
            {
                if (typeHandlers.TryGetValue(type, out ITypeHandler handler))
                {
                    return GetHandlerDeserializer(handler, type, startBound);
                }
                return GetTypeDeserializer(type, reader, startBound, length, returnNullIfFirstMissing);
            }
            return GetStructDeserializer(type, underlyingType ?? type, startBound);
        }

        /******************************************************************************************/



    }
}
