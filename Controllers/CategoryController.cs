using AspNewsAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNewsAPI.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }



        //get all categories.
        [HttpGet]
        public async Task<ActionResult<List<News>>> GetAll()
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }

        //get category by ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<News>> Get(int id)
        {
            var category = _context.Categories.FirstOrDefault(n => n.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        //create categories.
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //edit category.
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Category category, int id)
        {
            if (category.Id != id)
            {
                return BadRequest("El id del autor no coincide con el id de la URL.");
            }

            var exist = await _context.Categories.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //delete category.
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await _context.Categories.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            _context.Categories.Remove(new Category() { Id = id });
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
