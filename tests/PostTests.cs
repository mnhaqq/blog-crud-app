using BlogCrudApp.Manager;
using BlogCrudApp.Models;

namespace tests
{
    public class PostTests
    {
        private readonly PostsManager _context;
        public PostTests()
        {
            _context = new PostsManager(ContextGenerator.Generator());
        }

        [Fact]
        public async Task AddNewPostTest()
        {
            string postTitle = "Test Post";
            string postContent = "This is a test for adding posts";
            Post post = new() {Title = postTitle, Content = postContent};

            await _context.AddNewPost(post);

            Assert.Equal(postTitle, post.Title);
            Assert.Equal(postContent, post.Content);
        }

        [Fact]
        public async Task GetAllPostsTest()
        {
            string postTitle = "Test Post";
            string postContent = "This is a test for adding posts";
            Post post = new() {Title = postTitle, Content = postContent};

            await _context.AddNewPost(post);

            List<Post> posts = await _context.GetAllPosts();

            Assert.NotEmpty(posts);
        }

        [Fact]
        public async Task GetSinglePostTest()
        {
            string postTitle = "Test Post";
            string postContent = "This is a test for adding posts";
            Post post = new() {Title = postTitle, Content = postContent};

            await _context.AddNewPost(post);

            Post? postById = await _context.GetSinglePost(post.PostId);
            Assert.NotNull(postById);
            Assert.Equal(postTitle, postById.Title);
            Assert.Equal(postContent, postById.Content);
            await _context.DeletePost(post.PostId);
        }

        [Fact]
        public async Task PatchPostTest()
        {
            string postTitle = "Test Post";
            string postContent = "This is a test for adding posts";
            Post post = new() {Title = postTitle, Content = postContent};

            string patchedContent = "Patched content";
            Post newPost = new() {Title = postTitle, Content = patchedContent};
            await _context.AddNewPost(post);
            await _context.PatchPost(post.PostId, newPost);

            Assert.NotNull(post);
            Assert.Equal(newPost.Content, post.Content);
            await _context.DeletePost(post.PostId);
        }

        [Fact]
        public async Task DeletePostTest()
        {
            string postTitle = "Test Post";
            string postContent = "This is a test for adding posts";
            Post post = new() {Title = postTitle, Content = postContent};

            await _context.AddNewPost(post);
            
            await _context.DeletePost(post.PostId);

            List<Post> posts = await _context.GetAllPosts();
            Assert.Empty(posts);
        }
    }
    
}