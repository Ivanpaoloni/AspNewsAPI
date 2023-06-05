using AspNewsAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        //get news by ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<News>> Get(int id)
        {
            var news = _context.News.FirstOrDefault(n => n.Id == id);

            if (news == null)
            {
                return NotFound();
            }

            return Ok(news);
        }

        //create news.
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] News news)
        {
            news.PublicationDate = DateTime.Now;
            _context.News.Add(news);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
