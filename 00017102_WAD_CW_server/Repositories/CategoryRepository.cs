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

        public override async Task<bool> DeleteAsync(int id)
        {
            var defaultCategory = await _context.Categories.FindAsync(1);
            if (defaultCategory == null) 
            { 
                return false;
            }

            if(id == 1)
            {
                return false;
            }

            var category = await _context.Categories.Include(c => c.Posts).FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) return false;

            foreach(var post in category.Posts)
            {
                post.CategoryId = defaultCategory.Id;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Category> GetCategoryWithPostsAsync(int id)
        {
            return await _context.Categories.Include(c => c.Posts).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
