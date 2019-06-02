using HPC.DAL.Core.Enums;

namespace HPC.DAL.Core.Configs
{
    internal class ColTypeConfig
    {
        internal ParamTypeEnum BigInt(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                case DbEnum.SQLServer:
                    return ParamTypeEnum.BigInt_MySQL_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Binary(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                case DbEnum.SQLServer:
                    return ParamTypeEnum.Binary_MySQL_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Bit(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                case DbEnum.SQLServer:
                    return ParamTypeEnum.Bit_MySQL_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Blob(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.Blob_MySQL;
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
                case DbEnum.SQLServer:
                    return ParamTypeEnum.Char_MySQL_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Date(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                case DbEnum.SQLServer:
                    return ParamTypeEnum.Date_MySQL_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum DateTime(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                case DbEnum.SQLServer:
                    return ParamTypeEnum.DateTime_MySQL_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Decimal(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                case DbEnum.SQLServer:
                    return ParamTypeEnum.Decimal_MySQL_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Double(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.Double_MySQL;
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
                    return ParamTypeEnum.Enum_MySQL;
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
                case DbEnum.SQLServer:
                    return ParamTypeEnum.Float_MySQL_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Int(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                case DbEnum.SQLServer:
                    return ParamTypeEnum.Int_MySQL_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum LongBlob(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.LongBlob_MySQL;
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
                    return ParamTypeEnum.LongText_MySQL;
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
                    return ParamTypeEnum.MediumBlob_MySQL;
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
                    return ParamTypeEnum.MediumInt_MySQL;
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
                    return ParamTypeEnum.MediumText_MySQL;
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
                    return ParamTypeEnum.Set_MySQL;
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
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SmallInt_MySQL_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Text(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                case DbEnum.SQLServer:
                    return ParamTypeEnum.Text_MySQL_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Time(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                case DbEnum.SQLServer:
                    return ParamTypeEnum.Time_MySQL_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum TimeStamp(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                case DbEnum.SQLServer:
                    return ParamTypeEnum.TimeStamp_MySQL_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum TinyBlob(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.TinyBlob_MySQL;
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
                case DbEnum.SQLServer:
                    return ParamTypeEnum.TinyInt_MySQL_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum TinyText(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.TinyText_MySQL;
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
                case DbEnum.SQLServer:
                    return ParamTypeEnum.VarBinary_MySQL_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum VarChar(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                case DbEnum.SQLServer:
                    return ParamTypeEnum.VarChar_MySQL_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Year(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.Year_MySQL;
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
                    return ParamTypeEnum.NChar_SqlServer;
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
                    return ParamTypeEnum.NVarChar_SqlServer;
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
                    return ParamTypeEnum.NText_SqlServer;
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
                    return ParamTypeEnum.Image_SqlServer;
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
                    return ParamTypeEnum.Numeric_SqlServer;
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
                    return ParamTypeEnum.SmallMoney_SqlServer;
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
                    return ParamTypeEnum.Money_SqlServer;
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
                    return ParamTypeEnum.Real_SqlServer;
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

                    return ParamTypeEnum.DateTime2_SqlServer;
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
                    return ParamTypeEnum.SmallDateTime_SqlServer;
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
                    return ParamTypeEnum.DateTimeOffset_SqlServer;
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
                    return ParamTypeEnum.Sql_Variant_SqlServer;
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
                    return ParamTypeEnum.UniqueIdentifier_SqlServer;
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
                    return ParamTypeEnum.Xml_SqlServer;
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
                    return ParamTypeEnum.Cursor_SqlServer;
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
                    return ParamTypeEnum.Table_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }
    }
}
