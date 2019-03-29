using MyDAL.Core.Enums;

namespace MyDAL.Core.Configs
{
    internal class ColTypeConfig
    {
        internal ParamTypeEnum BigInt(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_BigInt;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_BigInt;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Binary(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_Binary;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_Binary;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Bit(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_Bit;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_Bit;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Blob(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_Blob;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.None;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Char(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_Char;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_Char;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Date(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_Date;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_Date;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum DateTime(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_DateTime;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_DateTime;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Decimal(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_Decimal;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_Decimal;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Double(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_Double;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.None;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Enum(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_Enum;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.None;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Float(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_Float;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_Float;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Int(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_Int;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_Int;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum LongBlob(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_LongBlob;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.None;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum LongText(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_LongText;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.None;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum MediumBlob(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_MediumBlob;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.None;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum MediumInt(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_MediumInt;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.None;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum MediumText(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_MediumText;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.None;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Set(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_Set;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.None;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum SmallInt(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_SmallInt;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_SmallInt;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Text(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_Text;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_Text;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Time(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_Time;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_Time;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum TimeStamp(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_TimeStamp;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_TimeStamp;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum TinyBlob(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_TinyBlob;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.None;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum TinyInt(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_TinyInt;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_TinyInt;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum TinyText(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_TinyText;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.None;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum VarBinary(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_VarBinary;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_VarBinary;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum VarChar(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_VarChar;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_VarChar;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Year(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.MySQL_Year;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.None;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum NChar(DbEnum db)
        {
            switch(db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.None;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_NChar;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum NVarChar(DbEnum db)
        {
            switch(db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.None;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_NVarChar;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum NText(DbEnum db)
        {
            switch(db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.None;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_NText;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Image(DbEnum db)
        {
            switch(db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.None;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_Image;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Numeric(DbEnum db)
        {
            switch(db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.None;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_Numeric;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum SmallMoney(DbEnum db)
        {
            switch(db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.None;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_SmallMoney;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Money(DbEnum db)
        {
            switch(db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.None;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_Money;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Real(DbEnum db)
        {
            switch(db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.None;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_Real;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum DateTime2(DbEnum db)
        {
            switch(db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.None;
                case DbEnum.SQLServer:

                    return ParamTypeEnum.SqlServer_DateTime2;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum SmallDateTime (DbEnum db)
        {
            switch(db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.None;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_SmallDateTime;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum DateTimeOffset(DbEnum db)
        {
            switch(db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.None;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_DateTimeOffset;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Sql_Variant(DbEnum db)
        {
            switch(db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.None;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_Sql_Variant;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum UniqueIdentifier(DbEnum db)
        {
            switch(db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.None;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_UniqueIdentifier;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Xml(DbEnum db)
        {
            switch(db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.None;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_Xml;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Cursor(DbEnum db)
        {
            switch(db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.None;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_Cursor;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Table(DbEnum db)
        {
            switch(db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.None;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SqlServer_Table;
                default:
                    return ParamTypeEnum.None;
            }
        }
    }
}
