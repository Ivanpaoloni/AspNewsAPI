using AspNewsAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNewsAPI.Controllers
{
    [Route("api/author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthorController(ApplicationDbContext context)
        {
            _context = context;
        }



        //get all authors.
        [HttpGet]
        public async Task<ActionResult<List<News>>> GetAll()
        {
            var authorList = await _context.Author.ToListAsync();
            return Ok(authorList);
        }

        //get author by ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<News>> Get(int id)
        {
            var author = _context.Author.FirstOrDefault(n => n.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        //create author.
        [HttpPost]
        public async Task<ActionResult> Post(Author author)
        {
            _context.Author.Add(author);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //edit author.
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Author author, int id)
        {
            if (author.Id != id)
            {
                return BadRequest("El id del autor no coincide con el id de la URL.");
            }

            var exist = await _context.Author.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            _context.Author.Update(author);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //delete author.
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await _context.Author.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            _context.Author.Remove(new Author() { Id = id });
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
