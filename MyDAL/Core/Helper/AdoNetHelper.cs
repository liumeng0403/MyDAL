using MyDAL.AdoNet;
using MyDAL.Core.Enums;
using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MyDAL.Core.Helper
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
                    var col = reader.GetName(i);
                    hash = (-79 * ((hash * 31) + (col?.GetHashCode() ?? 0))) + (reader.GetFieldType(i)?.GetHashCode() ?? 0);
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
            if (!XCache.TypeMaps.TryGetValue(mType, out var map))
            {
                map = new RowMap(mType);
                XCache.TypeMaps[mType] = map;
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

        /****************************************************************************************************************/

        internal static string ConditionAction(ActionEnum action)
        {
            switch (action)
            {
                case ActionEnum.None:
                    return "";
                case ActionEnum.Insert:
                    return "";
                case ActionEnum.Update:
                    return "";
                case ActionEnum.Select:
                    return "";
                case ActionEnum.From:
                    return "";
                case ActionEnum.InnerJoin:
                    return " inner join ";
                case ActionEnum.LeftJoin:
                    return " left join ";
                case ActionEnum.On:
                    return " on ";
                case ActionEnum.Where:
                    return " \r\n where ";
                case ActionEnum.And:
                    return " \r\n \t and ";
                case ActionEnum.Or:
                    return " \r\n \t or ";
                case ActionEnum.OrderBy:
                    return "";
            }
            return " ";
        }
        internal static string MultiConditionAction(ActionEnum action)
        {
            switch (action)
            {
                case ActionEnum.And:
                    return " && ";
                case ActionEnum.Or:
                    return " || ";
            }
            return " ";
        }
        internal static string ConditionOption(OptionEnum option)
        {
            switch (option)
            {
                case OptionEnum.None:
                    return "<<<<<";
                case OptionEnum.Insert:
                    return "";
                case OptionEnum.InsertTVP:
                    return "";
                case OptionEnum.Set:
                    return "=";
                case OptionEnum.ChangeAdd:
                    return "+";
                case OptionEnum.ChangeMinus:
                    return "-";
                case OptionEnum.Column:
                    return "";
                case OptionEnum.ColumnAs:
                    break;
                case OptionEnum.Compare:
                    return "";
                case OptionEnum.Like:
                    return " like ";
                case OptionEnum.In:
                    return " in ";
                case OptionEnum.InHelper:
                    break;
                case OptionEnum.NotIn:
                    return " not in ";
                case OptionEnum.Count:
                    return " count";
                case OptionEnum.CharLength:
                    return " char_length";
                case OptionEnum.Trim:
                    return " trim";
                case OptionEnum.LTrim:
                    return " ltrim";
                case OptionEnum.RTrim:
                    return " rtrim";
                case OptionEnum.OneEqualOne:
                    return "";
                case OptionEnum.IsNull:
                    return " is null ";
                case OptionEnum.IsNotNull:
                    return " is not null ";
                case OptionEnum.Asc:
                    return " asc ";
                case OptionEnum.Desc:
                    return " desc ";
            }
            return " ";
        }
        internal static string ConditionCompare(CompareEnum compare)
        {
            switch (compare)
            {
                case CompareEnum.None:
                    return " ";
                case CompareEnum.Equal:
                    return "=";
                case CompareEnum.NotEqual:
                    return "<>";
                case CompareEnum.LessThan:
                    return "<";
                case CompareEnum.LessThanOrEqual:
                    return "<=";
                case CompareEnum.GreaterThan:
                    return ">";
                case CompareEnum.GreaterThanOrEqual:
                    return ">=";
                case CompareEnum.Like:
                    return " like ";
                case CompareEnum.In:
                    return " in ";
                case CompareEnum.NotIn:
                    return " not in ";
            }
            return " ";
        }
        internal static string ConditionFunc(FuncEnum func)
        {
            switch (func)
            {
                case FuncEnum.None:
                    return "";
                case FuncEnum.Column:
                    return "";
                case FuncEnum.CharLength:
                    return " char_length";
            }
            return " ";
        }

    }
}
