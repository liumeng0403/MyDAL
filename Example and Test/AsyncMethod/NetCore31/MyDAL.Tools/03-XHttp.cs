using MyDAL.Test;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Tools
{
    public class _03_XHttp
        : TestBase
    {

        [Fact]
        public async Task Timeout_Test()
        {
            xx = string.Empty;

            try
            {
                var res = new XHttp().GET("http://www.baidu.com");
            }
            catch (Exception ex)
            {

            }

            xx = string.Empty;
        }

    }
}
