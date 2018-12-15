using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ResilienceHttpClient
{
    public class ResilienceHttpClientFactory
    {
        private readonly ILogger<ResilienceClient> _Logger;

        private readonly IHttpContextAccessor _HttpContextAccessor;

        /// <summary>
        /// 重试次数
        /// </summary>
        private readonly int _RetryCount;

        /// <summary>
        /// 重试几次后熔断
        /// </summary>
        private readonly int _RetryCountFormBreaking;

        public ResilienceHttpClientFactory(int RetryCount, int RetryCountFormBreaking, ILogger<ResilienceClient> Logger, IHttpContextAccessor HttpContextAccessor)
        {
            _RetryCount = RetryCount;
            _RetryCountFormBreaking = RetryCountFormBreaking;
            _Logger = Logger;
            _HttpContextAccessor = HttpContextAccessor;
        }

        public ResilienceClient CreateResilienceClient() =>
            new ResilienceClient((origin)=> CreatePolicies(origin), _Logger, _HttpContextAccessor);

        private Policy[] CreatePolicies(string origin)
        {
            return new Policy[] {
                Policy.Handle<HttpRequestException>()
                          .WaitAndRetryAsync(_RetryCount, (_RetryCount)=>TimeSpan.FromSeconds(_RetryCount*2),(exception,timeSpan,count,context)=> _Logger.LogWarning($"重试次数：{count}")),
                Policy.Handle<HttpRequestException>()
                          .CircuitBreakerAsync(_RetryCountFormBreaking,TimeSpan.FromSeconds(1))
            };
        }

    }
}
