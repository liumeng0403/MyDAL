﻿using System;
using System.Collections.Generic;

namespace MyDAL.Test.Entities.MySql
{
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

        /*--------------------------------------------------------------------------------*/   // int & string

        [XColumn(Name = "MySQL_Enum")]
        public MySQL_Enum Enum { get; set; }

        [XColumn(Name = "MySQL_Enum_Null")]
        public MySQL_Enum? Enum_Null { get; set; }

        /*--------------------------------------------------------------------------------*/  // int & long & string & List<string> 

        [XColumn(Name = "MySQL_Set_Field")]
        public string Set { get; set; }

        [XColumn(Name = "MySQL_Set_Null")]
        public string Set_Null { get; set; }

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

        /*--------------------------------------------------------------------------------*/   // int & string

        [XColumn(Name = "MySQL_Year")]
        public string Year { get; set; }

        [XColumn(Name = "MySQL_Year_Null")]
        public string Year_Null { get; set; }

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
