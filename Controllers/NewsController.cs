using AspNewsAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNewsAPI.Controllers;

namespace AspNewsAPI.Controllers
{
    [Route("api/news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NewsController(ApplicationDbContext context)
        {
            _context = context;
        }



        //get all news.
        [HttpGet]
        public async Task <ActionResult<List<News>>> GetAll()
        {
            var newsList = await _context.News.ToListAsync();
            return Ok(newsList);
        }
        //get all news by category.
        //[HttpGet("/sections/{name:string}")]
        //public async Task <ActionResult<List<News>>> GetAllByCategory(string name)
        //{
        //    var category = await categoryController.Get(name);
        //    var newsList = _context.News.Where(x => x.CategoryId == category.Value.Id).ToList();
        //    return Ok(newsList);
        //}

        //get news by ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<News>> Get(int id)
        {
            var news = await _context.News.FirstOrDefaultAsync(n => n.Id == id);

            if (news == null)
            {
                return NotFound();
            }

            return Ok(news);
        }

        //create news.
        [HttpPost]
        public async Task<ActionResult> Post(News news)
        {
            var authorExist = await _context.Author.AnyAsync(x => x.Id == news.AuthorId);
            var categoryExist = await _context.Categories.AnyAsync(x => x.Id == news.CategoryId);

            if (!authorExist)
            {
                return BadRequest($"No existe el autor de id: {news.AuthorId}");
            }

            if (!categoryExist)
            {
                return BadRequest($"No existe la categoria de id: {news.CategoryId}");
            }

            news.PublicationDate = DateTime.Now;
            _context.News.Add(news);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //edit news.
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(News news, int id)
        {
            if (news.Id != id)
            {
                return BadRequest("El id de la noticia no coincide con el id de la URL.");
            }

            var exist = await _context.News.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            _context.News.Update(news);
            await _context.SaveChangesAsync();
            return Ok();
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
