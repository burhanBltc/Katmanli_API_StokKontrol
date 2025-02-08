using _01_Stok.Entities.Models.Concrete;
using _04_Stok.UI.Areas.Admin.Models.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _04_Stok.UI.Areas.Admin.Controllers
{
    [Area("Admin")] //Admin'in areasıdır
    [Authorize] //yetkili
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        string uri = "http://localhost:5056/api"; //apiye istek atacagım, bir yere kadar root aynı
        ProductCreateVM vm = new ProductCreateVM();

        public async Task<IActionResult> Create() //View'a giderken product bilgileri, elimdeki category ve supplier dan seçtirebilirim => productVM
        {
            //aktif kategorilerimi al
            var response = await _httpClient.GetAsync($"{uri}/category/GetActiveCategory");
            if (response.IsSuccessStatusCode)
            {
                var categories = await response.Content.ReadFromJsonAsync<List<Category>>();
                vm.Categories = categories.Select(a => new SelectListItem() {Text=a.CategoryName, Value=a.ID.ToString() }).ToList();
            }

            var responseSup = await _httpClient.GetAsync($"{uri}/supplier/GetAllActiveSuppliers");
            if (responseSup.IsSuccessStatusCode)
            {
                var suppliers = await responseSup.Content.ReadFromJsonAsync<List<Supplier>>();
                vm.Suppliers = suppliers.Select(a => new SelectListItem() { Text = a.SupplierName, Value = a.ID.ToString() }).ToList();
            }
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM vm)
        {
            var response = await _httpClient.PostAsJsonAsync($"{uri}/product/AddProduct", vm.Product);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            return View(vm);
        }


        public async Task<IActionResult> List()
        {
            var response = await _httpClient.GetAsync($"{uri}/product/GetAllProduct");
            if (response.IsSuccessStatusCode)
            {
                var products = await response.Content.ReadFromJsonAsync<List<Product>>();
                return View(products);
            }
            return NoContent();
        }


        ProductUpdateVM uVm = new ProductUpdateVM();

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            //pozitif bir kod isvalid falan bakmadık
            var response = await _httpClient.GetAsync($"{uri}/product/GetProductById/{id}");
            if (response.IsSuccessStatusCode)
            {
                uVm.Product = await response.Content.ReadFromJsonAsync<Product>();
            }

            var responseC = await _httpClient.GetAsync($"{uri}/category/GetActiveCategory");
            if (responseC.IsSuccessStatusCode)
            {
                var categories = await responseC.Content.ReadFromJsonAsync<List<Category>>();
                uVm.Categories = categories.Select(a => new SelectListItem() { Text = a.CategoryName, Value = a.ID.ToString() }).ToList();
            }

            var responseSup = await _httpClient.GetAsync($"{uri}/supplier/GetAllActiveSuppliers");
            if (responseSup.IsSuccessStatusCode)
            {
                var suppliers = await responseSup.Content.ReadFromJsonAsync<List<Supplier>>();
                uVm.Suppliers = suppliers.Select(a => new SelectListItem() { Text = a.SupplierName, Value = a.ID.ToString() }).ToList();
            }
            return View(uVm);
        }

            //hatırlatma : pozitif bir kod, hata meydana gelip tekrar viewa giderse categories, suppliers null gelir!!!
            [HttpPost]
            public async Task<IActionResult> Update(ProductUpdateVM vm)
            {
                var response = await _httpClient.PutAsJsonAsync($"{uri}/product/UpdateProduct", vm.Product);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(List));
                }
                //categories - suppliers doldurman lazım
                return View(vm);
            }


        public async Task<IActionResult> MakePassive(int id)
        {
            var response = await _httpClient.DeleteAsync($"{uri}/product/DeleteProduct/{id}");
            if (response.IsSuccessStatusCode) return RedirectToAction(nameof(List));
            else return RedirectToAction("Index", "Home", new { area = "Admin" });
        }


        public async Task<IActionResult> MakeActive(int id)
        {
            var response = await _httpClient.GetAsync($"{uri}/product/MakeActiveProduct/{id}");
            if (response.IsSuccessStatusCode) return RedirectToAction(nameof(List));
            else return RedirectToAction("Index", "Home", new { area = "Admin" });
        }


        
    }
}
