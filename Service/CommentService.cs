using Microsoft.EntityFrameworkCore;
using ProjectComp1640.Data;
using ProjectComp1640.Interfaces;
using ProjectComp1640.Model;

namespace ProjectComp1640.Service
{
    public class CommentService : IComment
    {
        public readonly ApplicationDBContext _context;
        public CommentService(ApplicationDBContext context) 
        { 
            _context = context;
        }
        public Task<Comment> CreateAsync(Comment commentModel)
        {
            throw new NotImplementedException();
        }

        public Task<Comment?> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public Task<Comment?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            throw new NotImplementedException();
        }
    }
}
