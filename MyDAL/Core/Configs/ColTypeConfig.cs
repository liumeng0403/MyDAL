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
                default:
                    return ParamTypeEnum.None;
            }
        }
        internal ParamTypeEnum Binary(DbEnum db)
        {
            //switch(db)
        }
        internal ParamTypeEnum Bit(DbEnum db)
        {

        }
        internal ParamTypeEnum Blob(DbEnum db)
        {

        }
        internal ParamTypeEnum Char(DbEnum db)
        {

        }
        internal ParamTypeEnum Date(DbEnum db)
        {

        }
        internal ParamTypeEnum Datetime(DbEnum db)
        {

        }
        internal ParamTypeEnum Decimal(DbEnum db)
        {

        }
        internal ParamTypeEnum Double(DbEnum db)
        {

        }
        internal ParamTypeEnum Enum(DbEnum db)
        {

        }
        internal ParamTypeEnum Float(DbEnum db)
        {

        }
        internal ParamTypeEnum Int(DbEnum db)
        {

        }
        internal ParamTypeEnum LongBlob(DbEnum db)
        {

        }
        internal ParamTypeEnum LongText(DbEnum db)
        {

        }
        internal ParamTypeEnum MediumBlob(DbEnum db)
        {

        }
        internal ParamTypeEnum MediumInt(DbEnum db)
        {

        }
        internal ParamTypeEnum MediumText(DbEnum db)
        {

        }
        internal ParamTypeEnum Set(DbEnum db)
        {

        }
        internal ParamTypeEnum SmallInt(DbEnum db)
        {

        }
        internal ParamTypeEnum Text(DbEnum db)
        {

        }
        internal ParamTypeEnum Time(DbEnum db)
        {

        }
        internal ParamTypeEnum TimeStamp(DbEnum db)
        {

        }
        internal ParamTypeEnum TinyBlob(DbEnum db)
        {

        }
        internal ParamTypeEnum TinyInt(DbEnum db)
        {

        }
        internal ParamTypeEnum TinyText(DbEnum db)
        {

        }
        internal ParamTypeEnum VarBinary(DbEnum db)
        {

        }
        internal ParamTypeEnum VarChar(DbEnum db)
        {

        }
        internal ParamTypeEnum Year(DbEnum db)
        {

        }
    }
}
