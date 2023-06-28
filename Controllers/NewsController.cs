using AspNewsAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNewsAPI.Controllers;
using AutoMapper;
using AspNewsAPI.DTOs;

namespace AspNewsAPI.Controllers
{
    [Route("api/news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;

        public NewsController(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this.mapper = mapper;
        }



        //get all news.
        [HttpGet]
        public async Task <ActionResult<List<NewsDTO>>> GetAll()
        {
            var newsList = await _context.News
                .Include(x => x.Category)
                .Include(x => x.Author)
                .ToListAsync();
            return Ok(mapper.Map<List<NewsDTO>>(newsList));
        }
        //get all news by category.
        [HttpGet("sections/{id:int}")]
        public async Task<ActionResult<List<NewsDTO>>> GetByCategory(int id)
        {
            var newsList = await _context.News.Where(x => x.CategoryId == id)
                .Include(x => x.Category)
                .Include(x => x.Author)
                .ToListAsync();
            return Ok(mapper.Map<List<NewsDTO>>(newsList));
        }

        //get news by ID.
        [HttpGet("{id:int}", Name = "GetNews")]
        public async Task<ActionResult<NewsDTO>> Get(int id)
        {
            var news = await _context.News
                .Include(x => x.Category)
                .Include(x => x.Author)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (news == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<NewsDTO>(news));
        }

        //create news.
        [HttpPost]
        public async Task<ActionResult> Post(NewsCreationDTO newsCreationDTO)
        {
            var authorExist = await _context.Author.AnyAsync(x => x.Id == newsCreationDTO.AuthorId);
            var categoryExist = await _context.Categories.AnyAsync(x => x.Id == newsCreationDTO.CategoryId);

            if (!authorExist)
            {
                return BadRequest($"No existe el autor de id: {newsCreationDTO.AuthorId}");
            }

            if (!categoryExist)
            {
                return BadRequest($"No existe la categoria de id: {newsCreationDTO.CategoryId}");
            }

            newsCreationDTO.PublicationDate = DateTime.Now;

            var news = mapper.Map<News>(newsCreationDTO);

            _context.News.Add(news);
            await _context.SaveChangesAsync();

            var newsDTO = mapper.Map<NewsDTO>(news);

            return CreatedAtRoute("GetNews", new {id = news.Id}, newsDTO);
        }

        //edit news.
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(NewsUpdateDTO newsUpdateDTO, int id)
        {

            var exist = await _context.News.AnyAsync(x => x.Id == id);

            var result = await _context.News
                .FirstOrDefaultAsync(n => n.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            var news = mapper.Map<News>(newsUpdateDTO);
            news.Id = id;
            news.PublicationDate = result.PublicationDate;

            //desconecto el estado del seguimiento de result porque se superpone.
            _context.Entry(result).State = EntityState.Detached;


            _context.News.Update(news);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //delete news.
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await _context.News.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            _context.News.Remove(new News() { Id = id });
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
