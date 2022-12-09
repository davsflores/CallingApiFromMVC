using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CallingApiFromMVC.Models;
using Newtonsoft.Json;
using System.Text;

//using System.Text;
//using Newtonsoft.Json;

namespace CallingApiFromMVC.Controllers
{
    public class StudentsController : Controller
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        Student _oStudent = new Student();
        List<Student> _oStudents = new List<Student>();

        
        public StudentsController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }
        
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<Student>> GetAllStudents()
        {
            _oStudents = new List<Student>();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using(var response=await httpClient.GetAsync("https://localhost:7195/api/Students"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oStudents = JsonConvert.DeserializeObject<List<Student>>(apiResponse);
                }
            }
                return _oStudents;
        }

        [HttpGet]
        public async Task<Student> GetById(int studentId)
        {
            _oStudent = new Student();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:7195/api/Students" + studentId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oStudent = JsonConvert.DeserializeObject<Student>(apiResponse);
                }
            }
            return _oStudent;
        }


        [HttpPost]
        public async Task<Student> AddUpdateStudent(Student student)
        {
            _oStudent = new Student();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:7195/api/Students", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oStudent = JsonConvert.DeserializeObject<Student>(apiResponse);
                }
            }
            return _oStudent;
        }

        [HttpDelete]
        public async Task<string> Delete(int studentId)
        {
            string message = "";
            _oStudent = new Student();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:7195/api/Students" + studentId))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }
    }
}
