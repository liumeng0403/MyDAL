using MyDAL.Test;
using System;
using Xunit;

namespace MyDAL.Tools
{
    public class _03_XHttp
        : TestBase
    {

        [Fact]
        public void Timeout_Test()
        {
            xx = string.Empty;

            try
            {
                var res = new XHttp().GET("https://www.baidu.com");
            }
            catch (Exception ex)
            {

            }

            xx = string.Empty;
        }

    }
}
