using _01_Stok.Entities.Models.Concrete;
using _02_Stok.Repositories.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _03_Stok.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {

        //context e direk erişmeme gerek yok Repo erisiyor zaten
        private readonly IGenericRepo<Supplier> _repo;

        public SupplierController(IGenericRepo<Supplier> repo)
        {
            _repo = repo;
        }



        //herkes gelsin
        [HttpGet]
        public IActionResult GetAllSupplier()
        {
            return Ok(_repo.GetAll());
        }

        //aktif olan herkes gelsin
        [HttpGet]
        public IActionResult GetAllActiveSuppliers()
        {
            return Ok(_repo.GetActive());
        }

        //id sini bildigim gelsin
        [HttpGet("{id}")]
        public IActionResult GetSupplierById(int id)
        {
            return Ok(_repo.GetById(id));
        }


        [HttpPost]
        public IActionResult AddSupplier(Supplier supplier)
        {
            if (_repo.Add(supplier)) return Ok();

            else return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult SupplierUpdate(int id, Supplier supplier)
        {
            if (id != supplier.ID)
                return BadRequest();
            try
            {
                if (_repo.Any(a => a.ID == id))
                {
                    return _repo.Update(supplier) ? Ok() : BadRequest();

                }
            }
            catch (Exception)
            {
                return NoContent();//böyle birşey yok
                //return StatusCode(500, "Internal server error");
            }
            return Ok();
        }
        //return Ok(); ifadesi, try bloğunda herhangi bir istisna yakalanmadığında ve if koşulu sağlanmadığında çalışır. Bu durumda, SupplierUpdate metodunun sonuna ulaşıldığında, işlem başarılı olarak kabul edilir ve HTTP 200 durum kodu ile birlikte Ok() döndürülür. Bu, istemciye işlemin başarıyla tamamlandığını belirtir.

        [HttpDelete("{id}")]
        public IActionResult DeleteSupplier(int id)
        {
            Supplier supplier = _repo.GetById(id);
            if (supplier == null) return NotFound();
            else
            {
                _repo.Remove(supplier);
                return Ok();
            }
        }

        [HttpGet("{id}")] //HttpPut ile de gönderilir
        public IActionResult MakeActiveSupplier(int id)
        {
            Supplier supplier = _repo.GetById(id);
            if (supplier == null) return NotFound();
            else
            {
                return _repo.Activate(id) ? Ok() : BadRequest();
            }
        }



    }


    }
