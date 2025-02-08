using _01_Stok.Entities.Models.Concrete;
using _04_Stok.UI.Areas.Admin.Models.SupplierDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _04_Stok.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize] //yetkili
    public class SupplierController : Controller
    {
        private readonly HttpClient _httpClient;

        public SupplierController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }



        string uri = "http://localhost:5056/api/supplier"; //apiye istek atacagım 

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplierCreateDTO supplierDto) //swagger da nesne mantıgı yok, json formatında çalıştığı için sorun yok fakat api olmasaydı supplier ın kendisini isterdi
        {
            supplierDto.IsActive = true;
            var response = await _httpClient.PostAsJsonAsync($"{uri}/addsupplier", supplierDto);//Api katman Sup.controller daki httppost, AddSupplier() metodu 
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            return View(supplierDto);
        }

        public async Task<IActionResult> List() //json oldugu icin dto yu direk halletti
        {
            var response = await _httpClient.GetAsync($"{uri}/getallsupplier");
            if (response.IsSuccessStatusCode)
            {
                //var suppliers = await response.Content.ReadFromJsonAsync<List<Supplier>>();
                var suppliers = await response.Content.ReadFromJsonAsync<List<SupplierListDTO>>();

                return View(suppliers);
            }
            return View();
        }


        public async Task<IActionResult> Update(int id)
        {
            var response = await _httpClient.GetAsync($"{uri}/GetSupplierById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var supplierUpdateDto = await response.Content.ReadFromJsonAsync<SupplierUpdateDTO>();
                return View(supplierUpdateDto);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, SupplierUpdateDTO supplierUpdateDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{uri}/SupplierUpdate/{id}", supplierUpdateDto);
            if (response.IsSuccessStatusCode) return RedirectToAction(nameof(List));
            else return View(supplierUpdateDto);
        }

        public async Task<IActionResult> MakePassive(int id)
        {
            var response = await _httpClient.DeleteAsync($"{uri}/DeleteSupplier/{id}"); //apideki sup.cont.httpdelete DeleteSupplier idi
            if ((response.IsSuccessStatusCode))
            {
                return RedirectToAction(nameof(List));
            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        public async Task<IActionResult> MakeActive(int id)
        {
            var response = await _httpClient.GetAsync($"{uri}/MakeActiveSupplier/{id}"); //apideki sup.cont.httpget idi
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(List));
            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });

        }


        //supplier yapın, dto ve anotations kullanın








    }
}
