namespace HPC.DAL
{
    /// <summary>
    /// Model-Property 对应的 Table-Column 类型
    /// </summary>
    public enum ParamTypeEnum
    {
        /// <summary>
        /// 默认
        /// </summary>
        None,

        /*
         * MySQL
         */

        /*Binary Large OBjects 类型*/

        /// <summary>
        /// [0,255]byte
        /// </summary>
        TinyBlob_MySQL,
        /// <summary>
        /// 存放最大长度为 255 个字符的字符串。
        /// </summary>
        TinyText_MySQL,
        /// <summary>
        /// 用于 BLOBs (Binary Large OBjects)。存放最多 65,535 字节的数据。
        /// </summary>
        Blob_MySQL,
        /// <summary>
        /// MySQL: 存放最大长度为 65,535 个字符的字符串; 
        /// SqlServer: 可变长度的字符串, 最多 2GB 字符数据.
        /// </summary>
        Text_MySQL_SqlServer,
        /// <summary>
        /// 用于 BLOBs (Binary Large OBjects)。存放最多 16,777,215 字节的数据。
        /// </summary>
        MediumBlob_MySQL,
        /// <summary>
        /// 存放最大长度为 16,777,215 个字符的字符串。
        /// </summary>
        MediumText_MySQL,
        /// <summary>
        /// 用于 BLOBs (Binary Large OBjects)。存放最多 4,294,967,295 字节的数据。
        /// </summary>
        LongBlob_MySQL,
        /// <summary>
        /// 存放最大长度为 4,294,967,295 个字符的字符串。
        /// </summary>
        LongText_MySQL,

        /*Text 类型*/
        /// <summary>
        /// MySQL: 保存固定长度的字符串（可包含字母、数字以及特殊字符）, 最多 255 个字符; 
        /// SqlServer: 固定长度的字符串, 最多 8,000 个字符. 
        /// </summary>
        Char_MySQL_SqlServer,
        /// <summary>
        /// MySQL: 保存可变长度的字符串（可包含字母、数字以及特殊字符）, 最多 255 个字符, 如果值的长度大于 255，则被转换为 TEXT 类型; 
        /// SqlServer: 可变长度的字符串, 最多 8,000 个字符, varchar(max) -- 最多 1,073,741,824 个字符. 
        /// </summary>
        VarChar_MySQL_SqlServer,
        /// <summary>
        /// 可以在 ENUM 列表中列出最大 65535 个值。注释：可以按照此格式输入可能的值：ENUM('X','Y','Z')
        /// </summary>
        Enum_MySQL,
        /// <summary>
        /// SET 最多只能包含 64 个列表项，不过 SET 可存储一个以上的值。
        /// </summary>
        Set_MySQL,
        /*Number 类型*/
        /// <summary>
        /// MySQL: -128 到 127 常规, 0 到 255 无符号*; 
        /// SqlServer: 允许从 0 到 255 的所有数字; 
        /// </summary>
        TinyInt_MySQL_SqlServer,
        /// <summary>
        /// MySQL: -32768 到 32767 常规, 0 到 65535 无符号*; 
        /// SqlServer: 允许从 -32,768 到 32,767 的所有数字. 
        /// </summary>
        SmallInt_MySQL_SqlServer,
        /// <summary>
        /// -8388608 到 8388607 普通。0 to 16777215 无符号*。
        /// </summary>
        MediumInt_MySQL,
        /// <summary>
        /// MySQL: -2147483648 到 2147483647 常规, 0 到 4294967295 无符号*; 
        /// SqlServer: 允许从 -2,147,483,648 到 2,147,483,647 的所有数字. 
        /// </summary>
        Int_MySQL_SqlServer,
        /// <summary>
        /// MySQL: -9223372036854775808 到 9223372036854775807 常规, 0 到 18446744073709551615 无符号*; 
        /// SqlServer: 允许介于 -9,223,372,036,854,775,808 和 9,223,372,036,854,775,807 之间的所有数字. 
        /// </summary>
        BigInt_MySQL_SqlServer,
        /// <summary>
        /// MySQL: 带有浮动小数点的小数字, float(size,d) -- size - 最大位数,d - 小数点最大位数; 
        /// SqlServer: 从 -1.79E + 308 到 1.79E + 308 的浮动精度数字数据, float(24) 保存 4 字节, 而 float(53) 保存 8 字节, n 的默认值是 53. 
        /// </summary>
        Float_MySQL_SqlServer,
        /// <summary>
        /// 带有浮动小数点的大数字。double(size,d) -- size - 最大位数,d - 小数点最大位数。
        /// </summary>
        Double_MySQL,
        /// <summary>
        /// MySQL: 作为字符串存储的 DOUBLE 类型, 允许固定的小数点; 
        /// SqlServer: 固定精度和比例的数字, 允许从 -10^38 +1 到 10^38 -1 之间的数字, decimal(p,s) -- p 必须是 1 到 38 之间的值,默认是 18 -- s 必须是 0 到 p 之间的值,默认是 0. 
        /// </summary>
        Decimal_MySQL_SqlServer,
        /*Date 类型*/
        /// <summary>
        /// MySQL: 日期, 格式：YYYY-MM-DD , 支持的范围是从 '1000-01-01' 到 '9999-12-31'; 
        /// SqlServer: 仅存储日期, 从 0001 年 1 月 1 日 到 9999 年 12 月 31 日. 
        /// </summary>
        Date_MySQL_SqlServer,
        /// <summary>
        /// MySQL: *日期和时间的组合, 格式：YYYY-MM-DD HH:MM:SS  注释：支持的范围是从 '1000-01-01 00:00:00.000000' 到 '9999-12-31 23:59:59.999999' ; 
        /// SqlServer: 从 1753 年 1 月 1 日 到 9999 年 12 月 31 日，精度为 3.33 毫秒. 
        /// </summary>
        DateTime_MySQL_SqlServer,
        /// <summary>
        /// MySQL: *时间戳 , TIMESTAMP 值使用 Unix 纪元至今的描述来存储, 格式：YYYY-MM-DD HH:MM:SS , 支持的范围是从 '1970-01-01 00:00:01.000000' 到 '2038-01-19 03:14:07.999999' UTC ; 
        /// SqlServer: 存储唯一的数字，每当创建或修改某行时，该数字会更新, timestamp 基于内部时钟，不对应真实时间, 每个表只能有一个 timestamp 变量. 
        /// </summary>
        TimeStamp_MySQL_SqlServer,
        /// <summary>
        /// MySQL: 时间, 格式 HH:MM:SS , 支持的范围是从 '-838:59:59' 到 '838:59:59' ; 
        /// SqlServer: 仅存储时间。精度为 100 纳秒. 
        /// </summary>
        Time_MySQL_SqlServer,
        /// <summary>
        /// 2 位或 4 位格式的年。注释：4 位格式所允许的值：1901 到 2155。2 位格式所允许的值：70 到 69，表示从 1970 到 2069。
        /// </summary>
        Year_MySQL,
        /*其他数据类型*/
        /// <summary>
        /// MySQL: [1,64]bit ; 
        /// SqlServer: 允许 0、1 或 NULL . 
        /// </summary>
        Bit_MySQL_SqlServer,
        /// <summary>
        /// MySQL: binary(n) 表示 固定长度 n 个二进制字节 ; 
        /// SqlServer: 固定长度的二进制数据, 最多 8,000 字节 . 
        /// </summary>
        Binary_MySQL_SqlServer,
        /// <summary>
        /// MySQL: binary(n) 表示 可变长度 n 个二进制字节 ; 
        /// SqlServer: 可变长度的二进制数据, 最多 8,000 字节, varbinary(max) -- 最多 2GB 字节 . 
        /// </summary>
        VarBinary_MySQL_SqlServer,

