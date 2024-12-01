﻿using _00017102_WAD_CW_server.Data;
using _00017102_WAD_CW_server.models;
using Microsoft.EntityFrameworkCore;

namespace _00017102_WAD_CW_server.Repositories
{
    public class PostRepository : BaseRepository<Post>
    {
        private readonly GeneralDbContext _context;
        public PostRepository(GeneralDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Post?> CreateAsync(Post entity)
        {
            var category = await _context.Categories.FindAsync(entity.CategoryId);
            if (category == null) 
            { 
                return null;
            }
            var result = await _context.Posts.AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public override async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.Posts.Include(p => p.Category).ToListAsync();
        }

        public override async Task<Post> GetByIdAsync(int id)
        {
            return await _context.Posts.Include(p => p.Category).Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
