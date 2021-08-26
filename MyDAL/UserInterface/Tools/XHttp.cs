using MyDAL.Core;
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace MyDAL.Tools
{
    public sealed partial class XHttp
    {

        // 下一步改进  自动url encode ,  cookie 携带支持

        private byte[] Buffer { get; set; }
        private Stream RequestStream { get; set; }
        private HttpWebRequest Request { get; set; }
        private bool ResponseFlag { get; set; }
        private string Result { get; set; }
        private bool TimeoutFlag { get; set; }
        private int TimeoutTime { get; set; }
        private int RetryCount { get; set; }
        private int WaitSleep { get; set; }
        private int TrySleep { get; set; }

        private void RemoteNew(Action<XHttp, string> action)
        {
            var reNum = 0;
            for (var i = 0; i < this.RetryCount; i++)
            {
                reNum++;
                try
                {
                    //
                    var uri = URL;
                    this.Request = WebRequest.Create(uri) as HttpWebRequest;
                    this.Request.KeepAlive = false;
                    this.Request.Method = this.RequestMethod;
                    this.Request.Credentials = CredentialCache.DefaultCredentials;
                    if (Token.IsNotBlank())
                    {
                        Request.Headers.Add(HttpRequestHeader.Authorization, $" Bearer {Token}");
                    }
                    if (this.RequestMethod.Equals("POST", StringComparison.OrdinalIgnoreCase))
                    {
                        this.Buffer = Encoding.UTF8.GetBytes(this.JsonContent);
                        this.Request.ContentLength = this.Buffer.Length;
                        this.Request.ContentType = "application/json";
                        this.RequestStream = this.Request.GetRequestStream();
                        this.RequestStream.Write(this.Buffer, 0, this.Buffer.Length);
                        this.RequestStream.Close();
                    }

                    //
                    this.Request.BeginGetResponse((arr) =>
                    {
                        var state = arr.AsyncState as XHttp;
                        var response = state.Request.EndGetResponse(arr) as HttpWebResponse;
                        var respStream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                        action(state, respStream.ReadToEnd());
                        respStream.Close();
                        response.Close();
                    }, this);

                    break;
                }
                catch (AggregateException ae)
                {
                    string errMsg = string.Empty;
                    ae.Flatten().Handle(e =>
                    {
                        errMsg += e.Message;
                        return true;
                    });
                    if (reNum == this.RetryCount)
                    {
                        throw XConfig.EC.Exception(XConfig.EC._116, $"请求失败!请求次数:{this.RetryCount}次,失败原因:{errMsg};Url:{this.URL}");
                    }
                }
                catch (Exception ex)
                {
                    if (reNum == this.RetryCount)
                    {
                        throw XConfig.EC.Exception(XConfig.EC._117, $"请求失败!请求次数:{this.RetryCount}次,失败原因:{ex.Message};Url:{this.URL}");
                    }
                }

                if (this.RetryCount > 1)
                {
                    Thread.Sleep(this.TrySleep);
                    continue;
                }
            }
        }
        private void SetResult(XHttp state, string jsonData)
        {
            if (jsonData.IsNotBlank())
            {
                state.Result = jsonData;
                state.ResponseFlag = true;
            }
        }
        private void GetRemoteDataX()
        {
            //
            if (string.IsNullOrWhiteSpace(this.URL))
            {
                throw XConfig.EC.Exception(XConfig.EC._118, "requestURL, 未赋值 !");
            }

            // 
            RemoteNew(SetResult);

            //
            var timeNum = 0;
            while (true)
            {
                if (ResponseFlag)
                {
                    break;
                }
                if (TimeoutFlag)
                {
                    if (XConfig.RI.IsDebug
                        && timeNum < 10 * 60 * 1000)
                    {
                        // 调试模式下 10 分钟超时
                        TimeoutTime = 10 * 60 * 1000;
                    }
                    else
                    {
                        throw XConfig.EC.Exception(XConfig.EC._097, $"请求超时!超时时间:{TimeoutTime / 1000}S;Url:{this.URL}");
                    }
                }
                timeNum += WaitSleep;
                if (timeNum >= TimeoutTime)
                {
                    TimeoutFlag = true;
                }
                Thread.Sleep(WaitSleep);
            }
        }

        private string RequestMethod { get; set; }
        private string URL { get; set; }
        private string JsonContent { get; set; }
        private string Token { get; set; }

        /***********************************************************************************************************************************************/

        public string GET(string requestURL)
        {
            this.RequestMethod = "GET";
            this.URL = requestURL;
            this.GetRemoteDataX();
            return this.Result;
        }
        public string GET(string requestURL, string oAuth2Token)
        {
            this.RequestMethod = "GET";
            this.URL = requestURL;
            this.Token = oAuth2Token;
            this.GetRemoteDataX();
            return this.Result;
        }
        public string POST(string requestURL)
        {
            this.RequestMethod = "POST";
            this.URL = requestURL;
            this.GetRemoteDataX();
            return this.Result;
        }
        public string POST(string requestURL, string jsonRequestData)
        {
            this.RequestMethod = "POST";
            this.URL = requestURL;
            this.JsonContent = jsonRequestData;
            this.GetRemoteDataX();
            return this.Result;
        }
        public string POST(string requestURL, string jsonRequestData, string oAuth2Token)
        {
            this.RequestMethod = "POST";
            this.URL = requestURL;
            this.JsonContent = jsonRequestData;
            this.Token = oAuth2Token;
            this.GetRemoteDataX();
            return this.Result;
        }

        // 下一步改进  自动url encode ,  cookie 携带支持

        /***********************************************************************************************************************************************/

        /// <summary>
        /// Http Client
        /// </summary>
        /// <param name="timeoutTime">请求超时时间 ms</param>
        /// <param name="requestCount">请求次数: requestCount = retryCount + 1</param>
        public XHttp(int timeoutTime = 30 * 1000, int requestCount = 1)
        {
            //
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback
                = new RemoteCertificateValidationCallback((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) => true);

            // 
            this.URL = string.Empty;
            this.Request = default(HttpWebRequest);
            this.JsonContent = string.Empty;
            this.Buffer = default(byte[]);
            this.RequestStream = default(Stream);
            this.ResponseFlag = false;
            this.Result = string.Empty;
            this.TimeoutFlag = false;
            this.TimeoutTime = timeoutTime;
            this.RetryCount = requestCount;
            this.WaitSleep = 10;
            this.RequestMethod = "POST";
            this.TrySleep = 2 * 1000;
        }

    }
}
