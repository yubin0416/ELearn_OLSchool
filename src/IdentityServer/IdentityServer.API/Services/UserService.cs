using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HttpClient.Abstraction;
using IdentityServer.API.Models;
using Newtonsoft.Json;
using DnsClient;

namespace IdentityServer.API.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpClient _httpClient;
        private readonly string ConsulServiceName = "UserCenter";
        private string requesturl;
        private readonly IDnsQuery _dnsQuery;

        public  UserService(IHttpClient httpClient,IDnsQuery dnsQuery)
        {
            _httpClient = httpClient;
            _dnsQuery = dnsQuery;
            var result = dnsQuery.ResolveService("service.consul", ConsulServiceName);
            var address = result.First().AddressList.FirstOrDefault().ToString();
            var port = result.First().Port;
            requesturl = $"http://{address}:{port}/api/user";
        }

        public async Task<Student> Register_Student(Student student)
        {
            return null;
        }

        public async Task<Teacher> Register_Student(Teacher teacher)
        {
            return null;
        }

        public async Task<Student> Student_LoginByPassword(string mobile, string password)
        {
            return null;
        }

        public async Task<Student> Student_LoginBySms(string mobile, string code)
        {
            
            //设置地址
            string path = requesturl + "/Student_LoginBySms";
            //封装请求
            Dictionary<string, string> dic = new Dictionary<string, string>() { { "mobile", mobile },{ "code", "code" } };
            //发送并等待结果
            var responsemessage = await  _httpClient.PostAsync(path,dic);
            if(responsemessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Student>(await responsemessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<Teacher> Teacher_LoginByPassword(string mobile, string password)
        {
            return null;
        }

        public async Task<Teacher> Teacher_LoginBySms(string mobile, string code)
        {
            //设置地址
            string path = requesturl + "/Teacher_LoginBySms";
            //封装请求
            Dictionary<string, string> dic = new Dictionary<string, string>() { { "mobile", mobile }, { "code", code } };
            //发送并等待结果
            var responsemessage = await _httpClient.PostAsync(path, dic);
            if (responsemessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Teacher>(await responsemessage.Content.ReadAsStringAsync());
            }
            return null;
        }
    }
}
