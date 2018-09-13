using System;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Yunyong.DataExchange.Others
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
        
        internal static string LinqBinary { get; } = "System.Data.Linq.Binary";
        
        internal static MethodInfo EnumParse { get; } = typeof(Enum).GetMethod(nameof(Enum.Parse), new Type[] { typeof(Type), typeof(string), typeof(bool) });
        internal static MethodInfo GetItem { get; } = typeof(IDataRecord).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .Where(p => p.GetIndexParameters().Length > 0 && p.GetIndexParameters()[0].ParameterType == typeof(int))
                        .Select(p => p.GetGetMethod()).First();
        
    }
}
