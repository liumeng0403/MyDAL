using MyDAL.Tools;
using System;

namespace MyDAL.Test.Parallels
{
    public class XHttpTest
    {

        public string RequestMethod { get; set; }
        public string URL { get; set; }
        public string JsonContent { get; set; }
        public string Token { get; set; }

        /// <summary>
        /// 获取响应数据
        /// </summary>
        public None HttpForTest(None none)
        {
            if ("GET".Equals(this.RequestMethod, StringComparison.OrdinalIgnoreCase))
            {
                if (this.URL.IsNotBlank())
                {
                    return new None { String = new XHttp().GET(this.URL) };
                }
                if (this.URL.IsNotBlank()
                    && this.Token.IsNotBlank())
                {
                    return new None { String = new XHttp().GET(this.URL, this.Token) };
                }
            }

            if ("POST".Equals(this.RequestMethod, StringComparison.OrdinalIgnoreCase))
            {
                if (this.URL.IsNotBlank())
                {
                    return new None { String = new XHttp().POST(this.URL) };
                }
                if (this.URL.IsNotBlank()
                    && this.JsonContent.IsNotBlank())
                {
                    return new None { String = new XHttp().POST(this.URL, this.JsonContent) };
                }
                if (this.URL.IsNotBlank()
                    && this.JsonContent.IsNotBlank()
                    && this.Token.IsNotBlank())
                {
                    return new None { String = new XHttp().POST(this.URL, this.JsonContent, this.Token) };
                }
            }

            throw new Exception("请检查参数设置！");
        }

    }
}
