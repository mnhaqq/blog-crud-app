using BlogCrudApp.Data;
using BlogCrudApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogCrudApp.Manager
{
    public class CommentsManager
    {
        private readonly ApiDbContext _context;

        public CommentsManager(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllComments()
        {
            var comments = await _context.Comments.ToListAsync();
            return comments;
        }

        public async Task<Comment?> GetSingleComment(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == id);

            if (comment == null)
                throw new Exception("Invalid Id");

            return comment;
        }

        public async Task<int> AddNewComment(Comment comment)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == comment.PostId);

            if (post == null)
                throw new Exception("Invalid Post Id");
            
            post.Comments.Add(comment);
            
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment.CommentId;
        }

        public async Task PatchComment(int id, Comment updatedComment)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == id);

            if (comment == null)
                throw new Exception("Invalid Id");

            comment.Text = updatedComment.Text;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == id);

            if (comment == null)
                throw new Exception("Invalid Id");
            
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}