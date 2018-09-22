代码贡献必须符合一下要求:
1.API 代码提交
2.对应 API 有 table model
3.对应 API 有 test case
4.对应 API 有 sql 脚本&数据 (如数据涉及隐私等敏感信息,可发送 liumeng0403@163.com ,可保证 test case 所需数据的保密 )
6.说明 增加 API 的意图,简要说明即可
如上,我可能接受代码提交,也可能驳回代码提交,并且会给出原因说明.

table model 如下:
    /*
     * CREATE TABLE `addressinfo` (
     * `Id` char(36) NOT NULL,
     * `CreatedOn` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
     * `ContactName` longtext,
     * `ContactPhone` longtext,
     * `DetailAddress` longtext,
     * `IsDefault` bit(1) NOT NULL,
     * `UserId` char(36) NOT NULL,
     * PRIMARY KEY (`Id`)
     * ) ENGINE=InnoDB DEFAULT CHARSET=utf8
     */
    [Table("AddressInfo")]
    public class AddressInfo
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ContactName { get; set; }       
        public string ContactPhone { get; set; }        
        public string DetailAddress { get; set; }
        public bool IsDefault { get; set; }
        public Guid UserId { get; set; }
    }
    
test case 如下:
    public class _07_JoinTest : TestBase
    {
        [Fact]
        public async Task TwoJoinTest()
        {
            var xx2 = "";

            var res2 = await Conn.OpenDebug()
                .Joiner<Agent, AgentInventoryRecord>(out var agent2, out var record2)
                .From(() => agent2)
                .InnerJoin(() => record2)
                .On(() => agent2.Id == record2.AgentId)
                .Where(() => record2.CreatedOn >= WhereTest.CreatedOn)
                .QueryListAsync(() => new AgentVM
                {
                    nn = agent2.PathId,
                    yy = record2.Id,
                    xx = agent2.Id,
                    zz = agent2.Name,
                    mm = record2.LockedCount
                });
                
            var tuple2 = (XDebug.SQL, XDebug.Parameters);
        }
    }
    
    sql 数据要求 如下: 
    需要可以让 test case 重现运行,以保证类库的稳定性,可将sql 数据 以脚本的形式 发送至 邮箱 liumeng0403@163.com  .
    CREATE TABLE `addressinfo` (
      `Id` char(36) NOT NULL,
      `CreatedOn` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
      `ContactName` longtext,
      `ContactPhone` longtext,
      `DetailAddress` longtext,
      `IsDefault` bit(1) NOT NULL,
      `UserId` char(36) NOT NULL,
      PRIMARY KEY (`Id`)
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8;
    LOCK TABLES `addressinfo` WRITE;
    /*!40000 ALTER TABLE `addressinfo` DISABLE KEYS */;
    INSERT INTO `addressinfo` (`Id`, `CreatedOn`, `ContactName`, `ContactPhone`, `DetailAddress`, `IsDefault`, `UserId`) 
    VALUES ('64e4ec72-aa20-4806-af5a-0165529e81f1','2018-08-19 14:37:24.362872','我的姓名','13106022851','测试地址','','08d6036c-658a-e508-9fb5-9f30caac1308'),
           ('6f390324-2c07-40cf-90ca-0165569461b1','2018-08-20 09:04:49.609680','张x','159061370xx','上海xx','','08d6036c-66c8-7c2c-83b0-725f93ff8137'),
           ('6f64ba15-76cc-4640-ad3e-016556c4eabe','2018-08-20 09:57:50.400067','11','18888888888','999','\0','08d6036c-728c-8fbe-f89d-0881dcfe2551'),
           ('8a4cf725-504e-434e-824e-016557896355','2018-08-20 13:32:26.349516','2','13333333333','333','','08d6036c-728c-8fbe-f89d-0881dcfe2551');
    /*!40000 ALTER TABLE `addressinfo` ENABLE KEYS */;
    UNLOCK TABLES;
    
    
