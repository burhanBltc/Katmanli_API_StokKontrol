using _01_Stok.Entities.Models.Concrete;
using _04_Stok.UI.Models.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _04_Stok.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        string uri = "http://localhost:5056/api/user"; //apiye istek atacagım //swagger 5056 ile kalktı

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDTO dto)
        {                                   //http://localhost:5056/api/user/login/{dto.Email}/{dto.Password}
            var response = await _httpClient.GetAsync($"{uri}/login/{dto.Email}/{dto.Password}");
            if ((response.IsSuccessStatusCode))
            {
                var user = await response.Content.ReadFromJsonAsync<User>(); //UserDTO dogruysa User olusturacak
                if (user is not null)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.FirstName),
                        new Claim("Id", user.ID.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.ID.ToString())

                    };

                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties authenticationProperties = new AuthenticationProperties()
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.Now.AddMinutes(5)
                    };

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(principal, authenticationProperties);
                    return RedirectToAction("Index", "Home", new {area="Admin"});

                }
            }
            return View();
        }
        //api katmanındaki kontroller üzerinden repolara erisecegim, apiye erişim için httpClient a ihtiycım var








    }
}

/*
Kütüphaneler: Bu kütüphaneler, projede kullanılan sınıfları ve işlevleri içerir. Örneğin, Microsoft.AspNetCore.Authentication kimlik doğrulama işlemleri için gerekli olan sınıfları sağlar.

AccountController Sınıfı: Bu sınıf, kullanıcı hesap işlemlerini yönetir.

HttpClient: API'ye istek atmak için kullanılır. Bu, dış kaynaklara HTTP istekleri göndermek için kullanılan bir sınıftır.

uri: API'nin temel URL'si. Bu URL, API'ye istek atmak için kullanılır.

Login Metodu (GET): Kullanıcı giriş sayfasını döner. Bu metod, kullanıcıdan giriş bilgilerini almak için bir form görüntüler.

Login Metodu (POST): Kullanıcı giriş bilgilerini alır ve API'ye istek atar. Bu metod, kullanıcıdan alınan giriş bilgilerini kullanarak API'ye bir GET isteği gönderir. Eğer API'den başarılı bir yanıt alınırsa, kullanıcı bilgileri alınır ve kimlik doğrulama işlemi yapılır.

response.IsSuccessStatusCode: API'den başarılı bir yanıt alınıp alınmadığını kontrol eder.

ReadFromJsonAsync<User>: API'den dönen yanıtı User nesnesine dönüştürür.

Claims: Kullanıcı kimlik bilgilerini içerir. Bu bilgiler, kullanıcıyı tanımlamak için kullanılır.

ClaimsIdentity: Kullanıcı kimlik bilgilerini içerir ve kimlik doğrulama şemasını belirtir.

AuthenticationProperties: Kimlik doğrulama özelliklerini içerir. Örneğin, oturumun kalıcı olup olmadığını ve oturumun ne zaman sona ereceğini belirtir.

ClaimsPrincipal: Kullanıcının kimlik bilgilerini içerir ve kimlik doğrulama işlemi için kullanılır.

SignInAsync: Kullanıcıyı sisteme giriş yapar.
 */