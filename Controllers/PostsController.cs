using BlogCrudApp.Data;
using BlogCrudApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogCrudApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public PostsController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts = await _context.Posts.Include(c => c.Comments).ToListAsync();
            return Ok(posts);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var post = await _context.Posts.Include(c => c.Comments).FirstOrDefaultAsync(x => x.PostId == id);

            if (post == null)
                return BadRequest("Invalid Id");
        
            return Ok(post); 
        }

        [HttpPost]
        public async Task<IActionResult> Post(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = post.PostId }, post);
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(int id, Post updatedPost)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id);

            if (post == null)
                return BadRequest("Invalid Id");

            post.Content = updatedPost.Content;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id);

            if (post == null)
                return BadRequest("Invalid Id");

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}