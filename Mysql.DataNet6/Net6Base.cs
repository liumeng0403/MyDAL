using MyDAL;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Mysql.DataNet6
{
    public abstract class Net6Base
    {
        //
        // Nuget : Package : MySql.Data
        //
        // 不同版本 mysql 连接字符串一般如下：
        // "Server=localhost; Database=MyDAL_TestDB; Uid=SkyUser; Pwd=Sky@4321;"
        // "Server=localhost; Database=MyDAL_TestDB; Uid=SkyUser; Pwd=Sky@4321;SslMode=none;"
        // "Server=localhost; Database=MyDAL_TestDB; Uid=SkyUser; Pwd=Sky@4321;SslMode=none;allowPublicKeyRetrieval=true;"
        // "Server=localhost; Database=MyDAL_TestDB; Uid=SkyUser; Pwd=Sky@4321;SslMode=true;allowPublicKeyRetrieval=false;"
        //

        /// <summary>
        /// MySQL
        /// </summary>
        protected XConnection DB_MyDAL_DEV
        {
            get
            {
                return XConnection
                    .Builder()
                    .SetHost("10.211.55.3")
                    .SetDatabase("mydal_testdb")
                    .SetUser("mydal_dll")
                    .SetPassword("mydal_TEST__##")
                    .SetSslMode(true)
                    .SetMySqlDriver<MySqlConnection>()
                    .Build();
            }
        }

    }
}