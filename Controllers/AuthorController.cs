using AspNewsAPI.DTOs;
using AspNewsAPI.Entities;
using AutoMapper;
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
        private readonly IMapper mapper;

        public AuthorController(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this.mapper = mapper;
        }



        //get all authors.
        [HttpGet]
        public async Task<ActionResult<List<AuthorDTO>>> GetAll()
        {
            var authorList = await _context.Author.ToListAsync();
            return Ok(mapper.Map<List<AuthorDTO>>(authorList));
        }

        //get author by ID.
        [HttpGet("{id}", Name ="getAuthor") ]
        public async Task<ActionResult<AuthorDTO>> Get(int id)
        {
            var author = await _context.Author.FirstOrDefaultAsync(n => n.Id == id);
            
            if (author == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<AuthorDTO>(author));
        }


        //create author.
        [HttpPost]
        public async Task<ActionResult> Post(AuthorCreationDTO authorCreationDTO)
        {
            var exist = await _context.Categories.AnyAsync(n => n.Name == authorCreationDTO.Name); //AnyAsync return bool.
            if (exist)
            {
                return BadRequest($"Ya existe el autor {authorCreationDTO.Name}");
            }

            var author = mapper.Map<Author>(authorCreationDTO);

            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            var authorDTO = mapper.Map<AuthorDTO>(author);

            return CreatedAtRoute("getAuthor", new {id = author.Id}, authorDTO);
        }

        //edit author.
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(AuthorCreationDTO authorCreationDTO, int id)
        {

            var exist = await _context.Author.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            //automap author and asign Id (DTO dont have)
            var author = mapper.Map<Author>(authorCreationDTO);
            author.Id = id;

            _context.Author.Update(author);
            await _context.SaveChangesAsync();
            return NoContent();
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
