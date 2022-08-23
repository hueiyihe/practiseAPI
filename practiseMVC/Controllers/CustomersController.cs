using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace practiseMVC.Controllers
{
    public class CustomersController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7037/api");
        HttpClient client;
        public CustomersController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
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
    }
}
