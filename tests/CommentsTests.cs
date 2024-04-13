using BlogCrudApp.Manager;
using BlogCrudApp.Models;

namespace tests
{
    public class CommentTests
    {
        private readonly PostsManager postsManager;
        private readonly CommentsManager commentsManager;

        public CommentTests()
        {
            postsManager = new PostsManager(ContextGenerator.Generator());
            commentsManager = new CommentsManager(ContextGenerator.Generator());
        }

        [Fact]
        public async Task AddNewCommentTest()
        {
            string postTitle = "Test Post";
            string postContent = "This is a test for adding posts";
            Post post = new() {Title = postTitle, Content = postContent};

            await postsManager.AddNewPost(post);
            string text = "Comment on post";
            Comment comment = new() {Text = text, PostId = post.PostId};

            await commentsManager.AddNewComment(comment);

            Assert.Equal(comment.Text, text);

            await postsManager.DeletePost(post.PostId);   
        }

        [Fact]
        public async Task GetAllCommentsTest()
        {
            string postTitle = "Test Post";
            string postContent = "This is a test for adding posts";
            Post post = new() {Title = postTitle, Content = postContent};

            await postsManager.AddNewPost(post);
            string text = "Comment on post";
            Comment comment = new() {Text = text, PostId = post.PostId};

            await commentsManager.AddNewComment(comment);

            List<Comment> comments = await commentsManager.GetAllComments();
            
            Assert.NotEmpty(comments);
            await postsManager.DeletePost(post.PostId);  
        }

        [Fact]
        public async Task PatchCommentTest()
        {
            string postTitle = "Test Post";
            string postContent = "This is a test for adding posts";
            Post post = new() {Title = postTitle, Content = postContent};

            await postsManager.AddNewPost(post);
            string text = "Comment on post";
            Comment comment = new() {Text = text, PostId = post.PostId};

            await commentsManager.AddNewComment(comment);

            string patchedText = "patched comment";
            Comment newComment = new() {Text = patchedText, PostId = post.PostId};

            await commentsManager.PatchComment(comment.CommentId, newComment);

            Assert.Equal(comment.Text, patchedText);

            await postsManager.DeletePost(post.PostId); 
        }

        [Fact]
        public async Task DeleteCommentTest()
        {
            string postTitle = "Test Post";
            string postContent = "This is a test for adding posts";
            Post post = new() {Title = postTitle, Content = postContent};

            await postsManager.AddNewPost(post);
            string text = "Comment on post";
            Comment comment = new() {Text = text, PostId = post.PostId};

            await commentsManager.AddNewComment(comment);
            await commentsManager.Delete(comment.CommentId);

            List<Comment> comments = await commentsManager.GetAllComments();
            Assert.Empty(comments);

            await postsManager.DeletePost(post.PostId); 
        }
    }
}