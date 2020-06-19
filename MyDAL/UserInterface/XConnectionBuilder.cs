using MyDAL.Core;
using MyDAL.Core.Extensions;
using MyDAL.Tools;
using System.Collections.Generic;

namespace MyDAL
{
    public class XConnectionBuilder
    {
        private XConnection XConn { get; set; }
        private Dictionary<string, string> DbPairs { get; }

        /// <summary>
        /// 数据库位置:Server/host/data source/datasource/address/addr/network address 默认:127.0.0.1
        /// </summary>
        public XConnectionBuilder SetHost(string host)
        {
            if(host.IsNullStr())
            {
                throw XConfig.EC.Exception(XConfig.EC._037, "数据库位置 host 不能为空!");
            }
            DbPairs.AddOrReplace("host", host.Trim());
            return this;
        }
        /// <summary>
        /// 数据库名:Database/initial catalog 默认:""
        /// </summary>
		public XConnectionBuilder SetDatabase(string database)
        {
            if(database.IsNullStr())
            {
                throw XConfig.EC.Exception(XConfig.EC._054, "数据库名 Database 不能为空!");
            }
            DbPairs.AddOrReplace("Database", database.Trim());
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
        /// 端口:Port 默认:3306
        /// </summary>
        public XConnectionBuilder SetPort(int? port)
        {
            if (port.IsNullStr())
            {
                throw XConfig.EC.Exception(XConfig.EC._055, "端口 Port 不能为空!");
            }
            DbPairs.AddOrReplace("Port", port.ToString());
            return this;
        }
        /// <summary>
        /// 连接协议:ConnectionProtocol/protocol 默认:Sockets	
        /// </summary>
        public XConnectionBuilder SetProtocol(string protocol)
        {
            if(protocol.IsNullStr())
            {
                throw XConfig.EC.Exception(XConfig.EC._091, "连接协议 protocol 不能为空!");
            }
            DbPairs.AddOrReplace("protocol", protocol.Trim());
            return this;
        }
        /// <summary>
        /// 连接管道:PipeName/pipe 默认:MYSQL
        /// </summary>
        public XConnectionBuilder SetPipe(string pipe)
        {
            if(pipe.IsNullStr())
            {
                throw XConfig.EC.Exception(XConfig.EC._092, "连接管道 pipe 不能为空!");
            }
            DbPairs.AddOrReplace("pipe", pipe.Trim());
            return this;
        }
        /// <summary>
        /// 是否压缩:UseCompression/compress 默认:false	 
        /// </summary>
        public XConnectionBuilder SetCompress(bool compress)
        {
            DbPairs.AddOrReplace("compress", compress.ToString().ToLower());
            return this;
        }
        /// <summary>
        /// 是否压缩:UseCompression/compress 默认:false	 
        /// </summary>
        public XConnectionBuilder SetCompress(bool? compress)
        {
            if (compress.IsNullStr())
            {
                throw XConfig.EC.Exception(XConfig.EC._096, "是否压缩 compress 不能为空!");
            }
            DbPairs.AddOrReplace("compress", compress.ToString().ToLower());
            return this;
        }
        /// <summary>
        /// 是否允许一次执行多条SQL语句:AllowBatch 默认:true 
        /// </summary>
        public XConnectionBuilder SetAllowBatch(bool allowBatch)
        {
            DbPairs.AddOrReplace("AllowBatch", allowBatch.ToString().ToLower());
            return this;
        }
        /// <summary>
        /// 是否允许一次执行多条SQL语句:AllowBatch 默认:true 
        /// </summary>
        public XConnectionBuilder SetAllowBatch(bool? allowBatch)
        {
            if(allowBatch.IsNullStr())
            {
                throw XConfig.EC.Exception(XConfig.EC._097, "是否允许一次执行多条SQL语句 AllowBatch 不能为空!");
            }
            DbPairs.AddOrReplace("AllowBatch", allowBatch.ToString().ToLower());
            return this;
        }
        /// <summary>
        /// 启用日志:Logging 默认:false	
        /// </summary>
        public XConnectionBuilder SetLogging(bool logging)
        {
            DbPairs.AddOrReplace("Logging", logging.ToString().ToLower());
            return this;
        }
        /// <summary>
        /// 启用日志:Logging 默认:false	
        /// </summary>
        public XConnectionBuilder SetLogging(bool? logging)
        {
            if(logging.IsNullStr())
            {
                throw XConfig.EC.Exception(XConfig.EC._098, "启用日志 Logging 不能为空!");
            }
            DbPairs.AddOrReplace("Logging", logging.ToString().ToLower());
            return this;
        }
        /// <summary>
        /// 内存共享的名称:SharedMemoryName 默认:MYSQL	
        /// </summary>
        public XConnectionBuilder SetSharedMemoryName(string sharedMemoryName)
        {
            if(sharedMemoryName.IsNullStr())
            {
                throw XConfig.EC.Exception(XConfig.EC._099, "内存共享的名称 SharedMemoryName 不能为空!");
            }
            DbPairs.AddOrReplace("SharedMemoryName", sharedMemoryName);
            return this;
        }        
        /// <summary>
        /// 是否兼容旧版的语法:UseOldSyntax/old syntax/oldsyntax 默认:false	
        /// </summary>
        public XConnectionBuilder SetOldsyntax(bool oldsyntax)
        {
            DbPairs.AddOrReplace("oldsyntax", oldsyntax.ToString().ToLower());
            return this;
        }
        /// <summary>
        /// 是否兼容旧版的语法:UseOldSyntax/old syntax/oldsyntax 默认:false	
        /// </summary>
        public XConnectionBuilder SetOldsyntax(bool? oldsyntax)
        {
            if(oldsyntax.IsNullStr())
            {
                throw XConfig.EC.Exception(XConfig.EC._100, "是否兼容旧版的语法 oldsyntax 不能为空!");
            }
            DbPairs.AddOrReplace("oldsyntax", oldsyntax.ToString().ToLower());
            return this;
        }
        /// <summary>
        /// 连接超时等待时间:ConnectionTimeout/connection timeout 默认:15s	
        /// </summary>
        public XConnectionBuilder SetConnectionTimeout(int connectionTimeout)
        {
            DbPairs.AddOrReplace("ConnectionTimeout", connectionTimeout.ToString());
            return this;
        }
        /// <summary>
        /// 连接超时等待时间:ConnectionTimeout/connection timeout 默认:15s	
        /// </summary>
        public XConnectionBuilder SetConnectionTimeout(int? connectionTimeout)
        {
            if(connectionTimeout.IsNullStr())
            {
                throw XConfig.EC.Exception(XConfig.EC._101, "连接超时等待时间 ConnectionTimeout 不能为空!");
            }
            DbPairs.AddOrReplace("ConnectionTimeout", connectionTimeout.ToString());
            return this;
        }
        /// <summary>
        /// 命令执行超时时间:DefaultCommandTimeout/command timeout 默认:30s
        /// </summary>
        public XConnectionBuilder SetDefaultCommandTimeout(int defaultCommandTimeout)
        {
            DbPairs.AddOrReplace("DefaultCommandTimeout", defaultCommandTimeout.ToString());
            return this;
        }
        /// <summary>
        /// 命令执行超时时间:DefaultCommandTimeout/command timeout 默认:30s
        /// </summary>
        public XConnectionBuilder SetDefaultCommandTimeout(int? defaultCommandTimeout)
        {
            if(defaultCommandTimeout.IsNullStr())
            {
                throw XConfig.EC.Exception(XConfig.EC._102, "命令执行超时时间 DefaultCommandTimeout 不能为空!");
            }
            DbPairs.AddOrReplace("DefaultCommandTimeout", defaultCommandTimeout.ToString());
            return this;
        }
        /// <summary>
        /// 登录帐号:UserID/uid/username/user name/user 默认:root											-- 
        /// </summary>
        public XConnectionBuilder SetUID(string uid)
        {
            if(uid.IsNullStr())
            {
                throw XConfig.EC.Exception(XConfig.EC._103, "登录帐号 uid 不能为空!");
            }
            DbPairs.AddOrReplace("uid", uid.Trim());
            return this;
        }
        /// <summary>
        /// 登录密码:Password/pwd 默认:password 
        /// </summary>
        public XConnectionBuilder SetPwd(string pwd)
        {
            if(pwd.IsNullStr())
            {
                throw XConfig.EC.Exception(XConfig.EC._104, "登录密码 pwd 不能为空!");
            }
            DbPairs.AddOrReplace("pwd", pwd.Trim());
            return this;
        }
        /// <summary>
        /// 是否保持敏感信息:PersistSecurityInfo 默认:false	
        /// </summary>
        public XConnectionBuilder SetPersistSecurityInfo(bool persistSecurityInfo)
        {
            DbPairs.AddOrReplace("PersistSecurityInfo", persistSecurityInfo.ToString().ToLower());
            return this;
        }
        /// <summary>
        /// 是否保持敏感信息:PersistSecurityInfo 默认:false	
        /// </summary>
        public XConnectionBuilder SetPersistSecurityInfo(bool? persistSecurityInfo)
        {
            if(persistSecurityInfo.IsNullStr())
            {
                throw XConfig.EC.Exception(XConfig.EC._105, "是否保持敏感信息 PersistSecurityInfo 不能为空!");
            }
            DbPairs.AddOrReplace("PersistSecurityInfo", persistSecurityInfo.ToString().ToLower());
            return this;
        }
        // **** 加密连接, 已淘汰, 已用SSL替代，默认false  -- Encrypt **** 
        /// <summary>
        /// 证书文件(.pfx):CertificateFile 默认:"" 
        /// </summary>
        public XConnectionBuilder SetCertificateFile(string certificateFile)
        {
            if(certificateFile.IsNullStr())
            {
                throw XConfig.EC.Exception(XConfig.EC._106, "证书文件 CertificateFile 不能为空!");
            }
            DbPairs.AddOrReplace("CertificateFile", certificateFile.Trim());
            return this;
        }
        /// <summary>
        /// 证书的密码:CertificatePassword 默认:"" 
        /// </summary>
        public XConnectionBuilder SetCertificatePassword(string certificatePassword)
        {
            if(certificatePassword.IsNullStr())
            {
                throw XConfig.EC.Exception(XConfig.EC._107, "证书的密码 CertificatePassword 不能为空!");
            }
            DbPairs.AddOrReplace("CertificatePassword", certificatePassword.Trim());
            return this;
        }
        /// <summary>
        /// 证书的存储位置:CertificateStoreLocation 默认:"" 
        /// </summary>
        public XConnectionBuilder SetCertificateStoreLocation(string certificateStoreLocation)
        {
            if(certificateStoreLocation.IsNullStr())
            {
                throw XConfig.EC.Exception(XConfig.EC._108, "证书的存储位置 CertificateStoreLocation 不能为空!");
            }
            DbPairs.AddOrReplace("CertificateStoreLocation", certificateStoreLocation.Trim());
            return this;
        }
        /// <summary>
        /// 证书指纹:CertificateThumbprint 默认:"" 
        /// </summary>
        public XConnectionBuilder SetCertificateThumbprint(string certificateThumbprint)
        {
            if(certificateThumbprint.IsNullStr())
            {
                throw XConfig.EC.Exception(XConfig.EC._109, "证书指纹 CertificateThumbprint 不能为空!");
            }
            DbPairs.AddOrReplace("CertificateThumbprint", certificateThumbprint.Trim());
            return this;
        }

        public XConnectionBuilder()
        {
            DbPairs = new Dictionary<string, string>();
            DbPairs.AddOrReplace("host", "127.0.0.1");
            DbPairs.AddOrReplace("Database", "");
            DbPairs.AddOrReplace("Port", "3306");
            DbPairs.AddOrReplace("protocol", "Sockets");
            DbPairs.AddOrReplace("pipe", "MYSQL");
            DbPairs.AddOrReplace("compress", "false");
            DbPairs.AddOrReplace("AllowBatch", "true");
            DbPairs.AddOrReplace("Logging", "false");
            DbPairs.AddOrReplace("SharedMemoryName", "MYSQL");
            DbPairs.AddOrReplace("oldsyntax", "false");
            DbPairs.AddOrReplace("ConnectionTimeout", "15");
            DbPairs.AddOrReplace("DefaultCommandTimeout", "30");
            DbPairs.AddOrReplace("uid", "root");
            DbPairs.AddOrReplace("pwd", "password");
            DbPairs.AddOrReplace("PersistSecurityInfo", "false");
            DbPairs.AddOrReplace("CertificateFile", "");
            DbPairs.AddOrReplace("CertificatePassword", "");
            DbPairs.AddOrReplace("CertificateStoreLocation", "");
            DbPairs.AddOrReplace("CertificateThumbprint", "");

        }





  
  
  /*
  
  日期时间能否为零，默认 false								-- AllowZeroDateTime
  为零的日期时间是否转化为 DateTime.MinValue，默认 false		-- ConvertZeroDateTime
  是否启用助手，会影响数据库性能，默认 false					-- UseUsageAdvisor/usage advisor
同一时间能缓存几条存储过程，0为禁止，默认 25				-- ProcedureCacheSize/procedure cache/procedurecache
是否启用性能监视，默认 false								-- UsePerformanceMonitor/userperfmon/perfmon
是否忽略 Prepare() 调用，默认 true							-- IgnorePrepare
是否检查存储过程体、参数的有效性，默认 true					-- UseProcedureBodies/procedure bodies
是否自动使用活动的连接，默认 true							-- AutoEnlist
是否响应列上元数据的二进制标志，默认 true					-- RespectBinaryFlags
是否将 TINYINT(1) 列视为布尔型，默认 true					-- TreatTinyAsBoolean
是否允许 SQL 中出现用户变量，默认 false						-- AllowUserVariables
会话是否允许交互，默认 false								-- InteractiveSession/interactive
所有服务器函数是否按返回字符串处理，默认 false				-- FunctionsReturnString
是否用受影响的行数替代查找到的行数来返回数据，默认 false	-- UseAffectedRows
是否将 binary(16) 列作为 Guids，默认 false					-- OldGuids
保持 TCP 连接的秒数，默认0，不保持。						-- Keepalive
连接被销毁前在连接池中保持的最少时间（秒）。默认 0			-- ConnectionLifeTime
是否使用线程池，默认 true									-- Pooling
线程池中允许的最少线程数，默认 0							-- MinimumPoolSize/min pool size
线程池中允许的最多线程数，默认 100							-- MaximumPoolSize/max pool size
连接过期后是否自动复位，默认 false							-- ConnectionReset
向服务器请求连接所使用的字符集，默认：无					-- CharacterSet/charset
binary blobs 是否按 utf8 对待，默认 false					-- TreatBlobsAsUTF8
列的匹配模式，一旦匹配将按 utf8 处理，默认：无				-- BlobAsUTF8IncludePattern
是否启用 SSL 连接模式，默认：MySqlSslMode.None				-- SslMode

        */

    }
}
