using CallingApiFromMVC.Models;
using CallingApiFromMVC.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CallingApiFromMVC.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        StudentAPI _api = new StudentAPI();
        public async Task<IActionResult> Index()
        {
            List<Student> students = new List<Student>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/students");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                students = JsonConvert.DeserializeObject<List<Student>>(results);

            }
            return View(students);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var student = new Student();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/students/{Id}");

            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                student = JsonConvert.DeserializeObject<Student>(results);

            }
            return View(student);

        }

        
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult create(Student student)
        {
            HttpClient client = _api.Initial();
            //HTTP POST
            var postTask = client.PostAsJsonAsync<Student>("api/students", student);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            return View();
        }

        public async Task<IActionResult> Delete (int Id)
        {
            var student = new Student();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/students/{Id}");

            return RedirectToAction("Index");
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}