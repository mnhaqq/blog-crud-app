using BlogCrudApp.Data;
using BlogCrudApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogCrudApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public CommentsController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var comments = await _context.Comments.ToListAsync();
            return Ok(comments);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == id);

            if (comment == null)
                return BadRequest("Invalid Id");

            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Comment comment)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == comment.PostId);

            if (post == null)
                return BadRequest("Invalid Post Id");
            
            post.Comments.Add(comment);
            
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = comment.CommentId }, comment);
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(int id, Comment updatedComment)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == id);

            if (comment == null)
                return BadRequest("Invalid Id");

            comment.Text = updatedComment.Text;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == id);

            if (comment == null)
                return BadRequest("Invalid Id");
            
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}