using _00017102_WAD_CW_server.models;

namespace _00017102_WAD_CW_server.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetCategoryWithPostsAsync(int id);
    }
}
