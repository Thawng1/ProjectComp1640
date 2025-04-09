using Microsoft.EntityFrameworkCore;
using ProjectComp1640.Data;
using ProjectComp1640.Interfaces;
using ProjectComp1640.Model;
using ProjectComp1640.NotificationConnect;

namespace ProjectComp1640.Service
{
    public class CommentService : IComment
    {
        public readonly ApplicationDBContext _context;
        private readonly NotificationService _notificationService;
        public CommentService(ApplicationDBContext context, NotificationService notificationService) 
        { 
            _context = context;
            _notificationService = notificationService;
        }
        public async Task CreateAsync(Comment comment, string userId)
        {
            comment.UserId = userId;

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            var blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == comment.BlogId);
            if (blog != null && blog.UserId != userId)
            {
                var sender = await _context.Users.FindAsync(userId);
                if (sender != null)
                {
                    await _notificationService.SendNotification(
                        receiverId: blog.UserId,
                        senderId: userId,
                        message: $"📣 {sender.UserName} đã bình luận bài viết của bạn."
                    );
                }
            }
        }
        public async Task DeleteAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.Include(c => c.User).ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetByBlogIdAsync(int blogId)
        {
            return await _context.Comments
                .Where(c => c.BlogId == blogId)
                .Include(c => c.User)
                .ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null)
            {
                return null; // Comment không tồn tại
            }

            // Cập nhật dữ liệu
            existingComment.Content = commentModel.Content;

            _context.Comments.Update(existingComment);
            await _context.SaveChangesAsync();
            return existingComment;
        }
        public async Task<Blog?> GetBlogByIdAsync(int blogId)
        {
            return await _context.Blogs.FirstOrDefaultAsync(b => b.Id == blogId);
        }
    }
}
