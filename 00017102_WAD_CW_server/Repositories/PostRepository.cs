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

        public override async Task<Post?> UpdateAsync(Post post)
        {

            _context.Entry(post).State = EntityState.Modified;
            var result = _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return await _context.Posts.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == result.Entity.Id); ;
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var post = await _context.Posts.Include(c => c.Comments).FirstOrDefaultAsync(c => c.Id == id);
            if (post == null) return false;

            foreach (var comment in post.Comments)
            {
                _context.Comments.Remove(comment);
                _context.Entry(comment).State = EntityState.Deleted;
            }

            _context.Entry(post).State = EntityState.Deleted;

            _context.Posts.Remove(post);

            await _context.SaveChangesAsync();
            return true;
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
