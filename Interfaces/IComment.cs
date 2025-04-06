using ProjectComp1640.Model;

namespace ProjectComp1640.Interfaces
{
    public interface IComment
    {
        //Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject);
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task CreateAsync(Comment comment);
        Task<Comment?> UpdateAsync(int id, Comment commentModel);
        Task DeleteAsync(Comment comment);
        Task<IEnumerable<Comment>> GetByBlogIdAsync(int blogId);
        Task<Blog?> GetBlogByIdAsync(int blogId);

    }
}
