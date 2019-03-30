namespace MyDAL
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

        /*Text 类型*/
        /// <summary>
        /// 保存固定长度的字符串（可包含字母、数字以及特殊字符）。最多 255 个字符。
        /// </summary>
        MySQL_Char,
        /// <summary>
        /// 保存可变长度的字符串（可包含字母、数字以及特殊字符）。最多 255 个字符。如果值的长度大于 255，则被转换为 TEXT 类型。
        /// </summary>
        MySQL_VarChar,
        /// <summary>
        /// 存放最大长度为 255 个字符的字符串。
        /// </summary>
        MySQL_TinyText,
        /// <summary>
        /// 存放最大长度为 65,535 个字符的字符串。
        /// </summary>
        MySQL_Text,
        /// <summary>
        /// 用于 BLOBs (Binary Large OBjects)。存放最多 65,535 字节的数据。
        /// </summary>
        MySQL_Blob,
        /// <summary>
        /// 存放最大长度为 16,777,215 个字符的字符串。
        /// </summary>
        MySQL_MediumText,
        /// <summary>
        /// 用于 BLOBs (Binary Large OBjects)。存放最多 16,777,215 字节的数据。
        /// </summary>
        MySQL_MediumBlob,
        /// <summary>
        /// 存放最大长度为 4,294,967,295 个字符的字符串。
        /// </summary>
        MySQL_LongText,
        /// <summary>
        /// 用于 BLOBs (Binary Large OBjects)。存放最多 4,294,967,295 字节的数据。
        /// </summary>
        MySQL_LongBlob,
        /// <summary>
        /// 可以在 ENUM 列表中列出最大 65535 个值。注释：可以按照此格式输入可能的值：ENUM('X','Y','Z')
        /// </summary>
        MySQL_Enum,
        /// <summary>
        /// SET 最多只能包含 64 个列表项，不过 SET 可存储一个以上的值。
        /// </summary>
        MySQL_Set,
        /*Number 类型*/
        /// <summary>
        /// -128 到 127 常规。0 到 255 无符号*。
        /// </summary>
        MySQL_TinyInt,
        /// <summary>
        /// -32768 到 32767 常规。0 到 65535 无符号*。
        /// </summary>
        MySQL_SmallInt,
        /// <summary>
        /// -8388608 到 8388607 普通。0 to 16777215 无符号*。
        /// </summary>
        MySQL_MediumInt,
        /// <summary>
        /// -2147483648 到 2147483647 常规。0 到 4294967295 无符号*。
        /// </summary>
        MySQL_Int,
        /// <summary>
        /// -9223372036854775808 到 9223372036854775807 常规。0 到 18446744073709551615 无符号*。
        /// </summary>
        MySQL_BigInt,
        /// <summary>
        /// 带有浮动小数点的小数字。float(size,d) -- size - 最大位数,d - 小数点最大位数。
        /// </summary>
        MySQL_Float,
        /// <summary>
        /// 带有浮动小数点的大数字。double(size,d) -- size - 最大位数,d - 小数点最大位数。
        /// </summary>
        MySQL_Double,
        /// <summary>
        /// 作为字符串存储的 DOUBLE 类型，允许固定的小数点。
        /// </summary>
        MySQL_Decimal,
        /*Date 类型*/
        /// <summary>
        /// 日期。格式：YYYY-MM-DD  注释：支持的范围是从 '1000-01-01' 到 '9999-12-31'
        /// </summary>
        MySQL_Date,
        /// <summary>
        /// *日期和时间的组合。格式：YYYY-MM-DD HH:MM:SS  注释：支持的范围是从 '1000-01-01 00:00:00' 到 '9999-12-31 23:59:59'
        /// </summary>
        MySQL_DateTime,
        /// <summary>
        /// *时间戳。TIMESTAMP 值使用 Unix 纪元至今的描述来存储。格式：YYYY-MM-DD HH:MM:SS  注释：支持的范围是从 '1970-01-01 00:00:01' UTC 到 '2038-01-09 03:14:07' UTC
        /// </summary>
        MySQL_TimeStamp,
        /// <summary>
        /// 时间。格式：HH:MM:SS 注释：支持的范围是从 '-838:59:59' 到 '838:59:59' 
        /// </summary>
        MySQL_Time,
        /// <summary>
        /// 2 位或 4 位格式的年。注释：4 位格式所允许的值：1901 到 2155。2 位格式所允许的值：70 到 69，表示从 1970 到 2069。
        /// </summary>
        MySQL_Year,
        /*其他数据类型*/
        /// <summary>
        /// [1,64]bit
        /// </summary>
        MySQL_Bit,
        /// <summary>
        /// 
        /// </summary>
        MySQL_Binary,
        /// <summary>
        /// 
        /// </summary>
        MySQL_VarBinary,
        /// <summary>
        /// [0,255]byte
        /// </summary>
        MySQL_TinyBlob,
        
        /*
         * SQL Server
         */

        /*Character 字符串*/
        /// <summary>
        /// 固定长度的字符串。最多 8,000 个字符。
        /// </summary>
        SqlServer_Char,
        /// <summary>
        /// 可变长度的字符串。最多 8,000 个字符。varchar(max) -- 最多 1,073,741,824 个字符。
        /// </summary>
        SqlServer_VarChar,
        /// <summary>
        /// 可变长度的字符串。最多 2GB 字符数据。
        /// </summary>
        SqlServer_Text,
        /*Unicode 字符串*/
        /// <summary>
        /// 固定长度的 Unicode 数据。最多 4,000 个字符。
        /// </summary>
        SqlServer_NChar,
        /// <summary>
        /// 可变长度的 Unicode 数据。最多 4,000 个字符。nvarchar(max) -- 最多 536,870,912 个字符。
        /// </summary>
        SqlServer_NVarChar,
        /// <summary>
        /// 可变长度的 Unicode 数据。最多 2GB 字符数据。
        /// </summary>
        SqlServer_NText,
        /*Binary 类型*/
        /// <summary>
        /// 允许 0、1 或 NULL
        /// </summary>
        SqlServer_Bit,
        /// <summary>
        /// 固定长度的二进制数据。最多 8,000 字节。
        /// </summary>
        SqlServer_Binary,
        /// <summary>
        /// 可变长度的二进制数据。最多 8,000 字节。varbinary(max) -- 最多 2GB 字节。
        /// </summary>
        SqlServer_VarBinary,
        /// <summary>
        /// 可变长度的二进制数据。最多 2GB。
        /// </summary>
        SqlServer_Image,
        /*Number 类型*/
        /// <summary>
        /// 允许从 0 到 255 的所有数字。
        /// </summary>
        SqlServer_TinyInt,
        /// <summary>
        /// 允许从 -32,768 到 32,767 的所有数字。
        /// </summary>
        SqlServer_SmallInt,
        /// <summary>
        /// 允许从 -2,147,483,648 到 2,147,483,647 的所有数字。
        /// </summary>
        SqlServer_Int,
        /// <summary>
        /// 允许介于 -9,223,372,036,854,775,808 和 9,223,372,036,854,775,807 之间的所有数字。
        /// </summary>
        SqlServer_BigInt,
        /// <summary>
        /// 固定精度和比例的数字。允许从 -10^38 +1 到 10^38 -1 之间的数字。decimal(p,s) -- p 必须是 1 到 38 之间的值,默认是 18 -- s 必须是 0 到 p 之间的值,默认是 0。
        /// </summary>
        SqlServer_Decimal,
        /// <summary>
        /// 固定精度和比例的数字。允许从 -10^38 +1 到 10^38 -1 之间的数字。numeric(p,s) -- p 必须是 1 到 38 之间的值,默认是 18 -- s 必须是 0 到 p 之间的值,默认是 0。
        /// </summary>
        SqlServer_Numeric,
        /// <summary>
        /// 介于 -214,748.3648 和 214,748.3647 之间的货币数据。
        /// </summary>
        SqlServer_SmallMoney,
        /// <summary>
        /// 介于 -922,337,203,685,477.5808 和 922,337,203,685,477.5807 之间的货币数据。
        /// </summary>
        SqlServer_Money,
        /// <summary>
        /// 从 -1.79E + 308 到 1.79E + 308 的浮动精度数字数据。float(24) 保存 4 字节，而 float(53) 保存 8 字节。n 的默认值是 53。 
        /// </summary>
        SqlServer_Float,
        /// <summary>
        /// 从 -3.40E + 38 到 3.40E + 38 的浮动精度数字数据。
        /// </summary>
        SqlServer_Real,
        /*Date 类型*/
        /// <summary>
        /// 从 1753 年 1 月 1 日 到 9999 年 12 月 31 日，精度为 3.33 毫秒。
        /// </summary>
        SqlServer_DateTime,
        /// <summary>
        /// 从 1753 年 1 月 1 日 到 9999 年 12 月 31 日，精度为 100 纳秒。DateTime2(N)表示了秒钟的精度，N=0到7，表示精确到秒钟后的几位数。
        /// </summary>
        SqlServer_DateTime2,
        /// <summary>
        /// 从 1900 年 1 月 1 日 到 2079 年 6 月 6 日，精度为 1 分钟。
        /// </summary>
        SqlServer_SmallDateTime,
        /// <summary>
        /// 仅存储日期。从 0001 年 1 月 1 日 到 9999 年 12 月 31 日。
        /// </summary>
        SqlServer_Date,
        /// <summary>
        /// 仅存储时间。精度为 100 纳秒。
        /// </summary>
        SqlServer_Time,
        /// <summary>
        /// 从 1753 年 1 月 1 日 到 9999 年 12 月 31 日，精度为 100 纳秒。外加时区偏移。
        /// </summary>
        SqlServer_DateTimeOffset,
        /// <summary>
        /// 存储唯一的数字，每当创建或修改某行时，该数字会更新。timestamp 基于内部时钟，不对应真实时间。每个表只能有一个 timestamp 变量。
        /// </summary>
        SqlServer_TimeStamp,
        /*其他数据类型*/
        /// <summary>
        /// 存储最多 8,000 字节不同数据类型的数据，除了 text、ntext 以及 timestamp。
        /// </summary>
        SqlServer_Sql_Variant,
        /// <summary>
        /// 存储全局标识符 (GUID)。
        /// </summary>
        SqlServer_UniqueIdentifier,
        /// <summary>
        /// 存储 XML 格式化数据。最多 2GB。
        /// </summary>
        SqlServer_Xml,
        /// <summary>
        /// 存储对用于数据库操作的指针的引用。
        /// </summary>
        SqlServer_Cursor,
        /// <summary>
        /// 存储结果集，供稍后处理。
        /// </summary>
        SqlServer_Table

    }
}
