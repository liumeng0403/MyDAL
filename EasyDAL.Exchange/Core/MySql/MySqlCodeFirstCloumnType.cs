using MyDAL.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDAL.Core.MySql
{
    /*
     * CREATE TABLE `mysqlcloumntype` (
     * `Bool` bit(1) NOT NULL,
     * `BoolNull` bit(1) DEFAULT NULL,
     * `Byte` tinyint(3) unsigned NOT NULL,
     * `ByteNull` tinyint(3) unsigned DEFAULT NULL,
     * `Char` varchar(1) NOT NULL,
     * `CharNull` varchar(1) DEFAULT NULL,
     * `Decimal` decimal(65,30) NOT NULL,
     * `DecimalNull` decimal(65,30) DEFAULT NULL,
     * `Double` double NOT NULL,
     * `DoubleNull` double DEFAULT NULL,
     * `Float` float NOT NULL,
     * `FloatNull` float DEFAULT NULL,
     * `Int` int(11) NOT NULL,
     * `IntNull` int(11) DEFAULT NULL,
     * `Long` bigint(20) NOT NULL,
     * `LongNull` bigint(20) DEFAULT NULL,
     * `Sbyte` tinyint(4) NOT NULL,
     * `SbyteNull` tinyint(4) DEFAULT NULL,
     * `Short` smallint(6) NOT NULL,
     * `ShortNull` smallint(6) DEFAULT NULL,
     * `Uint` int(10) unsigned NOT NULL,
     * `UintNull` int(10) unsigned DEFAULT NULL,
     * `Ulong` bigint(20) unsigned NOT NULL,
     * `UlongNull` bigint(20) unsigned DEFAULT NULL,
     * `Ushort` smallint(5) unsigned NOT NULL,
     * `UshortNull` smallint(5) unsigned DEFAULT NULL,
     * `String` longtext,
     * `DateTime` datetime(6) NOT NULL,
     * `DateTimeNull` datetime(6) DEFAULT NULL,
     * `TimeSpan` time(6) NOT NULL,
     * `TimeSpanNull` time(6) DEFAULT NULL,
     * `Guid` char(36) NOT NULL,
     * `GuidNull` char(36) DEFAULT NULL,
     * `Enum` int(11) NOT NULL,
     * `EnumNull` int(11) DEFAULT NULL,
     * PRIMARY KEY (`Guid`)
     * ) ENGINE=InnoDB DEFAULT CHARSET=utf8
     */
    [Table("MySqlCodeFirstCloumnType")]
    internal class MySqlCodeFirstCloumnType
    {
        public bool Bool { get; set; }  // bit(1) NOT NULL
        public bool? BoolNull { get; set; }  // bit(1) DEFAULT NULL

        /*
         * 8位
         * 无符号整数
         */
        public byte Byte { get; set; }  // tinyint(3) unsigned NOT NULL
        public byte? ByteNull { get; set; }  // tinyint(3) unsigned DEFAULT NULL

        public char Char { get; set; }  // varchar(1) NOT NULL
        public char? CharNull { get; set; }  // varchar(1) DEFAULT NULL

        public decimal Decimal { get; set; }  // decimal(65,30) NOT NULL
        public decimal? DecimalNull { get; set; }  // decimal(65,30) DEFAULT NULL

        public double Double { get; set; }  // double NOT NULL
        public double? DoubleNull { get; set; }  // double DEFAULT NULL

        public float Float { get; set; }  // float NOT NULL
        public float? FloatNull { get; set; }  // float DEFAULT NULL

        /*
         * 32位
         * 有符号整数
         */
        public int Int { get; set; }  // int(11) NOT NULL
        public int? IntNull { get; set; }  // int(11) DEFAULT NULL

        /*
         * 64位
         * 有符号整数
         */
        public long Long { get; set; } // bigint(20) NOT NULL
        public long? LongNull { get; set; } // bigint(20) DEFAULT NULL

        /*
         * 8位 
         * 有符号整数
         */
        public sbyte Sbyte { get; set; }  // tinyint(4) NOT NULL
        public sbyte? SbyteNull { get; set; }  // tinyint(4) DEFAULT NULL

        /*
         * 16位
         * 有符号整数
         */
        public short Short { get; set; }  // smallint(6) NOT NULL
        public short? ShortNull { get; set; }  // smallint(6) DEFAULT NULL

        /*
         * 32位
         * 无符号整数
         */
        public uint Uint { get; set; } // int(10) unsigned NOT NULL
        public uint? UintNull { get; set; }  // int(10) unsigned DEFAULT NULL

        /*
         * 64位
         * 无符号整数
         */
        public ulong Ulong { get; set; }  // bigint(20) unsigned NOT NULL
        public ulong? UlongNull { get; set; }  // bigint(20) unsigned DEFAULT NULL

        /*
         * 16位
         * 无符号整数
         */
        public ushort Ushort { get; set; }  // smallint(5) unsigned NOT NULL
        public ushort? UshortNull { get; set; }  // smallint(5) unsigned DEFAULT NULL

        /*
         * 不被 CodeFirst 支持的类型
         */
        [NotMapped]
        public object Object { get; set; }

        public string String { get; set; }  // longtext

        public DateTime DateTime { get; set; }  // datetime(6) NOT NULL
        public DateTime? DateTimeNull { get; set; }  // datetime(6) DEFAULT NULL

        public TimeSpan TimeSpan { get; set; }  // time(6) NOT NULL
        public TimeSpan? TimeSpanNull { get; set; }  // time(6) DEFAULT NULL

        public Guid Guid { get; set; }  // char(36) NOT NULL
        public Guid? GuidNull { get; set; }  // char(36) DEFAULT NULL

        public ActionEnum Enum { get; set; }  // int(11) NOT NULL
        public ActionEnum? EnumNull { get; set; }  // int(11) DEFAULT NULL

    }
}
