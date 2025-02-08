using _01_Stok.Entities.Models.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _04_Stok.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize] //yetkili
    public class CategoryController : Controller
    {
        private readonly HttpClient _httpClient; //api isteklerimi client nesnesi üzerinden gerçekleştiricem api repository uzerinden contextle konusacak

        public CategoryController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        string uri = "http://localhost:5056/api/category"; //apiye istek atacagım 

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            category.IsActive = true;
            var response = await _httpClient.PostAsJsonAsync($"{uri}/addcategory", category);
            if (response.IsSuccessStatusCode)
            {
                //// var newCategory = await response.Content.ReadFromJsonAsync<Category>(); gerek yok
                return RedirectToAction("List");
            }
            return View(category); 
        }

        public async Task<IActionResult> List()
        {
            var response = await _httpClient.GetAsync($"{uri}/getallcategory");
            if (response.IsSuccessStatusCode)
            {
                var categories = await response.Content.ReadFromJsonAsync<List<Category>>();
                return View(categories);
            }
            return View();
        }


        public async Task<IActionResult> Update(int id)
        {
            var response = await _httpClient.GetAsync($"{uri}/GetCategoryById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var category = await response.Content.ReadFromJsonAsync<Category>();
                return View(category);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Category category)
        {
            var response = await _httpClient.PutAsJsonAsync($"{uri}/CategoryUpdate/{id}", category);
            if(response.IsSuccessStatusCode) return RedirectToAction(nameof(List));
            else return View(category);
        }

        public async Task<IActionResult> MakePassive(int id)
        {
            var response = await _httpClient.DeleteAsync($"{uri}/DeleteCategory/{id}"); //apideki cat.cont.httpget idi
            if ((response.IsSuccessStatusCode))
            {
                return RedirectToAction(nameof(List));
            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        public async Task<IActionResult> MakeActive(int id)
        {
            var response = await _httpClient.GetAsync($"{uri}/MakeActiveCategory/{id}"); //apideki cat.cont.httpget idi
            if ((response.IsSuccessStatusCode))
            {
                return RedirectToAction(nameof(List));
            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });

        }


        //supplier yapın, dto ve anotations




    }
}
