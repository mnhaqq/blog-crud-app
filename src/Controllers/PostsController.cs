using BlogCrudApp.Data;
using BlogCrudApp.Models;
using BlogCrudApp.Manager;
using Microsoft.AspNetCore.Mvc;


namespace BlogCrudApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ApiDbContext _context;
        PostsManager postsManager;

        public PostsController(ApiDbContext context)
        {
            _context = context;
            postsManager = new PostsManager(_context);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts = await postsManager.GetAllPosts();
            return Ok(posts);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var post = await postsManager.GetSinglePost(id);
                return Ok(post);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Post post)
        {
            var postId = await postsManager.AddNewPost(post);
            return CreatedAtAction(nameof(Get), new { id = postId }, post);
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(int id, Post updatedPost)
        {
            try
            {
                await postsManager.PatchPost(id, updatedPost);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await postsManager.DeletePost(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}