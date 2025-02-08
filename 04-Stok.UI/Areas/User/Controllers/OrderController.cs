using _01_Stok.Entities.Models.Concrete;
using _04_Stok.UI.Areas.User.Models.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _04_Stok.UI.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    //Role check olmadıgı icin kayıtlı olan herkes/iceri giren herkes gorecek fakat role check eklersen calısır
    //[Authorize(Role="admin",] identiy kullansaydık
    public class OrderController : Controller
    {
        private readonly HttpClient _httpClient;

        public OrderController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        string uri = "http://localhost:5056/api";


        OrderVM vm = new OrderVM();

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var response = await _httpClient.GetAsync($"{uri}/product/GetActiveProduct");
            if (response.IsSuccessStatusCode)
            {
                var products = await response.Content.ReadFromJsonAsync<List<Product>>();
                vm.AvailableProducts = products;
                vm.OrderDetails = products.Select(a=> new OrderDetailVM() { ProductID = a.ID}).ToList();
            }
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderVM vm) //apideki ordercont add metodu çok parametre alıyor, nesne değil querystring göndereceğim
        {
            var id = User.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.NameIdentifier).Value; //claims principal User'ı
            //value yazmayınca boş gitti ve hata oluştur
            var productsIds = vm.OrderDetails.Select(a=>a.ProductID).ToList();
            var productsQuantities = vm.OrderDetails.Select(a=>a.Quantity).ToList();

            //order cont. AddOrder metodunda FromQuery(http://localhost:3000/user?userId=5896544) var, url route(http://localhost:3000/user/5896544) vermedik, çoklu parametre ile : //Rsponse.Redirect("http://localhos/YourControllerName/ActionMethodName?querrystring1=querrystringvalue1&querrystring2=querrystringvalue2&querrystring3=querystringvalue3"); querystringle istediğin parametre alabiliyor, encode de olabiliyor
            var queryParams = $"?userId={id}&{string.Join("&", productsIds.Select((p, i) => $"productIDs={p}"))}&{string.Join("&", productsQuantities.Select((q, i) => $"quantities={q}"))}";


            var response = await _httpClient.PostAsync($"{uri}/order/AddOrder"+queryParams, null);//basit tiplerdeki digerlerindeki gibi nesneyi değil query stringi gönderdik

            if (response.IsSuccessStatusCode) return RedirectToAction("List");
            else return View(vm);
        }


        //public async Task<IActionResult> List()
        //{
        //    var response = await _httpClient.GetAsync($"{uri}/order/GetAllOrders");
        //    if(response.IsSuccessStatusCode)
        //    {
        //        var orders = await response.Content.ReadFromJsonAsync<List<Order>>();
        //        return View(orders);
        //    }
        //    return RedirectToAction("Index", "Home", new { area = "User" });
        //}

        public async Task<IActionResult> List()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var response = await _httpClient.GetAsync($"{uri}/order/GetOrdersByUserId/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var orders = await response.Content.ReadFromJsonAsync<List<Order>>();
                return View(orders);
            }
            return RedirectToAction("Index", "Home", new { area = "User" });
        }


        //1-iceri giren user id sini alabildigimiz icin her user'ın kendi sayfasında kendi siparişlerini görücez
        //2-kendi siparişi geldiğnde order ını (sepetini) komple silebilir yada orderdetails ini update/delete edeblir.Database den de oldugunu  kontrol et.


        [HttpPost]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var response = await _httpClient.DeleteAsync($"{uri}/order/DeleteOrder/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Index", "Home", new { area = "User" });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderDetail(OrderDetailVM orderDetail)
        {
            var response = await _httpClient.PutAsJsonAsync($"{uri}/order/UpdateOrderDetail", orderDetail);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Index", "Home", new { area = "User" });
        }


        [HttpPost]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            var response = await _httpClient.DeleteAsync($"{uri}/order/DeleteOrderDetail/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Index", "Home", new { area = "User" });
        }



    }
}
