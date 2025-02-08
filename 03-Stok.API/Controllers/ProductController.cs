using _01_Stok.Entities.Models.Concrete;
using _02_Stok.Repositories.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _03_Stok.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepo<Product> _pRepo;

        public ProductController(IGenericRepo<Product> pRepo)
        {
            _pRepo = pRepo;
        }


        [HttpGet]
        // aktif ürünlerimi ilişkileri ile getirsin
        public IActionResult GetActiveProduct()
        {
            return Ok(_pRepo.GetActive(a=>a.Category, b=>b.Suplier));
        }

        //herkes gelsin
        [HttpGet]
        public IActionResult GetAllProduct()
        {
            return Ok(_pRepo.GetAll(a => a.Category, b => b.Suplier));
        }



        //bir tanesini getirmek istrersem
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            return Ok(_pRepo.GetById(id));
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _pRepo.Add(product);
            //return Ok(product); 200  response body de aynı şey
            return CreatedAtAction("GetProductById", new {id=product.ID}, product); //201
        }

        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            if(!_pRepo.Any(a=>a.ID==product.ID)) return NotFound();
            return _pRepo.Update(product) ? Ok("ürün güncellendi") : BadRequest();
        }

        [HttpDelete("{id}")] //passivize et
        public IActionResult DeleteProduct(int id)
        {
            if(_pRepo.GetById(id) is null) return NotFound();
            return _pRepo.Remove(id) ? Ok("ürün silindi") : BadRequest("ürün silinemedi");
        }


        [HttpGet("{id}")]
        public IActionResult MakeActiveProduct(int id)
        {
            if (_pRepo.GetById(id) is null) return NotFound();
            return _pRepo.Activate(id) ? Ok("ürün aktifleşti") : BadRequest();
        }












    }
}
