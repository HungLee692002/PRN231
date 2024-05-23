using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;

namespace MyAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        MySaleDBContext _context = new MySaleDBContext();

        //Tạo api/get: Get all category
        [HttpGet]
        public ActionResult Get()
        {
            var categories = _context.Categories.Select(c=> new
            {
                CategoryId = c.CategoryId,
                CategoryName= c.CategoryName,
            }).ToList();
            return Ok(categories);
        }

        //Tạo api/get/id: get category by id
        //Báo not found nếu không có
        [HttpGet("{id}")]
        public ActionResult Get(int id) {

            var category = _context.Categories.Where(c => c.CategoryId == id).Select(c => new
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            }).FirstOrDefault();

            if(category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }


        //Tạo api/post/category: add new
        //Check name có empty
        [HttpPost]
        public ActionResult Post([FromBody] string name)
        {
            if (string.IsNullOrEmpty(name)){
                return BadRequest("name cannot be blanked");
            }

            Category newCategory = new Category { CategoryName = name };

            _context.Categories.Add(newCategory);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = newCategory.CategoryId }, newCategory);
        }


        //Tạo api/put/category: update category
        //Check exist
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Category updatedCatory) {
            var existedCategory = _context.Categories.Where(c => c.CategoryId == id).FirstOrDefault();
            if (updatedCatory == null || string.IsNullOrWhiteSpace(updatedCatory.CategoryName))
            {
                return BadRequest("Invalid category data.");
            }
            if (existedCategory == null)
            {
                return NotFound("Category not existed");
            }

            existedCategory.CategoryId = id;
            existedCategory.CategoryName=updatedCatory.CategoryName;

            _context.Categories.Update(existedCategory);
            _context.SaveChanges();

            return Ok(existedCategory);
        }

        //Tạo api/delete/id: delete category by id
        //Check exist, đang đc dùng ở bảng product
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existedCategory = _context.Categories.Where(c => c.CategoryId == id).FirstOrDefault();
            if(existedCategory == null)
            {
                return NotFound("Category not found");
            }
            if(existedCategory.Products.ToList().Count != 0) {

                return BadRequest("Cannot delete due to key problem");
            }
            _context.Remove(existedCategory);
            _context.SaveChanges();
            return Ok("Delete successfully");
        }

    }
}
