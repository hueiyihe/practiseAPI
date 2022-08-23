using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace practiseMVC.Controllers
{
    public class CustomersController : Controller
    {
        //Uri baseAddress = new Uri("https://localhost:7037/api");
        private static readonly HttpClient client;
        static CustomersController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7037/api");
        }

        public IActionResult Index()
        {
            List<Models.MVCCustomersModel> modelsList = new List<Models.MVCCustomersModel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Customers").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                modelsList = JsonConvert.DeserializeObject<List<Models.MVCCustomersModel>>(data);
            }
            return View(modelsList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.MVCCustomersModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Customers", content).Result;
            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
