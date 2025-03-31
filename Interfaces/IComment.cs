using ProjectComp1640.Model;

namespace ProjectComp1640.Interfaces
{
    public interface IComment
    {
        //Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject);
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment commentModel);
        Task<Comment?> UpdateAsync(int id, Comment commentModel);
        Task<Comment?> DeleteAsync(int id);
    }
}
