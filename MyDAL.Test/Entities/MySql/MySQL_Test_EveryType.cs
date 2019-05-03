using System;
using System.Collections.Generic;

namespace MyDAL.Test.Entities.MySql
{
    /*
     * create table MySQL_Test_EveryType
     * (
     *  	MySQL_Char char not null,
     *  	MySQL_Char_Null char null,
     *  	MySQL_VarChar varchar(50) not null,
     *  	MySQL_VarChar_Null varchar(50) null,
     *  	MySQL_TinyText tinytext not null,
     *  	MySQL_TinyText_Null tinytext null,
     *  	MySQL_Text text not null,
     *  	MySQL_Text_Null text null,
     *  	MySQL_Blob blob not null,
     *  	MySQL_Blob_Null blob null,
     *  	MySQL_MediumText mediumtext not null,
     *  	MySQL_MediumText_Null mediumtext null,
     *  	MySQL_MediumBlob mediumblob not null,
     *  	MySQL_MediumBlob_Null mediumblob null,
     *  	MySQL_LongText longtext not null,
     *  	MySQL_LongText_Null longtext null,
     *  	MySQL_LongBlob longblob not null,
     *  	MySQL_LongBlob_Null longblob null,
     *  	MySQL_Enum enum('A','B') not null,
     *  	MySQL_Enum_Null enum('A','B') null,
     *  	MySQL_Set_Field set('music','movie','swimming') not null,
     *  	MySQL_Set_Null set('music','movie','swimming') null,
     *  	MySQL_TinyInt tinyint not null,
     *  	MySQL_TinyInt_Null tinyint null,
     *  	MySQL_SmallInt smallint not null,
     *  	MySQL_SmallInt_Null smallint null,
     *  	MySQL_MediumInt mediumint not null,
     *  	MySQL_MediumInt_Null mediumint null,
     *  	MySQL_Int int not null,
     *  	MySQL_Int_Null int null,
     *  	MySQL_BigInt bigint not null,
     *  	MySQL_BigInt_Null bigint null,
     *  	MySQL_Float float not null,
     *  	MySQL_Float_Null float null,
     *  	MySQL_Double double not null,
     *  	MySQL_Double_Null double null,
     *  	MySQL_Decimal decimal not null,
     *  	MySQL_Decimal_Null decimal null,
     *  	MySQL_Date date not null,
     *  	MySQL_Date_Null date null,
     *  	MySQL_DateTime datetime not null,
     *  	MySQL_DateTime_Null datetime null,
     *  	MySQL_TimeStamp timestamp not null,
     *  	MySQL_TimeStamp_Null timestamp null,
     *  	MySQL_Time time not null,
     *  	MySQL_Time_Null time null,
     *  	MySQL_Year year not null,
     *  	MySQL_Year_Null year null,
     *  	MySQL_Bit bit not null,
     *  	MySQL_Bit_Null bit null,
     *  	MySQL_Binary binary not null,
     *  	MySQL_Binary_Null binary null,
     *  	MySQL_VarBinary varbinary(1000) not null,
     *  	MySQL_VarBinary_Null varbinary(1000) null,
     *  	MySQL_TinyBlob tinyblob not null,
     *  	MySQL_TinyBlob_Null tinyblob null
     * );
     */
    [XTable(Name = "MySQL_Test_EveryType")]
    public class MySQL_EveryType
    {
        [XColumn(Name = "MySQL_Char")]
        public string Char { get; set; }

        [XColumn(Name = "MySQL_Char_Null")]
        public string Char_Null { get; set; }

        [XColumn(Name = "MySQL_VarChar")]
        public string VarChar { get; set; }

        [XColumn(Name = "MySQL_VarChar_Null")]
        public string VarChar_Null { get; set; }

        /*--------------------------------------------------------------------------------*/

        [XColumn(Name = "MySQL_TinyText")]
        public string TinyText { get; set; }

        [XColumn(Name = "MySQL_TinyText_Null")]
        public string TinyText_Null { get; set; }

        [XColumn(Name = "MySQL_Text")]
        public string Text { get; set; }

        [XColumn(Name = "MySQL_Text_Null")]
        public string Text_Null { get; set; }

        [XColumn(Name = "MySQL_MediumText")]
        public string MediumText { get; set; }

        [XColumn(Name = "MySQL_MediumText_Null")]
        public string MediumText_Null { get; set; }

        [XColumn(Name = "MySQL_LongText")]
        public string LongText { get; set; }

        [XColumn(Name = "MySQL_LongText_Null")]
        public string LongText_Null { get; set; }

        /*--------------------------------------------------------------------------------*/

        [XColumn(Name = "MySQL_TinyBlob")]
        public byte[] TinyBlob { get; set; }

        [XColumn(Name = "MySQL_TinyBlob_Null")]
        public byte[] TinyBlob_Null { get; set; }

        [XColumn(Name = "MySQL_Blob")]
        public byte[] Blob { get; set; }

        [XColumn(Name = "MySQL_Blob_Null")]
        public byte[] Blob_Null { get; set; }

        [XColumn(Name = "MySQL_MediumBlob")]
        public byte[] MediumBlob { get; set; }

        [XColumn(Name = "MySQL_MediumBlob_Null")]
        public byte[] MediumBlob_Null { get; set; }

        [XColumn(Name = "MySQL_LongBlob")]
        public byte[] LongBlob { get; set; }

        [XColumn(Name = "MySQL_LongBlob_Null")]
        public byte[] LongBlob_Null { get; set; }

        /*--------------------------------------------------------------------------------*/

