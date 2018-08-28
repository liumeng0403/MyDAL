using EasyDAL.Exchange.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EasyDAL.Exchange.MapperX
{
    /// <summary>
    /// Permits specifying certain SqlMapper values globally.
    /// </summary>
    internal static class Settings
    {
        // disable single result by default; prevents errors AFTER the select being detected properly
        private const CommandBehavior DefaultAllowedCommandBehaviors = ~CommandBehavior.SingleResult;
        internal static CommandBehavior AllowedCommandBehaviors { get; private set; } = DefaultAllowedCommandBehaviors;
        private static void SetAllowedCommandBehaviors(CommandBehavior behavior, bool enabled)
        {
            if (enabled) AllowedCommandBehaviors |= behavior;
            else AllowedCommandBehaviors &= ~behavior;
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




        /// <summary>
        /// Indicates whether nulls in data are silently ignored (default) vs actively applied and assigned to members
        /// </summary>
        public static bool ApplyNullValues { get; set; }

        /// <summary>
        /// Should list expansions be padded with null-valued parameters, to prevent query-plan saturation? For example,
        /// an 'in @foo' expansion with 7, 8 or 9 values will be sent as a list of 10 values, with 3, 2 or 1 of them null.
        /// The padding size is relative to the size of the list; "next 10" under 150, "next 50" under 500,
        /// "next 100" under 1500, etc.
        /// </summary>
        /// <remarks>
        /// Caution: this should be treated with care if your DB provider (or the specific configuration) allows for null
        /// equality (aka "ansi nulls off"), as this may change the intent of your query; as such, this is disabled by 
        /// default and must be enabled.
        /// </remarks>
        public static bool PadListExpansions { get; set; }
        /// <summary>
        /// If set (non-negative), when performing in-list expansions of integer types ("where id in @ids", etc), switch to a string_split based
        /// operation if there are more than this many elements. Note that this feautre requires SQL Server 2016 / compatibility level 130 (or above).
        /// </summary>
        public static int InListStringSplitCount { get; set; } = -1;


        internal static string LinqBinary { get; } = "System.Data.Linq.Binary";

        internal static Dictionary<TypeCode, MethodInfo> ToStrings { get; } = new[]
        {
            typeof(bool),
            typeof(sbyte),
            typeof(byte),
            typeof(ushort),
            typeof(short),
            typeof(uint),
            typeof(int),
            typeof(ulong),
            typeof(long),
            typeof(float),
            typeof(double),
            typeof(decimal)
        }.ToDictionary(
            x => TypeExtensionsX.GetTypeCodeX(x),
            x => x.GetPublicInstanceMethodX(
                nameof(object.ToString),
                new[]
                {
                    typeof(IFormatProvider)
                }));


        internal static MethodInfo EnumParse { get; } = typeof(Enum).GetMethod(nameof(Enum.Parse), new Type[] { typeof(Type), typeof(string), typeof(bool) });
        internal static MethodInfo GetItem { get; } = typeof(IDataRecord).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .Where(p => p.GetIndexParameters().Length > 0 && p.GetIndexParameters()[0].ParameterType == typeof(int))
                        .Select(p => p.GetGetMethod()).First();


    }
}
