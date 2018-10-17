using System.Collections.Generic;

namespace MyDAL
{
    public sealed class XDebug
    {
        private static object _lock { get; } = new object();
        private static List<string> _sql { get; set; } = new List<string>();
        private static List<string> _parameters { get; set; } = new List<string>();

        //internal static bool Hint { get; set; }

        public static List<string> SQL
        {
            get
            {
                lock (_lock)
                {
                    return _sql;
                }
            }
            set
            {
                lock (_lock)
                {
                    _sql = value;
                }
            }
        }
        public static List<string> Parameters
        {
            get
            {
                lock (_lock)
                {
                    return _parameters;
                }
            }
            set
            {
                lock(_lock)
                {
                    _parameters = value;
                }
            }
        }
        public static List<string> SqlWithParam { get; set; }
    }
}
