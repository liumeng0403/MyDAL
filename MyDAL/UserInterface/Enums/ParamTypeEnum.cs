namespace MyDAL
{
    public enum ParamTypeEnum
    {
        None,

        /*
         * MySQL
         */

        /*整数类型*/
        /// <summary>
        /// 1byte
        /// </summary>
        MySQL_TinyInt,
        /// <summary>
        /// 2byte
        /// </summary>
        MySQL_SmallInt,
        /// <summary>
        /// 3byte
        /// </summary>
        MySQL_MediumInt,
        /// <summary>
        /// 4byte
        /// </summary>
        MySQL_Int,
        /// <summary>
        /// 8byte
        /// </summary>
        MySQL_BigInt,
        /*浮点数类型和定点数类型*/
        /// <summary>
        /// 4byte -- [0,7]位小数
        /// </summary>
        MySQL_Float,
        /// <summary>
        /// 8byte
        /// </summary>
        MySQL_Double,
        /// <summary>
        /// 
        /// </summary>
        MySQL_Decimal,
        /*日期与时间类型*/
        /// <summary>
        /// 1byte -- yyyy -- [1901,2155]
        /// </summary>
        MySQL_Year,
        /// <summary>
        /// 3byte -- HH:mm:ss
        /// </summary>
        MySQL_Time,
        /// <summary>
        /// 3byte -- yyyy-MM-dd -- [1000-01-01,9999-12-31]
        /// </summary>
        MySQL_Date,
        /// <summary>
        /// 8byte -- yyyy-MM-dd HH:mm:ss -- [1000-01-01 00:00:00,9999-12-31 23:59:59]
        /// </summary>
        MySQL_DateTime,
        /// <summary>
        /// 8byte -- yyyy-MM-dd HH:mm:ss -- [1070,2037]
        /// </summary>
        MySQL_TimeStamp,
        /*文本字符串类型*/
        /// <summary>
        /// [0,255]byte
        /// </summary>
        MySQL_Char,
        /// <summary>
        /// [0,65535]byte
        /// </summary>
        MySQL_VarChar,
        /// <summary>
        /// [0,255]byte
        /// </summary>
        MySQL_TinyText,
        /// <summary>
        /// [0,65535]byte
        /// </summary>
        MySQL_Text,
        /// <summary>
        /// [0,16777215]byte
        /// </summary>
        MySQL_MediumText,
        /// <summary>
        /// [0,4294967295]byte
        /// </summary>
        MySQL_LongText,
        /// <summary>
        /// [1,2]byte -- [0,65535]值
        /// </summary>
        MySQL_Enum,
        /// <summary>
        /// [1,8]byte -- [0,64]成员
        /// </summary>
        MySQL_Set,
        /*二进制字符串类型*/
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
        /// <summary>
        /// [0,65535]byte
        /// </summary>
        MySQL_Blob,
        /// <summary>
        /// [0,16777215]byte
        /// </summary>
        MySQL_MediumBlob,
        /// <summary>
        /// [0,4294967295]byte
        /// </summary>
        MySQL_LongBlob,

        /*
         * SQL Server
         */

        /*整数数据类型*/
        /// <summary>
        /// 4 个字节  -2的31次方 （-2 ，147 ，483 ，648） 到2的31次方-1 （2 ，147 ，483，647）
        /// </summary>
        SqlServer_Int,
        /// <summary>
        /// 2 个字节  -2的15次方（ -32， 768） 到2的15次方-1（ 32 ，767 ）
        /// </summary>
        SqlServer_SmallInt,
        /// <summary>
        /// 1 个字节  0 到255 
        /// </summary>
        SqlServer_TinyInt,
        /// <summary>
        /// 8个字节  -2^63 （-9 ，223， 372， 036， 854， 775， 807） 到2^63-1（ 9， 223， 372， 036 ，854 ，775， 807）
        /// </summary>
        SqlServer_BigInt,
        /*浮点数据类型*/
        /// <summary>
        /// 4 个字节   可精确到第7 位小数，其范围为从-3.40E -38 到3.40E +38
        /// </summary>
        SqlServer_Real,
        /// <summary>
        /// 8 个字节  可精确到第15 位小数，其范围为从-1.79E -308 到1.79E +308
        /// </summary>
        SqlServer_Float,
        /// <summary>
        /// 可以用2 到17 个字节来存储从-10的38次方-1 到10的38次方-1 之间的数值
        /// </summary>
        SqlServer_Decimal,
        /// <summary>
        /// NUMERIC数据类型与DECIMAL数据类型完全相同
        /// </summary>
        SqlServer_Numeric,
        /// <summary>
        /// 
        /// </summary>
        SqlServer_SmallMoney,
        /// <summary>
        /// 
        /// </summary>
        SqlServer_Money,
        /*二进制数据类型*/
        /// <summary>
        /// 
        /// </summary>
        SqlServer_Binary,
        /// <summary>
        /// 
        /// </summary>
        SqlServer_VarBinary,
        /*逻辑数据类型*/
        /// <summary>
        /// 1 个字节
        /// </summary>
        SqlServer_Bit,
        /*字符数据类型*/
        /// <summary>
        /// 
        /// </summary>
        SqlServer_Char,
        /// <summary>
        /// 
        /// </summary>
        SqlServer_NChar,
        /// <summary>
        /// 
        /// </summary>
        SqlServer_VarChar,
        /// <summary>
        /// 
        /// </summary>
        SqlServer_NVarChar,
        /*文本和图形数据类型*/
        SqlServer_Text,
        SqlServer_NText,
        SqlServer_Image,
        /*日期和时间数据类型*/
        SqlServer_SmallDateTime,
        SqlServer_DateTime,
        SqlServer_TimeStamp,
        /*Other*/
        SqlServer_UniqueIdentifier

    }
}
