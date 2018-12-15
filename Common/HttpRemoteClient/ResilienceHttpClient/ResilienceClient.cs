using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HttpClient.Abstraction;
using Polly;
using System.Collections.Concurrent;
using NetHttpClient =System.Net.Http.HttpClient;
using Polly.Wrap;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using System.Net;

namespace ResilienceHttpClient
{
    /// <summary>
    /// 弹性http访问客户端
    /// </summary>
    public class ResilienceClient : IHttpClient
    {
        private readonly NetHttpClient _httpClient;

        private readonly  Func<string, IEnumerable<Policy>> _PolicyCreator;

        private readonly ConcurrentDictionary<string, PolicyWrap> _PolicyWrappers;

        private readonly ILogger<ResilienceClient> _Logger;

        private readonly IHttpContextAccessor _HttpContextAccessor;

        public ResilienceClient(Func<string, IEnumerable<Policy>> PolicyCreator, ILogger<ResilienceClient> Logger, IHttpContextAccessor HttpContextAccessor)
        {
            _httpClient = new NetHttpClient();
            _PolicyCreator = PolicyCreator;
            _PolicyWrappers = new ConcurrentDictionary<string, PolicyWrap>();
            _Logger = Logger;
            _HttpContextAccessor = HttpContextAccessor;
            _Logger.LogWarning("ResilienceClient 初始化");
        }

        private HttpRequestMessage CreateHttpRequestMessage(HttpMethod method,string url,Dictionary<string,string> dic)
        {
            HttpRequestMessage message = new HttpRequestMessage(method, url);
            message.Content = new FormUrlEncodedContent(dic);
            return message;
        }

        private HttpRequestMessage CreateHttpRequestMessage<T>(HttpMethod method, string url, T dic)
        {
            HttpRequestMessage message = new HttpRequestMessage(method, url);
            message.Content = new StringContent(JsonConvert.SerializeObject(dic),Encoding.UTF8,"application/json");
            return message;
        }

       

        /// <summary>
        /// 获得url源头
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private static string GetOriginFromUrl(string uri)
        {
            var url = new Uri(uri);
            var origin = $"{url.Scheme}://{url.DnsSafeHost}:{url.Port}";
            return origin;
        }

        /// <summary>
        /// 转换为小写
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        private static string NormalizeOrigin(string origin)
        {
            return origin?.Trim()?.ToLower();
        }

        /// <summary>
        /// 设置授权头
        /// </summary>
        /// <param name="requestmessage"></param>
        private void SetAuthorization(HttpRequestMessage requestmessage)
        {
            var AuthenticationHeader = _HttpContextAccessor.HttpContext?.Request?.Headers["Authorization"];
            if (!string.IsNullOrEmpty(AuthenticationHeader))
            {
                requestmessage.Headers.Add("Authorization", new List<string> { AuthenticationHeader });
            }
        }

        public void Dispose()
        {
            _Logger.LogWarning("ResilienceClient 被释放");
            _httpClient.Dispose();
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string url, T item, string AuthenticationToken = null, string requestID = null, string AuthenticationMothed = "Bearer")
        {
            Func<HttpRequestMessage> func = ()=> CreateHttpRequestMessage(HttpMethod.Post, url, item);
            return await DoPostAsync(HttpMethod.Post,url, func, AuthenticationToken, requestID, AuthenticationMothed);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, Dictionary<string, string> item, string AuthenticationToken = null, string requestID = null, string AuthenticationMothed = "Bearer")
        {
            Func<HttpRequestMessage>  func = ()=> CreateHttpRequestMessage(HttpMethod.Post, url, item);
            return await DoPostAsync(HttpMethod.Post, url, func, AuthenticationToken, requestID, AuthenticationMothed);
        }

        private async Task<T> HttpInvoker<T>(string origin, Func<Context, Task<T>> action)
        {
            var normalized = NormalizeOrigin(origin);
            if (!_PolicyWrappers.TryGetValue(normalized, out PolicyWrap policyWrap))
            {
                policyWrap =  Policy.WrapAsync(_PolicyCreator(normalized).ToArray());
                _PolicyWrappers.TryAdd(normalized, policyWrap);
            }
            return await policyWrap.ExecuteAsync(action, new Context(normalized));
        }

        private async Task<HttpResponseMessage> DoPostAsync(HttpMethod method,string url, Func<HttpRequestMessage> requestMessagefunc, string AuthenticationToken = null, string requestID = null, string AuthenticationMothed = "Bearer")
        {
            if (method != HttpMethod.Post)
            {
                throw new ArgumentException("httpmethod is not post");
            }
            string origin = GetOriginFromUrl(url);

            return await HttpInvoker(origin, async (Context) => {

                var requestMessage = requestMessagefunc();

                SetAuthorization(requestMessage);

                if (AuthenticationToken != null)
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue(AuthenticationToken, AuthenticationMothed);
                }
                if (requestID != null)
                {
                    requestMessage.Headers.Add("x-requsetid", requestID);

                }
                var response = await _httpClient.SendAsync(requestMessage);

                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new HttpRequestException();
                }

                return response;
            });
        }
    }
}