        /*
         * SQL Server
         */

        /*Character 字符串*/
        /*Unicode 字符串*/
        /// <summary>
        /// 固定长度的 Unicode 数据。最多 4,000 个字符。
        /// </summary>
        NChar_SqlServer,
        /// <summary>
        /// 可变长度的 Unicode 数据。最多 4,000 个字符。nvarchar(max) -- 最多 536,870,912 个字符。
        /// </summary>
        NVarChar_SqlServer,
        /// <summary>
        /// 可变长度的 Unicode 数据。最多 2GB 字符数据。
        /// </summary>
        NText_SqlServer,
        /*Binary 类型*/
        /// <summary>
        /// 可变长度的二进制数据。最多 2GB。
        /// </summary>
        Image_SqlServer,
        /*Number 类型*/
        /// <summary>
        /// 固定精度和比例的数字。允许从 -10^38 +1 到 10^38 -1 之间的数字。numeric(p,s) -- p 必须是 1 到 38 之间的值,默认是 18 -- s 必须是 0 到 p 之间的值,默认是 0。
        /// </summary>
        Numeric_SqlServer,
        /// <summary>
        /// 介于 -214,748.3648 和 214,748.3647 之间的货币数据。
        /// </summary>
        SmallMoney_SqlServer,
        /// <summary>
        /// 介于 -922,337,203,685,477.5808 和 922,337,203,685,477.5807 之间的货币数据。
        /// </summary>
        Money_SqlServer,
        /// <summary>
        /// 从 -3.40E + 38 到 3.40E + 38 的浮动精度数字数据。
        /// </summary>
        Real_SqlServer,
        /*Date 类型*/
        /// <summary>
        /// 从 1753 年 1 月 1 日 到 9999 年 12 月 31 日，精度为 100 纳秒。DateTime2(N)表示了秒钟的精度，N=0到7，表示精确到秒钟后的几位数。
        /// </summary>
        DateTime2_SqlServer,
        /// <summary>
        /// 从 1900 年 1 月 1 日 到 2079 年 6 月 6 日，精度为 1 分钟。
        /// </summary>
        SmallDateTime_SqlServer,
        /// <summary>
        /// 从 1753 年 1 月 1 日 到 9999 年 12 月 31 日，精度为 100 纳秒。外加时区偏移。
        /// </summary>
        DateTimeOffset_SqlServer,
        /*其他数据类型*/
        /// <summary>
        /// 存储最多 8,000 字节不同数据类型的数据，除了 text、ntext 以及 timestamp。
        /// </summary>
        Sql_Variant_SqlServer,
        /// <summary>
        /// 存储全局标识符 (GUID)。
        /// </summary>
        UniqueIdentifier_SqlServer,
        /// <summary>
        /// 存储 XML 格式化数据。最多 2GB。
        /// </summary>
        Xml_SqlServer,
        /// <summary>
        /// 存储对用于数据库操作的指针的引用。
        /// </summary>
        Cursor_SqlServer,
        /// <summary>
        /// 存储结果集，供稍后处理。
        /// </summary>
        Table_SqlServer

    }
}
