代码贡献必须符合一下要求:<br/>
1.API 代码提交<br/>
2.对应 API 有 table model<br/>
3.对应 API 有 test case<br/>
4.对应 API 有 sql 脚本&数据 (如数据涉及隐私等敏感信息,可发送 liumeng0403@163.com ,可保证 test case 所需数据的保密 )<br/>
6.说明 增加 API 的意图,简要说明即可<br/>
如上,我可能接受代码提交,也可能驳回代码提交,并且会给出原因说明.<br/>
<br/>
table model 如下:<br/>
    /*<br/>
     * CREATE TABLE `addressinfo` (<br/>
     * `Id` char(36) NOT NULL,<br/>
     * `CreatedOn` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),<br/>
     * `ContactName` longtext,<br/>
     * `ContactPhone` longtext,<br/>
     * `DetailAddress` longtext,<br/>
     * `IsDefault` bit(1) NOT NULL,<br/>
     * `UserId` char(36) NOT NULL,<br/>
     * PRIMARY KEY (`Id`)<br/>
     * ) ENGINE=InnoDB DEFAULT CHARSET=utf8<br/>
     */<br/>
    [Table("AddressInfo")]<br/>
    public class AddressInfo<br/>
    {<br/>
        public Guid Id { get; set; }<br/>
        public DateTime CreatedOn { get; set; }<br/>
        public string ContactName { get; set; }<br/>       
        public string ContactPhone { get; set; }<br/>        
        public string DetailAddress { get; set; }<br/>
        public bool IsDefault { get; set; }<br/>
        public Guid UserId { get; set; }<br/>
    }<br/>
<br/>    
test case 如下:<br/>
    public class _07_JoinTest : TestBase<br/>
    {<br/>
        [Fact]<br/>
        public async Task TwoJoinTest()<br/>
        {<br/>
            var xx2 = "";<br/>
<br/>
            var res2 = await Conn.OpenDebug()<br/>
                .Joiner<Agent, AgentInventoryRecord>(out var agent2, out var record2)<br/>
                .From(() => agent2)<br/>
                .InnerJoin(() => record2)<br/>
                .On(() => agent2.Id == record2.AgentId)<br/>
                .Where(() => record2.CreatedOn >= WhereTest.CreatedOn)<br/>
                .QueryListAsync(() => new AgentVM<br/>
                {<br/>
                    nn = agent2.PathId,<br/>
                    yy = record2.Id,<br/>
                    xx = agent2.Id,<br/>
                    zz = agent2.Name,<br/>
                    mm = record2.LockedCount<br/>
                });<br/>
<br/>                
            var tuple2 = (XDebug.SQL, XDebug.Parameters);<br/>
        }<br/>
    }<br/>
<br/>    
    sql 数据要求 如下:<br/> 
    需要可以让 test case 重现运行,以保证类库的稳定性,可将sql 数据 以脚本的形式 发送至 邮箱 liumeng0403@163.com  .<br/>
    CREATE TABLE `addressinfo` (<br/>
      `Id` char(36) NOT NULL,<br/>
      `CreatedOn` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),<br/>
      `ContactName` longtext,<br/>
      `ContactPhone` longtext,<br/>
      `DetailAddress` longtext,<br/>
      `IsDefault` bit(1) NOT NULL,<br/>
      `UserId` char(36) NOT NULL,<br/>
      PRIMARY KEY (`Id`)<br/>
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8;<br/>
<br/>    
    LOCK TABLES `addressinfo` WRITE;<br/>
    INSERT INTO `addressinfo` (`Id`, `CreatedOn`, `ContactName`, `ContactPhone`, `DetailAddress`, `IsDefault`, `UserId`)<br/> 
    VALUES ('64e4ec72-aa20-4806-af5a-0165529e81f1',<br/>
            '2018-08-19 14:37:24.362872','我的姓名',<br/>
            '131060228yy',<br/>
            '测试地址',<br/>
            '\0',<br/>
            '08d6036c-658a-e508-9fb5-9f30caac1308'),<br/>
           ('6f390324-2c07-40cf-90ca-0165569461b1',<br/>
           '2018-08-20 09:04:49.609680',<br/>
           '张x',<br/>
           '159061370xx',<br/>
           '上海xx',<br/>
           '\1',<br/>
           '08d6036c-66c8-7c2c-83b0-725f93ff8137'),<br/>
           ('6f64ba15-76cc-4640-ad3e-016556c4eabe',<br/>
           '2018-08-20 09:57:50.400067',<br/>
           '11',<br/>
           '18888888888',<br/>
           '999',<br/>
           '\0',<br/>
           '08d6036c-728c-8fbe-f89d-0881dcfe2551'),<br/>
           ('8a4cf725-504e-434e-824e-016557896355',<br/>
           '2018-08-20 13:32:26.349516',<br/>
           '2',<br/>
           '13333333333',<br/>
           '333',<br/>
           '\1',<br/>
           '08d6036c-728c-8fbe-f89d-0881dcfe2551');<br/>
    UNLOCK TABLES;<br/>
    
    
