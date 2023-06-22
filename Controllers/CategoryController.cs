using AspNewsAPI.DTOs;
using AspNewsAPI.Entities;
using AutoMapper;
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
        private readonly IMapper mapper;

        public CategoryController(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this.mapper = mapper;
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
        public async Task<ActionResult<Category>> Get(int id)
        {
            var category = _context.Categories.FirstOrDefault(n => n.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }
        //get category by ID.
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Category>> GetByName(string name)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(n => n.Name.Contains(name));

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        //create categories.
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryCreationDTO categoryCreationDTO)
        {
            var exist = await _context.Categories.AnyAsync(n => n.Name == categoryCreationDTO.Name); //AnyAsync return bool.
            if (exist)
            {
                return BadRequest($"Ya existe la categoria {categoryCreationDTO.Name}");
            }

            var category = mapper.Map<Category>(categoryCreationDTO);

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
