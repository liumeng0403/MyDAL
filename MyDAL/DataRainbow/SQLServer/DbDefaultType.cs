using MyDAL.Core.Enums;
using System;

namespace MyDAL.DataRainbow.SQLServer
{

    /*
     * CREATE TABLE [dbo].[TSqlCloumnType](
	 * [Guid] [uniqueidentifier] NOT NULL,
	 * [Bool] [bit] NOT NULL,
	 * [BoolNull] [bit] NULL,
	 * [Byte] [tinyint] NOT NULL,
	 * [ByteNull] [tinyint] NULL,
	 * [Char] [nvarchar](1) NOT NULL,
	 * [CharNull] [nvarchar](1) NULL,
	 * [Decimal] [decimal](18, 2) NOT NULL,
	 * [DecimalNull] [decimal](18, 2) NULL,
	 * [Double] [float] NOT NULL,
	 * [DoubleNull] [float] NULL,
	 * [Float] [real] NOT NULL,
	 * [FloatNull] [real] NULL,
	 * [Int] [int] NOT NULL,
	 * [IntNull] [int] NULL,
	 * [Long] [bigint] NOT NULL,
	 * [LongNull] [bigint] NULL,
	 * [Sbyte] [smallint] NOT NULL,
	 * [SbyteNull] [smallint] NULL,
	 * [Short] [smallint] NOT NULL,
	 * [ShortNull] [smallint] NULL,
	 * [Uint] [bigint] NOT NULL,
	 * [UintNull] [bigint] NULL,
	 * [Ulong] [decimal](20, 0) NOT NULL,
	 * [UlongNull] [decimal](20, 0) NULL,
	 * [Ushort] [int] NOT NULL,
	 * [UshortNull] [int] NULL,
	 * [String] [nvarchar](max) NULL,
	 * [DateTime] [datetime2](7) NOT NULL,
	 * [DateTimeNull] [datetime2](7) NULL,
	 * [TimeSpan] [time](7) NOT NULL,
	 * [TimeSpanNull] [time](7) NULL,
	 * [GuidNull] [uniqueidentifier] NULL,
	 * [Enum] [int] NOT NULL,
	 * [EnumNull] [int] NULL,
     *    CONSTRAINT [PK_TSqlCodeFirstCloumnType] PRIMARY KEY CLUSTERED 
     *   (
	 *       [Guid] ASC
     *   )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
     *   ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
     */
    [XTable(Name = "TSqlCloumnType")]
    internal class DbDefaultType
    {
        public bool Bool { get; set; }  // [bit] NOT NULL
        public bool? BoolNull { get; set; }  // [bit] NULL

        /*
         * 8位
         * 无符号整数
         */
        public byte Byte { get; set; }  // [tinyint] NOT NULL
        public byte? ByteNull { get; set; }  // [tinyint] NULL

        public char Char { get; set; }  // [nvarchar](1) NOT NULL
        public char? CharNull { get; set; }  // [nvarchar](1) NULL

        public decimal Decimal { get; set; }  // [decimal](18, 2) NOT NULL
        public decimal? DecimalNull { get; set; }  // [decimal](18, 2) NULL

        public double Double { get; set; }  // [float] NOT NULL
        public double? DoubleNull { get; set; }  // [float] NULL

        public float Float { get; set; }  // [real] NOT NULL
        public float? FloatNull { get; set; }  // [real] NULL

        /*
         * 32位
         * 有符号整数
         */
        public int Int { get; set; }  // [int] NOT NULL
        public int? IntNull { get; set; }  // [int] NULL

        /*
         * 64位
         * 有符号整数
         */
        public long Long { get; set; } // [bigint] NOT NULL
        public long? LongNull { get; set; } // [bigint] NULL

        /*
         * 8位 
         * 有符号整数
         */
        public sbyte Sbyte { get; set; }  // [smallint] NOT NULL
        public sbyte? SbyteNull { get; set; }  // [smallint] NULL

        /*
         * 16位
         * 有符号整数
         */
        public short Short { get; set; }  // [smallint] NOT NULL
        public short? ShortNull { get; set; }  // [smallint] NULL

        /*
         * 32位
         * 无符号整数
         */
        public uint Uint { get; set; } // [bigint] NOT NULL
        public uint? UintNull { get; set; }  // [bigint] NULL

        /*
         * 64位
         * 无符号整数
         */
        public ulong Ulong { get; set; }  // [decimal](20, 0) NOT NULL
        public ulong? UlongNull { get; set; }  // [decimal](20, 0) NULL

        /*
         * 16位
         * 无符号整数
         */
        public ushort Ushort { get; set; }  // [int] NOT NULL
        public ushort? UshortNull { get; set; }  // [int] NULL

        ///*
        // * 不被 CodeFirst 支持的类型
        // */
        //[NotMapped]
        //public object Object { get; set; }

        public string String { get; set; }  // [nvarchar](max) NULL

        public DateTime DateTime { get; set; }  // [datetime2](7) NOT NULL
        public DateTime? DateTimeNull { get; set; }  // [datetime2](7) NULL

        public TimeSpan TimeSpan { get; set; }  // [time](7) NOT NULL
        public TimeSpan? TimeSpanNull { get; set; }  // [time](7) NULL

        //[Key]
        public Guid Guid { get; set; }  // [uniqueidentifier] NOT NULL
        public Guid? GuidNull { get; set; }  // [uniqueidentifier] NULL

        public ActionEnum Enum { get; set; }  // [int] NOT NULL
        public ActionEnum? EnumNull { get; set; }  // [int] NULL

    }

}
