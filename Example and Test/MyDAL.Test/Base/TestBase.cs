using MyDAL.Test.Entities;
using MyDAL.Test.Entities.MySql;
using MyDAL.Test.Enums;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDAL.Test
{
    public abstract class TestBase
    {

        protected string xx { get; set; }

        protected WhereTestModel WhereTest
        {
            get
            {
                return new WhereTestModel
                {
                    CreatedOn = Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30),
                    StartTime = Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30),
                    EndTime = DateTime.Now,
                    AgentLevelNull = null,
                    ContainStr2 = "~00-d-3-",
                    In_Array_枚举 = new AgentLevel?[]
                    {
                        AgentLevel.CityAgent,
                        AgentLevel.DistiAgent
                    },
                    In_Array_String = new string[]
                    {
                        "黄银凤",
                        "刘建芬"
                    }
                };
            }
        }

        //
        // Nuget : Package : MySql.Data
        //
        // 不同版本 mysql 连接字符串一般如下：
        // "Server=localhost; Database=MyDAL_TestDB; Uid=SkyUser; Pwd=Sky@4321;"
        // "Server=localhost; Database=MyDAL_TestDB; Uid=SkyUser; Pwd=Sky@4321;SslMode=none;"
        // "Server=localhost; Database=MyDAL_TestDB; Uid=SkyUser; Pwd=Sky@4321;SslMode=none;allowPublicKeyRetrieval=true;"
        // "Server=localhost; Database=MyDAL_TestDB; Uid=SkyUser; Pwd=Sky@4321;SslMode=true;allowPublicKeyRetrieval=false;"
        //
        protected XConnection MyDAL_TestDB
        {
            get
            {
                return XConnection
                    .Builder()
                    .SetHost("10.211.55.3")
                    .SetDatabase("mydal_testdb")
                    .SetUser("mydal_dll")
                    .SetPassword("mydal_TEST__##")
                    .SetSslMode(true)
                    //.SetAllowPublicKeyRetrieval(true)
                    .SetMySqlDriver<MySqlConnection>()
                    .Build();
            }
        }

        protected Task None()
        {
            return default(Task);
        }

        protected void PrintLog(string msg)
        {
            Console.WriteLine(msg);
        }

        protected int StepProcess<M>(IEnumerable<M> modelList, int stepNum, Func<IEnumerable<M>, int> func)
        {
            //
            var result = 0;
            var total = modelList.Count();
            var limit = default(int);
            if (stepNum <= 0)
            {
                limit = 100;
            }
            else
            {
                limit = stepNum;
            }
            var start = 0;

            //
            do
            {
                var models = modelList.Skip(start).Take(limit);
                if (func != null)
                {
                    result += func(models);
                }
                if (start < (total - limit))
                {
                    start = start + limit;
                }
                else
                {
                    start = total;
                }
            }
            while (start < total);

            //
            return result;
        }

        public void MySQL_PreData(char pk, int num)
        {
            xx = string.Empty;

            MyDAL_TestDB.Delete<MySQL_EveryType>(it => it.Char == $"{pk}-char");

            var flag = num % 2 == 0;

            var m = new MySQL_EveryType();

            m.Char = $"{pk}-char";
            m.Char_Null = flag ? null : $"{pk}- char null";

            m.VarChar = $"{pk}-var char";
            m.VarChar_Null = flag ? null : $"{pk}-var char null";

            m.TinyText = $"{pk}-tiny text";
            m.TinyText_Null = flag ? null : $"{pk}-tiny text null";

            m.Text = $"{pk}-text";
            m.Text_Null = flag ? null : $"{pk}-text null";

            m.MediumText = $"{pk}-medium text";
            m.MediumText_Null = flag ? null : $"{pk}-medium text null";

            m.LongText = $"{pk}-long text";
            m.LongText_Null = flag ? null : $"{pk}-long text null";

            m.TinyBlob = Encoding.UTF8.GetBytes($"{pk}-tiny blob");
            m.TinyBlob_Null = flag ? null : Encoding.UTF8.GetBytes($"{pk}- tiny blob null");

            m.Blob = Encoding.UTF8.GetBytes($"{pk}-blob");
            m.Blob_Null = flag ? null : Encoding.UTF8.GetBytes($"{pk}-blob null");

            m.MediumBlob = Encoding.UTF8.GetBytes($"{pk}-medium blob");
            m.MediumBlob_Null = flag ? null : Encoding.UTF8.GetBytes($"{pk}-medium blob null");

            m.LongBlob = Encoding.UTF8.GetBytes($"{pk}-long blob");
            m.LongBlob_Null = flag ? null : Encoding.UTF8.GetBytes($"{pk}-long blob null");

            m.Binary = Encoding.UTF8.GetBytes($"{pk}-binary");
            m.Binary_Null = flag ? null : Encoding.UTF8.GetBytes($"{pk}-binary null");

            m.VarBinary = Encoding.UTF8.GetBytes($"{pk}-var binary");
            m.VarBinary_Null = flag ? null : Encoding.UTF8.GetBytes($"{pk} var binary null");

            m.Enum = MySQL_Enum.A;
            m.Enum_Null = flag ? null : (MySQL_Enum?)MySQL_Enum.B;

            m.Set = string.Join(",", new List<string> { "music", "movie" });
            m.Set_Null = flag ? null : string.Join(",", new List<string> { "swimming" });

            m.TinyInt = (byte)pk;
            m.TinyInt_Null = flag ? null : (byte?)pk;

            m.SmallInt = short.MaxValue;
            m.SmallInt_Null = flag ? null : (short?)short.MinValue;

            m.MediumInt = 1000000;
            m.MediumInt_Null = flag ? null : (int?)1000000;

            m.Int = int.MaxValue;
            m.Int_Null = flag ? null : (int?)int.MinValue;

            m.BigInt = long.MaxValue;
            m.BigInt_Null = flag ? null : (long?)long.MinValue;

            m.Float = 50.00F;
            m.Float_Null = flag ? null : (float?)50.00F;

            m.Double = double.MaxValue;
            m.Double_Null = flag ? null : (double?)double.MinValue;

            m.Decimal = 600.00M;
            m.Decimal_Null = flag ? null : (decimal?)600.00M;

            m.Date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            m.Date_Null = flag ? null : (DateTime?)DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

            m.DateTime = DateTime.Now;
            m.DateTime_Null = flag ? null : (DateTime?)DateTime.Now;

            m.TimeStamp = DateTime.Now;
            m.TimeStamp_Null = flag ? null : (DateTime?)DateTime.Now;

            m.Year = DateTime.Now.Year.ToString();
            m.Year_Null = flag ? null : DateTime.Now.Year.ToString();

            m.Time = DateTime.Now - DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            m.Time_Null = flag ? null : (TimeSpan?)(DateTime.Now - DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")));

            m.Bit = true;
            m.Bit_Null = flag ? null : (bool?)false;

            MyDAL_TestDB.Insert(m);

            xx = string.Empty;
        }

    }
}
