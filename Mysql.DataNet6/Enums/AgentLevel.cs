using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mysql.Data_Net6.Enums
{
    public enum AgentLevel
    {
        //None = 0,
        NewCustomer = 1,
        Customer = 2,
        CityAgent = 4,
        ProvinceAgent = 16,
        DistiAgent = 128
    }
}
