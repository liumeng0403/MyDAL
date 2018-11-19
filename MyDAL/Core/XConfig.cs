using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Yunyong.DataExchange.Core.Configs;
using Yunyong.DataExchange.Core.Enums;

namespace Yunyong.DataExchange.Core
{
    internal class XConfig
    {
        internal static bool IsDebug { get; set; } = false;

        /************************************************************************************************************/

        internal static bool IsCodeFirst { get; set; } = false;
        internal static bool IsNeedChangeDb { get; set; } = true;
        internal static string TablesNamespace { get; set; } = string.Empty;

        /************************************************************************************************************/

        internal static string MySQL { get; } = "MySql.Data.MySqlClient.MySqlConnection";
        internal static DbEnum DB { get; set; } = DbEnum.None;

        internal static int CommandTimeout { get; set; } = 10;  // 10s 

        internal static BindingFlags ClassSelfMember { get; private set; } = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;

        internal static MethodInfo EnumParse { get; } = typeof(Enum).GetMethod(nameof(Enum.Parse), new Type[] { typeof(Type), typeof(string), typeof(bool) });

        internal static CommandBehavior MultiRow { get; } = CommandBehavior.SequentialAccess | CommandBehavior.SingleResult;
        internal static CommandBehavior SingleRow { get; } = CommandBehavior.SequentialAccess | CommandBehavior.SingleResult | CommandBehavior.SingleRow;

        /************************************************************************************************************/

        internal static Type XTableAttribute { get; private set; } = typeof(XTableAttribute);
        internal static string XTableFullName { get; private set; } = typeof(XTableAttribute).FullName;

        internal static Type XColumnAttribute { get; private set; } = typeof(XColumnAttribute);
        internal static string XColumnFullName { get; private set; } = typeof(XColumnAttribute).FullName;

        /************************************************************************************************************/

        internal static MethodInfo GetItem { get; } = typeof(IDataRecord)
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.GetIndexParameters().Length > 0 && p.GetIndexParameters()[0].ParameterType == typeof(int))
            .Select(p => p.GetGetMethod())
            .First();

        /************************************************************************************************************/

        internal static ExceptionConfig EC { get; } = new ExceptionConfig();
        internal static TypeConfig TC { get; } = new TypeConfig();
    }
}
