using _00017102_WAD_CW_server.Data;
using _00017102_WAD_CW_server.models;
using Microsoft.EntityFrameworkCore;

namespace _00017102_WAD_CW_server.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly GeneralDbContext _context;
        public CategoryRepository(GeneralDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Category> GetCategoryWithPostsAsync(int id)
        {
            return await _context.Categories.Include(c => c.Posts).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
