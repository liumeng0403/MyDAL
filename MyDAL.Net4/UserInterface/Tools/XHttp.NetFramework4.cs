using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace MyDAL.Tools
{
    public sealed partial class XHttp
    {

        // 下一步改进  自动url encode ,  cookie 携带支持


        /***********************************************************************************************************************************************/

        /// <summary>
        /// Http Client
        /// </summary>
        /// <param name="timeoutTime">请求超时时间 ms</param>
        /// <param name="requestCount">请求次数: requestCount = retryCount + 1</param>
        public XHttp(int timeoutTime= 30 * 1000, int requestCount=1)
        {
            //
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
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
            this.TimeoutTime = timeoutTime;
            this.RetryCount = requestCount;
            this.WaitSleep = 10;
            this.RequestMethod = "POST";
            this.TrySleep = 2 * 1000;
        }

    }
}
