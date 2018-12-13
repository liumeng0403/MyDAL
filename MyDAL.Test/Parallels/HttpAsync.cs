using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Yunyong.DataExchange;

namespace MyDAL.Test.Parallels
{
    internal class HttpAsync
    {

        private byte[] Buffer { get; set; }
        private Stream RequestStream { get; set; }
        private HttpWebRequest Request { get; set; }
        private bool ResponseFlag { get; set; }
        private string Result { get; set; }
        private bool TimeoutFlag { get; set; }
        private int TimeoutTime { get; set; }
        private bool RetryFlag { get; set; }
        private int RetryCount { get; set; }
        private int WaitSleep { get; set; }
        private int TrySleep { get; set; }
        private void RemoteNew(Action<HttpAsync, string> action)
        {
            var reNum = 0;
            for (var i = 0; i < this.RetryCount; i++)
            {
                try
                {
                    //
                    var uri = URL;
                    this.Request = WebRequest.Create(uri) as HttpWebRequest;
                    this.Request.KeepAlive = false;
                    this.Request.Method = this.RequestMethod;
                    this.Request.Credentials = CredentialCache.DefaultCredentials;
                    if (!Token.IsNullStr())
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
                        var state = arr.AsyncState as HttpAsync;
                        var response = state.Request.EndGetResponse(arr) as HttpWebResponse;
                        var respStream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                        action(state, respStream.ReadToEnd());
                        respStream.Close();
                        response.Close();
                    }, this);
                    break;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(this.TrySleep);
                    reNum++;
                    if (reNum == this.RetryCount)
                    {
                        throw new Exception(string.Format("重试失败!重试次数:{0}次,失败原因:{1}", this.RetryCount, ex.Message));
                    }
                    continue;
                }
            }
        }
        private void SetResult(HttpAsync state, string jsonData)
        {
            if (!string.IsNullOrWhiteSpace(jsonData))
            {
                state.Result = jsonData;
                state.ResponseFlag = true;
            }
        }

        /***********************************************************************************************************************************************/

        internal string URL { get; set; }
        internal string RequestMethod { get; set; }
        internal string JsonContent { get; set; }
        internal string Token { get; set; }

        internal HttpAsync()
        {
            //
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) => true);

            // 
            this.URL = string.Empty;
            this.Request = default(HttpWebRequest);
            this.JsonContent = string.Empty;
            this.Buffer = default(byte[]);
            this.RequestStream = default(Stream);
            this.ResponseFlag = false;
            this.Result = string.Empty;
            this.TimeoutFlag = false;
            this.TimeoutTime = 20 * 1000;
            this.RetryFlag = false;
            this.RetryCount = 3;
            this.WaitSleep = 10;
            this.RequestMethod = "POST";
            this.TrySleep = 2 * 1000;
        }

        /// <summary>
        /// 获取响应数据
        /// </summary>
        internal None GetRemoteData(None none)
        {
            //
            if (string.IsNullOrWhiteSpace(this.URL))
            {
                throw new Exception("HttpAsync.URL,未赋值!");
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
                    throw new Exception(string.Format("请求超时!超时时间:{0}S", TimeoutTime / 1000));
                }
                timeNum += WaitSleep;
                if (timeNum >= TimeoutTime)
                {
                    TimeoutFlag = true;
                }
                Thread.Sleep(WaitSleep);
            }

            //
            return new None
            {
                String = Result
            };
        }

    }
}
