using _01_Stok.Entities.Models.Concrete;
using _02_Stok.Repositories.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _03_Stok.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IGenericRepo<User> _uRepo;

        public UserController(IGenericRepo<User> uRepo)
        {
            _uRepo = uRepo;
        }

        //get - email-password ile login olacak
        [HttpGet("{email}/{password}")]
        public IActionResult Login(string email, string password)
        {
            User user = _uRepo.GetByDefault(a => a.Email == email && a.Password == password);
            return user is not null ? Ok(user) : NotFound();     
        }

        //get - id
        [HttpDelete("{id}")]
        public IActionResult GetUserById(int id)
        {
            return _uRepo.GetById(id) is not null ? Ok(_uRepo.GetById(id)) : NotFound();
        }

        //post - yeni ekle
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            return _uRepo.Add(user) ? Ok(user) : BadRequest();
        }

        //herkesi getir
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_uRepo.GetAll());
        }

        //todo : user için put, delete, aktifleştirme
        [HttpPut]
        public IActionResult UpdateUser(User user)
        {
            if (!_uRepo.Any(a => a.ID == user.ID)) return NotFound();
            return _uRepo.Update(user) ? Ok("user güncellendi") : BadRequest();
        }

        [HttpDelete("{id}")] //passivize et
        public IActionResult DeleteUser(int id)
        {
            if (_uRepo.GetById(id) is null) return NotFound();
            return _uRepo.Remove(id) ? Ok("kullanıcı silindi") : BadRequest("kullanıcı silinemedi");
        }

        [HttpGet("{id}")]
        public IActionResult MakeActiveUser(int id)
        {
            if (_uRepo.GetById(id) is null) return NotFound();
            return _uRepo.Activate(id) ? Ok("kullanıcı aktifleşti") : BadRequest();
        }



    }
}