        [XColumn(Name = "MySQL_Binary")]
        public byte[] Binary { get; set; }

        [XColumn(Name = "MySQL_Binary_Null")]
        public byte[] Binary_Null { get; set; }

        [XColumn(Name = "MySQL_VarBinary")]
        public byte[] VarBinary { get; set; }

        [XColumn(Name = "MySQL_VarBinary_Null")]
        public byte[] VarBinary_Null { get; set; }

        /*--------------------------------------------------------------------------------*/

        [XColumn(Name = "MySQL_Enum")]
        public MySQL_Enum Enum { get; set; }

        [XColumn(Name = "MySQL_Enum_Null")]
        public MySQL_Enum? Enum_Null { get; set; }

        [XColumn(Name = "MySQL_Set_Field")]
        public List<string> Set { get; set; }

        [XColumn(Name = "MySQL_Set_Null")]
        public List<string> Set_Null { get; set; }

        /*--------------------------------------------------------------------------------*/

        [XColumn(Name = "MySQL_TinyInt")]
        public byte TinyInt { get; set; }

        [XColumn(Name = "MySQL_TinyInt_Null")]
        public byte? TinyInt_Null { get; set; }

        /*--------------------------------------------------------------------------------*/

        [XColumn(Name = "MySQL_SmallInt")]
        public short SmallInt { get; set; }

        [XColumn(Name = "MySQL_SmallInt_Null")]
        public short? SmallInt_Null { get; set; }

        /*--------------------------------------------------------------------------------*/

        [XColumn(Name = "MySQL_MediumInt")]
        public int MediumInt { get; set; }

        [XColumn(Name = "MySQL_MediumInt_Null")]
        public int? MediumInt_Null { get; set; }

        [XColumn(Name = "MySQL_Int")]
        public int Int { get; set; }

        [XColumn(Name = "MySQL_Int_Null")]
        public int? Int_Null { get; set; }

        /*--------------------------------------------------------------------------------*/

        [XColumn(Name = "MySQL_BigInt")]
        public long BigInt { get; set; }

        [XColumn(Name = "MySQL_BigInt_Null")]
        public long? BigInt_Null { get; set; }

        /*--------------------------------------------------------------------------------*/

        [XColumn(Name = "MySQL_Float")]
        public float Float { get; set; }

        [XColumn(Name = "MySQL_Float_Null")]
        public float? Float_Null { get; set; }

        /*--------------------------------------------------------------------------------*/

        [XColumn(Name = "MySQL_Double")]
        public double Double { get; set; }

        [XColumn(Name = "MySQL_Double_Null")]
        public double? Double_Null { get; set; }

        /*--------------------------------------------------------------------------------*/

        [XColumn(Name = "MySQL_Decimal")]
        public decimal Decimal { get; set; }

        [XColumn(Name = "MySQL_Decimal_Null")]
        public decimal? Decimal_Null { get; set; }

        /*--------------------------------------------------------------------------------*/

        [XColumn(Name = "MySQL_Date")]
        public DateTime Date { get; set; }

        [XColumn(Name = "MySQL_Date_Null")]
        public DateTime? Date_Null { get; set; }

        [XColumn(Name = "MySQL_DateTime")]
        public DateTime DateTime { get; set; }

        [XColumn(Name = "MySQL_DateTime_Null")]
        public DateTime? DateTime_Null { get; set; }

        [XColumn(Name = "MySQL_TimeStamp")]
        public DateTime TimeStamp { get; set; }

        [XColumn(Name = "MySQL_TimeStamp_Null")]
        public DateTime? TimeStamp_Null { get; set; }

        [XColumn(Name = "MySQL_Year")]
        public DateTime MySQL_Year { get; set; }

        [XColumn(Name = "MySQL_Year_Null")]
        public DateTime? Year_Null { get; set; }

        /*--------------------------------------------------------------------------------*/

        [XColumn(Name = "MySQL_Time")]
        public TimeSpan Time { get; set; }

        [XColumn(Name = "MySQL_Time_Null")]
        public TimeSpan? Time_Null { get; set; }

        /*--------------------------------------------------------------------------------*/

        [XColumn(Name = "MySQL_Bit")]
        public bool Bit { get; set; }

        [XColumn(Name = "MySQL_Bit_Null")]
        public bool? Bit_Null { get; set; }


    }

    public enum MySQL_Enum
    {
        /*
         * 与 mysql enum 类型 对应时 枚举值 必须是 从1开始的顺序值，如：MySQL_Enum 枚举值 必须是 A=1 B=2 
         * 字段定义：enum('A','B')
         * 入库，值存入字段时，两种方式：
         * 方式一：单个字面值存入，如："A" 或者  "B"
         * 方式二：用数值代表，如：1 表示 "A"  ,   2 表示 "B"
         */

        A = 1,
        B = 2
    }

    public class MySQL_Set
    {
        /*
         * 与 mysql set 类型 对应时 若使用枚举值 注意 值必须是 2的n次方 顺序值 ，如 ： 1 ， 2，4， 8， 16  。。。
         * 字段定义：set('music','movie','swimming')
         * 入库，值存入字段时， 两种方式：
         * 方式一： 逗号分隔， 如： "movie,swimming"  或者 "music,movie,swimming"
         * 方式二： 用 数值和 代表 ，如：3=1+2 表示 "music,movie"   ,    6=2+4 表示  "movie,swimming" 
         */

        public string Music { get; } = "music";   // 1
        public string Movie { get; } = "movie";  // 2
        public string Swimming { get; } = "swimming";  // 4
    }
}
