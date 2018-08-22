using System;
using System.Data.SqlClient;
using Xunit;
using EasyDAL.Exchange.AdoNet;

namespace EasyDAL.Exchange.Tests
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class FactLongRunningAttribute : FactAttribute
    {
        public FactLongRunningAttribute()
        {
            Skip = "Long running";

        }

        public string Url { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class FactRequiredCompatibilityLevelAttribute : FactAttribute
    {
        public FactRequiredCompatibilityLevelAttribute(int level) : base()
        {
            if (DetectedLevel < level)
            {
                Skip = $"Compatibility level {level} required; detected {DetectedLevel}";
            }
        }

        public const int SqlServer2016 = 130;
        public static readonly int DetectedLevel;
        static FactRequiredCompatibilityLevelAttribute()
        {
            using (var conn = TestBase.GetOpenConnection())
            {
                try
                {
                    DetectedLevel = conn.QuerySingle<int>("SELECT compatibility_level FROM sys.databases where name = DB_NAME()");
                }
                catch { /* don't care */ }
            }
        }
    }

}
