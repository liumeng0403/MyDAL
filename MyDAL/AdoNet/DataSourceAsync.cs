using MyDAL.AdoNet.Bases;
using MyDAL.Core;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyDAL.AdoNet
{
    internal sealed class DataSourceAsync
        : DataSource
    {
        internal DataSourceAsync()
            : base()
        { }
        internal DataSourceAsync(Context dc)
            : base(dc)
        { }

        /*********************************************************************************************************************************************/

        internal async Task OpenAsync(IDbConnection cnn)
        {
            if (cnn is DbConnection dbConn)
            {
                await dbConn.OpenAsync();
            }
            else
            {
                throw new InvalidOperationException("请使用一个继承自 DbConnection 对象的实例!!!");
            }
        }
     
     
      
      
     

        /*********************************************************************************************************************************************/

       
 


  
 

    }
}
