using _01_Stok.Entities.Models.Concrete;
using _02_Stok.Repositories.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _03_Stok.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        //context e direk erişmeme gerek yok Repo erisiyor zaten
        private readonly IGenericRepo<Category> _repo;

        public CategoryController(IGenericRepo<Category> repo)
        {
            _repo = repo;
        }

        //herkes gelsin
        [HttpGet]
        public IActionResult GetAllCategory()
        {
            return Ok(_repo.GetAll());
        }

        //aktif olan herkes gelsin
        [HttpGet]
        public IActionResult GetActiveCategory()
        {
            return Ok(_repo.GetActive());
        }

        //id sini bildigim gelsin
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            return Ok(_repo.GetById(id));
        }


        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            if (_repo.Add(category)) return Ok();
            else return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult CategoryUpdate(int id, Category category)
        {
            if (id != category.ID) return BadRequest();
            try
            {
                if (_repo.Any(a => a.ID == id))
                {
                    return _repo.Update(category) ? Ok() : BadRequest();

                }
                return BadRequest();
            }
            catch (Exception)
            {
                return NoContent();//böyle birşey yok
            }
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            Category category = _repo.GetById(id);
            if (category == null) 
                return NotFound();
            else
            {
                _repo.Remove(category);
                return Ok();
            }
        }

        [HttpGet("{id}")] //HttpPut ile de gönderilir
        public IActionResult MakeActiveCategory(int id)
        {
            Category category = _repo.GetById(id);
            if(category==null) return NotFound();
            else
            {
                return _repo.Activate(id) ? Ok(): BadRequest();
            }
        }

        //suplier controller ı siz yapın, bunun gibi. product farklı onu birlikte yaparız


    }
}
