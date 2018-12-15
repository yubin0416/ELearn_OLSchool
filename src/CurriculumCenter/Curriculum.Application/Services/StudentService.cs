using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Curriculum.Application.Dtos;
using Curriculum.Domain;
using HttpClient.Abstraction;
using Newtonsoft.Json;

namespace Curriculum.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IHttpClient _httpClient;
        private readonly string ApiUrl = "http://localhost:5001/api/user";

        public StudentService(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<StudentDto> GetStudent(string ID)
        {
            string url = ApiUrl+"/GetStudent";
            Dictionary<string, string> item = new Dictionary<string, string>() { { "StudentID", ID} };
            var response =  await _httpClient.PostAsync(url, item);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<StudentDto>(await response.Content.ReadAsStringAsync());
            }
            return null;
        }
    }
}
