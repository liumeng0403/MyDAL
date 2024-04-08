using MyDAL.Core;
using MyDAL.Core.Enums.Conns;
using MyDAL.Core.Extensions;
using MyDAL.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyDAL
{
    public sealed class XConnectionBuilder
    {
        //  需要完善 都进入 这个 dic 里面
        private Dictionary<string, string> DbPairs { get; }
        private List<string> KvPairs { get; }
        /// <summary>
        /// 驱动类型
        /// </summary>
        internal Type DriverType { get; set; }
        /// <summary>
        /// 连接字符串
        /// </summary>
        internal string ConnStr { get; set; }
        internal XConnectionBuilder()
        {

            KvPairs = new List<string>();
            DbPairs = new Dictionary<string, string>();

            DbPairs.AddOrReplace("Host", "127.0.0.1");
            DbPairs.AddOrReplace("Port", "3306");
            DbPairs.AddOrReplace("User Id", "root");
            DbPairs.AddOrReplace("Password", "password");
            
            DbPairs.AddOrReplace("Protocol", "Socket");
            
            DbPairs.AddOrReplace("SslMode", SslModeTypeEnum.Preferred.ToString());
            
            DbPairs.AddOrReplace("CharSet", "utf8mb4");
            
            DbPairs.AddOrReplace("Database", "mysql");
            
            DbPairs.AddOrReplace("Pooling", "false");

            DbPairs.AddOrReplace("ConnectionTimeout", "15");
            DbPairs.AddOrReplace("DefaultCommandTimeout", "30");
            DbPairs.AddOrReplace("AllowPublicKeyRetrieval", "false");
            
            DbPairs.AddOrReplace("PersistSecurityInfo","true");

        }

        // ---------------------------------------------------------------------------------------------
        
        /// <summary>
        /// 内网地址最优，服务位置同义词：Host, Server, Data Source, DataSource, Address, Addr, Network Address 默认:127.0.0.1 集群','隔开:host01,host02,host03...
        /// </summary>
        public XConnectionBuilder SetHost(string host)
        {
            if (host.IsBlank())
            {
                throw XConfig.EC.Exception(XConfig.EC._144, "数据库位置 Host 不能为空!");
            }
            DbPairs.AddOrReplace("Host", host.Trim());
            return this;
        }
        /// <summary>
        /// 端口:Port 默认:3306
        /// </summary>
        public XConnectionBuilder SetPort(int port)
        {
            DbPairs.AddOrReplace("Port", port.ToString());
            return this;
        }
        /// <summary>
        /// 登录帐号:User Id, UserID, Username, Uid, User name, User 默认:root 
        /// </summary>
        public XConnectionBuilder SetUser(string user)
        {
            if (user.IsBlank())
            {
                throw XConfig.EC.Exception(XConfig.EC._103, "登录帐号 User Id 不能为空!");
            }
            DbPairs.AddOrReplace("User Id", user.Trim());
            return this;
        }
        /// <summary>
        /// 登录密码:Password, pwd 默认:password 
        /// </summary>
        public XConnectionBuilder SetPassword(string pwd)
        {
            if (pwd.IsBlank())
            {
                throw XConfig.EC.Exception(XConfig.EC._104, "登录密码 Password 不能为空!");
            }
            DbPairs.AddOrReplace("Password", pwd.Trim());
            return this;
        }
        /// <summary>
        /// 数据库名:Database, Initial Catalog	 默认:"mysql"
        /// </summary>
		public XConnectionBuilder SetDatabase(string database)
        {
            if(database.IsBlank())
            {
                throw XConfig.EC.Exception(XConfig.EC._054, "数据库名 Database 不能为空!");
            }
            DbPairs.AddOrReplace("Database", database.Trim());
            return this;
        }
        /// <summary>
        /// 连接协议:Protocol, ConnectionProtocol, Connection Protocol 默认:Socket	
        /// </summary>
        public XConnectionBuilder SetProtocol(ProtocolTypeEnum protocolType)
        {
            DbPairs.AddOrReplace("Protocol", protocolType.ToString());
            return this;
        }
        /// <summary>
        /// 连接管道:Pipe, PipeName, Pipe Name 默认:MYSQL
        /// </summary>
        public XConnectionBuilder SetPipe(string pipe)
        {
            if(pipe.IsBlank())
            {
                throw XConfig.EC.Exception(XConfig.EC._092, "连接管道 Pipe 不能为空!");
            }
            DbPairs.AddOrReplace("Pipe", pipe.Trim());
            return this;
        }
        
        // ---------------------------------------------------------------------------------------------

        /// <summary>
        /// 是否启用SSL连接模式:SslMode , mysql 5.7- 设置 false，mysql 8.0+ 设置 true ，默认 依据服务器设置
        /// </summary>
        public XConnectionBuilder SetSslMode(bool sslModeType)
        {
            if (sslModeType)
            {
                DbPairs.AddOrReplace("SslMode", SslModeTypeEnum.Required.ToString());
            }
            else
            {
                DbPairs.AddOrReplace("SslMode", SslModeTypeEnum.None.ToString());
            }
            return this;
        }
        /// <summary>
        /// 证书文件(.pfx):Certificate File, CertificateFile 默认:"" 
        /// </summary>
        public XConnectionBuilder SetCertificateFile(string certificateFile)
        {
            if (certificateFile.IsBlank())
            {
                throw XConfig.EC.Exception(XConfig.EC._106, "证书文件 CertificateFile 不能为空!");
            }
            DbPairs.AddOrReplace("CertificateFile", certificateFile.Trim());
            return this;
        }
        /// <summary>
        /// 证书的密码:Certificate Password, CertificatePassword 默认:"" 
        /// </summary>
        public XConnectionBuilder SetCertificatePassword(string certificatePassword)
        {
            if (certificatePassword.IsBlank())
            {
                throw XConfig.EC.Exception(XConfig.EC._107, "证书的密码 CertificatePassword 不能为空!");
            }
            DbPairs.AddOrReplace("CertificatePassword", certificatePassword.Trim());
            return this;
        }
        /// <summary>
        /// CA Certificate File, CACertificateFile, SslCa, Ssl-Ca 
        /// </summary>
        public XConnectionBuilder SetCACertificateFile(string cACertificateFile)
        {
            if (cACertificateFile.IsBlank())
            {
                throw XConfig.EC.Exception(XConfig.EC._091, "CACertificateFile 不能为空!");
            }
            DbPairs.AddOrReplace("CACertificateFile", cACertificateFile.Trim());
            return this;
        }
        /// <summary>
        /// 证书的存储位置:Certificate Store Location, CertificateStoreLocation 默认:None 
        /// </summary>
        public XConnectionBuilder SetCertificateStoreLocation(string certificateStoreLocation)
        {
            if (certificateStoreLocation.IsBlank())
            {
                throw XConfig.EC.Exception(XConfig.EC._108, "证书的存储位置 CertificateStoreLocation 不能为空!");
            }
            DbPairs.AddOrReplace("CertificateStoreLocation", certificateStoreLocation.Trim());
            return this;
        }
        /// <summary>
        /// 证书指纹:Certificate Thumbprint, CertificateThumbprint 默认:"" 
        /// </summary>
        public XConnectionBuilder SetCertificateThumbprint(string certificateThumbprint)
        {
            if (certificateThumbprint.IsBlank())
            {
                throw XConfig.EC.Exception(XConfig.EC._109, "证书指纹 CertificateThumbprint 不能为空!");
            }
            DbPairs.AddOrReplace("CertificateThumbprint", certificateThumbprint.Trim());
            return this;
        }
        /// <summary>
        /// Tls Version, TlsVersion, Tls-Version 
        /// </summary>
        public XConnectionBuilder SetTlsVersion(TlsVersionTypeEnum tlsVersionType)
        {
            DbPairs.AddOrReplace("TlsVersion", tlsVersionType.ToString());
            return this;
        }

        // ---------------------------------------------------------------------------------------------

        /// <summary>
        /// 是否使用线程池:Pooling 默认:false 
        /// </summary>
        public XConnectionBuilder SetPooling(bool pooling)
        {
            DbPairs.AddOrReplace("Pooling", pooling.ToString().ToLower());
            return this;
        }
        /// <summary>
        /// 连接被销毁前在连接池中保持的最少时间(秒):Connection Lifetime, ConnectionLifeTime 默认:0
        /// </summary>
        public XConnectionBuilder SetConnectionLifeTime(int connectionLifeTime)
        {
            DbPairs.AddOrReplace("ConnectionLifeTime", connectionLifeTime.ToString());
            return this;
        }
        /// <summary>
        /// 连接过期后是否自动复位:Connection Reset, ConnectionReset
        /// </summary>
        public XConnectionBuilder SetConnectionReset(bool connectionReset)
        {
            DbPairs.AddOrReplace("ConnectionReset", connectionReset.ToString().ToLower());
            return this;
        }

        /// <summary>
        /// 线程池中允许最多连接数数:Maximum Pool Size, Max Pool Size, MaximumPoolsize
        /// </summary>
        public XConnectionBuilder SetMaximumPoolSize(int maximumPoolSize)
        {
            DbPairs.AddOrReplace("MaximumPoolsize", maximumPoolSize.ToString());
            return this;
        }

        /// <summary>
        /// 线程池中保持最少连接数:Minimum Pool Size, Min Pool Size, MinimumPoolSize, 
        /// </summary>
        public XConnectionBuilder SetMinimumPoolSize(int minimumPoolSize)
        {
            DbPairs.AddOrReplace("MinimumPoolSize", minimumPoolSize.ToString());
            return this;
        }

        // ---------------------------------------------------------------------------------------------

        /// <summary>
        /// AllowLoadLocalInfile, Allow Load Local Infile  默认:false
        /// </summary>
        public XConnectionBuilder SetAllowLoadLocalInfile(bool allowLoadLocalInfile)
        {
            DbPairs.AddOrReplace("AllowLoadLocalInfile", allowLoadLocalInfile.ToString().ToLower());
            return this;
        }

        /// <summary>
        /// 是否许客户端自动从服务器请求公钥 默认:false
        /// </summary>
        public XConnectionBuilder SetAllowPublicKyunxueyRetrieval(bool allowPublicKeyRetrieval)
        {
            DbPairs.AddOrReplace("AllowPublicKeyRetrieval", allowPublicKeyRetrieval.ToString().ToLower());
            return this;
        }

        /// <summary>
        /// 是否允许SQL中出现用户变量:AllowUserVariables, Allow User Variables 默认:false
        /// </summary>
        public XConnectionBuilder SetAllowUserVariables(bool allowUserVariables)
        {
            DbPairs.AddOrReplace("AllowUserVariables", allowUserVariables.ToString().ToLower());
            return this;
        }

        /// <summary>
        /// 日期时间能否为零:AllowZeroDateTime, Allow Zero DateTime 默认:false 
        /// </summary>
        public XConnectionBuilder SetAllowZeroDateTime(bool allowZeroDateTime)
        {
            DbPairs.AddOrReplace("AllowZeroDateTime", allowZeroDateTime.ToString().ToLower());
            return this;
        }

        /// <summary>
        /// ApplicationName, Application Name 默认:""
        /// </summary>
        public XConnectionBuilder SetApplicationName(string applicationName)
        {
            if(applicationName.IsBlank())
            {
                throw XConfig.EC.Exception(XConfig.EC._138, "ApplicationName, Application Name 不能为空!");
            }
            DbPairs.AddOrReplace("ApplicationName", applicationName.Trim());
            return this;
        }
        /// <summary>
        /// 连接MySQL所使用的字符集:CharSet, Character Set, CharacterSet 默认:"utf8mb4"
        /// </summary>
        public XConnectionBuilder SetCharset(string charset)
        {
            if (charset.IsBlank())
            {
                throw XConfig.EC.Exception(XConfig.EC._131, "连接MySQL所使用的字符集 CharSet 不能为空!");
            }
            DbPairs.AddOrReplace("CharSet", charset.Trim());
            return this;
        }
        /// <summary>
        /// 是否压缩:Compress, Use Compression, UseCompression 默认:false	 
        /// </summary>
        public XConnectionBuilder SetCompress(bool compress)
        {
            DbPairs.AddOrReplace("Compress", compress.ToString().ToLower());
            return this;
        }

        /// <summary>
        /// 连接超时等待时间:Connect Timeout, Connection Timeout, ConnectionTimeout 默认:15s	
        /// </summary>
        public XConnectionBuilder SetConnectionTimeout(int connectionTimeout)
        {
            DbPairs.AddOrReplace("ConnectionTimeout", connectionTimeout.ToString());
            return this;
        }

        /// <summary>
        /// 零是否转化为最小日期:Convert Zero Datetime, ConvertZeroDateTime 默认:false
        /// </summary>
        public XConnectionBuilder SetConvertZeroDateTime(bool convertZeroDateTime)
        {
            DbPairs.AddOrReplace("ConvertZeroDateTime", convertZeroDateTime.ToString().ToLower());
            return this;
        }

        /// <summary>
        /// DateTimeKind  默认:Unspecified
        /// </summary>
        public XConnectionBuilder SetDateTimeKind(string dateTimeKind)
        {
            if(dateTimeKind.IsBlank())
            {
                throw XConfig.EC.Exception(XConfig.EC._139, "DateTimeKind 不能为空!");
            }
            DbPairs.AddOrReplace("DateTimeKind", dateTimeKind.Trim());
            return this;
        }
        /// <summary>
        /// GuidFormat 默认:Default
        /// </summary>
        public XConnectionBuilder SetGuidFormat(GuidFormatTypeEnum guidFormatType)
        {
            DbPairs.AddOrReplace("GuidFormat", guidFormatType.ToString());
            return this;
        }
        /// <summary>
        /// 命令执行超时时间:Default Command Timeout, Command Timeout, DefaultCommandTimeout 默认:30s
        /// </summary>
        public XConnectionBuilder SetDefaultCommandTimeout(int defaultCommandTimeout)
        {
            DbPairs.AddOrReplace("DefaultCommandTimeout", defaultCommandTimeout.ToString());
            return this;
        }

        /// <summary>
        /// IgnoreCommandTransaction, Ignore Command Transaction 默认:false
        /// </summary>
        public XConnectionBuilder SetIgnoreCommandTransaction(bool ignoreCommandTransaction)
        {
            DbPairs.AddOrReplace("IgnoreCommandTransaction", ignoreCommandTransaction.ToString().ToLower());
            return this;
        }

        /// <summary>
        /// 会话是否允许交互:Interactive, Interactive Session, InteractiveSession 默认:false	
        /// </summary>
        public XConnectionBuilder SetInteractive(bool interactive)
        {

            DbPairs.AddOrReplace("Interactive", interactive.ToString().ToLower());
            return this;
        }

        /// <summary>
        /// 保持TCP连接的秒数:Keep Alive, Keepalive 默认:0-默认操作系统设置
        /// </summary>
        public XConnectionBuilder SetKeepalive(int keepalive)
        {
            DbPairs.AddOrReplace("Keepalive", keepalive.ToString());
            return this;
        }

        /// <summary>
        /// Load Balance, LoadBalance  默认:RoundRobin
        /// </summary>
        public XConnectionBuilder SetLoadBalance(LoadBalanceTypeEnum loadBalanceType)
        {
            DbPairs.AddOrReplace("LoadBalance", loadBalanceType.ToString());
            return this;
        }
        /// <summary>
        /// No Backslash Escapes, NoBackslashEscapes 默认:false
        /// </summary>
        public XConnectionBuilder SetNoBackslashEscapes(bool noBackslashEscapes)
        {
            DbPairs.AddOrReplace("NoBackslashEscapes", noBackslashEscapes.ToString().ToLower());
            return this;
        }

        /// <summary>
        /// 连接下一次 open 是否保持敏感信息:  PersistSecurityInfo 默认:true	
        /// </summary>
        public XConnectionBuilder SetPersistSecurityInfo(bool persistSecurityInfo)
        {
            DbPairs.AddOrReplace("PersistSecurityInfo", persistSecurityInfo.ToString().ToLower());
            return this;
        }

        /// <summary>
        /// ServerRSAPublicKeyFile, Server RSA Public Key File
        /// </summary>
        public XConnectionBuilder SetServerRSAPublicKeyFile(string serverRSAPublicKeyFile)
        {
            if(serverRSAPublicKeyFile.IsBlank())
            {
                throw XConfig.EC.Exception(XConfig.EC._142, "ServerRSAPublicKeyFile, Server RSA Public Key File 不能为空!");
            }
            DbPairs.AddOrReplace("ServerRSAPublicKeyFile", serverRSAPublicKeyFile.Trim());
            return this;
        }
        /// <summary>
        /// 是否将TINYINT(1)列视为布尔型:Treat Tiny As Boolean, TreatTinyAsBoolean 默认:true 
        /// </summary>
        public XConnectionBuilder SetTreatTinyAsBoolean(bool treatTinyAsBoolean)
        {
            DbPairs.AddOrReplace("TreatTinyAsBoolean", treatTinyAsBoolean.ToString().ToLower());
            return this;
        }

        /// <summary>
        /// 是否用受影响的行数替代查找到的行数来返回数据:Use Affected Rows, UseAffectedRows 默认:false	
        /// </summary>
        public XConnectionBuilder SetUseAffectedRows(bool useAffectedRows)
        {
            DbPairs.AddOrReplace("UseAffectedRows", useAffectedRows.ToString().ToLower());
            return this;
        }

        // ---------------------------------------------------------------------------------------------

        /// <summary>
        /// 连接字符串: key1=value1;key2=value2;key3=...
        /// </summary>
        public XConnectionBuilder SetConnectionStrings(string connStr)
        {
            if (connStr.IsBlank())
            {
                throw XConfig.EC.Exception(XConfig.EC._098, "连接字符串 connStr 不能为空!");
            }
            if (!(connStr.Contains("=") && connStr.Contains(";")))
            {
                throw XConfig.EC.Exception(XConfig.EC._099, "连接字符串 connStr 必须是 key=value; 的格式!");
            }
            KvPairs.Add(connStr);
            return this;
        }
        /// <summary>
        /// connection string 片段: key1=value1;key2=value2;key3=...
        /// </summary>
        public XConnectionBuilder SetExtraConnectionStrings(string partConnStr)
        {
            if (partConnStr.IsBlank())
            {
                throw XConfig.EC.Exception(XConfig.EC._145, "connection string 片段 partConnStr 不能为空!");
            }
            if (!(partConnStr.Contains("=") && partConnStr.Contains(";")))
            {
                throw XConfig.EC.Exception(XConfig.EC._148, "connection string 片段 partConnStr 必须是 key=value; 的格式!");
            }
            KvPairs.Add(partConnStr);
            return this;
        }
        /// <summary>
        /// 设置 mysql 驱动，支持：MySql.Data、MySqlConnector、Devart.Data.MySql
        /// </summary>
        /// <typeparam name="MC">实现 IDbConnection 的驱动类</typeparam>
        public XConnectionBuilder SetMySqlDriver<MC>()
            where MC : class,IDbConnection
        {
            Type mySqlConnectionType = typeof(MC);
            if (!mySqlConnectionType.FullName.Contains("MySqlConnection", StringComparison.OrdinalIgnoreCase))
            {
                throw XConfig.EC.Exception(XConfig.EC._147, "本类库专精 mysql db , 不支持其它 db !");
            }
            this.DriverType = mySqlConnectionType;
            return this;
        }
        
        /// <summary>
        /// 返回：上下文线程安全的 MySQL DB 操作实例
        /// </summary>
        public XConnection Build()
        {
            if (null == this.DriverType)
            {
                throw XConfig.EC.Exception(XConfig.EC._149, "必须在 XConnectionBuilder.SetMySqlDriver(Type mySqlConnectionType) 设置驱动类型!");
            }
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string,string> kv in DbPairs)
            {
                sb.Append(kv.Key);
                sb.Append("=");
                sb.Append(kv.Value);
                sb.Append(";");
            }
            foreach (string v in KvPairs)
            {
                sb.Append(v);
            }
            this.ConnStr = sb.ToString();
            var xConn = new XConnection(this);
            
            
            try
            {
                IDbConnection obj = (IDbConnection)Activator.CreateInstance(xConn._Builder.DriverType, xConn._Builder.ConnStr);
                return new XConnection(obj);
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message.IsBlank() ? string.Empty : ex.Message;
                string innerErrMsg = ex.InnerException == null ? 
                    string.Empty : 
                    ex.InnerException.Message.IsBlank() ? string.Empty : ex.InnerException.Message;
                throw XConfig.EC.Exception(XConfig.EC._100, "MySQL 驱动异常: [1]" + errMsg + ". [2]" + innerErrMsg);
            }
        }
    }
}
