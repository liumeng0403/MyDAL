using MyDAL.AdoNet;
using MyDAL.Core.Enums;
using System;
using System.Data;
using System.Linq;
using System.Reflection;

namespace MyDAL.Core
{
    internal static class XSQL
    {
        
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
                case OptionEnum.Sum:
                    return " sum";
                case OptionEnum.CharLength:
                    return " char_length";
                case OptionEnum.DateFormat:
                    return " DATE_FORMAT";
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
