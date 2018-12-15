using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HttpClient.Abstraction
{
    public interface IHttpClient:IDisposable
    {
        //Task<HttpResponseMessage> GetAsync(string url,Dictionary<string,string> item,string AuthenticationToken = null,string requestID = null,string AuthenticationMothed ="Bearer");

        Task<HttpResponseMessage> PostAsync(string url, Dictionary<string, string> item, string AuthenticationToken = null, string requestID = null, string AuthenticationMothed = "Bearer");

        Task<HttpResponseMessage> PostAsync<T>(string url, T item, string AuthenticationToken = null, string requestID = null, string AuthenticationMothed = "Bearer");

        //Task<HttpResponseMessage> PutAsync(string url, Dictionary<string, string> item, string AuthenticationToken = null, string requestID = null, string AuthenticationMothed = "Bearer");

        //Task<HttpResponseMessage> PatchAsync(string url, Dictionary<string, string> item, string AuthenticationToken = null, string requestID = null, string AuthenticationMothed = "Bearer");

        //Task<HttpResponseMessage> DeleteAsync(string url, Dictionary<string, string> item, string AuthenticationToken = null, string requestID = null, string AuthenticationMothed = "Bearer");
    }
}
