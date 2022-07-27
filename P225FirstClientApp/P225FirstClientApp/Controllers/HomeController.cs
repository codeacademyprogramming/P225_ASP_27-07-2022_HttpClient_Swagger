using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using P225FirstClientApp.Models;
using P225FirstClientApp.ViewModels.AccountVMs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace P225FirstClientApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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

        public async Task<IActionResult> Register()
        {
            RegisterVM registerVM = new RegisterVM
            {
                UserName = "hamidvm",
                Name = "Hamid",
                SurName = "Mammadov",
                Email = "hamidvm@code.edu.az",
                Password = "Admin@123"
            };

            string registerUrl = "http://localhost:34689/api/accounts/register";

            HttpResponseMessage httpResponseMessage = null;

            using(HttpClient httpClient = new HttpClient())
            {
                string conent = JsonConvert.SerializeObject(registerVM);

                StringContent stringContent = new StringContent(conent, Encoding.UTF8, "application/json");

                httpResponseMessage = await httpClient.PostAsync(registerUrl, stringContent);
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                return Content("Qeydiyyatdan Kecdi");
            }

            return Content("Qeydiyyatdan kecmedi");
        }

        public async Task<IActionResult> Login()
        {
            LoginVM loginVM = new LoginVM
            {
                Email = "hamidvm@code.edu.az",
                Password = "Admin123"
            };

            string loginUrl = "http://localhost:34689/api/accounts/login";

            HttpResponseMessage httpResponseMessage = null;

            using(HttpClient httpClient = new HttpClient())
            {
                string content = JsonConvert.SerializeObject(loginVM);

                StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");

                httpResponseMessage = await httpClient.PostAsync(loginUrl, stringContent);
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                string content = await httpResponseMessage.Content.ReadAsStringAsync();

                LoginResponseVM loginResponseVM = JsonConvert.DeserializeObject<LoginResponseVM>(content);

                Response.Cookies.Append("AuthToken", loginResponseVM.Token);

                return Content(loginResponseVM.Token);
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
            {
                string content = await httpResponseMessage.Content.ReadAsStringAsync();

                return Content(content);
            }

            return Content("");
        }
    }
}
