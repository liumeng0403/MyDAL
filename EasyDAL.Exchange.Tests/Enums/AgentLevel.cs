using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDAL.Exchange.Tests.Enums
{
    public enum AgentLevel
    {
        None = 0,
        NewCustomer = 1,
        Customer = 2,
        CityAgent = 4,
        ProvinceAgent = 16,
        DistiAgent = 128
    }
}
