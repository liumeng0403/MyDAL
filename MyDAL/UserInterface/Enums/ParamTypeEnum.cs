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
        MySQL_LongBlob

    }
}
