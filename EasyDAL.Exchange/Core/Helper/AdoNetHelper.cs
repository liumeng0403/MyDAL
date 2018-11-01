using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Yunyong.DataExchange.AdoNet;
using Yunyong.DataExchange.Cache;
using Yunyong.DataExchange.Core.Enums;

namespace Yunyong.DataExchange.Core.Helper
{
    internal static class AdoNetHelper
    {

        internal static int GetColumnHash(IDataReader reader)
        {
            unchecked
            {
                int max = reader.FieldCount;
                int hash = max;
                for (int i = 0; i < max; i++)
                {
                    object tmp = reader.GetName(i);
                    hash = (-79 * ((hash * 31) + (tmp?.GetHashCode() ?? 0))) + (reader.GetFieldType(i)?.GetHashCode() ?? 0);
                }
                return hash;
            }
        }

        private static CommandBehavior DefaultAllowedCommandBehaviors { get; } = ~CommandBehavior.SingleResult;
        private static CommandBehavior AllowedCommandBehaviors { get; set; } = DefaultAllowedCommandBehaviors;

        private static void SetAllowedCommandBehaviors(CommandBehavior behavior, bool enabled)
        {
            if (enabled)
            {
                AllowedCommandBehaviors |= behavior;
            }
            else
            {
                AllowedCommandBehaviors &= ~behavior;
            }
        }
        internal static bool DisableCommandBehaviorOptimizations(CommandBehavior behavior, Exception ex)
        {
            if (AllowedCommandBehaviors == DefaultAllowedCommandBehaviors
                && (behavior & (CommandBehavior.SingleResult | CommandBehavior.SingleRow)) != 0)
            {
                if (ex.Message.Contains(nameof(CommandBehavior.SingleResult))
                    || ex.Message.Contains(nameof(CommandBehavior.SingleRow)))
                { // some providers just just allow these, so: try again without them and stop issuing them
                    SetAllowedCommandBehaviors(CommandBehavior.SingleResult | CommandBehavior.SingleRow, false);
                    return true;
                }
            }
            return false;
        }

        private static int[] ErrTwoRows { get; } = new int[2];
        private static int[] ErrZeroRows { get; } = new int[0];


        private static CommandBehavior GetBehavior(bool close, CommandBehavior @default)
        {
            return (close ? (@default | CommandBehavior.CloseConnection) : @default) & AllowedCommandBehaviors;
        }
        
        /// <summary>
        /// Internal use only
        /// </summary>
        /// <param name="type"></param>
        /// <param name="reader"></param>
        /// <param name="startBound"></param>
        /// <param name="length"></param>
        /// <param name="returnNullIfFirstMissing"></param>
        /// <returns></returns>
        private static Func<IDataReader, object> GetTypeDeserializer(Type type, IDataReader reader)
        {
            return TypeDeserializerCache.GetReader(type, reader);
        }
        
        /******************************************************************************************/

        internal static MethodInfo GetPropertySetter(PropertyInfo propertyInfo, Type mType)
        {
            if (propertyInfo.DeclaringType == mType)
            {
                return propertyInfo.GetSetMethod(true);
            }
            return propertyInfo
                .DeclaringType
                .GetProperty(propertyInfo.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, Type.DefaultBinder, propertyInfo.PropertyType, propertyInfo.GetIndexParameters().Select(p => p.ParameterType).ToArray(), null)
                .GetSetMethod(true);
        }

        /******************************************************************************************/

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
        public static RowMap GetTypeMap(Type mType)
        {
            if (!StaticCache.TypeMaps.TryGetValue(mType, out var map))
            {
                map = new RowMap(mType);
                StaticCache.TypeMaps[mType] = map;
            }
            return map;
        }

        /******************************************************************************************/

        internal static void ThrowMultipleRows(RowEnum row)
        {
            switch (row)
            {
                // get the standard exception from the runtime
                case RowEnum.Single:
                    ErrTwoRows.Single();
                    break;
                case RowEnum.SingleOrDefault:
                    ErrTwoRows.SingleOrDefault();
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
        internal static void ThrowZeroRows(RowEnum row)
        {
            switch (row)
            {
                // get the standard exception from the runtime
                case RowEnum.First:
                    ErrZeroRows.First();
                    break;
                case RowEnum.Single:
                    ErrZeroRows.Single();
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
        internal static Func<IDataReader, object> GetDeserializer(Type type, IDataReader reader)
        {
            return GetTypeDeserializer(type, reader);
        }
        internal static CacheInfo GetCacheInfo(Identity identity)
        {
            var info = new CacheInfo();
            Action<IDbCommand, DbParamInfo> reader = (cmd, paras) => paras.AddParameters(cmd, identity);
            info.ParamReader = reader;

            return info;
        }

        /******************************************************************************************/

        internal static Task<DbDataReader> ExecuteReaderWithFlagsFallbackAsync(DbCommand cmd, bool wasClosed, CommandBehavior behavior, CancellationToken cancellationToken)
        {
            var task = cmd.ExecuteReaderAsync(GetBehavior(wasClosed, behavior), cancellationToken);
            if (task.Status == TaskStatus.Faulted && DisableCommandBehaviorOptimizations(behavior, task.Exception.InnerException))
            {
                // we can retry; this time it will have different flags
                return cmd.ExecuteReaderAsync(GetBehavior(wasClosed, behavior), cancellationToken);
            }
            return task;
        }
        
    }
}
