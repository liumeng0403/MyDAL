using Yunyong.DataExchange.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yunyong.DataExchange
{
    public class XDebug
    {
        public static bool Hint { get; set; }
        private static List<string> _sql { get; set; } = new List<string>();
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
        private static List<string> _parameters { get; set; } = new List<string>();
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
        private static object _lock { get; } = new object();
    }
}
