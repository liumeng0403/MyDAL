using HPC.DAL.Core.Enums;

namespace HPC.DAL.DataRainbow.XCommon
{
    internal sealed class SqlParamDefaultType
    {
        internal ParamTypeEnum BoolProc(DbEnum db)
        {
            switch(db)
            {
                case DbEnum.MySQL:
                case DbEnum.SQLServer:
                    return ParamTypeEnum.Bit_MySQL_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }

        internal ParamTypeEnum ByteProc(DbEnum db)
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

        internal ParamTypeEnum ByteArrayProc(DbEnum db)
        {
            switch(db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.LongBlob_MySQL;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.Image_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }

        internal ParamTypeEnum CharProc(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.VarChar_MySQL_SqlServer;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.NVarChar_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }

        internal ParamTypeEnum DecimalProc(DbEnum db)
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

        internal ParamTypeEnum DoubleProc(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.Double_MySQL;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.Float_MySQL_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }

        internal ParamTypeEnum FloatProc(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.Float_MySQL_SqlServer;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.Real_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }

        internal ParamTypeEnum IntProc(DbEnum db)
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

        internal ParamTypeEnum LongProc(DbEnum db)
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

        internal ParamTypeEnum SbyteProc(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.TinyInt_MySQL_SqlServer;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.SmallInt_MySQL_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }

        internal ParamTypeEnum ShortProc(DbEnum db)
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

        internal ParamTypeEnum UintProc(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.Int_MySQL_SqlServer;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.BigInt_MySQL_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }

        internal ParamTypeEnum UlongProc(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.BigInt_MySQL_SqlServer;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.Decimal_MySQL_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }

        internal ParamTypeEnum UshortProc(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.SmallInt_MySQL_SqlServer;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.Int_MySQL_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }

        internal ParamTypeEnum StringProc(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.LongText_MySQL;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.NVarChar_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }

        internal ParamTypeEnum DateTimeProc(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.DateTime_MySQL_SqlServer;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.DateTime2_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }

        internal ParamTypeEnum TimeSpanProc(DbEnum db)
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

        internal ParamTypeEnum GuidProc(DbEnum db)
        {
            switch (db)
            {
                case DbEnum.MySQL:
                    return ParamTypeEnum.Char_MySQL_SqlServer;
                case DbEnum.SQLServer:
                    return ParamTypeEnum.UniqueIdentifier_SqlServer;
                default:
                    return ParamTypeEnum.None;
            }
        }
    }
}
